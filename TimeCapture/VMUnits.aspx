<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VMUnits.aspx.cs" Inherits="TimeCapture.VMUnits" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="navigator">
        <div id="PanelHeader">
                <table align="center" cellpadding="0" cellspacing="2">
                    <tr>
                        <td><asp:TextBox ID="TextBox7" runat="server" Width="80px" Text="Start Date" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:TextBox ID="txtStartDate" runat="server" CssClass="TextBoxClientOverviewRight" AutoPostBack="true" OnTextChanged="txtStartDate_TextChanged" Width="60px" />
                                <AjaxASP:CalendarExtender ID="StartDateCalendar" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy" />
                                <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  /></td>
                        
                        <td>&nbsp;</td>
                        
                        
                        
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="TextBox8" runat="server" Width="80px" Text="End Date" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:TextBox ID="txtEndDate" runat="server" CssClass="TextBoxClientOverviewRight" AutoPostBack="true" OnTextChanged="txtEndDate_TextChanged" Width="60px" />
                                <AjaxASP:CalendarExtender ID="EndDateCalendar" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy" />
                                <AjaxASP:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtEndDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  /></td>
                        
                        <td>&nbsp;</td>
                        
                        <td rowspan="2"><asp:TextBox ID="TextBox11" runat="server" Width="80px" Text="Company" CssClass="TextBoxClientOverviewLeft" /></td>
                        
                        <td rowspan="2"><asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="textboxgrid" AutoPostBack="true" Width="158px" /></td>
                        
                    </tr>

                </table>
            
            
            </div>
    </div>
    <div id="main"> 
        
    </div>

    <div id="footer">
        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/icon-behavior-retry-text.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnRefresh_Click"  />
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" >
            <progresstemplate>
                <div id="UpdateFooterRight" class="footerright">
                    <img src="Images/icon-drawer-processing-active.gif" />
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
