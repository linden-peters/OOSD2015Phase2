<%--
    Logout.aspx - Logout Page, automatically destroys session
    Author: Linden
    Written: 2015/07/20
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="You have successfully logged out."></asp:Label>
    <br />
    <a href="Home.aspx">Back to Home</a>
</asp:Content>

