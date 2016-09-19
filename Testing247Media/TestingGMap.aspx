<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestingGMap.aspx.cs" Inherits="Testing247Media.TestingGMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title></title>
	<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyDn5scaDriph6nXqc_SQKSh_WgstWO-aGk"></script>
	<script type="text/javascript">
		//function initialize() {
		//	var latLng = new google.maps.LatLng(-34.397, 150.644);
		//	var myOptions = {
		//		zoom: 8,
		//		center: latLng,
		//		mapTypeId: google.maps.MapTypeId.ROADMAP
		//	};
		//	var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
		//	var marker = new google.maps.Marker({
		//		position: latLng,
		//		map: map,
		//		title: 'Hello'
		//	});
		//}



		//Using Address
		var geocoder;
		var map;
		//var searchResult = $('.txtSearch').val();
		function initialize(lat, long) {
			geocoder = new google.maps.Geocoder();
			var latlng = new google.maps.LatLng(lat, long);
			var mapOptions = {
				zoom: 8,
				center: latlng
			}
			map = new google.maps.Map(document.getElementById('map'), mapOptions);
			var marker = new google.maps.Marker({
				position: latlng,
				map: map,
				draggable: true,
				title: 'Hello'
			});
		}

		<%--function codeAddress() {
			var address = document.getElementById('<%= txtSearch.ClientID %>').value;
			geocoder.geocode({ '<%= txtSearch.ClientID %>': address }, function (results, status) {
				if (status == 'OK') {
					map.setCenter(results[0].geometry.location);
					var marker = new google.maps.Marker({
						map: map,
						position: results[0].geometry.location
					});
				} else {
					alert('Geocode was not successful for the following reason: ' + status);
				}
			});
		}--%>

		function invalidLocation() {
			alert("The location that you entered could not be found");
		}
	</script>
</head>
<%--<body onload="initialize()">--%>
<body>
	<form runat="server">
		<asp:HiddenField ID="hdLat" runat="server" />
		<asp:HiddenField ID="hdLong" runat="server" />
		<div>
		</div>
		<div>
			<asp:Label ID="lblStorePageHeader" runat="server" Font-Size="X-Large"></asp:Label>
		</div>
		<div>
			<asp:Image runat="server" ID="imgStorefront" ImageUrl="" />
		</div>
		<div id="map" style="width: 600px; height: 400px; float: right;">
		</div>
		<asp:TextBox ID="txtSearch" runat="server" value="Port Louis"></asp:TextBox>
		<asp:Button ID="btnSearch" runat="server" Text="Button" OnClick="btnSearch_Click" />
		<%--<asp:Button ID="btnSearch" runat="server" Text="Button" OnClientClick="codeAddress()" />--%>
	</form>
</body>
</html>
