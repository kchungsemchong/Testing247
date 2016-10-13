<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReturnCoords.aspx.cs" Inherits="Testing247Media.ReturnCoords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.cdnjs.com/ajax/libs/json2/20110223/json2.js"></script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $.ajax({
        //        type: "POST",
        //        url: 'ReturnCoords.aspx/HelloWorld',
        //        data: '{}',
        //        contentType: 'application/json',
        //        dataType: 'json',
        //        success: function (data) {
        //            console.log(data.d);
        //            return false;
        //        },
        //        error: function (error) {
        //            alert(error);
        //        }
        //    });
        //});
        //$(document).ready(function () {
        //    $("input[id$='btnGetGeoLocations']").click(function () {
        //        $.ajax({
        //            type: "POST",
        //            url: 'ReturnCoords.aspx/GetGeoLocations',
        //            data: '{}',
        //            contentType: 'application/json',
        //            dataType: 'json',
        //            success: function (data) {
        //                console.log(data.d);
        //                return false;
        //            },
        //            error: function (error) {
        //                alert(error);
        //            }
        //        });
        //    });
        //});

        var id = "f1a3sd4f64asf";
        $(document).ready(function () {
            $("input[id$='btnGetGeoLocations']").mousedown(function () {
                $.ajax({
                    type: "POST",
                    url: 'ReturnCoords.aspx/HelloWorld',
                    data: JSON.stringify({ msg: 'MP3'}),
                    //data: "{'foo':'foovalue'}",
                    contentType: 'application/json',
                    dataType: 'json',
                    success: function (data) {
                        console.log(data.d);
                        return true;
                    },
                    error: function (error) {
                        console.log(error.responseText);
                        //alert(error.responseText);
                    }
                });
            });
        });
        function test() {
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtLocations" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnGetGeoLocations" runat="server" Text="GetGeoLocations" OnClientClick="return test();" OnClick="btnGetGeoLocations_Click" />
            <asp:Button ID="btnTest" runat="server" Text="test" OnClick="btnTest_Click" />
            <br />
            <%--  <asp:Repeater ID="rptTest" runat="server">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name").ToString() %>'></asp:Label>
                    <asp:Label ID="lblAdvertId" runat="server" Text='<%# Eval("AdvertId").ToString() %>'></asp:Label>
                    <asp:Label ID="lblGeoLocationId" runat="server" Text='<%# Eval("GeoLocationId").ToString() %>'></asp:Label>
                    <asp:Label ID="lblZoomLevel" runat="server" Text='<%# Eval("ZoomLevel").ToString() %>'></asp:Label>
                    <asp:Label ID="lblLatitude" runat="server" Text='<%# Eval("Latitude").ToString() %>'></asp:Label>
                    <asp:Label ID="lblLongitude" runat="server" Text='<%# Eval("Longitude").ToString() %>'></asp:Label>
                </ItemTemplate>
            </asp:Repeater>--%>
        </div>
    </form>
</body>
</html>
