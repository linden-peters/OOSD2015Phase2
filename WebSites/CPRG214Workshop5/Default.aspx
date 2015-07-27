<%--
    Default.aspx - Project Splash Page
    Author: Linden
    Written: 2015/07/20
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" Runat="Server">
    <div>
        <h1>Team 6 Product Demo</h1>
        <ul class="nav navbar-nav">
            <li><a href="Setup.aspx">Init DB</a><ul><li>Use only if DB does not already exist.</li><li>Creates default Travel Experts DB plus Team 6 modifications.</li></ul></li>
            <li><a href="Home.aspx">Start Demo</a><ul><li>Username: m</li><li>Password: m</li></ul></li>
        </ul>
    </div>
</asp:Content>
