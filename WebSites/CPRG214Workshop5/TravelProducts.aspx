<%--
 * Author : Sunny Xie
 * Date: July 20, 2015
 * Usage: user purchased packages and products file c#.
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TravelProducts.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="Styles/travelProducts.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
            color: #FF6666;
        }
        .auto-style2 {
            color: #FF6666;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" Runat="Server">
    <form id="form1" runat="server">
    <div>
    
        <br class="auto-style2" />
        <span class="auto-style1">You purchased Packages and Products. 
        </span>
        <br />
        <br />
        <asp:TreeView ID="tvProducts" runat="server" ExpandDepth="1" OnPreRender="tvProducts_PreRender">
        </asp:TreeView>
    
    </div>
        <br />
        <asp:Label ID="lblInfo" runat="server" BackColor="#9999FF" Font-Italic="True" Font-Size="Medium"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnHome" runat="server" OnClick="btnHome_Click" Text="Go Home" />
    </form>
</asp:Content>
