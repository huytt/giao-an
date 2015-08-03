//high Chart
ShowHighChart('date');
function ShowHighChart(type,store_id)
{
    var datefrom = '';
    var detailstore = '';
    var linkstoreid = '';
    if (store_id != null) {
        datefrom = '&datefrom='+ $('#store_fromDateDetails').text();
        detailstore = '_storeDetails';
        linkstoreid = '&storeId=' + store_id;
    }
    else {
        datefrom = '&datefrom=' + $('#store_fromDate').text();
    } 

    $(function () {

        $.getJSON('/StoreSTS/GetStoreStatistic2?type=' + type + datefrom + linkstoreid, function (data) {
            // Create the chart
            var name = ((store_id == null) ? "all Store" : $("#store_name_" + store_id).text());
            $('#container2' + detailstore).highcharts('StockChart', {
                rangeSelector: {
                    selected: 1
                },

                title: {
                    text: 'Daily visits at '+ name
                },

                series: [{
                    name: 'Visitors',
                    data: JSON.parse(data.v),
                    tooltip: {
                        valueDecimals: 0
                    }
                }, {
                    name: 'Members',
                    data: JSON.parse(data.m),
                    tooltip: {
                        valueDecimals: 0
                    }
                }]
            });
        });

    });

    $(function () {

        //// Get the CSV and create the chart
        //$.getJSON('StoreSTS/GetStoreStatistic2?type=' + type  + datefrom + linkstoreid, function (data) {
        //    var name = ((store_id == null) ? "all Store" : $("#store_name_" + store_id).text());
        //    $('#container' + detailstore).highcharts({

        //        data: {
        //            csv: data.csv
        //        },

        //        title: {
        //            text: 'Daily visits at '+ name
        //        },

        //        subtitle: {
        //            text: 'Source: Google Analytics'
        //        },

        //        xAxis: {
        //            tickInterval: 7 * 24 * 3600 * 1000, // one week
        //            tickWidth: 0,
        //            gridLineWidth: 1,
        //            labels: {
        //                align: 'left',
        //                x: 3,
        //                y: -3
        //            }
        //        },

        //        yAxis: [{ // left y axis
        //            title: {
        //                text: null
        //            },
        //            labels: {
        //                align: 'left',
        //                x: 3,
        //                y: 16,
        //                format: '{value:.,0f}'
        //            },
        //            showFirstLabel: false
        //        }, { // right y axis
        //            linkedTo: 0,
        //            gridLineWidth: 0,
        //            opposite: true,
        //            title: {
        //                text: null
        //            },
        //            labels: {
        //                align: 'right',
        //                x: -3,
        //                y: 16,
        //                format: '{value:.,0f}'
        //            },
        //            showFirstLabel: false
        //        }],

        //        legend: {
        //            align: 'left',
        //            verticalAlign: 'top',
        //            y: 20,
        //            floating: true,
        //            borderWidth: 0
        //        },

        //        tooltip: {
        //            shared: true,
        //            crosshairs: true
        //        },

        //        plotOptions: {
        //            series: {
        //                cursor: 'pointer',
        //                point: {
        //                    events: {
        //                        click: function (e) {
        //                            hs.htmlExpand(null, {
        //                                pageOrigin: {
        //                                    x: e.pageX || e.clientX,
        //                                    y: e.pageY || e.clientY
        //                                },
        //                                headingText: this.series.name,
        //                                maincontentText: Highcharts.dateFormat('%A, %b %e, %Y', this.x) + ':<br/> ' +
        //                                    this.y + ' visits',
        //                                width: 200
        //                            });
        //                        }
        //                    }
        //                },
        //                marker: {
        //                    lineWidth: 1
        //                }
        //            }
        //        },

        //        series: [{
        //            name: 'All visits',
        //            lineWidth: 4,
        //            marker: {
        //                radius: 4
        //            }
        //        }, {
        //            name: 'New visitors'
        //        }]
        //    });
        //});

    });

}

function ShowProductDetails(storeId)
{
    $("#store_title").text("Statistic of " + $("#store_name_" + storeId).text() + "");
    $("#storeIdHidden").val(storeId);
    ShowHighChart('date',storeId);
    $("#modalStoreDetails").modal("show");   

}
$("#btn_hour").click(function () {
    ShowHighChart('hour', $("#storeIdHidden").val());
});
$("#btn_date").click(function () {
    ShowHighChart('date', $("#storeIdHidden").val());
});
$("#btn_month").click(function () {
    ShowHighChart('month', $("#storeIdHidden").val());
});
$("#btn_year").click(function () {
    ShowHighChart('year', $("#storeIdHidden").val());
});




/* ========================================================================
 * dashboard-v1.js
 * Page/renders: index.html
 * Plugins used: flot, sparkline, selectize
 * ======================================================================== */

//'use strict';

//SetValueToChart('hour');
//function SetValueToChart(Type, Dateform, Dateto) {
//    if (Dateform == null || Dateto == null)
//    {
//        Dateform =  $('#store_fromDate').text();
//        Dateto = $('#store_toDate').text();
//    }
//    (function (factory) {
//        if (typeof define === 'function' && define.amd) {
//            define([
//                'selectize',
//                'jquery.flot',
//                'jquery.flot.resize',
//                'jquery.flot.categories',
//                'jquery.flot.time',
//                'jquery.flot.tooltip',
//                'jquery.flot.spline'
//            ], factory);
//        } else {
//            factory();
//        }
//    }(function () {

//        $(function () {
//            // Selectize
//            // ================================
//            $('#selectize-customselect').selectize();

//            // Sparkline
//            // ================================
//            $('.sparklines').sparkline('html', {
//                enableTagOptions: true
//            });

//            // Area Chart - Spline
//            // ================================
//            var lstChartData = [];
//            var timeformat = 'date'; 
//            switch (Type)
//            {
//                case 'hour':
//                    timeformat = "%m/%d %H:%M:%S";
//                    break;
//                case 'date':
//                    timeformat = "%m/%d";
//                    break;
//                case 'month':
//                    timeformat = '%m/%d';
//                    break;
//                case 'year':
//                    timeformat = '%m/%d';
//                    break;
//                default:
//                    timeformat = '%y/%m/%d';
//                    break;
//            }
//            $.ajax({
//                type: "POST",
//                url: 'StoreSTS/GetStoreStatistic',
//                data: { type: Type, datefrom: Dateform, dateto: Dateto },
//                cache: false,
//                dataType: "json",
//                success: function (dt) {
//                    $.each(dt.Array, function (key, val) {
//                        // alert(key + " " + val.Value);
//                        lstChartData.push( [ val.Label.toString(), val.Value]);
//                        //lstChartData.push(new Array(val.Label, val.Value));
//                    });
//                    $.plot('#chart-audience', [{
//                        label: 'Visit (All)',
//                        color: '#3b5998',
//                        data: lstChartData
//                    }],
//                {
//                    series: {
//                        lines: {
//                            show: false
//                        },
//                        splines: {
//                            show: true,
//                            tension: 0.1,
//                            lineWidth: 2,
//                            fill: 0.8
//                        },
//                        points: {
//                            show: true,
//                            radius: 4
                           
//                        }
                    
//                    },
//                    grid: {
//                        borderColor: 'rgba(0, 0, 0, 0.05)',
//                        borderWidth: 1,
//                        hoverable: true,
//                        backgroundColor: 'transparent'
//                    },
//                    tooltip: true,
//                    tooltipOpts: {
//                        content: function (label, xval, yval, flotItem) {
//                            var time = xval*10000; // Time value in ticks
//                            var days = Math.floor(time / (24 * 60 * 60 * 10000000)); // Math.floor() rounds a number downwards to the nearest whole integer, which in this case is the value representing the day
                         
//                            var hours = Math.round((time / (60 * 60 * 10000000)) % 24); // Math.round() rounds the number up or down
//                            var mins = Math.round((time / (60 * 10000000)) % 60);
//                            return "Vào lúc " + hours + " giờ  đến " + (hours +1)+ " giờ.<br/>Số phiên: <span>" + yval + "</span>"
//                        },
//                        dateFormat: "%y-%0m-%0d",
//                        defaultTheme: true
//                    },
//                    xaxis: {
//                        tickColor: 'rgba(0, 0, 0, 0.05)',
//                        mode: "time",
//                        timeformat: timeformat
//                    },
//                    yaxis: {
//                        tickColor: 'rgba(0, 0, 0, 0.05)'
//                    },
//                    shadowSize: 0,

//                });

//                }, error: function (error) { $("#data-ProductSearch").html("<tr><td colspan='2' style='text-align:center;'>Not found!</td>"); }
//            }); //end ajax call
//            // alert(lstChartData);

//        });
//    }));
//}