<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Confirmation.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlaceHolder" Runat="Server">
    <form id="form1" runat="server">

        <asp:Label ID="lblError" runat="server" CssClass="validator"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Thank you for registering with Travel Experts."></asp:Label>
    <br />
        Your customer Id is <asp:Label ID="lblCustomerId" runat="server"></asp:Label>
    <br />
    Please find your below details.
    <table id="Table1" runat="server">
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Username: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblUsername" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="FirstName: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFirstName" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="LastName: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLastName" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Address"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAddress" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="City: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCity" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Province: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblProv" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Postal: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblPostal" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Country: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCountry" runat="server" ></asp:Label>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Home Phone: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblHomePhone" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Business Phone: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBusPhone" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Email: "></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEmail" runat="server" ></asp:Label>
            </td>
        </tr>
    </table>
        <br />
    <a href="Home.aspx">Back to Home</a>
    </form>
</asp:Content>

