/* ========================================================================
 * google.js
 * Page/renders: maps-google.html
 * Plugins used: gmaps
 * ======================================================================== */

/* global GMaps */

'use strict';

(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define([
            'gmaps'
        ], factory);
    } else {
        factory();
    }
}(function () {
    var objGGmaplink = $("#GGMapLink").val();
    var JsonGGMapLink;
    if (objGGmaplink != '') {
        try {
            JsonGGMapLink = JSON.parse(objGGmaplink);
        } catch (e) {
            JsonGGMapLink = null;
        }       
    }
    $(function () {
        // gmaps - marker
        // ================================
        var marker = new GMaps({
            el: '#gmaps-marker',
            lat: JsonGGMapLink != null ? JsonGGMapLink.LatLng.Lat : 10.728716,
            lng: JsonGGMapLink != null ? JsonGGMapLink.LatLng.Lng : 106.719899,
            click: function (e) {
                //xóa những điểm (marker) trước đó.
				 while(marker.markers.length>0){
					 marker.removeMarker(marker.markers[0]);					
				 }				 
                //alert('You clicked in this marker '+e.latLng.A + ' '+e.latLng.F);
				 marker.addMarker({
					lat: e.latLng.A,
					lng: e.latLng.F,
					title: $("#GoogleMapAddressTitle").val(),
					infoWindow: {
					    content: '<p>' + $("#GoogleMapAddressContent").val() + '</p>'
					}
				 });
                //ghi Json
				 var obj = '{'
                           + '"LatLng" : { "Lat":' + e.latLng.A + ' ,"Lng" : ' + e.latLng.F + ' },'
                           + '"Title"  : "' + $("#GoogleMapAddressTitle").val() + '",'
                           + '"Content" : "' + $("#GoogleMapAddressContent").val() + '"'
                           + '}';
				 $("#GGMapLink").val(obj);
            }			
        });

        if (JsonGGMapLink != null) {
            //var JsonGGMapLink = JSON.parse(objGGmaplink);
            //load dữ liệu lên
            $("#GoogleMapAddressTitle").val(JsonGGMapLink.Title);
            $("#GoogleMapAddressContent").val(JsonGGMapLink.Content)
            marker.addMarker({
                lat: JsonGGMapLink.LatLng.Lat,
                lng: JsonGGMapLink.LatLng.Lng,
                title: JsonGGMapLink.Title,
                infoWindow: {
                    content: '<p>' + JsonGGMapLink.Content + '</p>'
                }
            });
        }
        //marker.addMarker({
        //    lat: -12.043333,
        //    lng: -77.03,
        //    title: 'Lima',
        //    details: {
        //        'database_id': 42,
        //        author: 'HPNeosdasdasdasd'
        //    },
        //    click: function(e){
        //        alert('You clicked in this marker');
				
        //    }
        //});

    });
}));