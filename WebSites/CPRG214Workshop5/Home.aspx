﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="Styles/Home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" Runat="Server">
    <%
        if(Session["CustomerId"] != null) 
        {
            Response.Write(@"<h5 align='right'>Hello " + Session["CustName"] + "</h5>");
        }
    %>
    <div style="float:left">
        <ul class="nav navbar-nav">
            <li><a href="Home.aspx">Home</a></li>
            <%
                if (Session["CustomerId"] != null)
                {
                    Response.Write(@"
                        <li><a href='Registration.aspx'>Edit/Update Account</a></li>
                        <li><a href='Package.aspx'>View Package/Booking Details</a></li>
                        <li><a href='Logout.aspx'>Logout</a></li>
                    ");
                }
                else
                {
                    Response.Write(@"<li><a href='Login.aspx'>Login</a></li>");
                }
            %>
        </ul>
    </div>
</asp:Content>

