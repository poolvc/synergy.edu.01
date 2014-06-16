<%@Page Language="C#" AutoEventWireup="true" CodeFile="test3.aspx.cs" Inherits="test3" %> 
 

<!--!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"-->
<html xmlns="http://www.w3.org/1999/xhtml"> 
<head> 
    <title>test</title> 
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" /> 
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" /> 
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script> 
    <script type="text/javascript">
        function initialize() {
            var latlng = new google.maps.LatLng(-34.397, 150.644);
            var myOptions = {
                zoom: 8,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        } 
 
    </script> 
</head> 
<body onload="initialize()"> 
    <form id="Form1" runat="server"> 
        <div id="map_canvas" style="width:100%; height:100%"></div> 
    </form> 
</body> 
</html>