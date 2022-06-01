<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectHours.aspx.cs" Inherits="TimeCapture.ProjectHours" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main">
        <iframe id="iFrameProjectHours" src="POWERBI_REPORT" height="1000" width="1000" />
    </div>
</asp:Content>
