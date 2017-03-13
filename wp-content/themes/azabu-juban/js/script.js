
(function ($) {
	$(window).load(function() {
		var showheight = $('#gmo-show-time').height();
		$('.site-main .sidebar-container').css({'top':showheight+40}); 

		var navhei = $('#navbar').offset().top;
		var sethei = $('#wpadminbar').height();
		var scrolltop = $(this).scrollTop();
		var navflag = navhei + sethei;
		if(scrolltop >= navflag){
			$('#navbar').css({
				'position':'fixed',
				'top':sethei + 0 + 'px'
			});
		}  else if(scrolltop <= navflag) {
			$('#navbar').css({
				'position':'relative',
				'top':'0'
			});
		}
		$(window).scroll(function () {
			var scrolltop = $(this).scrollTop();
			var navflag = navhei + sethei;
			if(scrolltop >= navflag){
				$('#navbar').css({
					'position':'fixed',
					'top':sethei + 0 + 'px'
				});
			}  else if(scrolltop <= navflag) {
				$('#navbar').css({
					'position':'relative',
					'top':'0'
				});
			}
		});
	});
})(jQuery);