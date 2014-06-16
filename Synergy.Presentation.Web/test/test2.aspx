<%@Page Language="C#" AutoEventWireup="true" CodeFile="test2.aspx.cs" Inherits="test1" %> 
 
<html xmlns="http://www.w3.org/1999/xhtml"> 
<head> 
    <title>test</title> 
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" /> 
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script> 
    <script type="text/javascript">
                  var map;
                  function initialize() {
                       var myOptions = {
                      zoom: 15,
                      center: new google.maps.LatLng(-12.13651251, -77.00080564),
                      mapTypeId: google.maps.MapTypeId.ROADMAP,
                      mapTypeControl: true,
                      mapTypeControlOptions: {
                          style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                          position: google.maps.ControlPosition.BOTTOM_CENTER
                      },
                      panControl: true,
                      panControlOptions: {
                          position: google.maps.ControlPosition.TOP_RIGHT
                      },
                      zoomControl: true,
                      zoomControlOptions: {
                          style: google.maps.ZoomControlStyle.LARGE,
                          position: google.maps.ControlPosition.LEFT_CENTER
                      },
                      scaleControl: true,
                      scaleControlOptions: {
                          position: google.maps.ControlPosition.TOP_LEFT
                      },
                      streetViewControl: true,
                      streetViewControlOptions: {
                          position: google.maps.ControlPosition.LEFT_TOP
                      }
                    };

                var contentString = '<div id="content">'+'<div id="bodyContent">'+'Oficina CIDE<br>Gerente de Operaciones: John Carrasco<br>Telefono: 448-16-53<br><a href="http://www.galexito.com" target=_blank>www.galexito.com</a>'+'</div>'+'</div>';
                    var myLatlng = new google.maps.LatLng(-12.13651251, -77.00080564);
                    var image = 'carrito2.png';
                    var map = new google.maps.Map(document.getElementById("map_canvas"),myOptions);   
                    var marker = new google.maps.Marker({position: myLatlng,map: map,icon: image});  

                var infowindow = new google.maps.InfoWindow({
                    content: contentString
                });

                google.maps.event.addListener(marker, 'click', function() {
                  infowindow.open(map,marker);
                });
       
            }

        google.maps.event.addDomListener(window, 'load', initialize);
    </script>
</head> 
<body onload="initialize()"> 
    <form id="Form1" runat="server"> 
        <div id="map_canvas" style="width:100%; height:100%"></div> 
    </form> 
</body> 
</html>