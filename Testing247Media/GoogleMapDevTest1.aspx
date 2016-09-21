<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleMapDevTest1.aspx.cs" Inherits="Testing247Media.GoogleMapDevTest1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyDn5scaDriph6nXqc_SQKSh_WgstWO-aGk"></script>
	<script type="text/javascript">

		var locations = [
		['Stadtbibliothek Zanklhof', 47.06976, 15.43154, 1],
		['Stadtbibliothek dieMediathek', 47.06975, 15.43116, 2],
		['Stadtbibliothek Gösting', 47.09399, 15.40548, 3],
		['Stadtbibliothek Graz West', 47.06993, 15.40727, 4],
		['Stadtbibliothek Graz Ost', 47.06934, 15.45888, 5],
		['Stadtbibliothek Graz Süd', 47.04572, 15.43234, 6],
		['Stadtbibliothek Graz Nord', 47.08350, 15.43212, 7],
		['Stadtbibliothek Andritz', 47.10280, 15.42137, 8]
		];

		//Using Address
		var geocoder;
		var map;
		//var searchResult = $('.txtSearch').val();
		function initialize() {
			geocoder = new google.maps.Geocoder();
			var latlng = new google.maps.LatLng(47.071876, 15.441456);
			var mapOptions = {
				zoom: 8,
				center: latlng
			}
			map = new google.maps.Map(document.getElementById('map'), mapOptions);

			var marker, i, j, k;
			var markers = [];

			for (i = 0; i < locations.length; i++) {
				marker = new google.maps.Marker({
					position: new google.maps.LatLng(locations[i][1], locations[i][2]),
					map: map,
					draggable: true,
					title: String(locations[i][0])
				});
				marker.set("id", i);
				markers.push(marker);
			}

			for (j = 0; j < markers.length; j++) {
				google.maps.event.addListener(markers[j], 'dragend', function () {
					for (k = 0; k < markers.length; k++) {
						console.log(markers[k].getPosition().lat());
						console.log(markers[k].getPosition().lng());
						console.log(markers[k].title);
					}
				});
			}

			//var marker1, marker2;

			//marker1 = new google.maps.Marker({
			//	position: new google.maps.LatLng(locations[0][1], locations[0][2]),
			//	map: map,
			//	draggable: true,
			//	title: 'locations[0][0]'
			//});

			//marker2 = new google.maps.Marker({
			//	position: new google.maps.LatLng(locations[1][1], locations[1][2]),
			//	map: map,
			//	draggable: true,
			//	title: 'locations[1][0]'
			//});

			//google.maps.event.addListener(marker1, 'dragend', function () {
			//	console.log(marker1.getPosition().lat());
			//	console.log(marker1.getPosition().lng());
			//});

			//google.maps.event.addListener(marker2, 'dragend', function () {
			//	console.log(marker2.getPosition().lat());
			//	console.log(marker2.getPosition().lng());
			//});
		}

	</script>
</head>
<body onload="initialize()">
	<form id="form1" runat="server">
		<div>
		</div>
		<div id="map" style="width: 600px; height: 400px; float: right;">
	</form>
</body>
</html>
