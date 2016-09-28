<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleMapDevTest1.aspx.cs" Inherits="Testing247Media.GoogleMapDevTest1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyDn5scaDriph6nXqc_SQKSh_WgstWO-aGk"></script>
    <script type="text/javascript">
        var markers = [];
        var locations;
        var errorMessage;
        function initialize(locations, errorMessage) {

            if (!(errorMessage === "NoError"))
                alert(errorMessage);

            if (locations.length > 4) {
                document.getElementById('<%= txtGeoLocation.ClientID%>').style.display = 'none';
                document.getElementById('<%= btnGeoLocation.ClientID%>').style.display = 'none';
            }
	       var jsonLocations = JSON.stringify(locations);
	       geocoder = new google.maps.Geocoder();
	       if (typeof locations === "undefined") {
	           var latlng = new google.maps.LatLng(55.378051, -3.43597299999999);
	           var mapOptions = {
	               zoom: 4,
	               center: latlng
	           }
	           map = new google.maps.Map(document.getElementById('map'), mapOptions);
	           return;
	       }
	       if (locations.length > 0) {
	           var latlng = new google.maps.LatLng(locations[0].Latitude, locations[0].Longitude);
	           var mapOptions = {
	               zoom: 12,
	               center: latlng
	           }
	       }
	       map = new google.maps.Map(document.getElementById('map'), mapOptions);

	       google.maps.event.addListener(map, 'zoom_changed', function () {
	           for (var m = 0; m < markers.length; m++) {
	               locations[m].ZoomLevel = map.getZoom();
	               var jsonLocations = JSON.stringify(locations);
	               document.getElementById('<%= txtLocations.ClientID%>').value = jsonLocations;
			 }
	       });
	       var marker, i, j, k;
	       var bounds = new google.maps.LatLngBounds();
	       var currentZoom = map.getZoom();
	       for (i = 0; i < locations.length; i++) {
	           marker = new google.maps.Marker({
	               position: new google.maps.LatLng(locations[i].Latitude, locations[i].Longitude),
	               map: map,
	               draggable: true,
	               title: String(locations[i].Name)
	           });
	           markers.push(marker);
	           bounds.extend(markers[i].getPosition());
	           latitude = locations[i].Latitude;
	           longitude = locations[i].Longitude;
	           document.getElementById('<%= txtLocations.ClientID%>').value = jsonLocations;
		  }
	       var isMarkerUnique = true;
	       var bounds = new google.maps.LatLngBounds();
	       var savedZoom = locations[0].ZoomLevel;
	       var latLng = new google.maps.LatLng(locations[0].Latitude, locations[0].Longitude)
	       for (j = 0; j < markers.length; j++) {
	           google.maps.event.addListener(markers[j], 'dragend', function () {
	               for (k = 0; k < markers.length; k++) {
	                   latitude = markers[k].getPosition().lat();
	                   longitude = markers[k].getPosition().lng();
	                   var initialMarkerLatitude = latitude;
	                   var intialMarkerLongitude = longitude;
	                   locations[k].Latitude = latitude;
	                   locations[k].Longitude = longitude;
	                   locations[k].ZoomLevel = map.getZoom();
	                   var jsonLocations = JSON.stringify(locations);
	                   document.getElementById('<%= txtLocations.ClientID%>').value = jsonLocations;
				}
	           });
		  }

	       if (markers.length > 0) {
	           if (markers.length === 1) {
	               map.setCenter(latLng);
	               map.setZoom(savedZoom);
	           }
	           if (markers.length > 1) {
	               for (j = 0; j < markers.length; j++) {
	                   bounds.extend(markers[j].getPosition());
	               }
	               map.fitBounds(bounds);

	               setTimeout(function () {
	                   currentZoom = map.getZoom();

	                   if (savedZoom < currentZoom) {
	                       map.setZoom(savedZoom);
	                   }
	               }, 2000);
	           }
	       }
	   }
    </script>
</head>
<%--<body onload="initialize()">--%>
<body>
    <form id="form1" runat="server">
	   <div>
		  <asp:ScriptManager ID="scGoogleMap" runat="server" EnablePageMethods="true" />
	   </div>
	   <asp:Button ID="btnGeoLocation" runat="server" Text="Add Location" OnClick="btnGeoLocation_Click" />
	   <div id="map" style="width: 600px; height: 400px; float: left;"></div>
	   <div>
		  <asp:TextBox ID="txtLocations" runat="server" AutoPostBack="true" />
		  <br />
		  <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
		  <asp:TextBox ID="txtGeoLocation" runat="server" />
		  <br />

		  <asp:GridView ID="gvGeoLocation" runat="server" AutoGenerateColumns="false"
			 OnRowDeleting="gvGeoLocation_RowDeleting" AutoGenerateDeleteButton="false">
			 <Columns>
				<asp:BoundField DataField="Name" HeaderText="Location"></asp:BoundField>
				<asp:BoundField DataField="Latitude" HeaderText="Latitude" ItemStyle-CssClass="hiddencol"
				    HeaderStyle-CssClass="hiddencol"></asp:BoundField>
				<asp:BoundField DataField="Longitude" HeaderText="Longitude" ItemStyle-CssClass="hiddencol"
				    HeaderStyle-CssClass="hiddencol"></asp:BoundField>
				<asp:CommandField DeleteText="Delete" ShowDeleteButton="true" />
			 </Columns>
		  </asp:GridView>
	   </div>

    </form>
</body>
</html>
