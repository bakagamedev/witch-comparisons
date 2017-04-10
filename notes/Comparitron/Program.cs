//For licence details see; http://www.wtfpl.net
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Comparitron
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage;");
                Console.WriteLine("Feed me a file formatted:");
                Console.WriteLine("Episode code (bw04, e1m1, etc)  - On the first line.");
                Console.WriteLine("framenum | witty caption   -  Until the end of the file.");
                Console.WriteLine("Blank lines are ok, but otherwise deviating from ^ will crash the program and give incomplete output files.");
                Console.WriteLine("For every frame referenced, the matching files in subdirectories 'new' and 'old' get copied to 'output'. Files should be in format 'tv-00000.jpg'.");
                Console.ReadKey();
                return;
            }

            var inFile = args[0];
            var outFile = Path.ChangeExtension(inFile, "divs.txt");

            List<string> frames = new List<string>();
            
            using (var output = new StreamWriter(outFile))
            {
                output.WriteLine("<ol>");

                int lineno = 0;
                string epcode = "changeme"; //Will hopefully get overwritten
                foreach (var line in File.ReadLines(inFile))
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    Console.WriteLine(line);

                    if (lineno == 0)    //First line for
                    {
                        var parts = line.Split('|');    ///Jusst in case we need more stuff.
                        epcode = parts[0].Trim();
                    }
                    else
                    {
                        var parts = line.Split('|');

                        if (parts.Length < 2)
                        {
                            Console.WriteLine("FORMAT ERROR: dumb line, not enough seperators");
                            return;
                        }
                        if (parts.Length > 2)
                        {
                            Console.WriteLine("FORMAT ERROR: help help, too many separators");
                            return;
                        }

                        var imagenumber = parts[0].Trim();
                        var text = parts[1].Trim();

                        frames.Add(imagenumber);

                        output.WriteLine("<li>");
                        output.WriteLine(text);
                        output.WriteLine("<div class=\"twentytwenty-container\">");
                        output.WriteLine("\t<img src=\"./images/{0}/tv-{1}.jpg\" />", epcode,imagenumber);
                        output.WriteLine("\t<img src=\"./images/{0}/bd-{1}.jpg\" />", epcode,imagenumber);
                        output.WriteLine("</div>");
                        output.WriteLine("</li>");
                    }

                    lineno++;
                }
                output.WriteLine("</ol>");
            }
            Console.WriteLine("HTML in {0}", outFile);

            if (!Directory.Exists(@"output"))
                Directory.CreateDirectory(@"output");

            foreach (var line in frames)
            {
                string tvName = string.Format("tv-{0}.jpg", line);
                if (File.Exists(@"output\" + tvName))
                    File.Delete(@"output\" + tvName);
                File.Copy(@"old\" + tvName, @"output\" + tvName);

                string bdName = string.Format("bd-{0}.jpg", line);
                if (File.Exists(@"output\" + bdName))
                    File.Delete(@"output\" + bdName);
                File.Copy(@"new\" + bdName, @"output\" + bdName);
            }

            Console.WriteLine("Done");
        }
    }
}
