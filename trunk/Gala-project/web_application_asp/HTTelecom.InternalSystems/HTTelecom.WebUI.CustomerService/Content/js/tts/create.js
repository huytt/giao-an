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
$(document).ready(function () {
    $(".printElement-User").click(function () {
        var l = $(".removeElementPrint");
        for (var i = 0; i < l.length; i++) {
            $(l[i]).css('display','none')
        }
        $("#" + $(this).attr('data-user-content')).printElement();
        var l = $(".removeElementPrint");
        for (var i = 0; i < l.length; i++) {
            $(l[i]).css('display', 'block')
        }
    });
});