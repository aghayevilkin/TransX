$(document).ready(function() {
	$('.count-num').counterUp({
		delay: 10,
		time: 1200
	});
});
$('.slider').slick({
	slidesToShow: 3,
	slidesToScroll: 1,
	dots: true,
	responsive: [
		{
			breakpoint: 1024,
			settings: {
				slidesToShow: 2,
				slidesToScroll: 2,
				infinite: true,
				dots: true
			}
		},
		{
			breakpoint: 600,
			settings: {
				slidesToShow: 2,
				slidesToScroll: 2
			}
		},
		{
			breakpoint: 480,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1
			}
		}
	]
});

$('.test-slider').slick({
	slidesToShow: 1,
	slidesToScroll: 1,
	dots: true,
	arrows: false,
	appendDots: $('.test-nav'),
	responsive: [
		{
			breakpoint: 1024,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				infinite: true,
				dots: true
			}
		},
		{
			breakpoint: 600,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1
			}
		},
		{
			breakpoint: 480,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1
			}
		}
	]
});

$('.logo-slider').slick({
	slidesToShow: 4,
	slidesToScroll: 4,
	dots: false,
	arrows: false,
	appendDots: $('.logo-slider-dots'),
	responsive: [
		{
			breakpoint: 1024,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 3,
				infinite: true,
				dots: true
			}
		},
		{
			breakpoint: 600,
			settings: {
				slidesToShow: 2,
				slidesToScroll: 2,
				dots: true
			}
		},
		{
			breakpoint: 480,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 1,
				dots: true
			}
		}
	]
});
$('.about-slider').slick({
	slidesToShow: 3,
	slidesToScroll: 2,
	dots: true,
	arrows: false,
	appendDots: $('.about-slider-nav'),
	responsive: [
		{
			breakpoint: 1024,
			settings: {
				slidesToShow: 3,
				slidesToScroll: 2,
				infinite: true,
				dots: true
			}
		},
		{
			breakpoint: 600,
			settings: {
				slidesToShow: 2,
				slidesToScroll: 2
			}
		},
		{
			breakpoint: 480,
			settings: {
				slidesToShow: 1,
				slidesToScroll: 4
			}
		}
	]
});

$("#sidebar-btn").click(function(){
	$(".sidebar-menu").addClass("sidebar-menu-active");
  });
  $(".sidebar-close").click(function(){
	$(".sidebar-menu").removeClass("sidebar-menu-active");
  });
  
  $(".sidebar-slide").click(function(){
	var dataCategory=$(this).data("category")
  $(".sidebar").addClass(".sidebar-inactive");
   $(".sidebar-inner").each((function(){
   $(this).data("value")===dataCategory && $(this).addClass("sidebar-inner-active");
   }))
});

  $(".sidebar-back").click(function(){
	$(".sidebar-inner").removeClass("sidebar-inner-active");
	$(".sidebar").removeClass(".sidebar-inactive");
  });
 

  $(window).scroll(function() {
    if ($(document).scrollTop() > 50) {
		$('.navbar-top').addClass('d-xl-none');
		$('.logo-light').addClass('d-none');
		$(".logo-dark").removeClass("d-none");
		$('.header-page').addClass('header-sticky');
		$(".header-sticky").removeClass("header-page");

        
    } else {
        $(".navbar-top").removeClass("d-xl-none");
		$('.logo-light').removeClass('d-none');
		$(".logo-dark").addClass("d-none");
		$('.header-sticky').addClass('header-page');
		$(".header-page").removeClass("header-sticky");
		
    }
});
