<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleMapDevTest.aspx.cs" Inherits="Testing247Media.GoogleMapDevTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title></title>
	<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=AIzaSyDn5scaDriph6nXqc_SQKSh_WgstWO-aGk"></script>
	<script type="text/javascript">
		var map;
		function initialize(lat, long) {
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

			google.maps.event.addListener(marker, 'dragend', function () {
				console.log(marker.getPosition().lat());
				console.log(marker.getPosition().lng());
			});
		}
	</script>
</head>
<%--<body onload="initialize()">--%>
<body>
	<form runat="server">
		<%--<asp:HiddenField ID="hdLat" runat="server" />
		<asp:HiddenField ID="hdLong" runat="server" />--%>
		<asp:TextBox ID="txtLat" runat="server"></asp:TextBox>
		<asp:TextBox ID="txtLng" runat="server"></asp:TextBox>
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
