<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PeriodReview.aspx.cs" Inherits="TimeCapture.PeriodReview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="navigator">
        <div id="PanelHeader">
            <table align="center" cellpadding="0" cellspacing="2">
                <tr>
                    <td><asp:TextBox ID="TextBox8" runat="server" Text="Start Month" Width="100px" AutoPostBack="false" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="txtStartDate" runat="server" CssClass="TextBoxClientOverviewRight" AutoPostBack="false" Width="60px" />
                        <AjaxASP:CalendarExtender ID="StartDateCalendar" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy" />
                        <AjaxASP:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="TextBox31" runat="server" Text="Number of Days" Width="100px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="txtNumberDays" runat="server" Text="1" Width="20px" CssClass="TextBoxClientOverviewRight" /></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox14" runat="server" Text="End Month" Width="100px" AutoPostBack="false" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="txtEndDate" runat="server" CssClass="TextBoxClientOverviewRight" AutoPostBack="false" Width="60px"  />
                        <AjaxASP:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy" />
                        <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtEndDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                        </td>
                    <td>&nbsp;&nbsp;&nbsp;</td>
                    <td><asp:TextBox ID="TextBox30" runat="server" Text="Reviewed only" Width="100px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:CheckBox ID="cbManagementReview" runat="server" CssClass="checkboxgrid" /></td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lblPeriod" runat="server" align="center" Font-Names="Segoe UI" Font-Size="16px" Font-Bold="true" ForeColor="#3C454F" />
        </div>
    </div>
    <div id="main">
        <table align="center" cellpadding="0" cellspacing="1" >
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td colspan="11"><asp:TextBox ID="TextBox6" runat="server" Text="BUSINESS COSTS" Width="625px"  CssClass="TextBoxClientOverviewCentre" BackColor="#3C454F"  /></td>
                <td>&nbsp;</td>
                <td colspan="7"><asp:TextBox ID="TextBox7" runat="server" Text="BUSINESS REVENUES" Width="412px" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#3C454F" /></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td colspan="3"><asp:TextBox ID="TextBox15" runat="server" Text="Hourly Costs" Width="200px" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                <td>&nbsp;</td>
                <td colspan="3"><asp:TextBox ID="TextBox17" runat="server" Text="Total Costs" Width="200px" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                <td>&nbsp;</td>
                <td colspan="3"><asp:TextBox ID="TextBox16" runat="server" Text="Total Hours" Width="200px" ReadOnly="true" CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td colspan="3"><asp:TextBox ID="TextBox21" runat="server" Text="Total Invoiced" Width="200px" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                <td>&nbsp;</td>
                <td colspan="3"><asp:TextBox ID="TextBox25" runat="server" Text="Daily Averages" Width="200px" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox9" runat="server" Text="On Site" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td><asp:TextBox ID="TextBox10" runat="server" Text="Off Site" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td><asp:TextBox ID="TextBox55" runat="server" Text="Total" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox11" runat="server" Text="On Site" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td><asp:TextBox ID="TextBox12" runat="server" Text="Off Site" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td><asp:TextBox ID="TextBox13" runat="server" Text="Total" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox18" runat="server" Text="On Site" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td><asp:TextBox ID="TextBox19" runat="server" Text="Off Site" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td><asp:TextBox ID="TextBox20" runat="server" Text="Total" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td><asp:TextBox ID="TextBox22" runat="server" Text="On Site" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td><asp:TextBox ID="TextBox23" runat="server" Text="Off Site" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td><asp:TextBox ID="TextBox24" runat="server" Text="Total" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox26" runat="server" Text="On Site" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td><asp:TextBox ID="TextBox27" runat="server" Text="Off Site" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td><asp:TextBox ID="TextBox28" runat="server" Text="Total" ReadOnly="true" Width="62px"  CssClass="TextBoxClientOverviewCentre"/></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox4" runat="server" Text="Incidents" Width="100px" CssClass="TextBoxClientOverviewLeft"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtHourlyIRCostOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtHourlyIRCostOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtHourlyIRCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalIRCostOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtTotalIRCostOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtTotalIRCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalIRTimeOnsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtTotalIRTimeOffsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtTotalIRTime" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td><asp:TextBox ID="txtTotalIRInvoicedOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtTotalIRInvoicedOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtTotalIRInvoiced" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtDailyAvgIROnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtDailyAvgIROffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtDailyAvgIR" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox49" runat="server" Text="Incidents" Width="100px" CssClass="TextBoxClientOverviewRightLabel"/></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox3" runat="server" Text="Projects" Width="100px"  CssClass="TextBoxClientOverviewLeft"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtHourlyPRCostOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtHourlyPRCostOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtHourlyPRCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalPRCostOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtTotalPRCostOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtTotalPRCost" runat="server" ReadOnly="true" Text="$0.00"  CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalPRTimeOnsite" runat="server" ReadOnly="true" Text="00:00"  CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtTotalPRTimeOffsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtTotalPRTime" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td><asp:TextBox ID="txtTotalPRInvoicedOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalPRInvoicedOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalPRInvoiced" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtDailyAvgPROnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtDailyAvgPROffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtDailyAvgPR" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox50" runat="server" Text="Projects" Width="100px"  CssClass="TextBoxClientOverviewRightLabel"/></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox2" runat="server" Text="Developments" Width="100px" CssClass="TextBoxClientOverviewLeft" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtHourlyDVCostOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtHourlyDVCostOffsite" runat="server" ReadOnly="true" Text="$0.00"  CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtHourlyDVCost" runat="server" ReadOnly="true" Text="$0.00"  CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalDVCostOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalDVCostOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalDVCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalDVTimeOnsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalDVTimeOffsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalDVTime" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td><asp:TextBox ID="TextBox46" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox47" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox48" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox40" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox41" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox42" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox51" runat="server" Text="Developments" Width="100px" CssClass="TextBoxClientOverviewRightLabel" /></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox1" runat="server" Text="Internals" Width="100px" CssClass="TextBoxClientOverviewLeft" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtHourlyINCostOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtHourlyINCostOffsite" runat="server" ReadOnly="true" Text="$0.00"  CssClass="TextBoxClientOverviewRight"/></td>
                <td><asp:TextBox ID="txtHourlyINCost" runat="server" ReadOnly="true" Text="$0.00"  CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalINCostOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalINCostOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalINCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalINTimeOnsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalINTimeOffsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalINTime" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td><asp:TextBox ID="TextBox29" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox32" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox33" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox34" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox35" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox36" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox52" runat="server" Text="Internals" Width="100px" CssClass="TextBoxClientOverviewRightLabel" /></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtRITsLabel" runat="server" Text="RITs" Width="100px" CssClass="TextBoxClientOverviewLeft" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtHourlyRTCostOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtHourlyRTCostOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtHourlyRTCost" runat="server" ReadOnly="true" Text="$0.00"  CssClass="TextBoxClientOverviewRight"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalRTCostOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalRTCostOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalRTCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalRTTimeOnsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalRTTimeOffsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalRTTime" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                <td><asp:TextBox ID="TextBox37" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox38" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox39" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox43" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox44" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="TextBox45" runat="server" ReadOnly="true" Text="$0.00"  CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox53" runat="server" Text="RITs" Width="100px" CssClass="TextBoxClientOverviewRightLabel" /></td>
                
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox5" runat="server" Text="Totals" Width="100px" CssClass="TextBoxClientOverviewLeft" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtHourlyOnsiteCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtHourlyOffsiteCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalOnsiteCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalOffsiteCost" runat="server" ReadOnly="true" Text="$0.00"  CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtTotalTimeOnsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalTimeOffsite" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalTime" runat="server" ReadOnly="true" Text="00:00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;&nbsp;&nbsp;</td>
                
                <td><asp:TextBox ID="txtTotalInvoicedOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalInvoicedOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtTotalInvoiced" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="txtDailyAvgOnsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtDailyAvgOffsite" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td><asp:TextBox ID="txtDailyAvg" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxClientOverviewRight" /></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox54" runat="server" Text="Totals" Width="100px" CssClass="TextBoxClientOverviewRightLabel" /></td>
            </tr>

        </table>
        <br />
        <asp:GridView ID="GridClientTotals" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridClientTotals_RowDataBound" Font-Names="Segoe UI" Font-Size="11px" HorizontalAlign="Center" 
            CellPadding="1" ForeColor="#333333" GridLines="none" HeaderStyle-BorderStyle="none" AllowPaging="true" PageSize="20" OnPageIndexChanging="GridClientTotals_PageIndexChanging" EmptyDataText="No entries for the selected period!">
            <Columns>
                <asp:TemplateField HeaderText="Company Name" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE" >
                    <itemtemplate>
                        <asp:Button ID="btnCompanyName" runat="server" Text='<%#Bind("CompanyName") %>'  Width="200px" ReadOnly="true" CssClass="CompanyName" OnClick="btnCompanyName_Click" CausesValidation="false" />
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI"  />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cost" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblProjectCost" runat="server" Text='<%#Bind("ProjectCost") %>'  Width="60px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Right" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblProjectInvoiced" runat="server" Text='<%#Bind("ProjectInvoiced") %>'  Width="60px" ReadOnly="true" CssClass="LabelClientsGrid" />
                    </itemtemplate>
                    <itemstyle horizontalalign="Right" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Margin" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblProjectMargin" runat="server" Text='<%#Bind("ProjectMargin") %>' Width="60px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Right" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="%" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblProjectMarginPercent" runat="server" Text='<%#Bind("ProjectMarginPercent") %>'  Width="60px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Right" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cost" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblMiscCost" runat="server" Text='<%#Bind("MiscCost") %>'   Width="60px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Right" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblMiscInvoiced" runat="server" Text='<%#Bind("MiscInvoiced") %>'  Width="60px" ReadOnly="true" CssClass="LabelClientsGrid" />
                    </itemtemplate>
                    <itemstyle horizontalalign="Right" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Margin" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblMiscMargin" runat="server" Text='<%#Bind("MiscMargin") %>'  Width="60px" ReadOnly="true" CssClass="LabelClientsGrid" />
                    </itemtemplate>
                    <itemstyle horizontalalign="Right" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="%" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblMiscMarginPercent" runat="server" Text='<%#Bind("MiscMarginPercent") %>'  Width="60px" ReadOnly="true" CssClass="LabelClientsGrid" />
                    </itemtemplate>
                    <itemstyle horizontalalign="Right" Font-Names="Segoe UI" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" >
                <progresstemplate>
                    <div id="ProgressBackground" />
                </progresstemplate>
        </asp:UpdateProgress>
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
