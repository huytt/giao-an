/* ========================================================================
 * xeditable.js
 * Page/renders: forms-xeditable.html
 * Plugins used: x-editable
 * ======================================================================== */

'use strict';

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

        // Dateui
        // ================================
        $('#store_fromDate').editable({
            format: 'yyyy-mm-dd',
            viewformat: 'dd/mm/yyyy',
            datepicker: {
                weekStart: 1
            }
        }); 

        $('#store_fromDateDetails').editable({
            format: 'yyyy-mm-dd',
            viewformat: 'dd/mm/yyyy',
            datepicker: {
                weekStart: 1
            }
        });

        $('#Product_fromDate').editable({
            format: 'yyyy-mm-dd',
            viewformat: 'dd/mm/yyyy',
            datepicker: {
                weekStart: 1
            }
        });

        $('#Product_fromDateDetails').editable({
            format: 'yyyy-mm-dd',
            viewformat: 'dd/mm/yyyy',
            datepicker: {
                weekStart: 1
            }
        });
        // Combodate
        // ================================
        //$('#store_fromDate').editable({
        //    format: 'YYYY-MM-DD',    
        //    viewformat: 'DD/MM/YYYY',    
        //    template: 'D / MMMM / YYYY',    
        //    combodate: {
        //        minYear: n-20,
        //        maxYear: n,
        //        minuteStep: 1
        //    }
        //});
      


        //$('#store_toDate').editable({
        //    format: 'YYYY-MM-DD',
        //    viewformat: 'DD/MM/YYYY',
        //    template: 'D / MMMM / YYYY',
        //    combodate: {
        //        minYear: n-20,
        //        maxYear: n,
        //        minuteStep: 1
        //    }
        //});     
    });
}));