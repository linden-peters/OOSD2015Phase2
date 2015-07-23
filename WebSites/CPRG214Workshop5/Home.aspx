<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="Styles/Home.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" Runat="Server">
    <div style="float:left">
        <ul class="nav navbar-nav">
            <li><a href="Home.aspx">Home</a></li>
            <%
                if (Session["CustomerId"] != null)
                {
                    Response.Write(@"
                        <li><a href='Registration.aspx'>Edit/Update Account</a></li>
                        <li><a href='TravelProducts.aspx'>View Package/Booking Details</a></li>
                        <li><a href='Logout.aspx'>Logout</a></li>
                    ");
                }
                else
                {
                    Response.Write(@"<li><a href='Login.aspx'>Login</a></li>");
                }
            %>
        </ul>
        <img src="Images/Travel.jpg" width="512" />
    </div>
</asp:Content>

