<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TravelProducts.aspx.cs" Inherits="TravelProducts" %>
<%-- * Author : Sunny Xie
 * Date: July 20, 2015
 * Usage: user purchased packages and products file c#.--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Packages,</title>
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
</head>
<body>
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
        <p>
            &nbsp;</p>
        <asp:Label ID="lblInfo" runat="server" BackColor="#9999FF" Font-Italic="True" Font-Size="X-Small"></asp:Label>
        <br />
        <asp:Button ID="btnHome" runat="server" OnClick="btnHome_Click" Text="Go Home" />
    </form>
</body>
</html>
