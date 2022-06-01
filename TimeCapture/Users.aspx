<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="TimeCapture.Users" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>
<%@ Register Assembly="BulkEditGridView" Namespace="BulkEditGridView" TagPrefix="MyAsp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .style1 {
            height:30px;
            width:70px;
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="navigator">
    </div>
    <div id="main">
        <%if (UserGroup == "administrators" || UserGroup == "managers"){  %>
        <table cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td><asp:TextBox ID="TextBox1" runat="server" Enabled="false" Width="248px" Text="Service Manager Users" CssClass="TextBoxClientOverviewCentre" Height="19px" BorderColor="#505B69" BackColor="#505B69"/></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox2" runat="server" Enabled="false" Width="248px" Text="Time Capture Users" CssClass="TextBoxClientOverviewCentre" Height="19px" BorderColor="#505B69" BackColor="#505B69"/></td>
                <td>&nbsp;</td>
                <td colspan="2"><asp:TextBox ID="TextBox3" runat="server" Enabled="false" Width="360px" Text="User Details" CssClass="TextBoxClientOverviewCentre" Height="19px" BorderColor="#505B69" BackColor="#505B69"/></td>
            </tr>
            <tr>
                <td rowspan="11"><asp:ListBox ID="lstSMUsers" runat="server" Width="250px" Height="250px" ></asp:ListBox></td>
                <td>&nbsp;</td>
                <td rowspan="11"><asp:ListBox ID="lstCurrentUsers" runat="server" Width="250px" Height="250px" OnSelectedIndexChanged="lstCurrentUsers_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox></td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox8" runat="server" Width="120px" Text="User ID" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td><asp:TextBox ID="tbServiceManagerID" runat="server" CssClass="textboxgrid" width="230" BackColor="White" Enabled="false" BorderStyle="None"/></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox4" runat="server" Width="120px" Text="Firstname" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td><asp:TextBox ID="tbFirstName" runat="server" CssClass="textboxgrid" Width="230px"/></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox5" runat="server" Width="120px" Text="Lastname" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td><asp:TextBox ID="tbLastName" runat="server" CssClass="textboxgrid" Width="230px"/></td>
            </tr>
            <tr>
                <td rowspan="2">&nbsp;<asp:ImageButton ID="btnAddUser" runat="server" ImageUrl="~/Images/InsertOne.png" OnClick="btnAddUser_Click" CausesValidation="false"/>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox6" runat="server" Width="120px" Text="Username" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td><asp:TextBox ID="tbUsername" runat="server" CssClass="textboxgrid" Width="230px"/></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox7" runat="server" Width="120px" Text="Password" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td><asp:TextBox ID="tbPassword" runat="server" CssClass="textboxgrid" Width="230px"/></td>
            </tr>
            <tr>
                <td rowspan="2">&nbsp;<asp:ImageButton ID="btnRemoveUser" runat="server" ImageUrl="~/Images/RemoveOne.png" OnClick="btnRemoveUser_Click" CausesValidation="false" />&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox9" runat="server" Width="120px" Text="Domain" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td><asp:TextBox ID="tbDomain" runat="server" CssClass="textboxgrid" Width="230px"/></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox10" runat="server" Width="120px" Text="Display Name" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td><asp:TextBox ID="tbDisplayName" runat="server" CssClass="textboxgrid" Width="230px"/></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox11" runat="server" Width="120px" Text="Email Address" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td><asp:TextBox ID="tbEmail" runat="server" CssClass="textboxgrid" Width="230px"/></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox12" runat="server" Width="120px" Text="User Group" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td>
                    <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="True"  CssClass="textboxgrid" Width="237px" Height="19px" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox13" runat="server" Width="120px" Text="Peer Review" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td><asp:DropDownList ID="ddlPeerReview" runat="server" AutoPostBack="True"  CssClass="textboxgrid" Width="237px" Height="18px" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:TextBox ID="TextBox34" runat="server" Width="120px" Text="Active" CssClass="TextBoxClientOverviewLeft" Enabled="false" /></td>
                <td><asp:CheckBox ID="cbUserActive" runat="server" Width="230px" Enabled="true" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><asp:CheckBox ID="cbShowActiveUsersOnly" runat="server" Width="230px" Enabled="true" Text="Show only active users" Checked="true" CausesValidation="false" OnCheckedChanged="cbShowActiveUsersOnly_CheckedChanged" /></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        
        <%if (UserGroup == "administrators"){ %>                
        <br />
        <br />
        <table cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td style="background-color:#505B69;color:#EEEEEE;font-family:'Segoe UI';font-size:16px;text-align:center;font-weight:bold;min-width:260px">User Cost</td>   
                <td style="background-color:#505B69;width:32px;text-align:center;vertical-align:middle;">
                    <asp:Panel runat="server" ID="pnlAddUserCostHeader" Height="32px">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/AddUser.png" Height="32px" />
                    </asp:Panel>
                    <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" CollapsedImage="~/Images/AddUser.png" 
                            ExpandedImage="~/Images/AddUser.png" ImageControlID="Image2" TargetControlID="pnlAddUserCostBody" Collapsed="true" 
                            CollapseControlID="pnlAddUserCostHeader" ExpandControlID="pnlAddUserCostHeader" >
                    </AjaxASP:CollapsiblePanelExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlAddUserCostBody" runat="server">
                        <table>
                            <tr>
                                <td><asp:TextBox ID="TextBox16" runat="server" Width="80px" Text="Valid From" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td>
                                    <asp:TextBox ID="txtValidFrom" runat="server" Width="70px" CssClass="textboxgrid"  />
                                    <AjaxASP:CalendarExtender ID="MyCalendar10" runat="server" TargetControlID="txtValidFrom" Format="dd/MM/yyyy" />
                                    <AjaxASP:MaskedEditExtender ID="MaskedEditExtender10" runat="server" TargetControlID="txtValidFrom" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                                </td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="TextBox17" runat="server" Width="80px" Text="Cost" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtUserCost" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:Button runat="server" ID="btnAddUserCost" CausesValidation="false" OnClick="btnAddUserCost_Click" CssClass="button_header" Text="Add User Cost" Width="150px" Enabled="false" /></td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="txtImportUserCost" runat="server" Width="200px" CssClass="textboxgrid" /></td>
                                <td><asp:Button ID="btnImportUserCost" runat="server" OnClick="btnImportUserCost_Click" CssClass="button_header" Text="Import" CausesValidation="false" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <MyAsp:BulkEditGridView ID="GridUserCost" runat="server" AutoGenerateColumns="False" BulkEdit="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowUpdating="GridUserCost_RowUpdating" 
                            AllowSorting="False" OnRowDataBound="GridUserCost_RowDataBound" EmptyDataText="No entries returned" HeaderStyle-BackColor="#6B7B8D" HeaderStyle-ForeColor="#eeeeee" Width="300px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#dddddd" >
                        <EmptyDataTemplate>No data available for the selected day</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="CostID" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                                <ItemStyle ForeColor="LightGray" HorizontalAlign="Left" CssClass="labelgrid" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgDeleteRecord" ImageUrl="Images/Griddelete.png" OnClick="imgDeleteRecord_Click" CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Username" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="170px" ItemStyle-Width="170px">
                                <itemtemplate>
                                    <asp:Label ID="lblDisplayName" runat="server" Text='<%#Bind("DisplayName") %>'/>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid From" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                <itemtemplate>
                                    <asp:TextBox ID="txtDate" runat="server" Text='<%#Bind("ValidFrom") %>' CssClass="lefttextboxgrid" BorderWidth="1px" BorderStyle="Solid" Width="70px" />
                                    <AjaxASP:CalendarExtender ID="MyCalendar" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy" />
                                    <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cost" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <itemtemplate>
                                    <asp:TextBox ID="txtCost" runat="server" Text='<%#Bind("Cost") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="White" BorderStyle="none"/>
                        <HeaderStyle BorderStyle="none" BackColor="#6B7B8D" ForeColor="#EEEEEE"  />
                    </MyAsp:BulkEditGridView>
                </td>
            </tr>
        </table>
        <%} %>
        <br />
        <br />

        <table cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td style="background-color:#505B69;color:#EEEEEE;font-family:'Segoe UI';font-size:16px;text-align:center;font-weight:bold;min-width:900px">User Rates</td>
                <td style="background-color:#505B69;width:32px;text-align:center;vertical-align:middle;">
                    <asp:Panel runat="server" ID="pnlAddUserRateHeader" Height="32px" Width="32px" Wrap="true" >
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/AddUser.png" Height="32px" Width="32px" />
                    </asp:Panel>
                    <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" CollapsedImage="~/Images/AddUser.png" 
                            ExpandedImage="~/Images/AddUser.png" ImageControlID="Image2" TargetControlID="pnlAddUserRateBody" Collapsed="true" 
                            CollapseControlID="pnlAddUserRateHeader" ExpandControlID="pnlAddUserRateHeader" >
                    </AjaxASP:CollapsiblePanelExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlAddUserRateBody" runat="server">
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
                                <td><asp:TextBox ID="txtUserRateDefaultOnsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox22" runat="server" Width="80px" Text="PR Onsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtUserRatePROnsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox23" runat="server" Width="80px" Text="Misc Onsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtUserRateMiscOnsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox20" runat="server" Width="80px" Text="Override" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:CheckBox ID="cbUserRateOverride" runat="server" CssClass="checkboxgrid" /></td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="TextBox26" runat="server" Width="80px" Text="Company" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:DropDownList ID="ddlUserRateCompany" runat="server" CssClass="textboxgrid" Width="70px" /></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox21" runat="server" Width="80px" Text="Default Offsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtUserRateDefaultOffsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox25" runat="server" Width="80px" Text="PR Offsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtUserRatePROffsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox27" runat="server" Width="80px" Text="Misc Offsite" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtUserRateMiscOffsite" runat="server" CssClass="textboxgrid" Width="70px"/></td>
                                <td>&nbsp;</td>
                                <td><asp:TextBox ID="TextBox24" runat="server" Width="80px" Text="Increment" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:CheckBox ID="cbIncrement" runat="server" CssClass="checkboxgrid" /></td>
                            </tr>
                            <tr>
                                <td colspan="12"><asp:Button runat="server" ID="btnAddUserRate" CausesValidation="false" OnClick="btnAddUserRate_Click" CssClass="button_header" Text="Add User Rates" Width="150px" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <MyAsp:BulkEditGridView ID="GridUserRate" runat="server" AutoGenerateColumns="False" BulkEdit="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowUpdating="GridUserRate_RowUpdating" 
                            AllowSorting="False" OnRowDataBound="GridUserRate_RowDataBound" EmptyDataText="No entries returned" HeaderStyle-BackColor="#6B7B8D" HeaderStyle-ForeColor="#eeeeee" >
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
                            <asp:TemplateField HeaderText="Username" HeaderStyle-Width="170px" ItemStyle-Width="170px">
                                <itemtemplate>
                                    <asp:Label ID="lblDisplayName" runat="server" Text='<%#Bind("DisplayName") %>' Width="160px"/>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid From" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                                <itemtemplate>
                                    <asp:TextBox ID="txtDate" runat="server" Text='<%#Bind("ValidFrom") %>' CssClass="lefttextboxgrid" BorderWidth="1px" BorderStyle="Solid" Width="70px"  />
                                    <AjaxASP:CalendarExtender ID="MyCalendar" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy" />
                                    <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Default Onsite" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <itemtemplate>
                                    <asp:TextBox ID="DefaultOnsiteRate" runat="server" Text='<%#Bind("DefaultOnsiteRate") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Def Offsite" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <itemtemplate>
                                    <asp:TextBox ID="DefaultOffsiteRate" runat="server" Text='<%#Bind("DefaultOffsiteRate") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PR Onsite" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <itemtemplate>
                                    <asp:TextBox ID="ProjectOnsiteRate" runat="server" Text='<%#Bind("ProjectOnsiteRate") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PR Offsite" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <itemtemplate>
                                    <asp:TextBox ID="ProjectOffsiteRate" runat="server" Text='<%#Bind("ProjectOffsiteRate") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Misc Onsite" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <itemtemplate>
                                    <asp:TextBox ID="MiscOnsiteRate" runat="server" Text='<%#Bind("MiscOnsiteRate") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Misc Offsite" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                <itemtemplate>
                                    <asp:TextBox ID="MiscOffsiteRate" runat="server" Text='<%#Bind("MiscOffsiteRate") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Override" HeaderStyle-Width="50px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                                <itemtemplate>
                                    <asp:CheckBox ID="cbOverride" runat="server" Checked='<%#Bind("Override") %>'  />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Incr" HeaderStyle-Width="50px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                                <itemtemplate>
                                    <asp:CheckBox ID="cbRounding" runat="server" Checked='<%#Bind("IsRoundingRecord") %>'  />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                <itemtemplate>
                                    <asp:TextBox ID="Company" runat="server" Text='<%#Bind("CompanyID") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="90px" />
                                </itemtemplate>
                                <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="White" BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px" />
                        <HeaderStyle Font-Bold="True" BorderStyle="None" BackColor="#6B7B8D" ForeColor="#EEEEEE"  />
                    </MyAsp:BulkEditGridView>
                </td>
            </tr>
        </table>

        <br />
        <br />

        <table cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td style="background-color:#505B69;color:#EEEEEE;font-family:'Segoe UI';font-size:16px;text-align:center;font-weight:bold;min-width:260px">User Work Time</td>   
                <td style="background-color:#505B69;width:32px;text-align:center;vertical-align:middle;">
                    <asp:Panel runat="server" ID="pnlAddUserWorkTimeHeader" Height="32px">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/AddUser.png" Height="32px" />
                    </asp:Panel>
                    <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" CollapsedImage="~/Images/AddUser.png" 
                            ExpandedImage="~/Images/AddUser.png" ImageControlID="Image2" TargetControlID="pnlAddUserWorkTimeBody" Collapsed="true" 
                            CollapseControlID="pnlAddUserWorkTimeHeader" ExpandControlID="pnlAddUserWorkTimeHeader" >
                    </AjaxASP:CollapsiblePanelExtender>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlAddUserWorkTimeBody" runat="server">
                        <table>
                            <tr>
                                <td><asp:TextBox ID="TextBox14" runat="server" Width="80px" Text="Valid From" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td>
                                    <asp:TextBox ID="txtValidFromUserWorkTime" runat="server" Width="70px" CssClass="textboxgrid"  />
                                    <AjaxASP:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtValidFromUserWorkTime" Format="dd/MM/yyyy" />
                                    <AjaxASP:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtValidFromUserWorkTime" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                                </td>
                                <td><asp:TextBox ID="TextBox15" runat="server" Width="80px" Text="Tuesday" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtTueWorkMinutes" runat="server" CssClass="textboxgrid" Width="40px"/></td>
                                <td><asp:TextBox ID="TextBox30" runat="server" Width="80px" Text="Thursday" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtThuWorkMinutes" runat="server" CssClass="textboxgrid" Width="40px"/></td>
                                <td><asp:TextBox ID="TextBox32" runat="server" Width="80px" Text="Saturday" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtSatWorkMinutes" runat="server" CssClass="textboxgrid" Width="40px"/></td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="TextBox28" runat="server" Width="80px" Text="Monday" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtMonWorkMinutes" runat="server" CssClass="textboxgrid" Width="40px"/></td>
                                <td><asp:TextBox ID="TextBox29" runat="server" Width="80px" Text="Wednesday" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtWedWorkMinutes" runat="server" CssClass="textboxgrid" Width="40px"/></td>
                                <td><asp:TextBox ID="TextBox31" runat="server" Width="80px" Text="Friday" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtFriWorkMinutes" runat="server" CssClass="textboxgrid" Width="40px"/></td>
                                <td><asp:TextBox ID="TextBox33" runat="server" Width="80px" Text="Sunday" CssClass="TextBoxClientOverviewLeft" Enabled="true" /></td>
                                <td><asp:TextBox ID="txtSunWorkMinutes" runat="server" CssClass="textboxgrid" Width="40px"/></td>
                            </tr>
                            <tr>
                                <td colspan="2"><asp:Button runat="server" ID="btnAddUserWorkTime" CausesValidation="false" OnClick="btnAddUserWorkTime_Click" CssClass="button_header" Text="Add User Work Time" Width="150px" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <MyAsp:BulkEditGridView ID="GridUserWorkTimes" runat="server" AutoGenerateColumns="False" BulkEdit="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowUpdating="GridUserWorkTimes_RowUpdating" 
                            AllowSorting="False" OnRowDataBound="GridUserWorkTimes_RowDataBound" EmptyDataText="No entries returned" HeaderStyle-BackColor="#6B7B8D" HeaderStyle-ForeColor="#eeeeee" Width="300px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#dddddd" >
                        <EmptyDataTemplate>No data available for the selected user</EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="WorkTimeID" ReadOnly="True">
                                <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                                <ItemStyle ForeColor="LightGray" HorizontalAlign="Left" CssClass="labelgrid" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ID="imgDeleteUserWorkTime" ImageUrl="Images/Griddelete.png" OnClick="imgDeleteUserWorkTime_Click" CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Username" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="170px" ItemStyle-Width="170px">
                                <itemtemplate>
                                    <asp:Label ID="lblDisplayNameWorkTime" runat="server" Text='<%#Bind("DisplayName") %>'/>
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valid From" HeaderStyle-Width="80px" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                <itemtemplate>
                                    <asp:TextBox ID="txtDateUserWorkTime" runat="server" Text='<%#Bind("ValidFrom") %>' CssClass="lefttextboxgrid" BorderWidth="1px" BorderStyle="Solid" Width="70px" />
                                    <AjaxASP:CalendarExtender ID="MyCalendar" runat="server" TargetControlID="txtDateUserWorkTime" Format="dd/MM/yyyy" />
                                    <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDateUserWorkTime" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mon" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <itemtemplate>
                                    <asp:TextBox ID="txtMonWork" runat="server" Text='<%#Bind("MonWorkMinutes") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tue" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <itemtemplate>
                                    <asp:TextBox ID="txtTueWork" runat="server" Text='<%#Bind("TueWorkMinutes") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Wed" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <itemtemplate>
                                    <asp:TextBox ID="txtWedWork" runat="server" Text='<%#Bind("WedWorkMinutes") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thu" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <itemtemplate>
                                    <asp:TextBox ID="txtThuWork" runat="server" Text='<%#Bind("ThuWorkMinutes") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fri" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <itemtemplate>
                                    <asp:TextBox ID="txtFriWork" runat="server" Text='<%#Bind("FriWorkMinutes") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sat" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <itemtemplate>
                                    <asp:TextBox ID="txtSatWork" runat="server" Text='<%#Bind("SatWorkMinutes") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sun" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <itemtemplate>
                                    <asp:TextBox ID="txtSunWork" runat="server" Text='<%#Bind("SunWorkMinutes") %>' CssClass="TextBoxClientOverviewRight" BackColor="White" BorderWidth="1px" BorderStyle="Solid" Width="40px" />
                                </itemtemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle BackColor="White" BorderStyle="none"/>
                        <HeaderStyle BorderStyle="none" BackColor="#6B7B8D" ForeColor="#EEEEEE"  />
                    </MyAsp:BulkEditGridView>
                </td>
            </tr>
        </table>

        <%} else {%>
        <table cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td>
                    <asp:Label ID="lblAccessDenied" runat="server" Text="Access Denied" />
                </td>
            </tr>
        </table>
        <%} %>
    </div>
    <div id="footer">
        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/icon-behavior-save-text.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnSave_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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

