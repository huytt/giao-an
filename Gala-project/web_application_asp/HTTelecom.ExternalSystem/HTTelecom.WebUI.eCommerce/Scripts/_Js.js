$("#menu-btnMenuLeft").sideNav({ menuWidth: 300 });
var d = new Date();
if (gLang == "vi")
    $('.datepicker').pickadate({
        selectMonths: true,
        selectYears: 100,
        formatSubmit: 'yyyy/mm/dd',
        hiddenName: true,
        min: new Date((d.getFullYear() - 100), 1, 1),
        max: new Date((d.getFullYear() - 6), 12, 31),
        today: '',
        monthsFull: gMonthsFull,
        monthsShort: gMonthsShort,
        weekdaysFull: gWeekdaysFull,
        weekdaysShort: gWeekdaysShort,
        weekdaysLetter: gWeekdaysLetter,
        clear: gClear,
        close: gClose,
        onSet: function (context) {
            if (typeof context.select != "undefined")
                this.close();
        }
    });
else
    $('.datepicker').pickadate({
        today: '',
        min: new Date(d.getFullYear() - 100, 1, 1),
        max: new Date(d.getFullYear() - 5, 12, 30),
        selectMonths: true,
        selectYears: 100,
        formatSubmit: 'yyyy/mm/dd',
        hiddenName: true,
        min: new Date((d.getFullYear() - 100), 1, 1),
        max: new Date((d.getFullYear() - 6), 12, 31),
        onSet: function (context) {
            if (typeof context.select != "undefined")
                this.close();
        }
    });
$(".changeStyleColour").click(function (e) {
    var fi = $(".changeStyleColour");
    for (var i = 0; i < fi.length; i++) $(fi[i]).removeClass("active")
    $(this).addClass("active")
    createCookie("colourGala", $(this).attr('data-value'), "365")
    changeColor($(this).attr('data-style'))
    //$("#banner").find("img").attr('src', '/Content/styles/' + $(this).attr('data-logo'))
});
var gPosition = -1;
$(window).scroll(function () {
    if ($(window).scrollTop() >= gPosition + 100) {
        $("#pnlProductPrice").fadeIn("slow");
        $("#pnlStoreFilter").fadeIn("slow");
        gPosition = $(window).scrollTop();
    }
    if ($(window).scrollTop() < gPosition - 100) {
        $("#pnlProductPrice").fadeOut("fast");
        $("#pnlStoreFilter").fadeOut("fast");
        gPosition = $(window).scrollTop();
    }
});
equalheight = function (t) { var i, e = 0, h = 0, r = new Array; $(t).each(function () { if (i = $(this), $(i).height("auto"), topPostion = i.position().top, h != topPostion) { for (currentDiv = 0; currentDiv < r.length; currentDiv++) r[currentDiv].height(e); r.length = 0, h = topPostion, e = i.height(), r.push(i) } else r.push(i), e = e < i.height() ? i.height() : e; for (currentDiv = 0; currentDiv < r.length; currentDiv++) r[currentDiv].height(e) }) };
var lstStyle = [{ name: "style1", value: "#FF8600", logo: "orange.png" }, { name: "style2", value: "#FF1066", logo: "purple.png" }, { name: "style3", value: "#00BBD9", logo: "blue.png" }, { name: "style4", value: "#00CC2E", logo: "green.png" }];
$(document).ready(function () {
    $("img.user-afterloadImage").lazyload(function () {
        $(window).resize();
    });
    $('.modal-trigger').leanModal();
    $("#selLanguage").material_select();
    ajaxGet(gLinkGetMoneyCart);
    ajaxGet_Message(gLinkMessage);
    var sm_temp = readCookie('smartBanner-gala-temp');
    if (sm_temp == null) {
        //$('.modal-trigger').leanModal();
        //$("#modal1").openModal();
        createCookie('smartBanner-gala-temp', '1', 1);
    }
    $("#footer-gala-title").click(function () {
        var n = $("#footer-article-Galagala"), a = $("#footer-gala-title");
        if (n.css("display") == "none") a.html('<i class="fa-icon fa-icon-top-111"></i>');
        else a.html('<i class="fa-icon fa-icon-bottom-114"></i>'); n.toggle();
    });
    var sm = readCookie('smartBanner-gala');
    if (sm == null) {
        var smlink = '<div id="smartbanner" class="ios" style=" position: fixed; bottom: 0px; z-index: 99; height: 79px;">'
            + '<div class="sb-container">'
             + '   <a href="#" class="sb-close" id="btn-Clost-smartBanner">×</a>'
             + '   <span class="sb-icon gloss" style="background-image: url(http://galagala.vn:88/Content/icon.png);"></span>'
             + '   <div class="sb-info">'
             + '       <strong>GALAGALA - website bán hàng trực tuyến</strong>'
             + '       <span>IOS - ANDROID</span>'
             + '       <span>GALAGALA.VN</span>'
             + '   </div>'
             + '   <a href="#" onclick="alert(\'Update later.\')" class="sb-button">'
             + '       <span>VIEW</span>'
             + '   </a>'
           + ' </div>'
       + ' </div>';
        $("#main-body-content").append(smlink);

    }
    $("#header-btnSearch").click(function () {
        $("#navTop-Search").css('display', 'block');
        $(".user-overlay-main").css('display', 'block');
        $(".user-overlay-main").css('opacity', '1');
        $("#Seach-txtSearch").focus();
    });
    $(".user-itemProductSale").click(function () {
        var e = $(this);
        e = typeof e.attr("data-x") == "undefined" ? e.prev() : e;
        if (e.hasClass("open")) $(e).attr('src', e.attr("data-x")); else $(e).attr('src', e.attr("data-y"));
        e.toggleClass("open");
    });
    $(".user-overlay-main").click(function () {
        $("#navTop-Search").css('display', 'none');
        $(".user-overlay-main").css('display', 'none');
        $(".user-overlay-main").css('opacity', '0');
    });
    $("#Seach-btnSearch").click(function () {
        $("#navTop-Search").css('display', 'none');
        $(".user-overlay-main").css('opacity', '0');
        $(".user-overlay-main").css('display', 'none');
        if ($("#Seach-txtSearch").val().trim().length > 0) {
            var href = $(this).attr('data-href');
            window.location.href = href.replace('__q__', encodeURIComponent($("#Seach-txtSearch").val())).replace('__q__', encodeURIComponent($("#Seach-txtSearch").val()));
        }
    });
    $("#btn-close-Search").click(function () {
        $("#navTop-Search").css('display', 'none');
        $(".user-overlay-main").css('display', 'none');
        $(".user-overlay-main").css('opacity', '0');
    });
    //btn-close-Search
    $("#btn-full-screen").click(function () {
        $("#full #contain").children().remove();
        $("#full").children(".content").children('.view').append($(".show-full-screen"));
        $("#full").css('display', 'table');
        var lstImage = $(".show-full-screen").find(".swiper-slide").find('img');
        var divContainer  = document.createElement("div"),  divWrapper = document.createElement("div");
        divContainer.className = "swiper-container full-main";
        divWrapper.className = "swiper-wrapper"
        divContainer.style.width = '100%';
        divContainer.style.height = '100%';
        divContainer.style.color = '#FFF';
        divContainer.style.textAlign = 'center';
        divContainer.appendChild(divWrapper)
            //width: 100%;height: 100%;color: #fff;text-align: center;
        for (var i = 0; i < lstImage.length; i++) {
            var div = document.createElement("div");
            div.className = "swiper-slide"
            $(div).css('min-height', '100%').css('display',' table')
            var div1 = document.createElement("div");
            $(div1).css('display', 'table-cell').css('vertical-align', 'middle')
            var sourceImage = document.createElement('img');
            sourceImage.src = lstImage[i].src == "" || typeof lstImage[i].src == "undefined" ? $(lstImage[i]).attr("data-src") : lstImage[i].src;
            $(sourceImage).css('max-width', '100%').css('max-height', '100%').css('max-height', '100vh');
            $(div1).append($(sourceImage).clone())
            div.appendChild(div1)
            $(divWrapper).append(div);
        }
        $("#full #contain").append(divContainer);
        var fullSwiper = new Swiper(".full-main", {
            paginationClickable: true
        })
        
        //$("#full").children(".content").children('.view').css('margin-top', $(window).width()+"px");
        $("#header").css('max-height', '0'), $("#main-body").css('max-height', '0'), $(".page-footer").css('max-height', '0')
        $("#header").css('overflow', 'hidden'), $("#main-body").css('overflow', 'hidden').css('min-height', '0px'), $(".page-footer").css('overflow', 'hidden')
    })
    $("#btn-normal-screen").click(function () {
        //$("#btn-full-screen").parent().append($("#full").children(".content").children('.view').children());
        $("#full").css('display', 'none');
        //$("#full #contain").children().remove();
        $("#header").css('max-height', '100%'), $("#main-body").css('max-height', '100%'), $(".page-footer").css('max-height', '100%')
        $("#header").css('overflow', 'initial'), $("#main-body").css('overflow', 'auto').css('min-height', '373px'), $(".page-footer").css('overflow', 'auto')
        //swiper1.update(true)
    })
    $(".itemCategory").click(function () {
        $(this).attr('data-type') == "false" ? window.location.href = $(this).attr('data-href') : '';
    });
    $("#btnAddToCart").click(function (e) {
        var id = $(this).attr('data-id'),
            Size = $("select[name='SizeId']").val();
        $(".alertErrorBuynow").remove();
        if (Size == null) {
            //var alert = '<div class="user-margin-0 user-col-0 user-margin-top-5 user-margin-bottom-5 alertErrorBuynow" style="background-color: #F2DEDE;color: #A94442;padding: 10px;border-radius: 5px;">'
            //+ 'Please select Size' + '</div>';
            //$("#divBuyNow").append(alert);
            alert("Please select Size")
            $('html,body').animate({ scrollTop: $(".alertErrorBuynow").offset().top - 200 })
            return;
        }
        if ($("select[name='numBuyNow']").val() == null || $("select[name='numBuyNow']").val() > 5 || $("select[name='numBuyNow']").val() < 0) {
            //var alert = '<div class="user-margin-0 user-col-0 user-margin-top-5 user-margin-bottom-5 alertErrorBuynow" style="background-color: #F2DEDE;color: #A94442;padding: 10px;border-radius: 5px;">'
            //+ 'Please select Quantity' + '</div>';
            //$("#divBuyNow").append(alert);

            alert("Please select Size")
            $('html,body').animate({ scrollTop: $(".alertErrorBuynow").offset().top - 200 })
            return;
        }
        $.ajax({
            type: "POST",
            url: $(this).attr('data-url'),
            data: { id: id, Size: Size, Quantity: $("select[name='numBuyNow']").val() },
            cache: false,
            dataType: "json",
            success: function (dt) {
                if (dt.result == true) {
                    //$(e.target).html($(e.target).attr('data-language')),
                    //$(e.target).removeAttr("id");
                    updateValue("#header-count-cart", dt.newValue);
                    updateValue("#header-count-cart-left", dt.newValue);
                    window.location = dt.url;
                    //if (typeof $(e.target).attr("data-run") != "undefined" && $(e.target).attr("data-run") == "true")
                    //    $("#header-count-cart").html(dt.newValue);
                    //else
                    //    $("#header-count-cart").html(dt.newValue)
                    //        .attr('data-run', 'true')
                    //        .animate({ color: 'black!important', 'font-size': '40px', 'margin-top': '50px', 'margin-left': '-30px', 'z-index': 999999 })
                    //        .delay(505)
                    //        .animate({ 'font-size': '15px', 'margin-top': '00px', 'margin-left': '0px', 'z-index': 999999 })
                    //        .attr('data-run', 'false');
                }
            }, error: function (error) { }
        }); //end ajax call
    });
    $(".user-collapsible").click(function () {
        var e = $(this);
        e.find(".collapsible-icon-change").toggleClass(e.attr("data-toggle"));
    })
    $(".btnAddToCart").click(function (e) {
        var id = $(this).attr('data-id');
        $.ajax({
            type: "POST",
            url: $(this).attr('data-url'),
            data: { id: id, Quantity: 1, Size: 0 },
            cache: false,
            dataType: "json",
            success: function (dt) {
                var target = e.target.hasAttribute('data-url') == true ? e.target : e.target.parentElement;
                if (dt.result == true) {
                    $(target).remove();
                    //if (typeof $(target).attr("data-run") != "undefined" && $(target).attr("data-run") == "true")
                    //    $("#header-count-cart").html(dt.newValue);
                    //else
                    updateValue("#header-count-cart", dt.newValue);
                    updateValue("#header-count-cart-left", dt.newValue);
                    //$("#header-count-cart").html(dt.newValue)
                    //    .attr('data-run', 'true')
                    //    .animate({ color: 'black!important', 'font-size': '40px', 'margin-top': '50px', 'margin-left': '-30px', 'z-index': 999999 })
                    //    .delay(505)
                    //    .animate({ 'font-size': '15px', 'margin-top': '00px', 'margin-left': '0px', 'z-index': 999999 })
                    //    .attr('data-run', 'false');
                }
            }, error: function (error) { }
        }); //end ajax call
    });
    $(".btnAddToWishlist").click(function (e) {
        var id = $(this).attr('data-id');
        $.ajax({
            type: "POST",
            url: $(this).attr('data-url'),
            data: { id: id, action: $(this).attr('data-value') },
            cache: false,
            dataType: "json",
            success: function (dt) {
                var target = e.target.hasAttribute('data-y') == true ? e.target : e.target.parentElement;
                if (dt.result == 1) {
                    var lst = $('.btnAddToWishlist[data-id="' + id + '"]');
                    for (var i = 0; i < lst.length; i++) {
                        $(lst[i]).html("<i class='" + $(lst[i]).attr('data-x') + "' style='font-size: 35px;'></i>");
                        $(lst[i]).attr('data-value', 'true');
                    }
                    updateValue("#header-count-wishlist", dt.newValue);
                }
                else if (dt.result == 2) {
                    var lst = $('.btnAddToWishlist[data-id="' + id + '"]');
                    for (var i = 0; i < lst.length; i++) {
                        $(lst[i]).html("<i class='" + $(lst[i]).attr('data-y') + "' style='font-size: 35px;'></i>"),
                        $(lst[i]).attr('data-value', 'false');
                    }
                    updateValue("#header-count-wishlist", dt.newValue);
                }
                else window.location.href = $(target).attr('data-redirect')
            }, error: function (error) { }
        }); //end ajax call
    });
    function updateValue(str, value) {
        if (typeof $(str).attr("data-run") != "undefined" && $(str).attr("data-run") == "true")
            $(str).html(value);
        else {
            $(str).html(value)
                            .attr('data-run', 'true')
                            .animate({ color: 'black!important', 'font-size': '40px', 'margin-top': '50px', 'margin-left': '-30px', 'z-index': 999999 });
            setTimeout(function () {
                $(str).animate({ 'font-size': '15px', 'margin-top': '00px', 'margin-left': '0px', 'z-index': 999999 })
                                .attr('data-run', 'false');
            }, 1000);
        }
        //$(str).html(value).delay(505)
        //                    .animate({ 'font-size': '15px', 'margin-top': '00px', 'margin-left': '0px', 'z-index': 999999 })
        //                    .attr('data-run', 'false');
    }
    $(".btnAddToWishlist-Product").click(function (e) {
        var id = $(this).attr('data-id');
        $.ajax({
            type: "POST",
            url: $(this).attr('data-url'),
            data: { id: id, action: $(this).attr('data-value') },
            cache: false,
            dataType: "json",
            success: function (dt) {
                var target = e.target.hasAttribute('data-y') == true ? e.target : e.target.parentElement;
                if (dt.result == 1) {
                    $(target).html("<i class='" + $(target).attr('data-x') + "' style='  transform: scale(.4);float:left;'></i> " + $(target).attr('data-language-x')),
                    $(target).attr('data-value', 'true');
                    //if (typeof $(target).attr("data-run") != "undefined" && $(target).attr("data-run") == "true")
                    //    $("#header-count-wishlist").html(dt.newValue);
                    //else
                    updateValue("#header-count-wishlist", dt.newValue);
                    //$("#header-count-wishlist").html(dt.newValue)
                    //    .animate({ color: 'black!important', 'font-size': '40px', 'margin-top': '50px', 'margin-left': '-30px', 'z-index': 999999 })
                    //    .delay(505)
                    //    .animate({ 'font-size': '15px', 'margin-top': '00px', 'margin-left': '0px', 'z-index': 999999 })
                }
                else if (dt.result == 2) {
                    $(target).html("<i class='" + $(target).attr('data-y') + "' style='  transform: scale(.4);float:left;margin-right: 15px;'></i>" + $(target).attr('data-language-y')),
                    $(target).attr('data-value', 'false');
                    //if (typeof $(target).attr("data-run") != "undefined" && $(target).attr("data-run") == "true")
                    //    $("#header-count-wishlist").html(dt.newValue);
                    //else
                    updateValue("#header-count-wishlist", dt.newValue);
                    //$("#header-count-wishlist").html(dt.newValue)
                    //    .animate({ color: 'black!important', 'font-size': '40px', 'margin-top': '50px', 'margin-left': '-30px', 'z-index': 999999 })
                    //    .delay(505)
                    //    .animate({ 'font-size': '15px', 'margin-top': '00px', 'margin-left': '0px', 'z-index': 999999 })
                }
                else window.location.href = $(target).attr('data-redirect')
            }, error: function (error) { }
        }); //end ajax call
    });
    $(".btnRemoveItemCart").click(function (e) {
        var id = $(this).attr('data-id');
        $.ajax({
            type: "POST",
            url: $(this).attr('data-url'),
            data: { id: id },
            cache: false,
            dataType: "json",
            success: function (dt) {
                var target = e.target.hasAttribute('data-url') == true ? e.target : e.target.parentElement;
                if (dt.result == true) {
                    $(target).parent().parent().remove();
                    //if (typeof $(target).attr("data-run") != "undefined" && $(target).attr("data-run") == "true")
                    //    $("#header-count-cart").html(dt.newValue);
                    //else
                    updateValue("#header-count-cart", dt.newValue);
                    updateValue("#header-count-cart-left", dt.newValue);
                    //$("#header-count-cart").html(dt.newValue)
                    //    .animate({ color: 'black!important', 'font-size': '40px', 'margin-top': '50px', 'margin-left': '-30px', 'z-index': 999999 })
                    //    .delay(505)
                    //    .animate({ 'font-size': '15px', 'margin-top': '00px', 'margin-left': '0px', 'z-index': 999999 });
                    setTotalMoney();
                }
                else alert("Dont delete")
            }, error: function (error) { }
        }); //end ajax call
    });
    $(".btnRemoveWishlist").click(function (e) {
        var id = $(this).attr('data-id');
        $.ajax({
            type: "POST",
            url: $(this).attr('data-url'),
            data: { id: id, action: $(this).attr('data-value') },
            cache: false,
            dataType: "json",
            success: function (dt) {
                var target = e.target.hasAttribute('data-y') == true ? e.target : e.target.parentElement;
                if (dt.result == 1) {
                    $(target).parent().parent().remove();
                    //if (typeof $(target).attr("data-run") != "undefined" && $(target).attr("data-run") == "true")
                    //    $("#header-count-wishlist").html(dt.newValue);
                    //else
                    updateValue("#header-count-wishlist", dt.newValue);
                    //$("#header-count-wishlist").html(dt.newValue)
                    //    .animate({ color: 'black!important', 'font-size': '40px', 'margin-top': '50px', 'margin-left': '-30px', 'z-index': 999999 })
                    //    .delay(505)
                    //    .animate({ 'font-size': '15px', 'margin-top': '00px', 'margin-left': '0px', 'z-index': 999999 });
                }
                else alert("Dont delete");
            }, error: function (error) { }
        }); //end ajax call
    });
    $(".step-card-number").keydown(function (e) {
        var t = $(this), nb = t.attr('step');
        if (t.val().length > 4) {
            t.val(t.val().substring(0, 4)), e.preventDefault();
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            /*e.preventDefault();*/
        }
        else if (t.val().length == 4 && parseInt(nb) < 4) {
            $('.step-card-number[step="' + (parseInt(nb) + 1) + '"]').select().focus();
            return;
        }
        else {
            //e.preventDefault();
        }
    });
    $(".number-price").keydown(function (e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
            return;
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $(".number-price-change").keydown(function (e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
            return;
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
    $(".number-price-change").change(function () {
        if ($(this).val() <= 0) $(this).val(1)
        if ($(this).val() > 5) $(this).val(5)
        var f = $($(this).parent().parent().children(".totalPricechilldren")).children();
        var money = Number($(this).val()) * Number(f.attr('data-money'));
        f.attr('data-totalmoney', money);
        money = format_money(money) + " Đ";
        f.text(money);
        setTotalMoney();
    });
    $("#Seach-txtSearch").keydown(function (e) {
        if (e.keyCode == 13) {
            $("#Seach-btnSearch").click();
        }
    });
    $("#btnDeleteAllWishList").click(function (e) {
        $.ajax({
            type: "POST",
            url: $(this).attr('data-url'),
            data: {},
            cache: false,
            dataType: "json",
            success: function (dt) {
                if (dt.result == 1) {
                    $("." + $("#btnDeleteAllWishList").attr('data-html')).remove(),
                    $("#btnDeleteAllWishList").remove();
                    if (typeof $(target).attr("data-run") != "undefined" && $(target).attr("data-run") == "true")
                        $("#header-count-wishlist").html(0);
                    else
                        updateValue("#header-count-wishlist", 0);
                }
            }
        }); //end ajax call
    });
    $(".user_read_more").click(function () {
        var target = $('div[data-target="' + $(this).attr("data-target") + '"]');
        if (target.attr('data-flag') == "false") {
            $('a[data-type="user-mini-div"]a[data-target="' + $(this).attr("data-target") + '"]').removeClass('user-hidden');
            var t = 0; target.children().each(function () {
                t = t + $(this).outerHeight();
            });
            target.animate({ height: (t + 10) }, 200);
            target.attr('data-flag', "true");
            $(this).html($(this).attr('data-y'));
        }
        else {
            $('a[data-type="user-mini-div"]a[data-target="' + $(this).attr("data-target") + '"]').addClass('user-hidden');
            var t = 0; target.children().each(function () {
                t = t + $(this).outerHeight();
            });
            target.animate({ height: '0px' }, 200);
            target.attr('data-flag', "false");
            $(this).html($(this).attr('data-x'));
        }
    });
    $(".user-mini-div").click(function () {
        var target = $('div[data-target="' + $(this).attr("data-target") + '"]');
        $(this).addClass('user-hidden');
        var t = 0; target.children().each(function () {
            t = t + $(this).outerHeight();
        });
        target.animate({ height: '0px' }, 200);
        target.attr('data-flag', "false");
        $('a[data-type="user-show-div"]a[data-target="' + $(this).attr("data-target") + '"]').html($($('a[data-type="user-show-div"]a[data-target="' + $(this).attr("data-target") + '"]')).attr('data-x'));
    });
    $(".btn-remove-public").click(function (e) {
        var t = $(e.target).hasClass("btn-remove-public") ? e.target : $(e.target).parent().hasClass("btn-remove-public") ? $(e.target).parent() : $(e.target).parent().parent();
        $("#" + $(t).attr("data-target")).remove();
        $(".user-overlay-main").css('opacity', 0).css('display', 'none');
    });
    $("#btnAdd-Quantity").click(function () {
        var target = $(this).attr("data-target");
        var oldVl = $("#" + target).val(), vl = oldVl == "" || oldVl <= 0 || isNaN(oldVl) == true ? 0 : oldVl >= 100 ? 99 : oldVl; $("#" + target).val(parseInt(vl) + 1);
    });
    $("#btnMinus-Quantity").click(function () {
        var target = $(this).attr("data-target");
        var oldVl = $("#" + target).val(),
            vl = oldVl == "" || oldVl <= 1 || isNaN(oldVl) == true ? 2 : oldVl;
        $("#" + target).val(parseInt(vl) - 1);
    });
    $("#btnSubmitBuyNow").click(function () {
        $(".alertErrorBuynow").remove();
        var target = $("#btnSubmitBuyNow"),
            num = $("#" + target.attr('data-object')).val(),
            f = target.attr('data-target'), Size = $("select[name='SizeId']").val();
        if (Size == null) {
            var alert = '<div class="user-margin-0 user-col-0 user-margin-top-5 user-margin-bottom-5 alertErrorBuynow" style="background-color: #F2DEDE;color: #A94442;padding: 10px;border-radius: 5px;">'
            + 'Please select Size' + '</div>';
            $("#divBuyNow").append(alert);
            return;
        }
        if (num == "" || num <= 0 || isNaN(num) == true || num > 5) {
            //$("#error_numberQuantity").css('display', 'block').text('Quantity must be number, between 1 to 100');
            var alert = '<div class="user-margin-0 user-col-0 user-margin-top-5 user-margin-bottom-5 alertErrorBuynow" style="background-color: #F2DEDE;color: #A94442;padding: 10px;border-radius: 5px;">'
            + 'Quantity must be number, between 1 to 5' + '</div>';
            $("#divBuyNow").append(alert);
            return;
        } else {
            if (gLogined)
                $("#" + f).submit();
            else {
                $.ajax({
                    type: "POST",
                    url: $(this).attr('data-partial'),
                    data: { quantity: num, product: target.attr('data-data') },
                    cache: false,
                    dataType: "html",
                    success: function (dt) {
                        $("body").append("<fieldset id='loginForm' class='modal'>" + dt + "</fieldset>");
                        $('#loginForm').openModal();
                    }, error: function (error) { }
                }); //end ajax call
            }
        }
    });
    $(".user-processing").click(function (e) {
        var load = '<div id="Loading" style="position: fixed;top: 0px;left: 0px;right: 0px;bottom: 0px;background: #000;z-index: 9999;opacity: 0.85;text-align: center;"><div style="width: 200px;height: 100px;position: absolute;top: 50%;left: 50%;margin-top: -50px;margin-left: -100px;background: white;"><img src="/Images/loading.gif"><p style="text-align: center;">Processing ... </p></div></div>';
        var target = $(this); $(target.attr('data-processing')).append(load);
    });
    $("#btn-Clost-smartBanner").click(function () {
        createCookie('smartBanner-gala', '1', 10);
        $(this).parent().parent().remove();

    });
});
$(document).on('click', '.user-toggle-button', function (e) {
    var button = $(e.target).hasClass("user-toggle-button") == true ? $(e.target) : $(e.target).parent();
    var target = $('.user-toggle-element[data-target="' + $(button).attr("data-target") + '"]');
    if ($(target).hasClass("user-toggle-hidden")) {
        $(target).removeClass("user-toggle-hidden");
        var t = 0; target.children().each(function () {
            t = t + $(this).outerHeight();
        });
        target.animate({ height: (t + 10) }, 200);
        $(button).children("i").addClass($(button).attr("data-i-min"));
    }
    else {
        $(target).addClass("user-toggle-hidden");
        target.animate({ height: $(button).attr("data-min") + "px" }, 200);
        $(button).children("i").removeClass($(button).attr("data-i-min"));
    }
});
//function changeColor(it) {
//    $.map($('[data-element="background"]'), function (ex, ix) { $(ex).css('background-color', it); })
//    $.map($('[data-element="color"]'), function (ex, ix) { $(ex).css('color', it); })
//    $.map($('[data-element="border-bottom"]'), function (ex, ix) { $(ex).css('border-bottom-color', it); })
//};
function setTotalMoney() {
    var l = $(".user-totalpriceProduct");
    var money = 0;
    $("#totalPriceAll").text('0 đ')
    for (var i = 0; i < l.length; i++)
        money += Number($(l[i]).attr('data-totalmoney')), $("#totalPriceAll").text(format_money(money) + " đ")
};
function format_money(n) {
    n = parseFloat(n);
    return n.toFixed(0).replace(/./g, function (c, i, a) {
        return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
    });
};

function ajaxGet(link) {
    $.ajax({
        type: "GET",
        url: link,
        //data: { id: id },
        cache: false,
        dataType: "string",
        success: function (dt) {
            //$("#menuLeft-moneyCart").text(dt)
            console.log(dt)
        }, error: function (error) {
            $("#menuLeft-moneyCart").text(error.responseText)
        }
    }); //end ajax call
}
function ajaxGet_Message(link) {
    $.ajax({
        type: "POST",
        url: link,
        cache: false,
        dataType: "json",
        success: function (dt) {
            console.log(dt)
            if (dt.result) {
                $('.modal-trigger').leanModal();
                $("#modal1").openModal()
            }
        }, error: function (error) {
            //$("#menuLeft-moneyCart").text(error.responseText)
            //$('.modal-trigger').leanModal();
            //$("#modal1").openModal();
        }
    }); //end ajax call
}
function changeLanguage(input) { var href = $(input).attr("data-url"), href = href.replace('_lang_', $(input).val()), href = href.replace(/&amp;/g, '&'); window.location.href = href; };
$.fn.materialbox = function () {

    return this.each(function () {

        if ($(this).hasClass('intialized')) {
            return;
        }

        $(this).addClass('intialized');

        var overlayActive = false;
        var doneAnimating = true;
        var inDuration = 275;
        var outDuration = 200;
        var origin = $(this);
        var placeholder = $('<div></div>').addClass('material-placeholder');
        var originalWidth = 0;
        var originalHeight = 0;
        origin.wrap(placeholder);


        origin.on('click', function () {
            var placeholder = origin.parent('.material-placeholder');
            var windowWidth = window.innerWidth;
            var windowHeight = window.innerHeight;
            var originalWidth = origin.width();
            var originalHeight = origin.height();


            // If already modal, return to original
            if (doneAnimating === false) {
                returnToOriginal();
                return false;
            }
            else if (overlayActive && doneAnimating === true) {
                returnToOriginal();
                return false;
            }


            // Set states
            doneAnimating = false;
            origin.addClass('active');
            overlayActive = true;

            // Set positioning for placeholder

            placeholder.css({
                width: placeholder[0].getBoundingClientRect().width,
                height: placeholder[0].getBoundingClientRect().height,
                position: 'relative',
                top: 0,
                left: 0
            });



            // Set css on origin
            origin.css({ position: 'absolute', 'z-index': 1000 })
            .data('width', originalWidth)
            .data('height', originalHeight);

            // Add overlay
            var overlay = $('<div id="materialbox-overlay"></div>')
              .css({
                  opacity: 0
              })
              .click(function () {
                  if (doneAnimating === true)
                      returnToOriginal();
              });
            overlay.append("<div style='position: fixed;top: 20px;left: 20px;right: 20px;bottom: 20px;text-align: center;'><img src='" + origin.attr('src') + "' style='  max-height: 100%;max-width: 100%;'/></div>")
            // Animate Overlay
            $('body').append(overlay);
            overlay.velocity({ opacity: 1 }, { duration: inDuration, queue: false, easing: 'easeOutQuad' }
              );


            //// Add and animate caption if it exists
            //if (origin.data('caption') !== "") {
            //    var $photo_caption = $('<div class="materialbox-caption"></div>');
            //    $photo_caption.text(origin.data('caption'));
            //    $('body').append($photo_caption);
            //    $photo_caption.css({ "display": "inline" });
            //    $photo_caption.velocity({ opacity: 1 }, { duration: inDuration, queue: false, easing: 'easeOutQuad' })
            //}



            // Resize Image
            var ratio = 0;
            var widthPercent = originalWidth / windowWidth;
            var heightPercent = originalHeight / windowHeight;
            var newWidth = 0;
            var newHeight = 0;

            if (widthPercent > heightPercent) {
                ratio = originalHeight / originalWidth;
                newWidth = windowWidth * 0.9;
                newHeight = windowWidth * 0.9 * ratio;
            }
            else {
                ratio = originalWidth / originalHeight;
                newWidth = (windowHeight * 0.9) * ratio;
                newHeight = windowHeight * 0.9;
            }
            var img = overlay.find("img"); img.css('height', newHeight + "px").css('width', newWidth + "px");
            // Animate image + set z-index
            if (origin.hasClass('responsive-img')) {
                origin.velocity({ 'max-width': newWidth, 'width': originalWidth }, {
                    duration: 0, queue: false,
                    complete: function () {
                        origin.css({ left: 0, top: 0 })
                        .velocity(
                          {
                              height: newHeight,
                              width: newWidth,
                              left: $(document).scrollLeft() + windowWidth / 2 - origin.parent('.material-placeholder').offset().left - newWidth / 2,
                              top: $(document).scrollTop() + windowHeight / 2 - origin.parent('.material-placeholder').offset().top - newHeight / 2
                          },
                          {
                              duration: inDuration,
                              queue: false,
                              easing: 'easeOutQuad',
                              complete: function () { doneAnimating = true; }
                          }
                        );
                    } // End Complete
                }); // End Velocity
            }
            else {
                origin.css('left', 0)
                .css('top', 0)
                .velocity(
                  {
                      height: newHeight,
                      width: newWidth,
                      left: $(document).scrollLeft() + windowWidth / 2 - origin.parent('.material-placeholder').offset().left - newWidth / 2,
                      top: $(document).scrollTop() + windowHeight / 2 - origin.parent('.material-placeholder').offset().top - newHeight / 2
                  },
                  {
                      duration: inDuration,
                      queue: false,
                      easing: 'easeOutQuad',
                      complete: function () { doneAnimating = true; }
                  }
                  ); // End Velocity
            }

        }); // End origin on click


        // Return on scroll
        $(window).scroll(function () {
            if (overlayActive) {
                returnToOriginal();
            }
        });

        // Return on ESC
        $(document).keyup(function (e) {

            if (e.keyCode === 27 && doneAnimating === true) {   // ESC key
                if (overlayActive) {
                    returnToOriginal();
                }
            }
        });


        // This function returns the modaled image to the original spot
        function returnToOriginal() {

            doneAnimating = false;

            var placeholder = origin.parent('.material-placeholder');
            var windowWidth = window.innerWidth;
            var windowHeight = window.innerHeight;
            var originalWidth = origin.data('width');
            var originalHeight = origin.data('height');

            origin.velocity("stop", true);
            $('#materialbox-overlay').velocity("stop", true);
            $('.materialbox-caption').velocity("stop", true);


            $('#materialbox-overlay').velocity({ opacity: 0 }, {
                duration: outDuration, // Delay prevents animation overlapping
                queue: false, easing: 'easeOutQuad',
                complete: function () {
                    // Remove Overlay
                    overlayActive = false;
                    $(this).remove();
                }
            });

            // Resize Image
            origin.velocity(
              {
                  width: originalWidth,
                  height: originalHeight,
                  left: 0,
                  top: 0
              },
              {
                  duration: outDuration,
                  queue: false, easing: 'easeOutQuad'
              }
            );

            // Remove Caption + reset css settings on image
            $('.materialbox-caption').velocity({ opacity: 0 }, {
                duration: outDuration, // Delay prevents animation overlapping
                queue: false, easing: 'easeOutQuad',
                complete: function () {
                    placeholder.css({
                        height: '',
                        width: '',
                        position: '',
                        top: '',
                        left: ''
                    });

                    origin.css({
                        height: '',
                        top: '',
                        left: '',
                        width: '',
                        'max-width': '',
                        position: '',
                        'z-index': ''
                    });

                    // Remove class
                    origin.removeClass('active');
                    doneAnimating = true;
                    $(this).remove();
                }
            });

        }
    });
};

$(document).ready(function () {
    $('.user-boxed').materialbox();
    console.log("%c Galagala, website bán hàng trực tuyến!", "font-size:25px;background-color:#0165bb;color:#fff;font-family:tahoma;padding:5px 10px;");
});


//MobileClick

$(document).ready(function () {
    $(".children-item").click(function () {
        var e = $(this);
        //$(e.attr('data-target')).animate({ 'margin-left': e.attr('data-valuechange') });
        var lstAll = $(".mLeft");
        $.map(lstAll, function (ele, ind) {
            $(ele).css('display', 'none');
            if ($(ele).attr('data-level') == e.attr('data-show') && typeof e.attr('data-menushow') != "undefined") {
                $(ele).css('display', 'block');
            }
        })

        var lst = $(".mLeft[data-level='" + e.attr('data-level') + "']");
        $.map(lst, function (ele, ind) {
            $(ele).css('display', 'none');
            if ($(ele).attr('data-content') == e.attr('data-menushow')) {
                $(ele).css('display', 'block');
            }
        })
        $(e.attr('data-target') + "[data-level='2']");
        $(e.attr('data-target')).animate({ 'margin-left': e.attr('data-valuechange') });
    });
    $(".mLeft-move").click(function (e) {
        var target = $(e.target).hasClass('mLeft-move') == true ? $(e.target) : $(e.target).parent();
        var lstAll = $(".mLeft");
        $.map(lstAll, function (ele, ind) {
            $(ele).css('display', 'none');
            if ($(ele).attr('data-level') == target.attr('data-show')) {
                $(ele).css('display', 'block');
            }
        })
        var lst = $(".mLeft[data-level='" + target.attr('data-show') + "']");
        $.map(lst, function (ele, ind) {
            $(ele).css('display', 'none');
            if ($(ele).attr('data-content') == target.attr('data-menushow')) {
                $(ele).css('display', 'block');
            }
        })
        if (typeof target.attr("data-id") != "undefined") {
            $.map($(".children-after[data-catelv='" + target.attr('data-getlv') + "']"), function (ele, ind) {
                $('.title-category[data-catelv="' + target.attr('data-getlv') + '"]').text(target.attr('data-name'));
                if ($(ele).attr('data-parent') == target.attr('data-id'))
                    $(ele).parent().css('display', 'block');
                else $(ele).parent().css('display', 'none');
            })
        }
        $(target.attr('data-target')).animate({ 'margin-left': target.attr('data-value') });
    })
});