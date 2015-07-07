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
        // text
        // ================================
        $('#xe_username').editable({
            title: 'Enter username'
        });

        // textarea
        // ================================
        $('#xe_comments').editable({
            title: 'Enter comments',
            rows: 5
        });

        // select
        // ================================
        $('#xe_status').editable({
            value: 2,    
            source: [
                  {value: 1, text: 'Active'},
                  {value: 2, text: 'Blocked'},
                  {value: 3, text: 'Deleted'}
               ]
        });

        // Checklist
        // ================================
        $('#xe_checklist').editable({
            value: [1],    
            source: [
                  {value: 1, text: 'option1'},
                  {value: 2, text: 'option2'},
                  {value: 3, text: 'option3'}
               ]
        });
        var date = new Date();
        // Combodate
        // ================================
        $('#DateOfBirth').editable( {           
                format: 'YYYY-MM-DD',    
                viewformat: 'YYYY-MM-DD',    
                template: 'D / MMMM / YYYY',    
            combodate: {
                minYear: date.getFullYear() - 50,
                maxYear: date.getFullYear(),
                minuteStep: 1
            }
        });       
    });
}));
