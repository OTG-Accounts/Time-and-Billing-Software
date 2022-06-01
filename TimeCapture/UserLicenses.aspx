<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserLicenses.aspx.cs" Inherits="TimeCapture.UserLicenses" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>
<%@ Register Assembly="BulkEditGridView" Namespace="BulkEditGridView" TagPrefix="MyAsp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="navigator">
        <table align="center" cellpadding="0" cellspacing="2">
            <tr>
                <td><asp:TextBox ID="TextBox7" runat="server" Width="80px" Text="Start Date" CssClass="TextBoxClientOverviewLeft" /></td>
                <td><asp:TextBox ID="txtStartDate" runat="server" CssClass="TextBoxClientOverviewRight" AutoPostBack="true" Width="60px" />
                            <AjaxASP:CalendarExtender ID="StartDateCalendar" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy" />
                            <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  /></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="TextBox8" runat="server" Width="80px" Text="End Date" CssClass="TextBoxClientOverviewLeft" /></td>
                <td><asp:TextBox ID="txtEndDate" runat="server" CssClass="TextBoxClientOverviewRight" AutoPostBack="true" Width="60px" />
                            <AjaxASP:CalendarExtender ID="EndDateCalendar" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy" />
                            <AjaxASP:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtEndDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  /></td>
            </tr>
        </table>
    </div>
    <div id="main">
        
            
         <asp:Panel ID="pnlError" runat="server" Visible="false">
            <table align="center" cellpadding="0" cellspacing="2">
                <tr>
                    <td><asp:Label ID="lblDaysError" runat="server"/></td>
                    
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlUserLicenses" runat="server" Visible="true" >
            
                        <asp:GridView ID="GridUsersCreated" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridUsersCreated_RowDataBound" Font-Names="Segoe UI" Font-Size="11px" HorizontalAlign="Center" 
            CellPadding="1" ForeColor="#333333" GridLines="none" HeaderStyle-BorderStyle="none" EmptyDataText="No user created for the selected period!">
            <Columns>
                <asp:TemplateField HeaderText="System" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE" >
                    <itemtemplate>
                        <asp:Label ID="lblSystem" runat="server" Text='<%#Bind("System") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI"  />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("Company") %>'  Width="200px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Display Name" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblDisplayName" runat="server" Text='<%#Bind("DisplayName") %>'  Width="150px" ReadOnly="true" CssClass="LabelClientsGrid" />
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Username" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblUsername" runat="server" Text='<%#Bind("Username") %>' Width="150px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Created On" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Date") %>'  Width="75px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                        <asp:GridView ID="GridUsersDeleted" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridUsersDeleted_RowDataBound" Font-Names="Segoe UI" Font-Size="11px" HorizontalAlign="Center" 
            CellPadding="1" ForeColor="#333333" GridLines="none" HeaderStyle-BorderStyle="none" EmptyDataText="No user created for the selected period!">
            <Columns>
                <asp:TemplateField HeaderText="System" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE" >
                    <itemtemplate>
                        <asp:Label ID="lblSystem" runat="server" Text='<%#Bind("System") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI"  />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("Company") %>'  Width="200px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Display Name" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblDisplayName" runat="server" Text='<%#Bind("DisplayName") %>'  Width="150px" ReadOnly="true" CssClass="LabelClientsGrid" />
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Username" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblUsername" runat="server" Text='<%#Bind("Username") %>' Width="150px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Deleted On" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Date") %>'  Width="75px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>                    
                        <asp:GridView ID="GridUsersEnabled" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridUsersEnabled_RowDataBound" Font-Names="Segoe UI" Font-Size="11px" HorizontalAlign="Center" 
            CellPadding="1" ForeColor="#333333" GridLines="none" HeaderStyle-BorderStyle="none" EmptyDataText="No user created for the selected period!">
            <Columns>
                <asp:TemplateField HeaderText="System" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE" >
                    <itemtemplate>
                        <asp:Label ID="lblSystem" runat="server" Text='<%#Bind("System") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI"  />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("Company") %>'  Width="200px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Display Name" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblDisplayName" runat="server" Text='<%#Bind("DisplayName") %>'  Width="150px" ReadOnly="true" CssClass="LabelClientsGrid" />
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Username" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblUsername" runat="server" Text='<%#Bind("Username") %>' Width="150px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Enabled On" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Date") %>'  Width="75px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                        <asp:GridView ID="GridUserDisabled" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridUserDisabled_RowDataBound" Font-Names="Segoe UI" Font-Size="11px" HorizontalAlign="Center" 
            CellPadding="1" ForeColor="#333333" GridLines="none" HeaderStyle-BorderStyle="none" EmptyDataText="No user created for the selected period!">
            <Columns>
                <asp:TemplateField HeaderText="System" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE" >
                    <itemtemplate>
                        <asp:Label ID="lblSystem" runat="server" Text='<%#Bind("System") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI"  />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("Company") %>'  Width="200px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Display Name" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblDisplayName" runat="server" Text='<%#Bind("DisplayName") %>'  Width="150px" ReadOnly="true" CssClass="LabelClientsGrid" />
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Username" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblUsername" runat="server" Text='<%#Bind("Username") %>' Width="150px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Disabled On" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Date") %>'  Width="75px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>                    
            <asp:GridView ID="GridUserLicense" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridUserLicense_RowDataBound" Font-Names="Segoe UI" Font-Size="11px" HorizontalAlign="Center" 
            CellPadding="1" ForeColor="#333333" GridLines="none" HeaderStyle-BorderStyle="none" EmptyDataText="No user created for the selected period!">
            <Columns>
                <asp:TemplateField HeaderText="System" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE" >
                    <itemtemplate>
                        <asp:Label ID="lblSystem" runat="server" Text='<%#Bind("System") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI"  />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("Company") %>'  Width="200px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Display Name" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblDisplayName" runat="server" Text='<%#Bind("DisplayName") %>'  Width="150px" ReadOnly="true" CssClass="LabelClientsGrid" />
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Username" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblUsername" runat="server" Text='<%#Bind("Username") %>' Width="150px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="Left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Changed on" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Date") %>'  Width="75px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mailbox" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblMailbox" runat="server" Text='<%#Bind("Mailbox") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Office365" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblOffice365" runat="server" Text='<%#Bind("Office365") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Workspace" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblWorkspace" runat="server" Text='<%#Bind("Workspace") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OfficePro" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblOfficePro" runat="server" Text='<%#Bind("OfficePro") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Project" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblProject" runat="server" Text='<%#Bind("Project") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Visio" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblVisio" runat="server" Text='<%#Bind("Visio") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CRM" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblCRM" runat="server" Text='<%#Bind("CRM") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sharepoint" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblSharepoint" runat="server" Text='<%#Bind("Sharepoint") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ThirdParty" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblThirdParty" runat="server" Text='<%#Bind("ThirdParty") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MEHS" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblMEHS" runat="server" Text='<%#Bind("MEHS") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="InTune" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblInTune" runat="server" Text='<%#Bind("InTune") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NFB" HeaderStyle-BackColor="#505B69" HeaderStyle-ForeColor="#EEEEEE">
                    <itemtemplate>
                        <asp:Label ID="lblNotForBilling" runat="server" Text='<%#Bind("NotForBilling") %>'  Width="50px" ReadOnly="true" CssClass="LabelClientsGrid"/>
                    </itemtemplate>
                    <itemstyle horizontalalign="center" Font-Names="Segoe UI" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
            
        </asp:Panel>
    
        
    </div>
    <div id="footer">
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
