'use strict';

(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define([
            'selectize',
            'jquery-ui',
            'jquery-ui-timepicker-addon',
            'inputmask',
            'select2'
        ], factory);
    } else {
        factory();
    }
}(function () {
    
    $(function () {
        //$('.dateIssued').datepicker({
        //    changeMonth: true,
        //    changeYear: true
        //});
        $('#areaTask').selectize();
        $('#statusProcess').selectize();
        $('#statusDirection').selectize();
        $('#priority').selectize();

    });
}));
$(document).ready(function () { $(".printElement-User").click(function () { $("#" + $(this).attr('data-user-content')).printElement(); }); });