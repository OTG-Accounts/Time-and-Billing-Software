<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Licenses.aspx.cs" Inherits="TimeCapture.Licenses" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="navigator">
        <asp:Label runat="server" ID="lblDate" Text="Date"/><asp:TextBox runat="server" ID="txtDate" AutoPostBack="true" CausesValidation="false" OnTextChanged="txtDate_TextChanged" /><br />
        
    <asp:Label runat="server" ID="lblMailboxes" Text="Exchange CAL (Mailboxes)"/><asp:TextBox runat="server" ID="txtMailboxes" /><br />
    <asp:Label runat="server" ID="lblOffice365" Text="Office 365 CAL"/><asp:TextBox runat="server" ID="txtOffice365" /><br />
    <asp:Label runat="server" ID="lblWorkspace" Text="Remote Desktop CAL"/><asp:TextBox runat="server" ID="txtWorkspace" /><br />
    <asp:Label runat="server" ID="lblWindowsCAL" Text="Windows CAL"/><asp:TextBox runat="server" ID="txtWindowsCAL" /><br />
    <asp:Label runat="server" ID="lblMsOffice" Text="Office CAL"/><asp:TextBox runat="server" ID="txtMsOffice" /><br />
    <asp:Label runat="server" ID="lblProject" Text="Project CAL"/><asp:TextBox runat="server" ID="txtMsProject" /><br />
    <asp:Label runat="server" ID="lblVisio" Text="Visio CAL"/><asp:TextBox runat="server" ID="txtMsVisio" /><br />
    <asp:Label runat="server" ID="lblCRM" Text="CRM CAL"/><asp:TextBox runat="server" ID="txtMsCRM" /><br />
    <asp:Label runat="server" ID="lblSharepoint" Text="Sharepoint CAL"/><asp:TextBox runat="server" ID="txtMsSharepoint" /><br />
    <asp:Label runat="server" ID="lblThirdParty" Text="Third Party"/><asp:TextBox runat="server" ID="txtThirdParty" /><br />
    <asp:Label runat="server" ID="lblMEHS" Text="Exchange Filtering CAL"/><asp:TextBox runat="server" ID="txtMEHS" /><br />
    <asp:Label runat="server" ID="lblInTune" Text="InTune CAL"/><asp:TextBox runat="server" ID="txtInTune" /><br />
    <asp:Label runat="server" ID="lblNFB" Text="Not For Billing"/><asp:TextBox runat="server" ID="txtNFB" /><br />
        

        </div>

</asp:Content>
