<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Setup.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" Runat="Server">
    <a name="head" />
    <h1>DB Initializer</h1>
    <ul class="nav navbar-nav">
        <li><a href="Default.aspx">Back To Splash</a></li>
        <li><a href="Home.aspx">Start Demo</a></li>
        <li><a href="#tail">To Tail</a></li>
    </ul>
    <asp:Label ID="lblOutput" runat="server"></asp:Label>
    <a href="#head">To Head</a>
    <a name="tail" />
</asp:Content>

