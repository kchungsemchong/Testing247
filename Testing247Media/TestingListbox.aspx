<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestingListbox.aspx.cs" Inherits="Testing247Media.TestingListbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<asp:GridView ID="GridView1" runat="server" DataKeyNames="Id" OnRowDataBound="GridView1_RowDataBound"
		 OnRowUpdating="GridView1_RowUpdating" OnRowCommand="GridView1_RowCommand"
		OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowEditing="GridView1_RowEditing">
		<Columns>
			<asp:ButtonField ButtonType="Link" CommandName="Edit" Text="Activated"/>
			<asp:TemplateField HeaderText="Activated">
				<ItemTemplate>
					<asp:CheckBox ID="chkActivated" runat="server" Checked='<%# Bind ("IsActivated") %>'/>
				</ItemTemplate>
			</asp:TemplateField>
			<%--<asp:TemplateField>
				<ItemTemplate>
				   <asp:Button ID="btnCheck" runat="server" Text="Update Status" CommandName="activate" CommandArgument="activate"/>
				</ItemTemplate>
			</asp:TemplateField>--%>
		</Columns>
	</asp:GridView>
	<asp:Button ID="btnSave" runat="server" Text="Save" OnClick ="btnSave_Click"/>
	<asp:Button ID="btnTest" runat="server" Text="Test" OnClick="btnTest_Click"/>
</asp:Content>

