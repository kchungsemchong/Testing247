<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReturnCoords.aspx.cs" Inherits="Testing247Media.ReturnCoords" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.cdnjs.com/ajax/libs/json2/20110223/json2.js"></script>
    <script type="text/javascript">
        <%--var test = "test";
        document.getElementById('<%= txtLocations.ClientID%>').value = test;--%>
        //$.ajax({
        //    type: "POST",
        //    url: "PageName.aspx/MethodName",
        //    data: "{}",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function(msg) {
        //        // Do something interesting with msg.d here.
        //    }
        $(document).ready(function () {
            $.ajax({
                type: "post",
                url: '/ReturnCoords.aspx/HelloWorld',
                data: '{test}',
                contentType: 'application/json',
                dataType: 'json',
                success: function (data) {
                    //console.log(data.d);
                    debugger;
                },
                error: function (error) {
                    alert(error);
                }
            });
        });
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtLocations" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
</html>
