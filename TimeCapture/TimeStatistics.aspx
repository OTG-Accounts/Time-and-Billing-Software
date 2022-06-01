<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimeStatistics.aspx.cs" Inherits="TimeCapture.TimeStatistics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>
<%@ Register Assembly="BulkEditGridView" Namespace="BulkEditGridView" TagPrefix="MyAsp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="navigator">
        <table align="center" cellpadding="0" cellspacing="2">
            <tr>
                <td><asp:TextBox ID="TextBox1" runat="server" Width="100px" Text="Statistics Type" CssClass="TextBoxClientOverviewCentre" /></td>
                <td>
                    <asp:DropDownList ID="ddlStatSelection" runat="server" CssClass="textboxgrid" AutoPostBack="true" Width="130px" OnSelectedIndexChanged="ddlStatSelection_SelectedIndexChanged">
                        <asp:ListItem Text="Overview" Value="Overview" />
                        <asp:ListItem Text="Logged Hours" Value="LoggedHours" />
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlLoggedHoursHeader" runat="server" CssClass="TimeStatPanelHeader" Visible="false">
            <table align="center" cellpadding="0" cellspacing="2">
                <tr>
                    <td><asp:TextBox ID="TextBox7" runat="server" Width="80px" Text="Start Date" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="txtStartDatePerPerson" runat="server" CssClass="TextBoxClientOverviewRight" AutoPostBack="true" OnTextChanged="txtStartDatePerPerson_TextChanged" Width="60px" />
                            <AjaxASP:CalendarExtender ID="StartDateCalendar" runat="server" TargetControlID="txtStartDatePerPerson" Format="dd/MM/yyyy" />
                            <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDatePerPerson" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                    </td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox8" runat="server" Width="80px" Text="End Date" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="txtEndDatePerPerson" runat="server" CssClass="TextBoxClientOverviewRight" AutoPostBack="true" OnTextChanged="txtEndDatePerPerson_TextChanged" Width="60px" />
                            <AjaxASP:CalendarExtender ID="EndDateCalendar" runat="server" TargetControlID="txtEndDatePerPerson" Format="dd/MM/yyyy" />
                            <AjaxASP:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtEndDatePerPerson" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlOverviewHeader" runat="server" CssClass="TimeStatPanelHeader" Visible="true" >
            <table align="center" cellpadding="0" cellspacing="2">
                <tr>
                    <td colspan="2"><asp:TextBox ID="TextBox122" runat="server" Text="Employee Information" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox123" runat="server" Text="Period" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="4"><asp:TextBox ID="TextBox126" runat="server" Text="Client Rates" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox127" runat="server" Text="Internal Rates" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox128" runat="server" Text="Adjustments" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox129" runat="server" Text="Yearly Revenues" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox130" runat="server" Text="Yearly Costs" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox2" runat="server" Width="100px" Text="Name" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:DropDownList ID="ddlEmployee" runat="server"  CssClass="textboxgrid" Width="120px" /></td>
                    <td><asp:TextBox ID="TextBox3" runat="server" Width="80px" Text="Start Date" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="txtStartDateOverview" runat="server" CssClass="TextBoxClientOverviewRight" AutoPostBack="true" OnTextChanged="txtStartDatePerPerson_TextChanged" Width="60px" />
                            <AjaxASP:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDateOverview" Format="dd/MM/yyyy" />
                            <AjaxASP:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartDateOverview" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  /></td>

                    <td><asp:TextBox ID="TextBox6" runat="server" Width="90px" Text="Incident Onsite" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="tbIncidentCltOnsiteRate" runat="server" CssClass="textboxgrid" Width="30px" Text="150" /></td>
                    <td><asp:TextBox ID="TextBox131" runat="server" Width="90px" Text="Project Onsite" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="tbProjectCltOnsiteRate" runat="server" CssClass="textboxgrid" Width="30px" Text="150" /></td>

                    <td><asp:TextBox ID="TextBox135" runat="server" Width="90px" Text="Incident" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="tbIncidentIntRate" runat="server" CssClass="textboxgrid" Width="30px" Text="75" /></td>
                    
                    <td><asp:TextBox ID="TextBox10" runat="server" Width="80px" Text="Inc. Expenses" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:CheckBox ID="chkInlcudeExpenses" runat="server" CssClass="checkboxgrid" width="15px"/></td>
                    
                    <td><asp:TextBox ID="TextBox13" runat="server" Width="80px" Text="RITs" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="tbRITSYear" runat="server" CssClass="textboxgrid" Width="50px" Text="290000" /></td>
                    <td><asp:TextBox ID="TextBox12" runat="server" Width="80px" Text="Company" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="tbExpensesYear" runat="server" CssClass="textboxgrid" Width="50px" text="960000"/></td>
                    
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox14" runat="server" Width="100px" Text="Total Number" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="tbNumberOfEmployees" runat="server" CssClass="textboxgrid" Width="113px" Text="14" /></td>
                    <td><asp:TextBox ID="TextBox5" runat="server" Width="80px" Text="End Date" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="txtEndDateOverview" runat="server" CssClass="TextBoxClientOverviewRight" AutoPostBack="true" OnTextChanged="txtStartDatePerPerson_TextChanged" Width="60px" />
                            <AjaxASP:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDateOverview" Format="dd/MM/yyyy" />
                            <AjaxASP:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtEndDateOverview" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  /></td>
                    
                    <td><asp:TextBox ID="TextBox9" runat="server" Width="90px" Text="Incident Offsite" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="tbIncidentCltOffsiteRate" runat="server" CssClass="textboxgrid" Width="30px" text="100"/></td>
                    <td><asp:TextBox ID="TextBox133" runat="server" Width="90px" Text="Project Offsite" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="tbProjectCltOffsiteRate" runat="server" CssClass="textboxgrid" Width="30px" Text="100" /></td>

                    <td><asp:TextBox ID="TextBox141" runat="server" Width="90px" Text="Project" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="tbProjectIntRate" runat="server" CssClass="textboxgrid" Width="30px" Text="75" /></td>


                    <td><asp:TextBox ID="TextBox11" runat="server" Width="80px" Text="Adj. Internal" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:CheckBox ID="chkAdjust" runat="server" CssClass="checkboxgrid"  width="15px"/></td>

                    <td><asp:TextBox ID="TextBox120" runat="server" Width="80px" Text="SAMs" CssClass="TextBoxClientOverviewLeft" /></td>
                    <td><asp:TextBox ID="tbSAMsYear" runat="server" CssClass="textboxgrid" Width="50px" Text="0" /></td>
                    
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </asp:Panel>
        
    </div>
    <div id="main">
        
        <asp:Panel ID="pnlLoggedHoursBody" runat="server" CssClass="TimeStatPanelBody" Visible="false">
            
            <table align="center" cellpadding="0" cellspacing="2">
                <tr>
                    <td>
                        <MyAsp:BulkEditGridView ID="GridWorkedHours" runat="server" AutoGenerateColumns="False" BulkEdit="False" CellPadding="4" ForeColor="#333333" GridLines="None"  
                            AllowSorting="False" OnRowDataBound="GridWorkedHours_RowDataBound" EmptyDataText="No entries returned" HeaderStyle-BackColor="#6B7B8D" HeaderStyle-ForeColor="#eeeeee" BorderStyle="Solid" BorderWidth="1px" BorderColor="#dddddd" >
                <EmptyDataTemplate>No data available</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="170px" ItemStyle-Width="170px">
                            <itemtemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%#Bind("Name") %>' Font-Size="14px" Font-Names="Segoe UI"/>
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hours Worked" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <itemtemplate>
                                <asp:TextBox ID="txtHoursWorked" runat="server" Text='<%#Bind("HoursWorked") %>' CssClass="TextBoxStatPageCentre" />
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Days Required" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <itemtemplate>
                                <asp:TextBox ID="txtDaysRequired" runat="server" Text='<%#Bind("DaysRequired") %>' CssClass="TextBoxStatPageCentre" />
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Days Worked" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <itemtemplate>
                                <asp:TextBox ID="txtDaysWorked" runat="server" Text='<%#Bind("DaysWorked") %>' CssClass="TextBoxStatPageCentre" />
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Average per Day" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <itemtemplate>
                                <asp:TextBox ID="txtDailyAverage" runat="server" Text='<%#Bind("DailyAverage") %>' CssClass="TextBoxStatPageCentre" />
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Peer Entries" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <itemtemplate>
                                <asp:TextBox ID="txtOutstandingPeerEntries" runat="server" Text='<%#Bind("OutstandingPeerEntries") %>' CssClass="TextBoxStatPageCentre" />
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Weekend Hours" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <itemtemplate>
                                <asp:TextBox ID="txtHoursWorkedWeekend" runat="server" Text='<%#Bind("HoursWorkedWeekend") %>' CssClass="TextBoxStatPageCentre" />
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Hours" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <itemtemplate>
                                <asp:TextBox ID="txtTotalHoursWorked" runat="server" Text='<%#Bind("TotalHoursWorked") %>' CssClass="TextBoxStatPageCentre" />
                            </itemtemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="White" BorderStyle="solid" BorderWidth="1px" BorderColor="#cccccc"/>
                    <HeaderStyle BorderStyle="none" BackColor="#6B7B8D" ForeColor="#EEEEEE"  />
            </MyAsp:BulkEditGridView>
                    </td>
                </tr>
            </table>
            
        </asp:Panel>
        <asp:Panel ID="pnlOverviewBody" runat="server" CssClass="TimeStatPanelBody" Visible="true">
            <br />
            <br />
            <table align="center" cellpadding="0" cellspacing="1" width="968px">
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="13"><asp:TextBox ID="TextBox15" runat="server" Text="COSTS vs PROFITS" Width="100%"  CssClass="TextBoxClientOverviewCentre" BackColor="#3C454F"  /></td>
                    <td><asp:TextBox ID="TextBox138" runat="server" Text="" Width="50px" backcolor="White" ReadOnly="true" BorderColor="White" BorderStyle="None" BorderWidth="0px"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox4" runat="server" Text="COMPANY COSTS" Width="100%"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"  /></td>
                </tr>
                <tr>
                    

                    <td>&nbsp;</td>
                    <td colspan="4"><asp:TextBox ID="TextBox16" runat="server" Text="Hours" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox17" runat="server" Text="Costs" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="4"><asp:TextBox ID="TextBox18" runat="server" Text="Billed" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="3"><asp:TextBox ID="TextBox19" runat="server" Text="Profits" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox139" runat="server" Text="Monthly" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbExpensesMonth" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox20" runat="server" Text="Logged" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox21" runat="server" Text="%" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox22" runat="server" Text="Target %" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox23" runat="server" Text="Daily" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox24" runat="server" Text="Total" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox25" runat="server" Text="Hourly" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox26" runat="server" Text="Hours" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox27" runat="server" Text="%" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox28" runat="server" Text="Actual" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox29" runat="server" Text="Potential" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox30" runat="server" Text="%" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox31" runat="server" Text="Actual" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox32" runat="server" Text="Potential" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox134" runat="server" Text="Daily" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbExpensesDay" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td><td>&nbsp;</td>
                    
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox33" runat="server" Text="Incident (Clt)" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIncidentCltHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentCltPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats" /></td>
                    <td><asp:TextBox ID="tbIncidentCltTarget" runat="server"  ReadOnly="true" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIncidentCltHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats" /></td>
                    <td><asp:TextBox ID="tbIncidentCltTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats" /></td>
                    <td><asp:TextBox ID="tbIncidentCltHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats" /></td>
                    <td><asp:TextBox ID="tbIncidentCltHrsBilled" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats" /></td>
                    <td><asp:TextBox ID="tbIncidentCltPctBilled" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats" /></td>
                    <td><asp:TextBox ID="tbIncidentCltActualBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats" /></td>
                    <td><asp:TextBox ID="tbIncidentCltPotentialBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats" /></td>
                    <td><asp:TextBox ID="tbIncidentCltPctRevenue" runat="server" Text="100" CssClass="TextBoxStatsYellow" /></td>
                    <td><asp:TextBox ID="tbIncidentCltActualRevenue" runat="server" Text="$0.00" ReadOnly="true" CssClass="TextBoxStats" /></td>
                    <td><asp:TextBox ID="tbIncidentCltPotentialRevenue" runat="server" Text="$0.00" ReadOnly="true" CssClass="TextBoxStats" /></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox137" runat="server" Text="Hourly" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbExpensesHour" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox34" runat="server" Text="Project (Clt)" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbProjectCltHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectCltPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectCltTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbProjectCltHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectCltTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectCltHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectCltHrsBilled" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectCltPctBilled" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectCltActualBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectCltPotentialBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectCltPctRevenue" runat="server" Text="100" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbProjectCltActualRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectCltPotentialRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox132" runat="server" Text="Employee per year" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbExpensesEmployeePerYear" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    
                </tr>
                <tr>
                    
                    <td><asp:TextBox ID="TextBox35" runat="server" Text="Incident (Int)" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIncidentIntHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentIntPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentIntTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIncidentIntHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentIntTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentIntHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentIntHrsBilled" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentIntPctBilled" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentIntActualBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentIntPotentialBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentIntPctRevenue" runat="server" Text="100" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIncidentIntActualRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentIntPotentialRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox136" runat="server" Text="Employee per mth" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbExpensesEmployeePerMonth" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    
                </tr>
                <tr>
                    
                    <td><asp:TextBox ID="TextBox36" runat="server" Text="Project (Int)" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbProjectIntHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectIntPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectIntTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbProjectIntHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectIntTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectIntHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectIntHrsBilled" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectIntPctBilled" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectIntActualBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectIntPotentialBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectIntPctRevenue" runat="server" Text="100" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbProjectIntActualRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbProjectIntPotentialRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox145" runat="server" Text="Employee per day" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbExpensesEmployeePerDay" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    
                </tr>
                <tr>
                    
                    <td><asp:TextBox ID="TextBox37" runat="server" Text="Internal" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbInternalHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbInternalPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbInternalTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbInternalHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbInternalTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbInternalHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbInternalHrsBilled" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbInternalPctBilled" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbInternalActualBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbInternalPotentialBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbInternalPctRevenue" runat="server" Text="100" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbInternalActualRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbInternalPotentialRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox146" runat="server" Text="Employee per hour" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbExpensesEmployeePerHour" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    
                </tr>
                <tr>
                    
                    <td><asp:TextBox ID="TextBox38" runat="server" Text="Development" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbDevelopHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDevelopPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDevelopTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbDevelopHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDevelopTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDevelopHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDevelopHrsBilled" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDevelopPctBilled" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDevelopActualBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDevelopPotentialBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDevelopPctRevenue" runat="server" Text="100" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbDevelopActualRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDevelopPotentialRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td colspan="2"><asp:TextBox ID="TextBox147" runat="server" Text="PERIOD INFORMATION" Width="100%"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"  /></td>
                    
                </tr>
                <tr>
                    
                    <td><asp:TextBox ID="TextBox39" runat="server" Text="RITs" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITSHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITSPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsTarget" runat="server" ReadOnly="true" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITSHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITSTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITSHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITSHrsBilled" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITSPctBilled" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITSActualBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITSPotentialBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITSPctRevenue" runat="server" Text="100" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITSActualRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITSPotentialRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox121" runat="server" Text="Selected Days" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbTotalDays" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    
                </tr>
                <tr>
                    
                    <td><asp:TextBox ID="TextBox119" runat="server" Text="SAMs" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbSAMsHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsTarget" runat="server" ReadOnly="true" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbSAMsHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsHrsBilled" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsPctBilled" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsActualBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsPotentialBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsPctRevenue" runat="server" Text="100" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbSAMsActualRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsPotentialRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox140" runat="server" Text="Working Days" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbTotalWorkingDays" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    
                </tr>
                <tr>
                    
                    <td colspan="14">
                        <hr />
                    </td>
                    <td>&nbsp;</td>
                    <td colspan="2"><asp:TextBox ID="TextBox148" runat="server" Text="RITs REVENUES" Width="100%"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"  /></td>
                    
                </tr>
                <tr>
                    
                    <td><asp:TextBox ID="TextBox40" runat="server" Text="Totals" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbTotalHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbTotalPctLog" runat="server" ReadOnly="true" Text="100%" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="tbTotalHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbTotalHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbTotalHrsBilled" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbTotalPctBilled" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbTotalActualBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbTotalPotentialBilled" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="tbTotalActualRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbTotalPotentialRevenue" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox149" runat="server" Text="Hourly" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITSHour" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    
                </tr>
            </table>
            <br />
            <br />
           
            <br />
            
            <!-- Breakdown Tables-->
                <table align="center" cellpadding="0" cellspacing="1" width="572">
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="7"><asp:TextBox ID="TextBox41" runat="server" Text="INTERNAL ENTRIES BREAKDOWN" Width="100%"  CssClass="TextBoxClientOverviewCentre" BackColor="#3C454F"  /></td>
                    <td><asp:TextBox ID="TextBox118" runat="server" Text="" Width="50px" backcolor="White" ReadOnly="true" BorderColor="White" BorderStyle="None" BorderWidth="0px"/></td>
                    <td>&nbsp;</td>
                    <td colspan="7"><asp:TextBox ID="TextBox85" runat="server" Text="INCIDENT ENTRIES BREAKDOWN" Width="100%"  CssClass="TextBoxClientOverviewCentre" BackColor="#3C454F"  /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="5"><asp:TextBox ID="TextBox42" runat="server" Text="Hours" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox43" runat="server" Text="Costs" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="5"><asp:TextBox ID="TextBox86" runat="server" Text="Hours" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox87" runat="server" Text="Costs" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox44" runat="server" Text="Logged" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox45" runat="server" Text="%" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox65" runat="server" Text="% Total" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox46" runat="server" Text="Target %" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox47" runat="server" Text="Daily" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox48" runat="server" Text="Total" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox49" runat="server" Text="Hourly" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox88" runat="server" Text="Logged" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox89" runat="server" Text="%" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox91" runat="server" Text="% Total" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox92" runat="server" Text="Target %" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox93" runat="server" Text="Daily" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox94" runat="server" Text="Total" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox95" runat="server" Text="Hourly" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox50" runat="server" Text="Accounting" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntAccHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntAccPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntAccPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbACTTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntAccHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntAccTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntAccHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox96" runat="server" Text="SC" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbSCHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSCPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSCPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSCTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbSCHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSCTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSCHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox51" runat="server" Text="Administration" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntAdmHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntAdmPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntAdmPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbADMTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntAdmHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntAdmTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntAdmHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox97" runat="server" Text="ST" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbSTHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSTPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSTPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSTTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbSTHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSTTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSTHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox52" runat="server" Text="Business" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntBusHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntBusPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntBusPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbBUSTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntBusHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntBusTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntBusHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox98" runat="server" Text="CC" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbCCHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCCPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCCPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCCTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbCCHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCCTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCCHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox53" runat="server" Text="Comms & Sched" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntCmsHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntCmsPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntCmsPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCOMTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntCmsHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntCmsTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntCmsHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox99" runat="server" Text="CT" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbCTHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCTPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCTPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCTTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbCTHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCTTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCTHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox54" runat="server" Text="Customer Relations" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntCRHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntCRPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntCRPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbCRMTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntCRHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntCRTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntCRHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox100" runat="server" Text="UC" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbUCHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbUCPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbUCPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbUCTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbUCHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbUCTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbUCHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox55" runat="server" Text="Human Resources" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntHRHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntHRPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntHRPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbHRTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntHRHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntHRTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntHRHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox101" runat="server" Text="UT" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbUTHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbUTPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbUTPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbUTTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbUTHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbUTTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbUTHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox56" runat="server" Text="Management" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntMgmtHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntMgmtPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntMgmtPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbMGTTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntMgmtHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntMgmtTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntMgmtHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox102" runat="server" Text="OC" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbOCHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOCPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOCPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOCTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbOCHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOCTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOCHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox57" runat="server" Text="Operations" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntOpsHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntOpsPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntOpsPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOPSTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntOpsHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntOpsTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntOpsHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox103" runat="server" Text="OT" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbOTHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOTPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOTPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOTTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbOTHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOTTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOTHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox58" runat="server" Text="Personal" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntPerHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntPerPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntPerPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbPERTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntPerHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntPerTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntPerHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox104" runat="server" Text="Service Reuqest" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbSRHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSRPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSRPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbServiceRequestTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbSRHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSRTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSRHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox59" runat="server" Text="Projects" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntPrjHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntPrjPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntPrjPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbPRJTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntPrjHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntPrjTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntPrjHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td colspan="8">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox60" runat="server" Text="Ref. & Learning" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntRLHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntRLPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntRLPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRLTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntRLHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntRLTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntRLHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox105" runat="server" Text="Totals" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIncidentTotalHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentTotalPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentTotalPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="tbIncidentTotalHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIncidentTotalHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox61" runat="server" Text="Sales & Marketing" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntSMHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntSMPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntSMPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSMTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntSMHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntSMTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntSMHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox62" runat="server" Text="Timesheets" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntTmsHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntTmsPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntTmsPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbTMSTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbIntTmsHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntTmsTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntTmsHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="7"><asp:TextBox ID="TextBox66" runat="server" Text="RITs ENTRIES BREAKDOWN" Width="100%"  CssClass="TextBoxClientOverviewCentre" BackColor="#3C454F"  /></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox63" runat="server" Text="Unknown" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntUnkHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntUnkPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntUnkPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="tbIntUnkHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntUnkTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntUnkHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td colspan="5"><asp:TextBox ID="TextBox67" runat="server" Text="Hours" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox68" runat="server" Text="Costs" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                </tr>
                <tr>
                    <td colspan="8">
                        <hr />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox69" runat="server" Text="Logged" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox70" runat="server" Text="%" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox71" runat="server" Text="% Total" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox72" runat="server" Text="Target %" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox73" runat="server" Text="Daily" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox74" runat="server" Text="Total" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox75" runat="server" Text="Hourly" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox64" runat="server" Text="Totals" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbIntTotalHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntTotalPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntTotalPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="tbIntTotalHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbIntTotalHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox76" runat="server" Text="Week 1" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITsWK1HrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK1PctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK1PctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbWK1Target" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITsWK1HrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK1TotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK1HourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox77" runat="server" Text="Week 2" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITsWK2HrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK2PctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK2PctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbWK2Target" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITsWK2HrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK2TotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK2HourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox78" runat="server" Text="Week 3" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITsWK3HrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK3PctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK3PctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbWK3Target" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITsWK3HrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK3TotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK3HourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="7"><asp:TextBox ID="TextBox106" runat="server" Text="SAMs ENTRIES BREAKDOWN" Width="100%"  CssClass="TextBoxClientOverviewCentre" BackColor="#3C454F"  /></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox79" runat="server" Text="Week 4" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITsWK4HrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK4PctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK4PctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbWK4Target" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITsWK4HrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK4TotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsWK4HourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="5"><asp:TextBox ID="TextBox107" runat="server" Text="Hours" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td colspan="2"><asp:TextBox ID="TextBox108" runat="server" Text="Costs" Width="100%" ReadOnly="true"  CssClass="TextBoxClientOverviewCentre" BackColor="#505B69"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox80" runat="server" Text="Daily (Backup)" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITsDayHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsDayPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsDayPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbDAYTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITsDayHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsDayTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsDayHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox109" runat="server" Text="Logged" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox110" runat="server" Text="%" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox111" runat="server" Text="% Total" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox112" runat="server" Text="Target %" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox113" runat="server" Text="Daily" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox114" runat="server" Text="Total" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td><asp:TextBox ID="TextBox115" runat="server" Text="Hourly" ReadOnly="true" Width="100%"  CssClass="TextBoxClientOverviewCentre"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox81" runat="server" Text="Test Restores" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITsTRSHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsTRSPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsTRSPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbTRSTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITsTRSHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsTRSTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsTRSHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox116" runat="server" Text="SCOM Monitoring" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMonHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMonPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMonPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSCOMMonTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMonHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMonTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMonHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox82" runat="server" Text="System Center" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITsSCMHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsSCMPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsSCMPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSCMTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITsSCMHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsSCMTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsSCMHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox117" runat="server" Text="SCOM Maintenance" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMMHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMMPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMMPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSCOMMMTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMMHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMMTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsSCOMMMHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox83" runat="server" Text="Config Manager" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITsSCCMHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsSCCMPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsSCCMPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsSCCMTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITsSCCMHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsSCCMTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsSCCMHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox124" runat="server" Text="Other" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbSAMsOthHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsOthPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsOthPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsOthTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbSAMsOthHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsOthTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsOthHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox84" runat="server" Text="Other" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITsOthHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsOthPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsOthPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbOTHTarget" runat="server" Text="" CssClass="TextBoxStatsYellow"/></td>
                    <td><asp:TextBox ID="tbRITsOthHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsOthTotalCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsOthHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                <tr>
                    <td colspan="8">
                        <hr />
                    </td>
                    <td>&nbsp;</td>
                    <td colspan="8">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="TextBox125" runat="server" Text="Totals" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbSAMsTotalHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsTotalPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsTotalPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="tbSAMsTotalHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsTotalCostDet" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbSAMsTotalHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="TextBox90" runat="server" Text="Totals" Width="110px" CssClass="TextBoxClientOverviewLeft"/></td>
                    <td><asp:TextBox ID="tbRITsTotalHrsLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsTotalPctLog" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsTotalPctLogTot" runat="server" ReadOnly="true" Text="0.00%" CssClass="TextBoxStats"/></td>
                    <td>&nbsp;</td>
                    <td><asp:TextBox ID="tbRITsTotalHrsDailyLog" runat="server" ReadOnly="true" Text="0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsTotalCostDet" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                    <td><asp:TextBox ID="tbRITsTotalHourlyCost" runat="server" ReadOnly="true" Text="$0.00" CssClass="TextBoxStats"/></td>
                </tr>
                </table>

                <br />
                <br />

            <table align="center" cellpadding="0" cellspacing="1" width="1000px">
                <tr>
                    <td>
                        <asp:Chart ID="myChart" runat="server" Height="700px" Width="1000px">
                            <ChartAreas>
				                <asp:ChartArea Name="ChartArea1" />
			                </ChartAreas>
                        </asp:Chart>
                    </td>
                </tr>
            </table>
                    

                   
                
        </asp:Panel>

 

    </div>
    <div id="footer">
        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/icon-behavior-save-text.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnSave_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/icon-behavior-retry-text.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnRefresh_Click" />
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" >
            <progresstemplate>
                <div id="UpdateFooterRight" class="footerright">
                    <img src="Images/icon-drawer-processing-active.gif" />
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
