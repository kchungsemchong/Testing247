<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Testing247Media.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <p>Use this area to provide additional information.</p>

    <asp:Label ID="lblSite" runat="server" Text="Site name"></asp:Label>
    <asp:TextBox ID="txtSite" runat="server" AutoPostBack="true" OnTextChanged="txtBox_TextChanged" Width="243px"></asp:TextBox>
    <asp:Label ID="lblArticle" runat="server" Text="Article "></asp:Label>
    <asp:TextBox ID="txtArticle" runat="server" AutoPostBack="true" OnTextChanged="txtBox_TextChanged" Width="243px"></asp:TextBox>
</asp:Content>