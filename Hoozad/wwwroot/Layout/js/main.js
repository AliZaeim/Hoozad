/***************************** show password in login js****************************/

/*import { func } from "prop-types";*/

$("#basic-addon2").on("click", function () {
    let passwordField = $("#password");
    let passwordFieldAttr = passwordField.attr("type");

    if (passwordFieldAttr == "password") {
        passwordField.attr("type", "text");
        $(this).html('<i class="fa fa-eye-slash" aria-hidden="true"></i>');
    } else {
        passwordField.attr("type", "password");
        $(this).html('<i class="fa fa-eye" aria-hidden="true"></i>');
    }
});
$("#basic-addon3").on("click", function () {
    let passwordField = $("#repassword");
    let passwordFieldAttr = passwordField.attr("type");

    if (passwordFieldAttr == "password") {
        passwordField.attr("type", "text");
        $(this).html('<i class="fa fa-eye-slash" aria-hidden="true"></i>');
    } else {
        passwordField.attr("type", "password");
        $(this).html('<i class="fa fa-eye" aria-hidden="true"></i>');
    }
});

////////////////////////////////////////////

function showPassword() {
    var key_attr = $('.inputpassword').attr('type');
    if (key_attr != 'text') {
        $('.checkbox').addClass('show');
        $('.inputpassword').attr('type', 'text');
    } else {
        $('.checkbox').removeClass('show');
        $('.inputpassword').attr('type', 'password');
    }
};

////////////////////////////////////////////

function showPassword2() {
    var key_attr = $('.inputpassword-repetition').attr('type');
    if (key_attr != 'text') {
        $('.checkbox').addClass('show');
        $('.inputpassword-repetition').attr('type', 'text');
    } else {
        $('.checkbox').removeClass('show');
        $('.inputpassword-repetition').attr('type', 'password');
    }
};

/*************************************** slider js***************************************/

//slider-1  
if ($("#slider_header").length) {
    $(".slider_baner-homepage").owlCarousel({
        items: 1,
        animateOut: 'fadeOut',
        animateIn: 'fadeIn',
        autoplay: true,
        rtl: true,
        loop: true,
        smartSpeed: 500,
        nav: true,
        navText: ["<i class='fal fa-chevron-circle-right'></i>", "<i class='fal fa-chevron-circle-left'></i>"],
        dots: true,
        lazyLoadEager: 3,

    })
};

//slider-2

if ($(".slider_product").length) {
    $(".slider_product").owlCarousel({
        margin: 30,
        rtl: true,
        loop: false,
        autoplay: true,
        autoplaytimeout: 4000,
        smartSpeed: 500,
        nav: true,
        navText: ["<i class='fal fa-chevron-circle-right'></i>", "<i class='fal fa-chevron-circle-left'></i>"],
        dots: true,
        lazyLoadEager: 3,
        responsive: {
            // breakpoint from 0 up
            0: {
                items: 1,
            },
            // breakpoint from 576 up
            576: {
                items: 2,
            },
            // breakpoint from 768 up
            768: {
                items: 3,
            },

            992: {
                items: 3,
            },

            1200: {
                items: 4,
            }
        }
    })
};

//slider-3
if ($(".slider-product-bestSale-style2").length) {
    $(".slider-product-bestSale-style2").owlCarousel({
        rtl: true,
        loop: true,
        autoplaytimeout: 4000,
        smartSpeed: 1300,
        nav: true,
        navText: ["<i class='fas fa-chevron-right'></i>", "<i class='fas fa-chevron-left'></i>"],
        dots: true,
        lazyLoadEager: 3,
        responsive: {
            // breakpoint from 0 up
            0: {
                items: 1,
            },
            // breakpoint from 576 up
            576: {
                items: 2,
            },
            // breakpoint from 768 up
            768: {
                items: 3,
            },

            992: {
                items: 4,
            },

            1200: {
                items: 5,
            }
        }
    })
};


//slider-4
if ($(".slider-product-style2").length) {
    $(".slider-product-style2").owlCarousel({
        rtl: true,
        loop: true,
        autoplaytimeout: 4000,
        smartSpeed: 1300,
        nav: true,
        navText: ["<i class='fas fa-chevron-right'></i>", "<i class='fas fa-chevron-left'></i>"],
        dots: true,
        lazyLoadEager: 3,
        responsive: {
            // breakpoint from 0 up
            0: {
                items: 1,
            },
            // breakpoint from 576 up
            576: {
                items: 2,
            },
            // breakpoint from 768 up
            768: {
                items: 3,
            },

            992: {
                items: 4,
            },

            1200: {
                items: 4,
            }
        }
    })
};


//slider-5
if ($(".slider-sidebar").length) {
    $(".slider-sidebar").owlCarousel({
        rtl: true,
        loop: true,
        autoplaytimeout: 4000,
        smartSpeed: 1300,
        dots: true,
        lazyLoadEager: 3,
        nav: true,
        navText: ["<i class='fas fa-chevron-right'></i>", "<i class='fas fa-chevron-left'></i>"],
        responsive: {
            // breakpoint from 0 up
            0: {
                items: 1,
            },
            // breakpoint from 576 up
            576: {
                items: 2,
            },
            // breakpoint from 768 up
            768: {
                items: 3,
            },

            992: {
                items: 1,
            },

            1200: {
                items: 1,
            }
        }
    })

};

//slider-6
if ($(".slider-off-product").length) {
    $(".slider-off-product").owlCarousel({
        rtl: true,
        loop: true,
        autoplaytimeout: 4000,
        smartSpeed: 1300,
        nav: true,
        navText: ["<i class='fas fa-chevron-right'></i>", "<i class='fas fa-chevron-left'></i>"],
        dots: true,
        lazyLoadEager: 3,
        responsive: {
            // breakpoint from 0 up
            0: {
                items: 1,
            },
            // breakpoint from 576 up
            576: {
                items: 2,
            },

            1200: {
                items: 2,
            }
        }
    })
};


//slider-7
if ($(".slider-product-toSidebar").length) {
    $(".slider-product-toSidebar").owlCarousel({
        margin: 30,
        rtl: true,
        loop: true,
        autoplaytimeout: 4000,
        smartSpeed: 500,
        nav: true,
        navText: ["<i class='fal fa-chevron-circle-right'></i>", "<i class='fal fa-chevron-circle-left'></i>"],
        dots: true,
        responsive: {
            // breakpoint from 0 up
            0: {
                items: 1,
            },
            // breakpoint from 576 up
            576: {
                items: 2,
            },
            // breakpoint from 768 up
            768: {
                items: 3,
            },

            992: {
                items: 2,
            },

            1200: {
                items: 3,
            }
        }
    })
};

//slider-8
if ($(".slider_brand").length) {
    $(".slider_brand").owlCarousel({
        // items: 9,
        center: true,
        margin: 15,
        rtl: true,
        loop: true,
        // autoplay: true,
        autoplaytimeout: 4000,
        smartSpeed: 500,
        nav: true,
        navText: ["<i class='fal fa-chevron-circle-right'></i>", "<i class='fal fa-chevron-circle-left'></i>"],
        dots: true,
        lazyLoadEager: 3,
        responsive: {
            // breakpoint from 0 up
            0: {
                items: 2,
            },
            400: {
                items: 3,
            },

            // breakpoint from 576 up
            576: {
                items: 4,

            },
            // breakpoint from 768 up
            768: {
                items: 5,
            },

            992: {
                items: 7,
            },

            1200: {
                items: 8,
            }
        }
    })
};


//slider-9
if ($("#Related-posts-slider").length) {
    $(".slider-Related-posts").owlCarousel({
        margin: 30,
        rtl: true,
        loop: true,
        autoplay: true,
        autoplaytimeout: 4000,
        smartSpeed: 500,
        nav: true,
        navText: ["<i class='fal fa-chevron-circle-right'></i>", "<i class='fal fa-chevron-circle-left'></i>"],
        dots: true,
        lazyLoadEager: 3,
        responsive: {
            // breakpoint from 0 up
            0: {
                items: 1,
                margin: 20,

            },
            // breakpoint from 576 up
            576: {
                items: 1,
                margin: 20,

            },
            // breakpoint from 768 up
            768: {
                items: 2,
                margin: 8,
            },

            992: {
                items: 3,
                center: true,
            },

            1200: {
                items: 3,
                margin: 10,
                center: true,
            }
        }
    })
}
//slider-10
if ($("#testimonial-slider").length) {
    $(".slider-show-customers-comment").owlCarousel({
        items: 1,
        rtl: true,
        loop: true,
        autoplay: true,
        autoplaytimeout: 5000,
        smartSpeed: 500,
        nav: true,
        navText: ["<i class='fal fa-chevron-circle-right'></i>", "<i class='fal fa-chevron-circle-left'></i>"],
        dots: true,
    })
}


//slider-11
if ($("#team-slider").length) {
    $(".slider-team").owlCarousel({
        margin: 20,
        rtl: true,
        loop: true,
        smartSpeed: 500,
        navText: ["<i class='fal fa-chevron-circle-right'></i>", "<i class='fal fa-chevron-circle-left'></i>"],
        dots: true,
        responsive: {
            // breakpoint from 0 up
            0: {
                items: 1,
            },
            // breakpoint from 576 up
            576: {
                items: 2,
            },
            // breakpoint from 768 up
            768: {
                items: 3,
            },

            992: {
                items: 3,
            },

            1200: {
                items: 4,
            }
        }
    })
}

//slider-12

if ($(".slider-product-sty2").length) {
    $(".slider-product-sty2").owlCarousel({
        margin: 0,
        rtl: true,
        loop: true,
        autoplay: false,
        autoplaytimeout: 4000,
        smartSpeed: 500,
        nav: true,
        navText: ["<i class='fal fa-angle-right'></i>", "<i class='fal fa-angle-left'></i>"],
        dots: true,
        lazyLoadEager: 3,
        responsive: {
            // breakpoint from 0 up
            0: {
                items: 1,
            },
            // breakpoint from 576 up
            576: {
                items: 2,
            },
            // breakpoint from 768 up
            768: {
                items: 3,
            },

            992: {
                items: 3,
            },

            1200: {
                items: 4,
            }
        }
    })
};
/*************************************** countdown js***************************************/

if ($("#countdown-1").length) {
    const second = 1000,
        minute = second * 60,
        hour = minute * 60,
        day = hour * 24;
    let countDown = new Date('Sep 19, 2022 00:00:00').getTime(),
        x = setInterval(function () {
            let now = new Date().getTime(),
                distance = countDown - now;
            document.getElementById('days').innerText = Math.floor(distance / (day)),
                document.getElementById('hours').innerText = Math.floor((distance % (day)) / (hour)),
                document.getElementById('minutes').innerText = Math.floor((distance % (hour)) / (minute))
            document.getElementById('seconds').innerText = Math.floor((distance % (minute)) / second);
        }, second)
}


if ($("#countdown-2").length) {
    const second = 1000,
        minute = second * 60,
        hour = minute * 60,
        day = hour * 24;
    let countDown = new Date('Sep 19, 2022 00:00:00').getTime(),
        x = setInterval(function () {
            let now = new Date().getTime(),
                distance = countDown - now;
            document.getElementById('hours').innerText = Math.floor((distance % (day)) / (hour)),
                document.getElementById('minutes').innerText = Math.floor((distance % (hour)) / (minute))
            document.getElementById('seconds').innerText = Math.floor((distance % (minute)) / second);
        }, second)
}

/*************************************** search js***************************************/

$(".icon-search ").on("click", function () {
    alert('se');
    $(document).find(".bg-searchform").css({ "opacity": "1", "visibility": " visible", "transform": " scaleY(1)" });
});

$(".area-close-search").on("click", function (e) {
    $(".bg-searchform").css({ "opacity": "0", "visibility": "hidden", "transform": " scaleY(0)" });
});
$("#btnSearch").on("click", function () {
    alert("s");
});
/*************************************** box cart shopping ***************************************/

$(".icon-shooping-cart").on("click", function () {
    $(".box-add-to-cart-header").addClass("cart-woocommerce-toogle");
    $(".bg-close").css({ "display": "block" });
});

$(".bg-close").on("click", function () {
    $(".box-add-to-cart-header").removeClass("cart-woocommerce-toogle");
    $(this).css({ "display": "none" });
});


/*************************************** menu mobile js***************************************/

$(".icon_meni_bar i").on("click", function () {
    $(".menu_mobile").css({ "right": "0" });
    $(".bg-close").css({ "display": "block" });
});
$(".close_menu_mobile").on("click", function () {
    $(".menu_mobile").css({ "right": "-100%" });
    $(".bg-close").css({ "display": "none" });
});
$(".bg-close").on("click", function () {
    $(".menu_mobile").css({ "right": "-100%" });
    $(this).css({ "display": "none" });
});

////////////////////////////////
$("#radio-color-1").on("click", function () {
    var empty = $('#colorOptionText').text();
    var span = "قرمز";
    if (empty === " ") {
        $("#colorOptionText").append(span);
    }
    else {
        return
    }
});

////////////////////////////////
$(window, document, undefined).ready(function () {

    $('#modal-subscribe').modal('show');


    $('.input').on("blur", function () {
        var $this = $(this);
        if ($this.val())
            $this.addClass('used');
        else
            $this.removeClass('used');
    });
    if ($("#body-homepage").length) {

        wow = new WOW(
            {
                boxClass: 'wow',      // default
                animateClass: 'animated', // default
                offset: 0,          // default
                mobile: false,       // default
                live: true        // default
            }
        )
        wow.init();
    }

    if ($(".exzoom").length) {
        $('.container').imagesLoaded(function () {
            $("#exzoom").exzoom({
                autoPlay: false,
            });
            $("#exzoom").removeClass('hidden')
        });
    }
    $("#LModalchk").on("change", function () {

        var check = $(this).prop('checked');
        if (check === true) {
            $.cookie('mdata', 'yes', { expires: 1 });
        }

        else {
            $.cookie('mdata', 'no');
        }


    });
});
////////////////////////////////
$('#tab1').on('click', function () {
    $('#tab1').addClass('login-shadow');
    $('#tab2').removeClass('signup-shadow');
});

$('#tab2').on('click', function () {
    $('#tab2').addClass('signup-shadow');
    $('#tab1').removeClass('login-shadow');

});

/********************************* multilevel accordion menu mobile js ********************************/

// prevent page from jumping to top from  # href link
$('li.menu-item-has-children > a').on("click", function (e) {
    e.preventDefault();
});
// remove link from menu items that have children
$("li.menu-item-has-children > a").attr("href", "#");
//  function to open / close menu items
$(".menu-multi-level a").on("click", function () {
    var link = $(this);
    var closest_ul = link.closest("ul");
    var parallel_active_links = closest_ul.find(".active")
    var closest_li = link.closest("li");
    var link_status = closest_li.hasClass("active");
    var count = 0;
    closest_ul.find("ul").slideUp(function () {
        if (++count == closest_ul.find("ul").length)
            parallel_active_links.removeClass("active");
    });
    if (!link_status) {
        closest_li.children("ul").slideDown();
        closest_li.addClass("active");
    }
})

/****************************** scroll link ******************************/

$('.scrollTo').on("click", function () {

    $('html, body').animate({
        scrollTop: 100
    }, 500);
    return false;
});


/****************************** scroll to top ******************************/

$(window).on("scroll", function () {
    if ($(window).scrollTop() > 200) {
        $("a.scroll-To-top").css({ "visibility": " visible", "transform": "translateY(0)" });
    } else {
        $("a.scroll-To-top").css({ "visibility": " hidden", "transform": "translateY(2rem)" });
    }
});


/**************************** wow animation to scroll ****************************/




/****************************** fancybox photo ******************************/

// add all to same gallery
$(".gallery a").attr("data-fancybox", "mygallery");
// assign captions and title from alt-attributes of images:
$(".gallery a").each(function () {
    $(this).attr("data-caption", $(this).find("img").attr("alt"));
    $(this).attr("title", $(this).find("img").attr("alt"));
});


/******************************  product Gallery and Zoom ******************************/

if ($(".single-product-zoom").length) {

    // activation carousel plugin
    var galleryThumbs = new Swiper('.gallery-thumbs', {
        spaceBetween: 5,
        freeMode: true,
        watchSlidesVisibility: true,
        watchSlidesProgress: true,
        breakpoints: {
            0: {
                slidesPerView: 3,
            },
            992: {
                slidesPerView: 4,
            },
        }
    });
    var galleryTop = new Swiper('.gallery-top', {
        spaceBetween: 10,
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        thumbs: {
            swiper: galleryThumbs
        },
    });
    // change carousel item height
    // gallery-top
    let productCarouselTopWidth = $('.gallery-top').outerWidth();
    $('.gallery-top').css('height', productCarouselTopWidth);

    // gallery-thumbs
    let productCarouselThumbsItemWith = $('.gallery-thumbs .swiper-slide').outerWidth();
    $('.gallery-thumbs').css('height', productCarouselThumbsItemWith);

    // activation zoom plugin
    var $easyzoom = $('.easyzoom').easyZoom();

}

/*************************************** input type number cart ***************************************/

document.addEventListener('DOMContentLoaded', function () {
    var inputs = document.getElementsByClassName('input-number-wrapper')

    function incInputNumber(input, step) {
        var val = +input.value
        if (isNaN(val)) val = 0
        val += step
        input.value = val > 1 ? val : 1
        // If you need to change the input value in the DOM :
        // var newValue = val > 0 ? val : 0;
        // input.setAttribute("value", newValue);
    }

    Array.prototype.forEach.call(inputs, function (el) {
        var input = el.getElementsByTagName('input')[0]

        el.getElementsByClassName('increase')[0].addEventListener('click', function () { incInputNumber(input, 1) })
        el.getElementsByClassName('decrease')[0].addEventListener('click', function () { incInputNumber(input, -1) })
    })

});


/*************************************** animation image about us***************************************/

if ($(".area-img-about-us").length) {
    /* Store the element in el */
    let el = document.getElementById('tilt')

    /* Get the height and width of the element */
    const height = el.clientHeight
    const width = el.clientWidth

    /*
      * Add a listener for mousemove event
      * Which will trigger function 'handleMove'
      * On mousemove
      */
    el.addEventListener('mousemove', handleMove)

    /* Define function a */
    function handleMove(e) {
        /*
          * Get position of mouse cursor
          * With respect to the element
          * On mouseover
          */
        /* Store the x position */
        const xVal = e.layerX
        /* Store the y position */
        const yVal = e.layerY

        /*
          * Calculate rotation valuee along the Y-axis
          * Here the multiplier 20 is to
          * Control the rotation
          * You can change the value and see the results
          */
        const yRotation = 20 * ((xVal - width / 2) / width)

        /* Calculate the rotation along the X-axis */
        const xRotation = -20 * ((yVal - height / 2) / height)

        /* Generate string for CSS transform property */
        const string = 'perspective(2000px) scale(1) rotateX(' + xRotation + 'deg) rotateY(' + yRotation + 'deg)'

        /* Apply the calculated transformation */
        el.style.transform = string
    }

    /* Add listener for mouseout event, remove the rotation */
    el.addEventListener('mouseout', function () {
        el.style.transform = 'perspective(500px) scale(1) rotateX(0) rotateY(0)'
    })

    /* Add listener for mousedown event, to simulate click */
    el.addEventListener('mousedown', function () {
        el.style.transform = 'perspective(500px) scale(0.9) rotateX(0) rotateY(0)'
    })

    /* Add listener for mouseup, simulate release of mouse click */
    el.addEventListener('mouseup', function () {
        el.style.transform = 'perspective(500px) scale(1.1) rotateX(0) rotateY(0)'
    })

}

/*************************************** slidtoggle forms checkout ***************************************/


$('#link-login').on("click", function (e) {
    e.preventDefault();
    $('.area-form-login').slideToggle(300);

});


$('#link-coupon').on("click", function (e) {
    e.preventDefault();
    $('.area-coupon').slideToggle(300);

});

$('#createaccount').on("click", function (e) {

    $('.area-create-account').slideToggle(300);

});


$('#input-shipping').on("click", function (e) {

    $('.area-formShippingFields').slideToggle(300);

});
$(".payment_method .form-check-input").on("change", function () {
    var cartsum = $(this).attr("data-crtsum");
    var ct = $(this).attr("data-cur");
    var cur = "ریال";
    if (ct === "T") {
        cur = "تومان";
    }
    var total = cartsum;
    var shipval = $(this).attr("data-shval");
    if ($("#twostep_payment").is(":checked")) {
        total = parseInt(Number(cartsum) / 2, 10);// parseInt(total,10) / 2;
    }
    total = parseInt(total) + parseInt(shipval);
    var subtxt = "پرداخت " + total.toLocaleString() + " " + cur;
    $("#btn-submit-checkout").attr("data-sumpay", total);
    $("#btn-submit-checkout").text(subtxt);
});
$(".payment_method input[type=checkbox]").on("change", function () {
    $(".payment_method input[type=checkbox]").prop('checked', false);
    $(this).prop('checked', true);
});
$("#twostep_payment").on("change", function () {
    var cartsum = $(this).attr("data-crtsum");
    var ct = $(this).attr("data-cur");
    var cur = "ریال";
    if (ct === "T") {
        cur = "تومان";
    }

    var shipval = $(this).attr("data-shval");
    if ($("#shwithpost").is(":checked")) {
        shipval = $("#shwithpost").attr("data-shval");
    }

    var total = parseInt(cartsum, 10);

    if ($(this).is(":checked")) {
        total = parseInt(Number(total) / 2, 10);
    }
    total += parseInt(shipval, 10);
    var subtxt = "پرداخت " + total.toLocaleString() + " " + cur;
    $("#btn-submit-checkout").attr("data-sumpay", total);
    $("#btn-submit-checkout").text(subtxt);
});

/*************************************** shop filter drawer js***************************************/

$("#button-filter-offCavas").on("click", function () {
    $(".sidebar-product-filter").css({ "left": "0" });
    $(".bg-close").css({ "display": "block" });
});
$(".close-sidebarProductFilter").on("click", function () {
    $(".sidebar-product-filter").css({ "left": "-100%" });
    $(".bg-close").css({ "display": "none" });
});

$(".bg-close").on("click", function () {
    $(".sidebar-product-filter").css({ "left": "-100%" });
    $(this).css({ "display": "none" });
});


$("#button-filter-drawer").on("click", function (e) {
    e.preventDefault();
    $(".container-content-shop").toggleClass("active");

});


/*************************************** sticky addtocart single post js***************************************/

$(window).on("scroll", function () {
    if ($(window).scrollTop() > 600) {
        var widthWindows = window.innerWidth;
        if (widthWindows >= 992) {
            $(".area-addToCart-sticky-singlePost").css({ "bottom": "0" });
        }
        if (widthWindows < 992) {
            $(".area-addToCart-sticky-singlePost").css({ "bottom": "3.3rem" });
        }
    } else {
        $(".area-addToCart-sticky-singlePost").css({ "bottom": "-100%" });
    }
});

/***************************************  modal reload homepage js***************************************/
//$(document).ready(function () {
//    $('#modal-subscribe').modal('show');
//    if ($(".exzoom").length) {
//        $('.container').imagesLoaded(function () {
//            $("#exzoom").exzoom({
//                autoPlay: false,
//            });
//            $("#exzoom").removeClass('hidden')
//        });
//    }
//});

/***************************** box side sidebar-account js*****************************/

$(".icon_meni_bar").on("click", function () {
    $(".sidebar-account").css({ "right": "0" });
    $(".bg-close").css({ "display": "block" });
});
$(".close_addtocart").on("click", function () {
    $(".sidebar-account").css({ "right": "-100%" });
    $(".bg-close").css({ "display": "none" });
});

$(".bg-close").on("click", function () {
    $(".sidebar-account").css({ "right": "-100%" });
    $(this).css({ "display": "none" });
});


/***************************** category menu homepage-2 js*****************************/

$(".container-category-menu").on("click", function () {

    $(".area-category-menu>.category-drupdown-menu >ul >li").slideToggle();
});

$(".addTocart").on("click", function () {
    var allowsingle = $(this).attr("data-allowsinglebuy");

    var prId = $(this).attr("data-pid");
    var allowadd = true;
    if (allowsingle == "False") {

        $.ajax({
            url: "/Products?handler=CheckCartForAdding",
            data: { pId: prId },
            type: "GET",
            success: function (result) {
                if (result == false) {
                    Swal.fire({
                        icon: 'warning',
                        title: 'ابتدا محصول دیگری را انتخاب کنید !',
                        showConfirmButton: true,
                        confirmButtonText: 'متوجه شدم',
                    });
                    allowadd = false;
                    return;
                }

            }
        });
    };

    if (allowadd == false) {
        return;
    }

    //prdPrice
    var btnType = $(this).attr("data-btnAddType");
    var datatype = $(this).attr("data-type");
    var returl = $(location).attr('pathname');

    var hascolor = $(this).attr("data-hascolor");
    var hassize = $(this).attr("data-hassize");
    var hascomp = $(this).attr("data-hascomponent");
    var hashight = $(this).attr("data-hasheight");
    var wheree = $(this).attr("data-where");

    var psize = "";
    if (hassize == "True") {
        var selectedsize_option = "";
        if (btnType == "comp") {
            selectedsize_option = $(this).parents(".subs").find(".propdiv .pSize option:selected");
        }
        else if (btnType == "main") {
            selectedsize_option = $(this).parents(".mainprDiv").find(".pSize option:selected");
        }
        psize = selectedsize_option.val();
        if (selectedsize_option.val() != "") {
            selectSize = true;
        }
        else {
            Swal.fire({
                icon: 'warning',
                title: 'سایز را انتخاب کنید !',
                showConfirmButton: true,
                confirmButtonText: 'باشه',
                timer: 4000
            });
            return;
        }
    }
    var pheight = ""
    if (hashight == "True") {
        var selectedhight_option = "";
        if (btnType == "comp") {
            selectedhight_option = $(this).parents(".subs").find(".propdiv .pHeight option:selected");
        }
        else if (btnType == "main") {
            selectedhight_option = $(this).parents(".mainprDiv").find(".pHeight option:selected");
        }
        pheight = selectedhight_option.val();
        if (selectedhight_option.val() != "") {
            selectSize = true;
        }
        else {
            Swal.fire({
                icon: 'warning',
                title: 'قد را انتخاب کنید !',
                showConfirmButton: true,
                confirmButtonText: 'باشه',
                timer: 4000
            });
            return;
        }
    }
    var pcolor = "";
    if (hascolor == "True") {
        var selectedcolor_option = "";
        if (btnType == "comp") {
            selectedcolor_option = $(this).parents(".subs").find(".propdiv .pColor option:selected");
        }
        else if (btnType == "main") {

            selectedcolor_option = $(this).parents(".mainprDiv").find(".pColor option:selected");
        }

        pcolor = selectedcolor_option.val();
        if (selectedcolor_option.val() !== "") {
            selectcolor = true;
        }
        else {
            Swal.fire({
                icon: 'warning',
                title: 'رنگ را انتخاب کنید !',
                showConfirmButton: true,
                confirmButtonText: 'باشه',
                timer: 4000
            });
            return;
        }
    }
    var pq = 1;
    if (btnType == "comp") {
        pq = $(this).parents(".subs").find(".input-number-custtom").val();
    }
    else if (btnType == "main") {
        pq = $(this).parents(".mainprDiv").find(".input-number-custtom").val();
    }
    $.ajax({
        type: "GET",
        url: "/Products?handler=AddtoCart",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: { "where": wheree, "pcount": pq, "pid": prId, "pcolor": pcolor, "pheight": pheight, "psize": psize, retUrl: returl },
        success: function (response) {
            if (response == "yes") {
                Swal.fire({
                    icon: 'success',
                    title: 'به سبد اضافه شد',
                    showConfirmButton: true,
                    confirmButtonText: 'باشه',
                    timer: 4000
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = returl;
                    }
                    else {
                        setTimeout(function () {
                            window.location.href = returl;
                        }, 1);
                    }
                })
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });


});

$(".remove-product,.close_product_addtocart").on("click", function () {

    var itemid = $(this).attr("data-cartitemId");
    var cartid = $(this).attr("data-cartId");
    var pName = $(this).attr('data-pname');

    Swal.fire({
        title: 'آیا مطمئن به حذف ' + pName + ' از سبد هستید؟',
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: 'بله',
        denyButtonText: 'خیر',
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                traditional: true,
                url: "/Products?handler=CheckRemoveCartItem",
                data: { ciId: itemid, cId: cartid },
            }).done(function (response) {
                if (response.canRemove === "yes") {
                    if (response.removed === "yes") {
                        Swal.fire({
                            title: 'محصول از سبد حذف شد !',
                            icon: 'warning',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'باشه',
                            timer: "3000"
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.reload();
                            }
                            else {
                                setTimeout(function () {
                                    window.location.reload();
                                }, 2000);
                            }
                        });
                    }

                }
                else {
                    if (response.mess !== "") {
                        Swal.fire({
                            title: '<h2 class="text-danger">' + 'هشدار' + '</h2>',
                            html: response.mess,
                            icon: 'warning',
                            showCancelButton: true,
                            cancelButtonColor: '#3085d6',
                            confirmButtonColor: '#f00',
                            confirmButtonText: 'باشه',
                            cancelButtonText: 'خیر'
                        }).then((result) => {
                            if (result.isConfirmed) {


                                $.ajax({
                                    type: "POST",
                                    beforeSend: function (xhr) {
                                        xhr.setRequestHeader("XSRF-TOKEN",
                                            $('input:hidden[name="__RequestVerificationToken"]').val());
                                    },
                                    url: "/Products?handler=RemoveCartItems",
                                    dataType: "json",
                                    traditional: true,
                                    data: { cisId: response.remCIs, cId: cartid },
                                    success: function (response) {

                                        if (response.rem === "yes") {
                                            Swal.fire({
                                                icon: 'success',
                                                title: 'اطلاعات با موفقیت حذف شدند !',
                                                showConfirmButton: true,
                                                confirmButtonText: "متوجه شدم",
                                                timer: 3000
                                            }).then((result) => {
                                                if (result.isConfirmed) {
                                                    window.location.reload();
                                                }
                                                else {
                                                    setTimeout(function () {
                                                        window.location.reload();
                                                    }, 2000);
                                                }
                                            });

                                        }
                                    }
                                });
                            }

                        });
                    }
                }
            });

        }
    });
});
$("#btnReturn").on("click", function () {
    $("#myModal .modal-title").html("<h5 calss='alert alert-info text-center'>شرایط ارسال و عودت</h5>");
    var html = "<div>";
    html += "<h4 class='text-danger'>";
    html += "ارسال سفارش به چه صورت است ؟";
    html += "</h4>";
    html += "<p class='text-justify'>";
    html += "ارسال بسته ها از طریق پست پیشتاز انجام میشود. در صورت تمایل مشتری به ارسال با تیپاکس، به صورت پرداخت درب منزل هم امکان پذیر است. برای مشتریان عزیز تهران و کرج در صورت درخواست ، ارسال با اسنپ (هزینه به عهده مشتری) انجام میشود.";
    html += "</p>";
    html += "<h4 class='text-danger'>";
    html += "عودت هم دارین؟";
    html += "</h4>";
    html += "<p>";
    html += "در صورتیکه مشکلی تو سفارشتون وجود داشته باشه و از جانب ما باشه ، با شماره موجود در قسمت پشتیبانی سایت تماس بگیرین تا همکاران ما، شما رو راهنمایی کنن.";
    html += "</p>";
    html += "</div>";
    $("#myModal .modal-body").html(html);
    $('#myModal').modal('show');
});
$(".pHeight").on("change", function () {
    var heght = $(this).find(':selected').val();
    var Eltype = $(this).find(':selected').attr("data-type");
    var sz = $(this).parents(".propdiv").find(".pSize").val();
    if (Eltype === "single") {
        sz = $(this).parents(".mainprDiv").find(".pSize").val();
    }
    if (typeof (heght) !== "undefined" && typeof (sz) !== "undefined") {
        if (heght !== "" & sz !== "") {
            var lblid = $(this).children("option:selected").attr("data-lblId");
            var prdcode = $(this).find(':selected').attr("data-pcode");
            var locality = 'en-US';
            $.ajax({
                url: "/Products?handler=ProductPriceInfo",
                data: { "prCode": prdcode, "height": heght, "size": sz },
                type: "GET",
                success: function (result) {
                    if (Eltype === "multi") {
                        $("#" + lblid).text(result.netPrice.toLocaleString(locality));
                    }
                    else if (Eltype === "single") {
                        $(document).find("#prdPrice").text(result.netPrice.toLocaleString(locality));
                    }
                },
                error: function () {
                    alert('error');
                }
            });
        }
    }


});
$(".pSize").on("change", function () {
    var siz = $(this).find(':selected').val();
    var Eltype = $(this).find(':selected').attr("data-type");
    var hgt = $(this).parents(".propdiv").find(".pHeight").val();
    if (Eltype === "single") {
        hgt = $(this).parents(".mainprDiv").find(".pHeight").val();
    }

    if (typeof (hgt) !== "undefined" && typeof (sz) !== "undefined") {
        if (siz !== "" && hgt !== "") {

            var lblid = $(this).children("option:selected").attr("data-lblId");
            var prdcode = $(this).find(':selected').attr("data-pcode");
            var locality = 'en-US';
            $.ajax({
                url: "/Products?handler=ProductPriceInfo",
                data: { "prCode": prdcode, "height": hgt, "size": siz },
                type: "GET",
                success: function (result) {
                    if (Eltype === "multi") {
                        $("#" + lblid).text(result.netPrice.toLocaleString(locality));
                    }
                    else if (Eltype === "single") {
                        $(document).find("#prdPrice").text(result.netPrice.toLocaleString(locality));
                    }
                },
                error: function () {
                    alert('error');
                }
            });
        };
    }

});

$(".btnShowSize").on("click", function () {
    var pid = $(this).attr("data-productId");

    $.ajax({
        url: "/Products?handler=ShowProductSizes",
        data: { pId: pid },
        type: "GET",
        success: function (result) {
            Swal.fire({
                customClass: 'swal-width',
                confirmButtonText: `بستن`,
                html:
                    result,
            });
        }
    });

});
$(".remove_pInfofromcartItem").on("click", function () {
    var itemId = $(this).attr("data-id");
    var prName = $(this).attr("data-pname");
    var cartidd = $(this).attr("data-cartid");
    //alert(cartidd);
    Swal.fire({
        text: 'آیا مطمئن به حذف یک ردیف از ویژگی‌های محصول' + ' ' + prName + ' ' + 'از سبد هستید؟ ',
        /*text: "You won't be able to revert this!",*/
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                url: "/Products?handler=CheckRemoveCPIFromCartItem",
                data: { cpiId: itemId, cId: cartidd },
            }).done(function (response) {
                if (response.canRemove === "yes") {
                    if (response.removed === "yes") {
                        Swal.fire({
                            title: 'ردیف محصول حذف شد !',
                            icon: 'warning',
                            showCancelButton: false,
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: 'باشه',
                            timer: "3000"
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.reload();
                            }
                            else {
                                setTimeout(function () {
                                    window.location.reload();
                                }, 2000);
                            }
                        });
                    }

                }
                else {
                    if (response.mess !== "") {
                        Swal.fire({
                            title: '<h2 class="text-danger">' + 'هشدار' + '</h2>',
                            html: response.mess,
                            icon: 'warning',
                            showCancelButton: true,
                            cancelButtonColor: '#3085d6',
                            confirmButtonColor: '#f00',
                            confirmButtonText: 'باشه',
                            cancelButtonText: 'خیر'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $.ajax({
                                    type: "POST",
                                    beforeSend: function (xhr) {
                                        xhr.setRequestHeader("XSRF-TOKEN",
                                            $('input:hidden[name="__RequestVerificationToken"]').val());
                                    },
                                    url: "/Products?handler=RemoveCartItems",
                                    dataType: "json",
                                    traditional: true,
                                    data: { cisId: response.remCIs, cId: cartidd },
                                    success: function (response) {

                                        if (response.rem === "yes") {
                                            Swal.fire({
                                                icon: 'success',
                                                title: 'اطلاعات با موفقیت حذف شدند !',
                                                showConfirmButton: true,
                                                confirmButtonText: "متوجه شدم",
                                                timer: 3000
                                            }).then((result) => {
                                                if (result.isConfirmed) {
                                                    window.location.reload();
                                                }
                                                else {
                                                    setTimeout(function () {
                                                        window.location.reload();
                                                    }, 2000);
                                                }
                                            });

                                        }
                                    }
                                });
                            }

                        });
                    }
                }
            });

        }
    })



});

$('#z-owl').owlCarousel({
    items: 1,
    loop: true,
    nav: true,
    rtl: true,
    autoplay: true,
    autoplayTimeout: 5000,
    autoplayHoverPause: true,
    mouseDrag: true,
    margin: 0,
    center: true,
    dots: false,
    smartSpeed: 2000,
    slideSpeed: 5000,
    animateOut: "rotateOut",
    animateIn: "zoomIn",
    lazyLoad: true,
});
/*https://animate.style*/

$("#zcrousel .item").on("click", function () {
    var lnk = $(this).attr("data-z");
    showContent(lnk);
    $('html, body').animate({
        scrollTop: $("#pills-tabContent").offset().top - 150
    }, 1000);

});
function showContent(lnk) {
    var tabid = lnk + "-tab";
    $("#tabs li a").removeClass("active");
    $("#tabs " + tabid).addClass("active");
    var num = lnk.match(/\d+/);
    var target = "#pills-" + num;
    $(".tab-pane").removeClass("show active");
    $(target).addClass("show active");
};

$("#zcrousel").on("mouseenter", function () {
    $(".owl-nav").css("display", "block");
});
$("#zcrousel").on("mouseleave", function () {
    $(".owl-nav").css("display", "none");
});






