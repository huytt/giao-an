/* ========================================================================
 * xeditable editable
 * Page/renders: forms-xeditable.html
 * Plugins used: x-editable
 * ======================================================================== */

'use strict';

//
//xử lí xedittable
$.each($('a[name=xedit]'), function (key, val) {
    //alert(key + " " + val + this.id.split('_'));
    var lst_obj = this.id.split('_');
    if (lst_obj.length == 2) {
        add_xedittable(this.id.split('_')[1],'');
    }
    else {
        add_xedittable(this.id.split('_')[1], this.id.split('_')[2]);
    }
});
function add_xedittable(id_product, id_size) {
    (function (factory) {
        if (typeof define === 'function' && define.amd) {
            define([
                'xeditable'
            ], factory);
        } else {
            factory();
        }

    }(function () {
        var quantity_value;
        $(function () {
            if (id_size == '') {
                $('#xequantity_' + id_product).editable({
                    title: 'Enter Quantity of Product Item you want to add.',
                    value: '',
                    validate: function (value) {
                        if ($.trim(value) == '') {
                            alert('This field is required');
                            return false;
                        }
                        if ($.isNumeric(value) == '') {
                            alert('Please insert number!');
                            return false;
                        }
                        quantity_value = value;
                        $('#xequantity_' + id_product).editable('setValue', null);
                    },
                    display: function (value, response) {
                        return false;//disable this method
                    },
                    success: function (response, newValue) {
                        // $('#xequantity_' + id_product).text("Tesst");             
                        var formData = new FormData();
                        formData.append("ProductItemId", id_product);
                        formData.append("Quantity", quantity_value);
                        $.ajax({
                            type: 'POST',
                            url: '/ProductItem/AddQuantity',
                            data: formData,
                            async: false,
                            cache: false,
                            contentType: false,
                            //mimeType: "multipart/form-data",
                            processData: false,
                            dataType: 'json',
                            success: function (data) {
                                if (data.quantity != -1) {
                                    $('#xequantity_' + id_product).text(data.quantity);
                                }
                                else {
                                    alert("Error: " + data.error);
                                }
                            },
                            error: function (xhr) {
                                // alert(xhr.responseText);
                            }
                        });
                    },
                });
            }
            else {
                $('#xequantity_' + id_product + '_' + id_size).editable({
                    title: 'Enter Quantity of Product Item you want to add.',
                    value: '',
                    validate: function (value) {
                        if ($.trim(value) == '') {
                            alert('This field is required');
                            return false;
                        }
                        if ($.isNumeric(value) == '') {
                            alert('Please insert number!');
                            return false;
                        }
                        quantity_value = value;
                        $('#xequantity_' + +id_product + '_' + id_size).editable('setValue', null);
                    },
                    display: function (value, response) {
                        return false;//disable this method
                    },
                    success: function (response, newValue) {     
                        var formData = new FormData();
                        formData.append("ProductItemId", id_product);
                        formData.append("SizeId", id_size);
                        formData.append("Quantity", quantity_value);
                        $.ajax({
                            type: 'POST',
                            url: '/ProductItem/AddQuantityProductInSize',
                            data: formData,
                            async: false,
                            cache: false,
                            contentType: false,
                            //mimeType: "multipart/form-data",
                            processData: false,
                            dataType: 'json',
                            success: function (data) {
                                if (data.quantity != -1) {
                                    $('#xequantity_' + id_product + '_' + id_size).text(data.quantity);
                                }
                                else {
                                    alert("Error: " + data.error);
                                }
                            },
                            error: function (xhr) {
                                // alert(xhr.responseText);
                            }
                        });
                    },
                });
            }      
        });
    }));
}


$.each($('a[name=xedit]'), function (key, val) {
    //alert(key + " " + val + this.id.split('_'));
    add_xeditable_Ship(this.id.split('_')[1]);
});
function add_xeditable_Ship(id_Ship)
{
    (function (factory) {
        if (typeof define === 'function' && define.amd) {
            define([
                'xeditable'
            ], factory);
        } else {
            factory();
        }

    }(function () {
        var Price_value;
        $(function () {
            // text
            // ================================
            $('#xePrice_' + id_Ship).editable({
                title: 'Enter Price of Item you want to change.',
                value: '',
                validate: function (value) {
                    if ($.trim(value) == '') {
                        alert('This field is required');
                        return false;
                    }
                    if ($.isNumeric(value) == '') {
                        alert('Please insert number!');
                        return false;
                    }
                    Price_value = value;
                    $('#xePrice_' + id_Ship).editable('setValue', null);
                },
                display: function (value, response) {
                    return false;//disable this method
                },
                success: function (response, newValue) {
                    // $('#xequantity_' + id_product).text("Tesst");             
                    var formData = new FormData();
                    formData.append("ShipId", id_Ship);
                    formData.append("Price", Price_value);
                    $.ajax({
                        type: 'POST',
                        url: '/Ship/ChangePrice',
                        data: formData,
                        async: false,
                        cache: false,
                        contentType: false,
                        //mimeType: "multipart/form-data",
                        processData: false,
                        dataType: 'json',
                        success: function (data) {
                            if (data.success == true) {
                                $('#xePrice_' + id_Ship).text(data.price);
                            }
                            else {
                                alert("Error: " + data.error);
                            }
                        },
                        error: function (xhr) {
                            // alert(xhr.responseText);
                        }
                    });
                },
            });
            // text
            // ================================
            var FreeShip_value = 0;
            $('#xeFreeShip_' + id_Ship).editable({
                title: 'Enter Freeship of Item you want to Change.',
                value: '',
                validate: function (value) {
                    if ($.trim(value) == '') {
                        alert('This field is required');
                        return false;
                    }
                    if ($.isNumeric(value) == '') {
                        alert('Please insert number!');
                        return false;
                    }
                    FreeShip_value = value;
                    $('#xeFreeShip_' + id_Ship).editable('setValue', null);
                },
                display: function (value, response) {
                    return false;//disable this method
                },
                success: function (response, newValue) {
                    // $('#xequantity_' + id_product).text("Tesst");             
                    var formData = new FormData();
                    formData.append("ShipId", id_Ship);
                    formData.append("FreeShip", FreeShip_value);
                    $.ajax({
                        type: 'POST',
                        url: '/Ship/ChangeFreeShip',
                        data: formData,
                        async: false,
                        cache: false,
                        contentType: false,
                        //mimeType: "multipart/form-data",
                        processData: false,
                        dataType: 'json',
                        success: function (data) {
                            if (data.success ==true) {
                                $('#xeFreeShip_' + id_Ship).text(data.freeship);
                            }
                            else {
                                alert("Error: " + data.error);
                            }
                            
                        },
                        error: function (xhr) {
                            // alert(xhr.responseText);
                        }
                    });
                },
            });
        });
    }));
}

$.each($('a[name=xedit]'), function (key, val) {
    //alert(key + " " + val + this.id.split('_'));
    add_xeditable_Weight(this.id.split('_')[1]);
});
function add_xeditable_Weight(id_Weight) {
    (function (factory) {
        if (typeof define === 'function' && define.amd) {
            define([
                'xeditable'
            ], factory);
        } else {
            factory();
        }

    }(function () {
        var Price_value;
        $(function () {
            // text
            // ================================
            $('#xeWeightPrice_' + id_Weight).editable({
                title: 'Enter Price of Item you want to change.',
                value: '',
                validate: function (value) {
                    if ($.trim(value) == '') {
                        alert('This field is required');
                        return false;
                    }
                    if ($.isNumeric(value) == '') {
                        alert('Please insert number!');
                        return false;
                    }
                    Price_value = value;
                    $('#xeWeightPrice_' + id_Weight).editable('setValue', null);
                },
                display: function (value, response) {
                    return false;//disable this method
                },
                success: function (response, newValue) {
                    // $('#xequantity_' + id_product).text("Tesst");             
                    var formData = new FormData();
                    formData.append("WeightId", id_Weight);
                    formData.append("Price", Price_value);
                    $.ajax({
                        type: 'POST',
                        url: '/Weight/ChangePrice',
                        data: formData,
                        async: false,
                        cache: false,
                        contentType: false,
                        //mimeType: "multipart/form-data",
                        processData: false,
                        dataType: 'json',
                        success: function (data) {
                            if (data.success == true) {
                                $('#xeWeightPrice_' + id_Weight).text(data.price);
                            }
                            else {
                                alert("Error: " + data.error);
                            }
                        },
                        error: function (xhr) {
                            // alert(xhr.responseText);
                        }
                    });
                },
            });
        });
    }));
}

(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define([
            'xeditable'
        ], factory);
    } else {
        factory();
    }
   
}(function () {  
    $(function () {
        var d = new Date();
        var n = d.getFullYear();
        // Combodate
        // ================================
        var date_DateManufactured;
        $('#xe_DateManufactured').editable({
            type: 'textbox',
            format: 'DD-MM-YYYY',
            viewformat: 'DD-MM-YYYY',
            template: 'D / MMMM / YYYY',
            combodate: {
                minYear: n-20,
                maxYear: n,
                minuteStep: 1
            },
            validate: function (value) {
                date_DateManufactured = value._d;
            },
            success: function () {
                var month = date_DateManufactured.getUTCMonth() + 1; //months from 1-12
                var day = date_DateManufactured.getUTCDate() + 1;
                var year = date_DateManufactured.getUTCFullYear();
                var str_date = month + '-' + day + '-' + year;
                $("#DateManufactured").val(str_date);
            },
        });

        var date_SellDate;
        $('#xe_SellDate').editable({
            format: 'DD-MM-YYYY',
            viewformat: 'DD-MM-YYYY',
            template: 'D / MMMM / YYYY',
            combodate: {
                minYear: n - 20,
                maxYear: n,
                minuteStep: 1
            },
            validate: function (value) {
                date_SellDate = value._d;
            },
            success: function () {
                var month = date_SellDate.getUTCMonth() + 1; //months from 1-12
                var day = date_SellDate.getUTCDate()+1;
                var year = date_SellDate.getUTCFullYear();
                var str_date = month + '-' + day + '-' + year;
                $("#SellDate").val(str_date);
            },
        });

        //// text
        //// ================================
        //$('#xe_username').editable({
        //    title: 'Enter username'
        //});

        //// textarea
        //// ================================
        //$('#xe_comments').editable({
        //    title: 'Enter comments',
        //    rows: 5
        //});

        //// select
        //// ================================
        //$('#xe_status').editable({
        //    value: 2,    
        //    source: [
        //          {value: 1, text: 'Active'},
        //          {value: 2, text: 'Blocked'},
        //          {value: 3, text: 'Deleted'}
        //       ]
        //});

        //// Checklist
        //// ================================
        //$('#xe_checklist').editable({
        //    value: [1],    
        //    source: [
        //          {value: 1, text: 'option1'},
        //          {value: 2, text: 'option2'},
        //          {value: 3, text: 'option3'}
        //       ]
        //});

        

        //// Dateui
        //// ================================
        //$('#xe_dateui').editable({
        //    format: 'yyyy-mm-dd',    
        //    viewformat: 'dd/mm/yyyy',    
        //    datepicker: {
        //        weekStart: 1
        //    }
        //});

        //// Typehead
        //// ================================
        //$('#xe_typehead').editable({
        //    value: 'ru',
        //    typeahead: {
        //        name: 'country',
        //        local: [
        //            {value: 'ru', tokens: ['Russia']}, 
        //            {value: 'gb', tokens: ['Great Britain']}, 
        //            {value: 'us', tokens: ['United States']}
        //        ],
        //        template: function(item) {
        //            return item.tokens[0] + ' (' + item.value + ')'; 
        //        } 
        //    }
        //});
    });
}));
