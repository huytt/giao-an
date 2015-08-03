var Galagala = Galagala || {};
Galagala.mall = {
    get: {
        index: function () {

        }
    },
    create: function () {

    },
    remove: function () {
    },
    init: function () {

    }
}
Galagala.category = {
    items: [],
    get: {
        index: function () {
            var a = this;
            $.ajax({
                type: "GET",
                url: dataLink.c.index.link,
                cache: true,
                dataType: "json",
                success: function (dt) {
                    //Level 1
                    var rs = "";
                    rs += '<li style="background-color: #e9e9e9; text-align: left; color: black; line-height: 42px; height: 42px; padding: 0px;position: relative; ">'
                     + '<a class="mLeft-move cursor-point" data-value="0px" data-show="0" data-target="#side-nav-left-content" style="float: left;padding: 0px 10px;width:100%;">'
                     + '<i class="fa-icon fa-icon-left-81" style="float: left; transform: scale(0.35); -ms-transform: scale(.35); -webkit-transform: scale(.35); -moz-transform: scale(.35); -o-transform: scale(.35); -webkit-transform: scale(.35); margin-top: 6px; "></i>'
                     + dataLink.c.index.Title
                     + '</a>'
                     + '</li>';
                    for (var i = 0; i < dt.cate_1.length; i++) {
                        if (typeof dt.cate_1[0].Logo_Url != "undefined") {
                            var flag = false;
                            var mClass = '';
                            var link = galagala_config.HOST + dt.cate_1[i].Link;
                            $.each(dt.cate_2, function (ind, ele) {
                                if (ele.ParentCateId == dt.cate_1[i].CategoryId) flag = true;
                            })
                            if (flag) {
                                mClass = 'mLeft-move';
                                link = '#';
                            }
                            rs += '<li style="clear: both; padding: 0px; border-bottom: 1px solid #d2d2d2;position: relative;">'
                                + '<a class="children-after ' + mClass + ' user-padding-0" href="'
                                + link
                                + '" data-show="3" data-name="'
                                + dt.cate_1[i].CategoryName +
                                '" data-catelv=" 1" data-getlv="2" style="min-height: 0px;" data-target="#side-nav-left-content" data-value="-600px" data-id="'
                                + dt.cate_1[i].CategoryId + '">'
                                + '<img src="' + galagala_config.HOST_MEDIA + dt.cate_1[i].Logo_Url + dt.cate_1[i].Logo_MediaName + '" style=" width: 36px; height: 36px; float: left; margin-right: 5px;" />'
                                + '<p style=" height: 44px; overflow: hidden; margin: 0px;font-size:13px;">' + dt.cate_1[i].CategoryName + '</p>'
                                + '<div style="position: absolute;top: 0px;right: 0px;background-color: white;">';
                            if (flag) {
                                rs += '<span class="secondary-content Cate_ProCount" style="padding:0px;">'
                                + '<i class="fa-icon fa-icon-right-44" style="float: right; transform: scale(0.35); -ms-transform: scale(.35); -webkit-transform: scale(.35); -moz-transform: scale(.35); -o-transform: scale(.35); -webkit-transform: scale(.35); margin-top: 6px; "></i>'
                                + '</span>';
                            }
                            else { rs += ' <span style="width: 32px; float: right; height: 100%;"></span>'; }
                            rs += ' <span class="secondary-content"  style="color:orange;">' + dt.cate_1[i].ProductCount + '</span>'
                                +'</div>'
                            + '</a>'
                            + '</li>';
                        }
                    }
                    $("#lstCategory1").html(rs);
                    //Level 2 
                    rs = "";
                    rs += '<li style="background-color: #e9e9e9; text-align: left; color: black; line-height: 42px; height: 42px; padding: 0px; ">'
                    + '<a class="mLeft-move cursor-point" data-show="2" data-menushow="category" data-value="-280px" data-target="#side-nav-left-content" style="float: left; padding: 0px 10px; width: 100%;position: relative;">'
                    + '<i class="fa-icon fa-icon-left-81" style="float: left; transform: scale(0.35); -ms-transform: scale(.35); -webkit-transform: scale(.35); -moz-transform: scale(.35); -o-transform: scale(.35); -webkit-transform: scale(.35); margin-top: 6px; "></i>'
                   + '<span class="title-category" data-catelv="2"></span>'
                    + '</a>'
                    + '</li>'
                    for (var i = 0; i < dt.cate_2.length; i++) {
                        var flag = false;
                        $.each(dt.cate_3, function (ind, ele) {
                            if (ele.ParentCateId == dt.cate_2[i].CategoryId) flag = true;
                        })
                        var mClass = "";
                        var link = galagala_config.HOST + dt.cate_2[i].Link;
                        if (flag) {
                            link = "#";
                            mClass = "mLeft-move";
                        }
                        rs += '<li style="clear: both; padding: 0px; border-bottom: 1px solid #d2d2d2;position: relative;">'
                        + '<a class="children-after ' + mClass + '" href="' + link + '" data-show="4" data-name="' + dt.cate_2[i].CategoryName + '" data-catelv="2" data-getlv="3" style="min-height: 0px;" data-target="#side-nav-left-content" data-value="-900px" data-id="' + dt.cate_2[i].CategoryId + '" data-parent="' + dt.cate_2[i].ParentCateId + '">'
                        //+ '<img src="' + galagala_config.HOST_MEDIA + dt.cate_2[i].Logo_Url + dt.cate_2[i].Logo_MediaName + '" style=" width: 36px; height: 36px; float: left; margin-right: 5px;" />'
                       + '<p style="height: 44px; overflow: hidden; margin: 0px;">' + dt.cate_2[i].CategoryName + '</p>'
                        + '<div style="position: absolute;top: 0px;right: 0px;background-color: white;">';
                        if (flag) {
                            rs += '<span class="secondary-content Cate_ProCount" style="padding:0px;">'
                           + ' <i class="fa-icon fa-icon-right-44" style="float: right; transform: scale(0.35); -ms-transform: scale(.35); -webkit-transform: scale(.35); -moz-transform: scale(.35); -o-transform: scale(.35); -webkit-transform: scale(.35); margin-top: 6px; "></i>'
                            + '</span>'
                        }
                        else
                            rs += '<span style="width: 32px; float: right; height: 100%;"></span>'
                        rs += ' <span class="secondary-content "  style="color:orange;">' + dt.cate_2[i].ProductCount + '</span>'
                            +'</div>'
                        + '</a>'
                       + ' </li>'
                    }
                    $("#lstCategory2").html(rs);
                    rs = "";
                    //Level 3
                    rs += ' <li style="background-color: #e9e9e9; text-align: left; color: black; line-height: 42px; height: 42px; padding:0px;position: relative;">'
                        + '<a style="float: left; padding: 0px 10px; width: 100%; " data-show="3" class="mLeft-move cursor-point" data-value="-600px" data-target="#side-nav-left-content">'
                        + '<i class="fa-icon fa-icon-left-81" style="float: left; transform: scale(0.35); -ms-transform: scale(.35); -webkit-transform: scale(.35); -moz-transform: scale(.35); -o-transform: scale(.35); -webkit-transform: scale(.35); margin-top: 6px; "></i>'
                        + ' <span class="title-category" data-catelv="3"></span>'
                        + ' </a>'
                        + ' </li>'
                    for (var i = 0; i < dt.cate_3.length; i++) {
                        rs += '<li style="clear: both; padding: 0px; border-bottom: 1px solid #d2d2d2; height: 44px;position: relative; ">'
                            + '<a class="children-after" href="' + dt.cate_3[i].Link + '" data-catelv="3" style="min-height: 0px;" data-target="#side-nav-left-content" data-value="-900px" data-id="' + dt.cate_3[i].CategoryId + '" data-parent="' + dt.cate_3[i].ParentCateId + '">'
                                //+ '<img src="' + galagala_config.HOST_MEDIA + dt.cate_3[i].Logo_Url + dt.cate_3[i].Logo_MediaName + '" style=" width: 36px; height: 36px; float: left; margin-right: 5px;" />'
                                + '<p style=" height: 44px; overflow: hidden; margin: 0px;">' + dt.cate_3[i].CategoryName + '</p>'
                                + '<div style="position: absolute;top: 0px;right: 0px;background-color: white;margin-right: 5px;">'
                                //+ '<span style="width: 32px; float: right; height: 100%;"></span>'
                                + '<span class="secondary-content" style="color:orange;">' + dt.cate_3[i].ProductCount + '</span>'
                                +'</div>'
                            + '</a>'
                        + '</li>'
                    }
                    $("#lstCategory3").html(rs);
                    //$(dataLink.c.index.target).html(rs)


                    if (g_Category.lv == "2") {
                        $("#lstCategory3").children('li').hide()
                        $($("#lstCategory3").children('li')[0])
                            .show().children()
                            .html( '<i class="fa-icon fa-icon-left-81" style="float: left; transform: scale(0.35); -ms-transform: scale(.35); -webkit-transform: scale(.35); -moz-transform: scale(.35); -o-transform: scale(.35); -webkit-transform: scale(.35); margin-top: 6px; "></i>'
                        + ' <span class="title-category" data-catelv="3">' + g_Category.name_2 + '</span>')
                        $("#lstCategory3").find('a[data-parent="' + g_Category.cate_2 + '"]').parent().show()

                        $("#lstCategory2").children('li').hide()
                        $($("#lstCategory2").children('li')[0])
                            .show().children()
                            .html('<i class="fa-icon fa-icon-left-81" style="float: left; transform: scale(0.35); -ms-transform: scale(.35); -webkit-transform: scale(.35); -moz-transform: scale(.35); -o-transform: scale(.35); -webkit-transform: scale(.35); margin-top: 6px; "></i>'
                        + ' <span class="title-category" data-catelv="2">' + g_Category.name_1 + '</span>')
                        $("#lstCategory2").find('a[data-parent="' + g_Category.cate_1 + '"]').parent().show()

                    }
                    if (g_Category.lv == "1") {
                        $("#lstCategory2").children('li').hide()
                        $($("#lstCategory2").children('li')[0])
                            .show().children()
                            .html('<i class="fa-icon fa-icon-left-81" style="float: left; transform: scale(0.35); -ms-transform: scale(.35); -webkit-transform: scale(.35); -moz-transform: scale(.35); -o-transform: scale(.35); -webkit-transform: scale(.35); margin-top: 6px; "></i>'
                        + ' <span class="title-category" data-catelv="2">' + g_Category.name_2 + '</span>')
                        $("#lstCategory2").find('a[data-parent="' + g_Category.cate_2 + '"]').parent().show()
                    }

                }, error: function (error) { }
            }); //end ajax call
            this.method()
        },
        method: function () {
            $(document).on('click', '.children-item', function (e) {
                var e = $(this);
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
            $(document).on("click", ".mLeft-move", function (e) {
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
        }
    },
    create: function () {
        var a = this;
        console.log('Galagala.Category.Create')
        $.ajax({
            type: "GET",
            url: '/Particle/GetStore',
            cache: false,
            dataType: "json",
            success: function (dt) {

            }, error: function (error) { }
        }); //end ajax call
    },
    remove: function () {
    },
    init: function () {
        var a = this;
        this.create()
    },
}
Galagala.store = {
    items: [],
    get: {
        all: function () {
            //Trang Home
            $.ajax({
                type: "GET",
                url: dataLink.s.all.link,
                cache: true,
                dataType: "json",
                success: function (dt) {
                    var number = dt.data.length % 6 == 0 ? dt.data.length / 6 : parseInt(dt.data.length / 6) + 1;;
                    //var tl = '<div class="swiper-wrapper" style="height:100%;">';
                    //for (var i = 0; i < number; i++) {
                    //    var step_f = i * 6, stemp_l = step_f + 6;
                    //    tl += '<div class="swiper-slide">'
                    //    tl += '<div class="row">'
                    //    for (var j = step_f; j < stemp_l; j++) {
                    //        if (typeof dt.data[j] != "undefined") {
                    //            tl += ' <div class="col s6" style="margin-bottom: 5px;padding:0px 3px;">'
                    //            tl += '<div class="card margin-0">'
                    //            tl += ' <div class="card-image">'
                    //            tl += '<a href="' + dt.data[j].Link + '">'
                    //            tl += '<img class="swiper-lazy user-col-0" data-src="http://galagala.vn:8888' + dt.data[j].Url + dt.data[j].MediaName + '" class="swiper-lazy" style="max-height: 100%; max-width: 100%; width: 100%; height: auto;">'
                    //            tl += ' </a>'
                    //            tl += ' </div>'
                    //            tl += ' <div class="card-action user-padding-0" style="  display: table;width: 100%;height: 33px;overflow: hidden;vertical-align: middle;">'
                    //            tl += ' <div class="margin-0" style="height: 33px; vertical-align: middle; width: 100%; text-align: center; max-height: 33px; display: table-cell;">'
                    //            tl += '   <a href="' + dt.data[j].Link + '" data-element="color" title="@item.Store.StoreName" style="  width: 100%;white-space: normal;font-size:12px;margin: 0px;color:black;">' + dt.data[j].StoreName + '</a>'
                    //            tl += '   </div>'
                    //            tl += '   </div>'
                    //            tl += '   </div>'
                    //            tl += '  </div>'
                    //        }
                    //    }
                    //    tl += ' </div>'
                    //    tl += ' </div>'

                    //}
                    //tl += ' </div>'
                    //tl += ' <div class="swiper-pagination swiper-pagination5"></div>'




                    var tl = '';
                    for (var i = 0; i < number; i++) {
                        var step_f = i * 6, stemp_l = step_f + 6;
                        tl += '<div class="item">'
                        tl += '<div class="row">'
                        for (var j = step_f; j < stemp_l; j++) {
                            if (typeof dt.data[j] != "undefined") {
                                tl += ' <div class="col s6" style="margin-bottom: 5px;padding:0px 3px;">'
                                tl += '<div class="card margin-0">'
                                tl += ' <div class="card-image">'
                                tl += '<a href="' + dt.data[j].Link + '">'
                                tl += '<img src="http://galagala.vn:8888' + dt.data[j].Url + dt.data[j].MediaName + '" class="swiper-lazy" style="max-height: 100%; max-width: 100%; width: 100%; height: auto;">'
                                tl += ' </a>'
                                tl += ' </div>'
                                tl += ' <div class="card-action user-padding-0" style="  display: table;width: 100%;height: 33px;overflow: hidden;vertical-align: middle;">'
                                tl += ' <div class="margin-0" style="height: 33px; vertical-align: middle; width: 100%; text-align: center; max-height: 33px; display: table-cell;">'
                                tl += '   <a href="' + dt.data[j].Link + '" data-element="color" title="@item.Store.StoreName" style="  width: 100%;white-space: normal;font-size:12px;margin: 0px;color:black;">' + dt.data[j].StoreName + '</a>'
                                tl += '   </div>'
                                tl += '   </div>'
                                tl += '   </div>'
                                tl += '  </div>'
                            }
                        }
                        tl += ' </div>'
                        tl += ' </div>'

                    }


                    $(dataLink.s.all.target).html(tl)
                    $(dataLink.s.all.target).owlCarousel({
                        itemsCustom: [
       [0, 1],
       [450, 1],
       [600, 1],
       [700, 1],
       [1000, 1],
       [1200, 1],
       [1400, 1],
       [1600, 1]
                        ],
                        navigation: true
                    });
                    //new Swiper(dataLink.s.all.target, {
                    //    pagination: '.swiper-pagination5',
                    //    paginationClickable: true,
                    //    preloadImages: false,
                    //    lazyLoading: true,
                    //});
                }, error: function (error) { }
            }); //end ajax call
        },
        filter: function () {
            var a = this;
            $.ajax({
                type: "GET",
                url: dataLink.s.filter,
                cache: true,
                dataType: "json",
                success: function (dt) {

                }, error: function (error) { }
            }); //end ajax call
        }
    },
    create: function () {

    },
    remove: function () {
    },
    init: function () {

    }
}
Galagala.product = {
    items: [],
    get: {
        gen: function (dt) {
            //var tl = '<div class="swiper-wrapper">';
            //for (var j = 0; j < dt.data.length; j++) {
            //    tl += '<div class="swiper-slide truncate" style="width: 150px; height: 157px;">'
            //    tl += '<div class="card" style="width: 150px; height: 157px;">'
            //    tl += '<div class="card-image">'
            //    tl += '<a href="' + dt.data[j].Link + '">'
            //    tl += '  <img class="lazyload" data-sizes="auto" src="data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" data-src="' + galagala_config.HOST_MEDIA + dt.data[j].Url + dt.data[j].MediaName + '" height="84" style="min-height: 84px; min-width: 150px; width: 150px !important; height: 84px !important; ">'
            //    tl += ' </a>'
            //    tl += '  </div>'
            //    tl += '  <div class="card-content user-padding-0">'
            //    tl += '  <div style="padding: 0px; overflow: hidden; max-height: 36px; line-height: 15px; height: 36px; vertical-align: middle; text-align: center; display: table-cell; text-align: center; width: 150px; white-space: normal; ">'
            //    tl += '  <a style="color: #000; margin: 0px;font-size:12px;" href="' + dt.data[j].Link + '">'
            //    tl += dt.data[j].ProductName
            //    tl += '  </a>'
            //    tl += ' </div>'
            //    tl += ' <div style="text-align: center; padding: 0px 5px; ">'
            //    if (+dt.data[j].PromotePrice < +dt.data[j].MobileOnlinePrice) {
            //        tl += '  <a style=" font-size: 10px; color: black; text-decoration: line-through; float: left;width:100%;">'
            //        tl += dt.data[j].MobileOnlinePrice_write
            //        tl += '   </a>'
            //        tl += '  <a class="user-color-red" style=" font-size: 14px; font-weight: bold; float: left; width: 100%;">'
            //        tl += dt.data[j].PromotePrice_write
            //        tl += '   </a>'
            //    }
            //    else {
            //        tl += '   <a class="user-color-red" style="font-size: 14px; font-weight: bold; float: left; width: 100%;">'
            //        tl += dt.data[j].MobileOnlinePrice_write
            //        tl += ' </a>'
            //    }
            //    tl += ' <div class="clearfix"></div>'
            //    tl += ' </div>'
            //    tl += '  <div class="clearfix"></div>'
            //    tl += ' </div>'
            //    tl += '  </div>'
            //    tl += '    </div>'
            //}
            //tl += ' </div>'
            //tl += ' <div class="swiper-button-next ' + dt.next + ' swiper-button-white user-swiper-button-next-prev"></div>'
            //tl += '  <div class="swiper-button-prev ' + dt.prev + ' swiper-button-white user-swiper-button-next-prev"></div>'


            var tl = '';
            for (var j = 0; j < dt.data.length; j++) {
                tl += '<div class="item SwipProduct">'
                tl += '<div class="card" style="width: 150px; height: 157px;">'
                tl += '<div class="card-image">'
                tl += '<a href="' + dt.data[j].Link + '">'
                tl += '  <img src="' + galagala_config.HOST_MEDIA + dt.data[j].Url + dt.data[j].MediaName + '" height="84" style="min-height: 84px; min-width: 150px; width: 150px !important; height: 84px !important; ">'
                tl += ' </a>'
                tl += '  </div>'
                tl += '  <div class="card-content user-padding-0">'
                tl += '  <span class="title-name">'
                tl += '  <a style="color: #000; margin: 0px;font-size:12px;" href="' + dt.data[j].Link + '">'
                tl += dt.data[j].ProductName
                tl += '  </a>'
                tl += ' </span>'
                tl += ' <p class="title-price">'
                tl += (dt.data[j].PromotePrice != null && dt.data[j].PromotePrice < dt.data[j].PromotePrice ? dt.data[j].PromotePrice_write : dt.data[j].MobileOnlinePrice_write);
                //if (+dt.data[j].PromotePrice < +dt.data[j].MobileOnlinePrice) {
                //    //tl += '  <a style=" font-size: 10px; color: black; text-decoration: line-through; float: left;width:100%;">'
                //    tl += dt.data[j].MobileOnlinePrice_write
                //    //tl += '   </a>'
                //    //tl += '  <a class="user-color-red" style=" font-size: 14px; font-weight: bold; float: left; width: 100%;">'
                //    tl += dt.data[j].PromotePrice_write
                //    //tl += '   </a>'
                //}
                //else {
                //    tl += '   <a class="user-color-red" style="font-size: 14px; font-weight: bold; float: left; width: 100%;">'
                //    tl += dt.data[j].MobileOnlinePrice_write
                //    tl += ' </a>'
                //}
                tl += ' </p>'
                tl += '  <div class="clearfix"></div>'
                tl += ' </div>'
                tl += '  </div>'
                tl += '    </div>'
            }


            //, width: (dt.data.length*150) + 'px' 


            $(dt.target).html(tl).css({display: 'block'});
            //    $(dt.target).owlCarousel({
            ////        itemsCustom: [
            ////[0, 2],
            ////[450, 4],
            ////[600, 7],
            ////[700, 9],
            ////[1000, 10],
            ////[1200, 12],
            ////[1400, 13],
            ////[1600, 15]
            ////        ],
            //        lazyLoad: true,
            //        navigation: false
            //    });
        },
        p_buy: function () {
            $.ajax({
                type: "GET",
                url: dataLink.p.buy.link,
                cache: true,
                dataType: "json",
                success: function (dt) {
                    Galagala.product.get.gen({ data: dt.data, target: dataLink.p.buy.target, next: dataLink.p.buy.next, prev: dataLink.p.buy.prev })
                }, error: function (error) { }
            }); //end ajax call
        },
        p_hot: function () {
            $.ajax({
                type: "GET",
                url: dataLink.p.hot.link,
                cache: true,
                dataType: "json",
                success: function (dt) {
                    Galagala.product.get.gen({ data: dt.data, target: dataLink.p.hot.target, next: dataLink.p.hot.next, prev: dataLink.p.hot.prev })
                }, error: function (error) { }
            }); //end ajax call
        },
        p_recent: function () {
            if ($(dataLink.p.recent.target).length > 0) {
                $.ajax({
                    type: "GET",
                    url: dataLink.p.recent.link,
                    cache: true,
                    dataType: "json",
                    success: function (dt) {
                        Galagala.product.get.gen({ data: dt.data, target: dataLink.p.recent.target, next: dataLink.p.recent.next, prev: dataLink.p.recent.prev })
                    }, error: function (error) { }
                }); //end ajax call
            }
        },
        p_category: function () {
            $.ajax({
                type: "GET",
                url: dataLink.p.category.link,
                cache: true,
                dataType: "json",
                success: function (dt) {
                    if (dt.data.length == 0) 
                        $(dataLink.p.category.target).parent().parent().remove()
                    else Galagala.product.get.gen({ data: dt.data, target: dataLink.p.category.target, next: dataLink.p.category.next, prev: dataLink.p.category.prev })
                }, error: function (error) { }
            }); //end ajax call
        },
        p_store: function () {
            $.ajax({
                type: "GET",
                url: dataLink.p.top_store.link,
                cache: true,
                dataType: "json",
                success: function (dt) {
                    Galagala.product.get.gen({ data: dt.data, target: dataLink.p.top_store.target, next: dataLink.p.top_store.next, prev: dataLink.p.top_store.prev })
                }, error: function (error) { }
            }); //end ajax call
        },
        b_suggestions: function () {
            $.ajax({
                type: "GET",
                url: dataLink.p.b_suggestions.link,
                cache: true,
                dataType: "json",
                success: function (dt) {
                    Galagala.product.get.gen({ data: dt.data, target: dataLink.p.b_suggestions.target, next: dataLink.p.b_suggestions.next, prev: dataLink.p.b_suggestions.prev })
                }, error: function (error) { }
            }); //end ajax call
        }
    },
    create: function () {

    },
    remove: function () {
    },
    init: function () {

    }
}
Galagala.brand = {
    items: [],
    get: {
        home_top: function () {
            $.ajax({
                type: "GET",
                url: dataLink.b.top.link,
                cache: true,
                dataType: "json",
                success: function (dt) {
                    //var tl = '<div class="swiper-wrapper swiper-wrapper">';
                    //for (var j = 0; j < dt.data.length; j++) {
                    //    if (typeof dt.data[j].Logo_Url != "undefined" && dt.data[j].Logo_Url != "") {
                    //        tl += '<div class="swiper-slide" style="width:auto;">'
                    //        tl += '<a href="' + dt.data[j].Link + '">'
                    //        tl += ' <img class="lazyload user-height-0 user-col-0" data-sizes="auto" src="http://galagala.vn:8888' + dt.data[j].Logo_Url + dt.data[j].Logo_MediaName + '" width="100" height="100" style="max-height:100px;height:100px;width:auto;"/>'
                    //        tl += '</a>'
                    //        tl += ' </div>'
                    //    }
                    //}
                    //tl += ' </div>'
                    //<div class="item"><h1>1</h1></div>
                    var tl = '';
                    for (var j = 0; j < dt.data.length; j++) {
                        if (typeof dt.data[j].Logo_Url != "undefined" && dt.data[j].Logo_Url != "") {
                            //tl += '<div class="item">'
                            tl += '<a href="' + dt.data[j].Link + '">'
                            tl += ' <img src="http://galagala.vn:8888' + dt.data[j].Logo_Url + dt.data[j].Logo_MediaName + '" width="100" height="100" style="max-height:100px;height:100px;width:auto;display: inline-block;white-space: nowrap;"/>'
                            tl += '</a>'
                                //+ '</div>'
                        }
                    }
                    //tl += ' </div>'
                    $(dataLink.b.top.target).html(tl).css({ display: 'block'});
                    //$(dataLink.b.top.target).html(tl)

                    //$(dataLink.b.top.target).owlCarousel({
                    //    itemsCustom: [[0, 2], [450, 3], [600, 4], [700, 5], [1000, 5], [1200, 7], [1400, 7], [1600, 7]],
                    //    navigation: false,
                    //    lazyLoad: true

                    //});
                    //new Swiper(dataLink.b.top.target, {
                    //    slidesPerView: 'auto',
                    //    centeredSlides: false,
                    //    paginationClickable: true,
                    //    spaceBetween: 0,
                    //    autoplay: 4000,
                    //    autoplayDisableOnInteraction: false,
                    //    freeMode: true
                    //});
                }, error: function (error) { }
            }); //end ajax call
        }
    },
    create: function () {

    },
    remove: function () {
    },
    init: function () {

    }
}
Galagala.language = {
    addtocart: '',
    get: function () {
    },
    create: function () {

    },
    remove: function () {
    },
    init: function () {

    }
}
Galagala.province = {
    get: function (id) {
    },
    create: function () {

    },
    remove: function (id) {
    },
    init: function () {

    }
}
Galagala.district = {
    get: function (id) {
    },
    getByProvinceId: function (id) {
    },
    create: function () {

    },
    remove: function (id) {
    },
    init: function () {

    }
}
Galagala.alert = {
    get: function () {
    },
    create: function () {

    },
    remove: function () {
    },
    init: function () {

    }
}