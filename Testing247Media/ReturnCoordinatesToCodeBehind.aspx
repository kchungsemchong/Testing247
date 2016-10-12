<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReturnCoordinatesToCodeBehind.aspx.cs" Inherits="Testing247Media.ReturnCoordinatesToCodeBehind" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function(){
            var test = "test";
            callServer();

        function callServer() {
            document.getElementById('<%= txtLocations.ClientID%>').value = test;
                alert(<%= txtLocations.ClientID%>.value);
                
                    $.ajax({
                        url: 'ReturnCoordinatesToCodeBehind.aspx/GetValue',
                        type: 'GET',
                        contentType: "application/json; charset=utf-8", 
                        dataType: 'json',
                        data: { result : test},
                        success: function (data) {
                            alert(data);
                        },
                        error: function onFailure() {
                            alert("Failure!");
                        }
                    });

                }
            
        })
       // callServer();
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="smTest" runat="server" EnablePageMethods="true"></asp:ScriptManager>
            <asp:TextBox ID="txtLocations" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtTest" runat="server"></asp:TextBox>
        </div>
        
    </form>
</body>
</html>
