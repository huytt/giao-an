//(function ($) {
//    //equalheight = function (t) {
//    //    var i, e = 0, h = 0, r = new Array;
//    //    $(t).each(function () { if (i = $(this), $(i).height("auto"), topPostion = i.position().top, h != topPostion) { for (currentDiv = 0; currentDiv < r.length; currentDiv++) r[currentDiv].height(e); r.length = 0, h = topPostion, e = i.height(), r.push(i) } else r.push(i), e = e < i.height() ? i.height() : e; for (currentDiv = 0; currentDiv < r.length; currentDiv++) r[currentDiv].height(e) })
//    //};
//    //Galagala.product.get.p_recent()
//    //Galagala.category.get.index()
//    $(document).ready(function () {
//        $('.modal-trigger').leanModal()
//        $("#footer-gala-title").click(function () {
//            $("#footer-article-Galagala").toggle(),
//            $("#footer-gala-title").children().toggleClass("fa-icon-bottom-114");
//        })
//        $("#header-btnSearch").click(function () {
//            //$(".main-section").css('display', 'none'),
//            $("#navTop-Search").css('display', 'block'),
//            $(".user-overlay-main").css({ 'display': 'block', 'opacity': '1' }),
//            $("#Seach-txtSearch").focus();
//        })
//        $(".user-overlay-main").click(function () {
//            $("#navTop-Search").css('display', 'none'),
//            $(".user-overlay-main").css({ 'display': 'none', 'opacity': '0' })
//            //$(".main-section").css('display', 'block');
//        })
//        $("#Seach-btnSearch").click(function () {
//            $("#navTop-Search").css('display', 'none'),
//            $(".user-overlay-main").css({ 'opacity': '0', 'display': 'none' })
//            //$(".main-section").css('display', 'block');
//            if ($("#Seach-txtSearch").val().trim().length > 0) {
//                var href = $(this).attr('data-href')
//                window.location.href = href.replace('__q__',
//                    encodeURIComponent($("#Seach-txtSearch").val())).replace('__q__', encodeURIComponent($("#Seach-txtSearch").val()));
//            }
//        })
//        $("#btn-close-Search").click(function () {
//            $("#navTop-Search").css('display', 'none');
//            $(".user-overlay-main").css({ 'opacity': '0', 'display': 'none' });
//            //$(".main-section").css('display', 'block');
//        })
//        $("#Seach-txtSearch").unbind("input").bind("input", function (q) {
//            var c = $("#product_popular"), t = $("#Seach-txtSearch")
//            if (q.target.value.trim().length==0) {
//                c.hide()
//            }
//            else {
//                c.show()
//                $.ajax({
//                    type: "GET",
//                    url: t.attr('data-url'),
//                    data: { q: q.target.value },
//                    cache: false,
//                    dataType: "json",
//                    success: function (dt) {
//                        //var lstSearch = t.val().split(' ');
//                        if (dt.data.length == 0)
//                            c.hide()
//                        else {
//                            //c.children(".header").css('display', 'block')
//                            c.children(".item").remove();
//                        }
//                        $.map(dt.data, function (ele, ind) {
//                            var a = document.createElement("a");
//                            a.href = ele.Link;
//                            a.innerHTML = (ele.ProductComplexName == null ? ele.ProductName : ele.ProductComplexName) + (ele.Color == null ? "" : " (" + ele.Color + ")");
//                            var li = document.createElement("li");
//                            li.className = "item";
//                            li.appendChild(a)
//                            c.append(li)
//                        })
//                    }, error: function (error) { }
//                }); //end ajax call
//            }
//        })
//        $("#Seach-txtSearch").keydown(function (e) {
//            if (e.keyCode == 13) {
//                $("#Seach-btnSearch").click();
//            }
//        })
//        $("#btn-full-screen, .show-full-screen").click(function () {
//            $("#full #contain").children().remove();
//            $("#full").children(".content").children('.view').append($(".show-full-screen"));
//            $("#full").css('display', 'table');
//            var lstImage = $(".show-full-screen").find(".owl-item").find('img');
//            var divContainer = document.createElement("div");
//            divContainer.className = "owl-carousel full-main";
//            divContainer.style.cssText = "width:100%;height:100%;color:#FFF;text-align:center;";
//            for (var i = 0; i < lstImage.length; i++) {
//                var div = document.createElement("div");
//                div.className = "item"
//                $(div).css({ 'min-height': '100%', 'display': ' table', 'height': '100vh', 'width': '100%' })
//                var div1 = document.createElement("div");
//                $(div1).css({ 'display': 'table-cell', 'vertical-align': 'middle' });
//                var sourceImage = document.createElement('img');
//                sourceImage.src = lstImage[i].src == "" || typeof lstImage[i].src == "undefined" ? $(lstImage[i]).attr("data-src") : lstImage[i].src;
//                $(sourceImage).css({ 'max-width': '100%', 'max-height': '100vh', 'width': '100%' });
//                $(div1).append($(sourceImage).clone())
//                div.appendChild(div1)
//                $(divContainer).append(div);
//            }
//            $("#full #contain").append(divContainer);
//            var fullSwiper = $(".full-main").owlCarousel({
//                navigation: false,
//                slideSpeed: 300,
//                singleItem: true
//            });
//            $(".main-section").css('display', 'none');
//        })
//        $("#btn-normal-screen").click(function () {
//            $("#full").css('display', 'none');
//            $(".main-section").css('display', 'block');
//        })
//        $(".itemCategory").click(function () {
//            $(this).attr('data-type') == "false" ? window.location.href = $(this).attr('data-href') : '';
//        })
//        $(".user-collapsible").click(function () {
//            var e = $(this);
//            e.find(".collapsible-icon-change").toggleClass(e.attr("data-toggle"));
//        })
//        $(".btnAddToWishlist").click(function (e) {
//            var t = $(this)
//            $.ajax({
//                type: "POST",
//                url:t.attr('data-url'),
//                data: { id: t.attr('data-id'), action: t.attr('data-value') },
//                cache: false,
//                dataType: "json",
//                success: function (dt) {
//                    var target = e.target.hasAttribute('data-y') == true ? e.target : e.target.parentElement;
//                    if (dt.result == 1 || dt.result == 2)
//                    {
//                        var new_data = dt.result == 1 ? $(lst).attr('data-x') : $(lst).attr('data-y');
//                        var lst = $('.btnAddToWishlist[data-id="' + id + '"]');
//                        $(lst).html("<i class='" + new_data + "' style='font-size: 35px;'></i>");
//                        updateValue("#header-count-wishlist", dt.newValue);
//                    }
//                    else window.location.href = $(target).attr('data-redirect')
//                }, error: function (error) { }
//            }); //end ajax call
//        })
//        $(".btnAddToWishlist-Product").click(function (e) {
//            var t = $(this)
//            $.ajax({
//                type: "POST",
//                url: t.attr('data-url'),
//                data: { id: t.attr('data-id'), action: t.attr('data-value') },
//                cache: false,
//                dataType: "json",
//                success: function (dt) {
//                    var target = e.target.hasAttribute('data-y') == true ? e.target : e.target.parentElement;
//                    if (dt.result == 1 || dt.result == 2) {
//                        var rs_icon = dt.result == 1 ? $(target).attr('data-x') : $(target).attr('data-y');
//                        var rs_lang = dt.result == 1 ? $(target).attr('data-language-x') : $(target).attr('data-language-y');
//                        var rs_data = dt.result == 1 ? 'true' : 'false';
//                        $(target).html("<i class='" + rs_icon + "' style='  transform: scale(.4);float:left;'></i> " + rs_lang),
//                        $(target).attr('data-value', rs_data);
//                        updateValue("#header-count-wishlist", dt.newValue);
//                    }
//                    else window.location.href = $(target).attr('data-redirect')
//                }, error: function (error) { }
//            }); //end ajax call
//        })
//        $(".number-price").keydown(function (e) {
//            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
//                return;
//            }
//            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
//                e.preventDefault();
//            }
//        })
//        $(".number-price-change").keydown(function (e) {
//            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 || (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39)) {
//                return;
//            }
//            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
//                e.preventDefault();
//            }
//        })
//        $(".number-price-change").change(function () {
//            var t = $(this)
//            if (t.val() <= 0) t.val(1)
//            if (t.val() > 5) t.val(5)
//            var f = $(t.parent().parent().children(".totalPricechilldren")).children();
//            var money = Number(t.val()) * Number(f.attr('data-money'));
//            f.attr('data-totalmoney', money);
//            money = format_money(money) + " Đ";
//            f.text(money);
//            setTotalMoney();
//        })
//        $(".user_read_more").click(function () {
//            var t = $(this)
//            var target = $('div[data-target="' + t.attr("data-target") + '"]');
//            target.toggleClass("user-height-0")
//            target.hasClass("user-height-0") ? t.html(t.attr("data-y")) : t.html(t.attr("data-x"));
//        })
//        $(".user-mini-div").click(function () {
//            var tt = $(this)
//            var target = $('div[data-target="' + tt.attr("data-target") + '"]');
//           tt.addClass('user-hidden');
//            var t = 0; target.children().each(function () {
//                t = t + tt.outerHeight();
//            });
//            target.animate({ height: '0px' }, 200);
//            target.attr('data-flag', "false");
//            $('a[data-type="user-show-div"]a[data-target="' + tt.attr("data-target") + '"]').html($($('a[data-type="user-show-div"]a[data-target="' +tt.attr("data-target") + '"]')).attr('data-x'));
//        })
//        $('.children-item').click(function (e) {
//            var e = $(this);
//            var lstAll = $(".mLeft");
//            $.map(lstAll, function (ele, ind) {
//                $(ele).css('display', 'none');
//                if ($(ele).attr('data-level') == e.attr('data-show') && typeof e.attr('data-menushow') != "undefined") {
//                    $(ele).css('display', 'block');
//                }
//            })
//            var lst = $(".mLeft[data-level='" + e.attr('data-level') + "']");
//            lst.hide();
//            $(".mLeft[data-level='" + e.attr('data-level') + "'][data-content='" + e.attr('data-menushow') + "']").show()
//            //$.map(lst, function (ele, ind) {
//            //    $(ele).css('display', 'none');
//            //    if ($(ele).attr('data-content') == e.attr('data-menushow')) {
//            //        $(ele).css('display', 'block');
//            //    }
//            //})
//            $(e.attr('data-target') + "[data-level='2']");
//            $(e.attr('data-target')).animate({ 'margin-left': e.attr('data-valuechange') });
//        })
//        $(".mLeft-move").click(function (e) {
//            var target = $(e.target).hasClass('mLeft-move') == true ? $(e.target) : $(e.target).parent();
//            var lstAll = $(".mLeft");
//            lstAll.hide();
//            $(".mLeft[data-level='"+target.attr('data-show')+"']").show()
//            //$.map(lstAll, function (ele, ind) {
//            //    $(ele).css('display', 'none');
//            //    if ($(ele).attr('data-level') == target.attr('data-show')) {
//            //        $(ele).css('display', 'block');
//            //    }
//            //})
//            var lst = $(".mLeft[data-level='" + target.attr('data-show') + "']");
//            $.map(lst, function (ele, ind) {
//                $(ele).css('display', 'none');
//                if ($(ele).attr('data-content') == target.attr('data-menushow')) {
//                    $(ele).css('display', 'block');
//                }
//            })
//            if (typeof target.attr("data-id") != "undefined") {
//                $.map($(".children-after[data-catelv='" + target.attr('data-getlv') + "']"), function (ele, ind) {
//                    $('.title-category[data-catelv="' + target.attr('data-getlv') + '"]').text(target.attr('data-name'));
//                    if ($(ele).attr('data-parent') == target.attr('data-id'))
//                        $(ele).parent().css('display', 'block');
//                    else $(ele).parent().css('display', 'none');
//                })
//            }
//            $(target.attr('data-target')).animate({ 'margin-left': target.attr('data-value') });
//        })
//    });
//    $(document).on('click', '.user-toggle-button', function (e) {
//        var button = $(e.target).hasClass("user-toggle-button") == true ? $(e.target) : $(e.target).parent();
//        var target = $('.user-toggle-element[data-target="' + $(button).attr("data-target") + '"]');
//        if ($(target).hasClass("user-toggle-hidden")) {
//            $(target).removeClass("user-toggle-hidden");
//            var t = 0; target.children().each(function () {
//                t = t + $(this).outerHeight();
//            });
//            target.animate({ height: (t + 10) }, 200);
//            $(button).children("i").addClass($(button).attr("data-i-min"));
//        }
//        else {
//            $(target).addClass("user-toggle-hidden");
//            target.animate({ height: $(button).attr("data-min") + "px" }, 200);
//            $(button).children("i").removeClass($(button).attr("data-i-min"));
//        }
//    });
//    function updateValue(str, value) {
//        if (typeof $(str).attr("data-run") != "undefined" && $(str).attr("data-run") == "true")
//            $(str).html(value);
//        else {
//            $(str).html(value)
//                            .attr('data-run', 'true')
//                            .animate({ color: 'black!important', 'font-size': '40px', 'margin-top': '50px', 'margin-left': '-30px', 'z-index': 999999 });
//            setTimeout(function () {
//                $(str).animate({ 'font-size': '15px', 'margin-top': '00px', 'margin-left': '0px', 'z-index': 999999 })
//                                .attr('data-run', 'false');
//            }, 1000);
//        }
//    }
//    function setTotalMoney() {
//        var l = $(".user-totalpriceProduct");
//        var money = 0;
//        $("#totalPriceAll").text('0 đ')
//        for (var i = 0; i < l.length; i++)
//            money += Number($(l[i]).attr('data-totalmoney')), $("#totalPriceAll").text(format_money(money) + " đ")
//    }
//    function format_money(n) {
//        n = parseFloat(n);
//        return n.toFixed(0).replace(/./g, function (c, i, a) {
//            return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
//        });
//    }
//    function changeLanguage(input) {
//        var href = $(input).attr("data-url"),
//            href = href.replace('_lang_', $(input).val()),
//            href = href.replace(/&amp;/g, '&');
//        window.location.href = href;
//    }
//})(jQuery);
//$(document).ready(function () {
//    var clickedTab = $(".tabs > .active");
//    var tabWrapper = $(".tab__content");
//    var activeTab = tabWrapper.find(".active");
//    var activeTabHeight = activeTab.outerHeight();
//    activeTab.show();
//    tabWrapper.height(activeTabHeight);
//    $(".tabs > li").on("click", function () {
//        $(".tabs > li").removeClass("active");
//        $(this).addClass("active");
//        clickedTab = $(".tabs .active");
//        activeTab.fadeOut(100, function () {
//            $(".tab__content > li").removeClass("active");
//            var clickedTabIndex = clickedTab.index();
//            $(".tab__content > li").eq(clickedTabIndex).addClass("active");
//            activeTab = $(".tab__content > .active");
//            activeTabHeight = activeTab.outerHeight();
//            tabWrapper.stop().animate({
//                height: activeTabHeight
//            }, 100, function () {
//                activeTab.fadeIn(50);

//            });
//        });
//    });
//})