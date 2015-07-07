//$("#menu-btnMenuLeft").sideNav({ menuWidth: 300 });
//$('.slider').slider({ full_width: true, indicators: false });
$(".changeStyleColour").click(function (e) {
    var fi = $(".changeStyleColour");
    for (var i = 0; i < fi.length; i++) $(fi[i]).removeClass("active")
    $(this).addClass("active")
    createCookie("colourGala", $(this).attr('data-value'), "365")
    changeColor($(this).attr('data-style'))
    $("#banner").find("img").attr('src', '/Content/styles/' + $(this).attr('data-logo'))
})
var gPosition = -1;
$(window).scroll(function () {
    if ($(window).scrollTop() >= 50) {
        $("#header-top").fadeOut("fast"), $("#header-content-search-keyword").fadeOut("fast"), $("#main-content").css("margin-top", "68px");
    }
    else { $("#header-top").fadeIn("slow"), $("#header-content-search-keyword").fadeIn("slow"), $("#main-content").css("margin-top", "116px"); }
    //header-top
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
var lstStyle = [{ name: "style1", value: "#FF8600", logo: "orange.png" }, { name: "style2", value: "#FF1066", logo: "purple.png" }, { name: "style3", value: "#00BBD9", logo: "blue.png" }, { name: "style4", value: "#00CC2E", logo: "green.png" }];
$(document).ready(function () {
    //var cookie = readCookie("colourGala")
    //if (cookie == null) createCookie("colourGala", "style4", "365")
    //cookie = readCookie("colourGala")
    //var color = "";
    //$('.modal-trigger').leanModal();
    //$.map(lstStyle, function (e, i) { if (e.name == cookie) { changeColor(e.value), $("#banner").find("img").attr('src', '/Content/styles/' + e.logo), color = e.value } })
    //var fis = $(".changeStyleColour");
    //for (var i = 0; i < fis.length; i++) $(fis[i]).removeClass("active");
    //$('.changeStyleColour[data-style="' + color + '"]').addClass("active")
    //$("#selLanguage").material_select()
    $("#header-btnSearch").click(function () {
        $("#navTop-Search").css('display', 'block');
        $(".user-overlay-main").css('display', 'block');
        $(".user-overlay-main").css('opacity', '1');
        $("#Seach-txtSearch").focus();
    })
    $(".user-overlay-main").click(function () {
        $("#navTop-Search").css('display', 'none');
        $(".user-overlay-main").css('display', 'none');
        $(".user-overlay-main").css('opacity', '0');
    })
    $("#Seach-btnSearch").click(function () {
        $("#navTop-Search").css('display', 'none');
        $(".user-overlay-main").css('opacity', '0');
        $(".user-overlay-main").css('display', 'none');
        if ($("#Seach-txtSearch").val().trim().length > 0) {
            var href = $(this).attr('data-href');
            window.location.href = href.replace('__q__', escape($("#Seach-txtSearch").val())).replace('__q__', escape($("#Seach-txtSearch").val()));
        }
    })
    $(".itemCategory").click(function () {
        $(this).attr('data-type') == "false" ? window.location.href = $(this).attr('data-href') : '';
    });
    $("#btnAddToCart").click(function (e) {
        var id = $(this).attr('data-id');
        $.ajax({
            type: "POST",
            url: $(this).attr('data-url'),
            data: { id: id },
            cache: false,
            dataType: "json",
            success: function (dt) {
                if (dt.result == true) {
                    $(e.target).html($(e.target).attr('data-language')), $(e.target).removeAttr("id"), $("#header-count-cart").html(dt.newValue)
                }
            }, error: function (error) { }
        }); //end ajax call
    })
    $(".btnAddToCart").click(function (e) {
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
                    $(target).remove(), $("#header-count-cart").html(dt.newValue)
                }
            }, error: function (error) { }
        }); //end ajax call
    })
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
                if (dt.result == 1) { $(target).html("<i class='" + $(target).attr('data-x') + "'></i>"), $(target).attr('data-value', 'true'), $("#header-count-wishlist").html(dt.newValue) }
                else if (dt.result == 2) { $(target).html("<i class='" + $(target).attr('data-y') + "'></i>"), $(target).attr('data-value', 'false'), $("#header-count-wishlist").html(dt.newValue) }
                else window.location.href = $(target).attr('data-redirect')
            }, error: function (error) { }
        }); //end ajax call
    })
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
                if (dt.result == true) { $(target).parent().parent().remove(), $("#header-count-cart").html(dt.newValue) }
                else alert("Dont delete")
            }, error: function (error) { }
        }); //end ajax call
    })
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
                if (dt.result == 1) { $(target).parent().parent().parent().remove(), $("#header-count-wishlist").html(dt.newValue) }
                else alert("Dont delete")
            }, error: function (error) { }
        }); //end ajax call
    })
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
        if ($(this).val() > 50) $(this).val(50)
        var f = $($(this).parent().next()).children();
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
    })
    $("#btnDeleteAllWishList").click(function (e) {
        $.ajax({
            type: "POST",
            url: $(this).attr('data-url'),
            data: {},
            cache: false,
            dataType: "json",
            success: function (dt) {
                if (dt.result == 1) {
                    $("." + $("#btnDeleteAllWishList").attr('data-html')).remove(), $("#btnDeleteAllWishList").remove()
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
            target.animate({ height: (t + 10) }, 1000);
            target.attr('data-flag', "true");
            $(this).html($(this).attr('data-y'));
        }
        else {
            $('a[data-type="user-mini-div"]a[data-target="' + $(this).attr("data-target") + '"]').addClass('user-hidden');
            var t = 0; target.children().each(function () {
                t = t + $(this).outerHeight();
            });
            target.animate({ height: '100px' }, 1000);
            target.attr('data-flag', "false");
            $(this).html($(this).attr('data-x'));
        }
    })
    $(".user-mini-div").click(function () {
        var target = $('div[data-target="' + $(this).attr("data-target") + '"]');
        $(this).addClass('user-hidden');
        var t = 0; target.children().each(function () {
            t = t + $(this).outerHeight();
        });
        target.animate({ height: '100px' }, 1000);
        target.attr('data-flag', "false");
        $('a[data-type="user-show-div"]a[data-target="' + $(this).attr("data-target") + '"]').html($($('a[data-type="user-show-div"]a[data-target="' + $(this).attr("data-target") + '"]')).attr('data-x'));
    })
})
$(document).on('click', '.user-toggle-button', function (e) {
    var button = $(e.target).hasClass("user-toggle-button") == true ? $(e.target) : $(e.target).parent();
    var target = $('.user-toggle-element[data-target="' + $(button).attr("data-target") + '"]');
    if ($(target).hasClass("user-toggle-hidden")) {
        $(target).removeClass("user-toggle-hidden");
        var t = 0; target.children().each(function () {
            t = t + $(this).outerHeight();
        });
        target.animate({ height: (t + 10) }, 1000);
        $(button).children("i").addClass($(button).attr("data-i-min"));
    }
    else {
        $(target).addClass("user-toggle-hidden");
        target.animate({ height: $(button).attr("data-min") + "px" }, 1000);
        $(button).children("i").removeClass($(button).attr("data-i-min"));
    }
})
function changeColor(it) {
    $.map($('[data-element="background"]'), function (ex, ix) { $(ex).css('background-color', it); })
    $.map($('[data-element="color"]'), function (ex, ix) { $(ex).css('color', it); })
    $.map($('[data-element="border-bottom"]'), function (ex, ix) { $(ex).css('border-bottom-color', it); })
}
function setTotalMoney() {
    var l = $(".user-totalpriceProduct");
    var money = 0;
    for (var i = 0; i < l.length; i++) money += Number($(l[i]).attr('data-totalmoney'))
    $("#totalPriceAll").text(format_money(money) + " Đ")
}
function format_money(n) {
    return n.toFixed(0).replace(/./g, function (c, i, a) {
        return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
    });
}
function changeLanguage(input) { window.location.href = input; }