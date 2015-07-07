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
        $('.select2').select2();

        tinyMCE.init({
            selector: '.txt_tinyMCE',
            height: 150,
            menubar:false,
            plugins: [
                 "advlist autolink link image lists charmap print preview hr pagebreak",
                 "searchreplace wordcount visualblocks visualchars insertdatetime",
                 "save table contextmenu directionality emoticons paste textcolor"
            ],
            style_formats: [{
                title: "Inline", items: [
            { title: "Bold", icon: "bold", format: "bold" },
            { title: "Italic", icon: "italic", format: "italic" },
            { title: "Underline", icon: "underline", format: "underline" },
            { title: "Strikethrough", icon: "strikethrough", format: "strikethrough" },
            { title: "Superscript", icon: "superscript", format: "superscript" },
            { title: "Subscript", icon: "subscript", format: "subscript" },
            { title: "Code", icon: "code", format: "code" }
                ]
            },
    {
        title: "Blocks", items: [
           { title: "Paragraph", format: "p" },
           { title: "Blockquote", format: "blockquote" },
           { title: "Div", format: "div" },
           { title: "Pre", format: "pre" }
        ]
    },
    {
        title: "Alignment", items: [
           { title: "Left", icon: "alignleft", format: "alignleft" },
           { title: "Center", icon: "aligncenter", format: "aligncenter" },
           { title: "Right", icon: "alignright", format: "alignright" },
           { title: "Justify", icon: "alignjustify", format: "alignjustify" }
        ]
    }],
            toolbar: "undo redo | fontsizeselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | preview | forecolor backcolor",
            fontsize_formats: "8pt 10pt 12pt 14pt 18pt 24pt 36pt 72pt",
            file_browser_callback: RoxyFileBrowser
        });

    });
    function RoxyFileBrowser(field_name, url, type, win) {
        var roxyFileman = '../../Content/filebrowser/index.html';
        if (roxyFileman.indexOf("?") < 0) {
            roxyFileman += "?type=" + type;
        }
        else {
            roxyFileman += "&type=" + type;
        }
        //roxyFileman += '&input=' + field_name + '&value=' + win.document.getElementById(field_name).value;
        if (tinyMCE.activeEditor.settings.language) {
            roxyFileman += '&langCode=' + tinyMCE.activeEditor.settings.language;
        }
        tinyMCE.activeEditor.windowManager.open({
            file: roxyFileman,
            title: 'Roxy Fileman',
            width: 850,
            height: 650,
            resizable: "yes",
            plugins: "media",
            inline: "yes",
            close_previous: "no"
        }, { window: win });
        return false;
    }
}));
$(document).ready(function () { $(".printElement-User").click(function () { $("#" + $(this).attr('data-user-content')).printElement(); }); });