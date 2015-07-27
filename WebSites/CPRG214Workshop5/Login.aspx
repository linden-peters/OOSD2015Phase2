<%--
    Login.aspx - Login Form and Handler
    Author: Geetha, Linden
    Written: 2015/07/20
--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" Runat="Server">
    <ul class="nav navbar-nav">
        <li><a href="Home.aspx">Home</a></li>
    </ul>
    <form id="login" runat="server" defaultbutton="btnLogin" defaultfocus="txtUsername">
        <asp:Label ID="lblError" runat="server" CssClass="validator"></asp:Label>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server" CausesValidation="True" MaxLength="25"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username is required." CssClass="validator" ControlToValidate="txtUsername" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="password" CausesValidation="True" MaxLength="25"></asp:TextBox>  
                    <asp:RequiredFieldValidator ID="ReqPassword" runat="server" ErrorMessage="Password is required" ControlToValidate="txtPassword" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                </td>
                <td>
                    <asp:Button ID="btnClear" runat="server" Text="Reset" CausesValidation="False" OnClientClick="document.forms[0].reset();for(var i=0; i &lt; Page_Validators.length; i++){Page_Validators[i].style.display = &quot;none&quot;;}return false;" UseSubmitBehavior="False" />
                </td>
            </tr>
            <tr aria-invalid="false">
                <td>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Registration.aspx">Register here</asp:HyperLink>  
                </td>                  
            </tr>
        </table>       
    </form>
</asp:Content>

