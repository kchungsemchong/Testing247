<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestCalendaraspx.aspx.cs" Inherits="Testing247Media.TestCalendaraspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<h3>Calendar Example</h3>
			Select a date on the Calendar control.<br />
			<br />
			<asp:Calendar ID="calBirthdate" runat="server"
				SelectionMode="Day"
				ShowGridLines="True" OnSelectionChanged ="calBirthdate_SelectionChanged">
				<SelectedDayStyle BackColor="Yellow"
					ForeColor="Red"></SelectedDayStyle>
			</asp:Calendar>
			<hr />
			<br />
			<asp:Label ID="lblBirthdate" runat="server" />
		</div>
		<asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click"/>
	</form>
</body>
</html>
