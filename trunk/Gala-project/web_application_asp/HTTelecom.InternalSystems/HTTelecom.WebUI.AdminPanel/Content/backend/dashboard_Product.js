//high Chart
ShowHighChart('date');
function ShowHighChart(type, Product_id) {
    var datefrom = '';
    var detailProduct = '';
    var linkProductid = '';
    if (Product_id != null) {
        datefrom = '&datefrom=' + $('#Product_fromDateDetails').text();
        detailProduct = '_ProductDetails';
        linkProductid = '&ProductId=' + Product_id;
    }
    else {
        datefrom = '&datefrom=' + $('#Product_fromDate').text();
    }

    $(function () {

        $.getJSON('/ProductSTS/GetProductStatistic2?type=' + type + datefrom + linkProductid, function (data) {
            // Create the chart
            var name = ((Product_id == null) ? "all Product" : $("#Product_name_" + Product_id).text());
            $('#container2' + detailProduct).highcharts('StockChart', {
                rangeSelector: {
                    selected: 1
                },

                title: {
                    text: 'Daily visits at ' + name
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

}

function ShowProductDetails(ProductId) {
    $("#Product_title").text("Statistic of " + $("#Product_name_" + ProductId).text() + "");
    $("#ProductIdHidden").val(ProductId);
    ShowHighChart('date', ProductId);
    $("#modalProductDetails").modal("show");

}
$("#btn_hour").click(function () {
    ShowHighChart('hour', $("#ProductIdHidden").val());
});
$("#btn_date").click(function () {
    ShowHighChart('date', $("#ProductIdHidden").val());
});
$("#btn_month").click(function () {
    ShowHighChart('month', $("#ProductIdHidden").val());
});
$("#btn_year").click(function () {
    ShowHighChart('year', $("#ProductIdHidden").val());
});
