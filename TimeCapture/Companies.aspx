<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Companies.aspx.cs" Inherits="TimeCapture.Companies" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>
<%@ Register Assembly="BulkEditGridView" Namespace="BulkEditGridView" TagPrefix="MyAsp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="navigator">

    </div>
    <div id="main">
        <table cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td style="background-color:#505B69;color:#EEEEEE;font-family:'Segoe UI';font-size:16px;text-align:center;font-weight:bold;min-width:200px">Company List</td>   
                <td style="background-color:#505B69;width:32px;text-align:center;vertical-align:middle;">
                    <asp:Panel runat="server" ID="pnlAddCompanyHeader" Height="32px">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/AddUser.png" Height="32px" />
                    </asp:Panel>
                    <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" CollapsedImage="~/Images/AddUser.png" 
                            ExpandedImage="~/Images/AddUser.png" ImageControlID="Image2" TargetControlID="pnlAddCompanyBody" Collapsed="true" 
                            CollapseControlID="pnlAddCompanyHeader" ExpandControlID="pnlAddCompanyHeader" >
                    </AjaxASP:CollapsiblePanelExtender>
                </td>
                
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlAddCompanyBody" runat="server">
                        <table>
                            <tr>
                                <td style="width:100px;"><asp:TextBox ID="TextBox16" runat="server" Width="100px" Text="Company ID" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtCompanyID" runat="server" CssClass="textboxgrid" Width="50px"/></td>    
                            </tr>
                            <tr>
                                <td style="width:100px;"><asp:TextBox ID="TextBox17" runat="server" Width="100px" Text="Company Name" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtCompanyNameAdd" runat="server" CssClass="textboxgrid" Width="150px"/></td>
                            </tr>
                            <tr>
                                <td style="width:100px;"><asp:TextBox ID="TextBox3" runat="server" Width="100px" Text="Translate To" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtTranslateToAdd" runat="server" CssClass="textboxgrid" Width="150px"/></td>
                            </tr>
                            <tr>
                                <td style="width:100px;"><asp:TextBox ID="TextBox1" runat="server" Width="100px" Text="Create Jobs" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td style="width:20px;"><asp:CheckBox ID="cbCreateJobs" runat="server" CssClass="checkboxgrid" Width="20px"/></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:Button runat="server" ID="btnAddCompany" CausesValidation="false" OnClick="btnAddCompany_Click" CssClass="button_header" Text="Add Company" Width="150px" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                
            </tr>
            <tr>
                <td colspan="2">
                    <MyAsp:BulkEditGridView ID="GridCompanies" runat="server" AutoGenerateColumns="False" BulkEdit="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowUpdating="GridCompanies_RowUpdating" 
                            AllowSorting="True" OnRowDataBound="GridCompanies_RowDataBound" EmptyDataText="No entries returned" HeaderStyle-BackColor="#6B7B8D" OnSelectedIndexChanged="GridCompanies_SelectedIndexChanged" 
                            HeaderStyle-ForeColor="#eeeeee" BorderStyle="Solid" BorderWidth="1px" BorderColor="#dddddd" DataKeyNames="CompanyID" AllowPaging="true" PageSize="20" OnPageIndexChanging="GridCompanies_PageIndexChanging" OnSorting="GridCompanies_Sorting" >
                        <EmptyDataTemplate>No data available for the selected day</EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgDeleteRecord" ImageUrl="Images/Griddelete.png" OnClick="imgDeleteRecord_Click" CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CompanyID" ReadOnly="True" HeaderText="ID" SortExpression="CompanyID">
                                <HeaderStyle HorizontalAlign="center" Wrap="False" Width="50px" />
                                <ItemStyle HorizontalAlign="center" CssClass="labelgrid" Width="50px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Company Name" HeaderStyle-Width="270px" ItemStyle-Width="270px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="CompanyName" >
                                <itemtemplate>
                                    <asp:TextBox ID="txtCompanyName" runat="server" Text='<%#Bind("CompanyName") %>' CssClass="lefttextboxgrid" Enabled="true" BorderWidth="1px" BorderStyle="Solid" Width="270px" />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Translate To" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="TranslateTo" >
                                <itemtemplate>
                                    <asp:TextBox ID="txtTranslateTo" runat="server" Text='<%#Bind("TranslateTo") %>' CssClass="lefttextboxgrid" Enabled="true" BorderWidth="1px" BorderStyle="Solid" Width="50px" />
                                </itemtemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="White" BorderStyle="none"/>
                        <HeaderStyle BorderStyle="none" BackColor="#6B7B8D" ForeColor="#EEEEEE"  />
                        <SelectedRowStyle BackColor="#8D9DAF" />
                    </MyAsp:BulkEditGridView>





                </td>
            </tr>

        </table>

        <br />
        <br />

        <table cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td style="background-color:#505B69;color:#EEEEEE;font-family:'Segoe UI';font-size:16px;text-align:center;font-weight:bold;min-width:600px">Company Rates</td>   
                <td style="background-color:#505B69;width:32px;text-align:center;vertical-align:middle;">
                    <asp:Panel runat="server" ID="pnlCompanyRatesHeader" Height="32px">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/AddUser.png" Height="32px" />
                    </asp:Panel>
                    <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" CollapsedImage="~/Images/AddUser.png" 
                            ExpandedImage="~/Images/AddUser.png" ImageControlID="Image2" TargetControlID="pnlCompanyRatesBody" Collapsed="true" 
                            CollapseControlID="pnlCompanyRatesHeader" ExpandControlID="pnlCompanyRatesHeader" >
                    </AjaxASP:CollapsiblePanelExtender>
                </td>
                <td style="width:60px;" rowspan="2">&nbsp;</td>
                <td style="background-color:#505B69;color:#EEEEEE;font-family:'Segoe UI';font-size:16px;text-align:center;font-weight:bold;min-width:280px">Company Jobs</td>   
                <td style="background-color:#505B69;width:32px;text-align:center;vertical-align:middle;">
                    <asp:Panel runat="server" ID="pnlCompanyJobsHeader" Height="32px">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/AddUser.png" Height="32px" />
                    </asp:Panel>
                    <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" CollapsedImage="~/Images/AddUser.png" 
                            ExpandedImage="~/Images/AddUser.png" ImageControlID="Image2" TargetControlID="pnlCompanyJobsBody" Collapsed="true" 
                            CollapseControlID="pnlCompanyJobsHeader" ExpandControlID="pnlCompanyJobsHeader" >
                    </AjaxASP:CollapsiblePanelExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlCompanyRatesBody" runat="server">
                        <table>
                            <tr>
                                <td><asp:TextBox ID="TextBox18" runat="server" Width="80px" Text="Valid From" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td>
                                    <asp:TextBox ID="txtValidFromRate" runat="server" Width="70px" CssClass="textboxgrid"  />
                                    <AjaxASP:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtValidFromRate" Format="dd/MM/yyyy" />
                                    <AjaxASP:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtValidFromRate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                                </td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox19" runat="server" Width="80px" Text="Default Onsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtCompanyRateDefaultOnsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox22" runat="server" Width="80px" Text="PR Onsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtCompanyRatePROnsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox23" runat="server" Width="80px" Text="Misc Onsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtCompanyRateMiscOnsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="TextBox24" runat="server" Width="80px" Text="Increment" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:CheckBox ID="cbIncrement" runat="server" CssClass="checkboxgrid" /></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox21" runat="server" Width="80px" Text="Default Offsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtCompanyRateDefaultOffsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox25" runat="server" Width="80px" Text="PR Offsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtCompanyRatePROffsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox27" runat="server" Width="80px" Text="Misc Offsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtCompanyRateMiscOffsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                            </tr>
                            <tr>
                                <td colspan="11"><asp:Button runat="server" ID="btnAddCompanyRate" CausesValidation="false" OnClick="btnAddCompanyRate_Click1" CssClass="button_header" Text="Add Company Rates" Width="150px" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td colspan="2">
                    <asp:Panel ID="pnlCompanyJobsBody" runat="server">
                        <table>
                            <tr>
                                <td><asp:TextBox ID="TextBox2" runat="server" Width="80px" Text="Job Number" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtJobNumber" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="TextBox11" runat="server" Width="80px" Text="Job Name" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtJobName" runat="server" CssClass="textboxgrid" Width="190px"/></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:Button runat="server" ID="btnAddCompanyJob" CausesValidation="false" OnClick="btnAddCompanyJob_Click" CssClass="button_header" Text="Add Company Job" Width="150px" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align:top">
                    <MyAsp:BulkEditGridView ID="GridCompanyRate" runat="server" AutoGenerateColumns="False" BulkEdit="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowUpdating="GridCompanyRate_RowUpdating" 
                            AllowSorting="False" OnRowDataBound="GridCompanyRate_RowDataBound" EmptyDataText="No entries returned" HeaderStyle-BackColor="#6B7B8D" HeaderStyle-ForeColor="#eeeeee" >
                        <EmptyDataTemplate>No data available for the selected day</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="RateID" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                                <ItemStyle ForeColor="LightGray" HorizontalAlign="Left" CssClass="labelgrid" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgDeleteRecordRate" ImageUrl="Images/Griddelete.png" OnClick="imgDeleteRecordRate_Click" CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <itemtemplate>
                                    <asp:Label ID="lblCompanyID" runat="server" Text='<%#Bind("CompanyID") %>' Width="50px"/>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid From" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                <itemtemplate>
                                    <asp:TextBox ID="txtDate" runat="server" Text='<%#Bind("ValidFrom") %>' CssClass="lefttextboxgrid" BorderWidth="1px" BorderStyle="Solid" Width="70px"  />
                                    <AjaxASP:CalendarExtender ID="MyCalendar" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy" />
                                    <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Onsite" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                <itemtemplate>
                                    <asp:TextBox ID="DefaultOnsite" runat="server" Text='<%#Bind("DefaultOnsite") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="50px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Offsite" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                <itemtemplate>
                                    <asp:TextBox ID="DefaultOffsite" runat="server" Text='<%#Bind("DefaultOffsite") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="50px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PR Onsite" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                <itemtemplate>
                                    <asp:TextBox ID="ProjectOnsite" runat="server" Text='<%#Bind("ProjectOnsite") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="50px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PR Offsite" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                <itemtemplate>
                                    <asp:TextBox ID="ProjectOffsite" runat="server" Text='<%#Bind("ProjectOffsite") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="50px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Misc Onsite" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                <itemtemplate>
                                    <asp:TextBox ID="MiscOnsite" runat="server" Text='<%#Bind("MiscOnsite") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="50px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Misc Offsite" HeaderStyle-Width="60px" ItemStyle-Width="60px">
                                <itemtemplate>
                                    <asp:TextBox ID="MiscOffsite" runat="server" Text='<%#Bind("MiscOffsite") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="50px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Incr" HeaderStyle-Width="60px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" >
                                <itemtemplate>
                                    <asp:CheckBox ID="cbRounding" runat="server" Checked='<%#Bind("IsRoundingRecord") %>'  />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="White" BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px" />
                        <HeaderStyle Font-Bold="True" BorderStyle="None" BackColor="#6B7B8D" ForeColor="#EEEEEE"  />
                    </MyAsp:BulkEditGridView>
                </td>
                <td style="width:60px;">&nbsp;</td>
                <td colspan="2" style="vertical-align:top">
                    <MyAsp:BulkEditGridView ID="GridCompanyJobs" runat="server" AutoGenerateColumns="False" BulkEdit="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowUpdating="GridCompanyJobs_RowUpdating" 
                            AllowSorting="False" OnRowDataBound="GridCompanyJobs_RowDataBound" EmptyDataText="No Jobs returned" HeaderStyle-BackColor="#6B7B8D" HeaderStyle-ForeColor="#eeeeee" BorderStyle="Solid" BorderWidth="1px" BorderColor="#dddddd" >
                        <EmptyDataTemplate>No data available for the selected day</EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgDeleteRecord" ImageUrl="Images/Griddelete.png" OnClick="imgDeleteRecord_Click" CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="JobNumber">
                                <HeaderStyle HorizontalAlign="Left" Wrap="False" Width="80px" />
                                <ItemStyle CssClass="TextBoxClientOverviewLeft" ForeColor="Black" BackColor="White" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Left" Width="70px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Job Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                <itemtemplate>
                                    <asp:TextBox ID="txtCost" runat="server" Text='<%#Bind("JobName") %>' CssClass="TextBoxClientOverviewLeft" Enabled="true" ForeColor="Black" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="200px" />
                                </itemtemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="White" BorderStyle="none"/>
                        <HeaderStyle BorderStyle="none" BackColor="#6B7B8D" ForeColor="#EEEEEE"  />
                    </MyAsp:BulkEditGridView>
                </td>
            </tr>
        </table>
    </div>
        <div id="footer">
        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/icon-behavior-save-text.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnSave_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/icon-behavior-retry-text.png" CssClass="labelgeneric" CausesValidation="false" />
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" >
            <progresstemplate>
                <div id="UpdateFooterRight" class="footerright">
                    <img src="Images/icon-drawer-processing-active.gif" />
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
