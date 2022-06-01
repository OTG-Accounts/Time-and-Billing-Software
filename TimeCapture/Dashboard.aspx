<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TimeCapture.Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>

<%@ Register Assembly="BulkEditGridView" Namespace="BulkEditGridView" TagPrefix="MyAsp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
    
    <link href="./Styles/StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="mainform" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>        
            <div id="content">
                <div id="header">
                    <div id="header_left">
                        <img src="./Images/logo.bmp" />
                    </div>
                    <div id="header_right">
                        <div id="header_username">
                            <asp:Panel ID="pnlConfigHeader" runat="server">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/layout-arrow-dn.png" />
                                <asp:Label ID="lblUsername" runat="server" Text="User Unknown" CssClass="labelgeneric"  />
                            </asp:Panel>
                            <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" runat="server" CollapsedImage="~/Images/layout-arrow-dn.png" 
                                ExpandedImage="~/Images/layout-arrow-up.png" ImageControlID="Image2" TargetControlID="pnlConfigBody" Collapsed="true" 
                                CollapseControlID="pnlConfigHeader" ExpandControlID="pnlConfigHeader" >
                            </AjaxASP:CollapsiblePanelExtender>
                        </div>
                        <div id="header_config">
                            <asp:Panel ID="pnlConfigBody" runat="server" BackColor="#3C454F">
                                <asp:Button ID="btnLogout" runat="server" Text="SIGN OUT" CssClass="button_header" OnClick="btnLogout_Click" CausesValidation="false" />
                                <asp:Button ID="btnPassChange" runat="server" Text="CHANGE PASSWORD" CssClass="button_header" OnClick="btnPassChange_Click" CausesValidation="false"/>
                                <div id="Main_ChangePassword">
                                    <input id="dummy" type="button" style="display: none" runat="server" />
                                    <AjaxASP:ModalPopupExtender ID="mpeChangePassword" runat="server" PopupControlID="pnlChangePassword" TargetControlID="dummy" BackgroundCssClass="modalBackground" />
                                    <asp:Panel ID="pnlChangePassword" runat="server" CssClass="modalPopup">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div id="ChangePasswordHeader">
                                                    <asp:Label ID="Label24" runat="server" Text="CHANGE PASSWORD" />
                                                </div>
                                                <div id="ChangePasswordBody">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="width:50%">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbNewPassword" ErrorMessage="New password is required." Font-Names="Segoe UI" Font-Size="14px" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                <asp:Label ID="Label25" runat="server" Text="New Password" CssClass="labelgrid" />
                                                            </td>
                                                            <td style="width:50%">
                                                                <asp:TextBox ID="tbNewPassword" runat="server" CssClass="textboxgrid" TextMode="Password" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width:50%">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbConfirmPassword" ErrorMessage="Confirm password is required." Font-Names="Segoe UI" Font-Size="14px" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                <asp:Label ID="Label26" runat="server" Text="Confirm Password" CssClass="labelgrid" />
                                                            </td>
                                                            <td style="width:50%">
                                                                <asp:TextBox ID="tbConfirmPassword" runat="server" CssClass="textboxgrid" TextMode="Password" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div id="ChangePasswordFooter">
                                                    <asp:Button ID="btnChangePasswordCancel" runat="server" Text="Cancel" CssClass="button_header" Width="50px" Height="20px" OnClick="btnChangePasswordCancel_Click" CausesValidation="false" />
                                                    <asp:Button ID="btnChangePassword" runat="server" Text="OK" CssClass="button_header" Width="50px" Height="20px" OnClick="btnChangePassword_Click" />
                                                    <br />
                                                    <asp:ValidationSummary runat="server" HeaderText="Error" Font-Names="Segoe UI" Font-Size="12px" ForeColor="Red" />
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="The passwords do not match!" ControlToValidate="tbConfirmPassword" ControlToCompare="tbNewPassword" Font-Names="Segoe UI" Font-Size="12px" ForeColor="Red"></asp:CompareValidator>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div id="navigation_background">

                </div>
                <div id="content_background">

                </div>
                <div id="footer">
            
                </div>
                <div id="mainbody">
                    <AjaxASP:TabContainer ID="TabContainer1" runat="server" UseVerticalStripPlacement="true" CssClass="tabs">
                        <AjaxASP:TabPanel ID="tab1" HeaderText="Time Capture" runat="server"  >
                            <HeaderTemplate>
                                <img src="./Images/TAB_TimeCapture.png" width="200px" height="50px"  />
                            </HeaderTemplate>
                            <ContentTemplate>
                                <!--  ####################   TIME CAPTURE TAB    ############### -->
                                <div id="tab1_header" class="tab_header">
                            <div id="tab1_headerleft" class="tab_headerleft">
                                <asp:Label ID="Label1" runat="server" Text="Time Capture" Font-Names="Segoe UI" Font-Size="20px" />
                            </div>
                            <div id="tab1_headerright" class="tab_headerright">
                                <asp:Panel ID="PnlFilterHeader" runat="server" Font-Names="Segoe UI" Font-Size="12px" Height="16px" Width="173px">
                                    <asp:Label ID="lblFilterHeader" runat="server" CssClass="labelgeneric" />&nbsp<asp:Image ID="Image1" runat="server" ImageUrl="~/Images/icon-chevron-collapsed.png" />&nbsp
                                </asp:Panel>
                            </div>
                            
                                </div>
                                <div id="tab1_body" class="tab_body">
                            <asp:Panel ID="pnlFilterBody" runat="server" CssClass="FilterPanel">
                                <img src="Images/Filter.png" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label2" runat="server" Text="Completed -> " />
                                <asp:DropDownList ID="ddlCompleted" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompleted_SelectedIndexChanged" CssClass="textboxgrid" >
                                    <asp:ListItem Text="No" Value="0" />
                                    <asp:ListItem Text="Yes" Value="1" />
                                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label3" runat="server" Text="From ->" />
                                <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="True" ontextchanged="txtFromDate_TextChanged" Width="70px" CssClass="textboxgrid" />&nbsp;&nbsp;&nbsp;&nbsp;
                                <AjaxASP:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                                </AjaxASP:MaskedEditExtender>
                                <AjaxASP:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFromDate" Enabled="True" />
                                <asp:Label ID="Label4" runat="server" Text="To -> " />
                                <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="True" ontextchanged="txtToDate_TextChanged" Width="70px" CssClass="textboxgrid" />
                                <AjaxASP:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtToDate" Enabled="True" />
                            </asp:Panel>

                            <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" CollapseControlID="pnlFilterHeader" Collapsed="True" CollapsedImage="Images/icon-chevron-collapsed.png" ExpandedImage="../Images/icon-chevron-expanded.png" 
                                        CollapsedText="Show Filters" ExpandControlID="pnlFilterHeader" ExpandedText="Hide Filter" TargetControlID="pnlFilterBody" 
                                        TextLabelID="lblFilterHeader" ImageControlID="Image1" Enabled="True">
                            </AjaxASP:CollapsiblePanelExtender>

                      
                            
                            <MyAsp:BulkEditGridView ID="GridView1" runat="server" AutoGenerateColumns="False" BulkEdit="False" OnPageIndexChanging="GridView1_PageIndexChanging" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" OnRowUpdating="GridView1_RowUpdating" CssClass="GridLayout" 
                                    AllowSorting="True" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound" >
                            <Columns>
                                <asp:BoundField DataField="EntityChangeLogId" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Left" Width="1px" Wrap="False" />
                                        <ItemStyle ForeColor="LightGray" HorizontalAlign="Left" Width="1px" CssClass="labelgrid" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Incident" SortExpression="IncidentID" >
                                    <itemtemplate>
                                        <asp:Label ID="lblIncidentID" runat="server" Text='<%#Bind("IncidentID") %>' CssClass="labelgrid" />
                                    </itemtemplate>
                                    <headerstyle wrap="False" Width="50px" />
                                    <itemstyle horizontalalign="Center" Width="50px" Font-Names="Segoe UI" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date" SortExpression="EnteredDate">
                                    <itemtemplate>
                                        <asp:TextBox ID="txtDate" runat="server" Text='<%#Bind("EnteredDate") %>' Width="70px" CssClass="textboxgrid" />
                                        <AjaxASP:CalendarExtender ID="MyCalendar" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy" />
                                        <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                                    </itemtemplate>
                                    <headerstyle wrap="False" Width="50px" Font-Names="Segoe UI"/>
                                    <itemstyle horizontalalign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Time">
                                    <itemtemplate>
                                        <asp:TextBox ID="txtStartTime" runat="server" Text='<%#Bind("StartTime") %>' Width="50px" CssClass="textboxgrid"/>
                                        <AjaxASP:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartTime" Mask="99:99:99" MaskType="Time" />
                                    </itemtemplate>
                                    <headerstyle wrap="False" HorizontalAlign="Center" Width="50px"  Font-Names="Segoe UI"/>
                                    <itemstyle horizontalalign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mins">
                                    <itemtemplate>
                                        <asp:TextBox ID="txtTimeInMinutes" runat="server" Text='<%#Bind("TimeInMinutes") %>' Width="30px" CssClass="textboxgrid" />
                                    </itemtemplate>
                                    <headerstyle wrap="False" Width="30px" Font-Names="Segoe UI"/>
                                    <itemstyle horizontalalign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="O">
                                    <itemtemplate>
                                        <asp:CheckBox ID="cbOnsite" runat="server" Checked='<%#Bind("OnSite") %>' Width="20px"/>
                                    </itemtemplate>
                                    <headerstyle wrap="False" Width="20px" Font-Names="Segoe UI"/>
                                    <itemstyle horizontalalign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Incident Title">
                                    <itemtemplate>
                                        <asp:TextBox ID="txtTitle" runat="server" Text='<%#Bind("Title") %>' Width="285px" TextMode="MultiLine" CssClass="textboxgrid" ReadOnly="true"/>
                                    </itemtemplate>
                                    <headerstyle wrap="False" Width="285px" Font-Names="Segoe UI"/>
                                    <itemstyle horizontalalign="Left"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comments">
                                    <itemtemplate>
                                        <asp:TextBox Wrap="true" ID="txtComment" runat="server" Text='<%#Bind("Comment") %>' Width="475px" CssClass="textboxgrid" TextMode="MultiLine" />
                                    </itemtemplate>
                                    <headerstyle wrap="False" Width="475px" Font-Names="Segoe UI"/>
                                    <itemstyle horizontalalign="Left"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="D">
                                    <itemtemplate>
                                        <asp:CheckBox ID="cbCompleted" runat="server" Checked='<%#Bind("Completed") %>' Width="20px" CssClass="checkboxgrid" />
                                    </itemtemplate>
                                    <headerstyle wrap="False" Width="20px" Font-Names="Segoe UI"/>
                                    <itemstyle horizontalalign="Left" />
                                </asp:TemplateField>  

                 
                            </Columns>
                            <EditRowStyle BackColor="White" />
                            <HeaderStyle Font-Bold="True" BorderStyle="None" ForeColor="#465C71" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </MyAsp:BulkEditGridView>
                            
                        </div>
                                <div id="tab1_footer" class="tab_footer">
                            <div id="tab1_footercenter" class="tab_footercenter">
                                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/icon-behavior-save-text.png" CssClass="labelgeneric" OnClick="btnSave_Click" CausesValidation="false"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/icon-behavior-retry-text.png" CssClass="labelgeneric" OnClick="btnRefresh_Click" CausesValidation="false" />
                            </div>
                            <div id="tab1_footerright" class="tab_footerright">
                                <img id="imgprogress" src="Images/icon-drawer-processing-inactive.png" />
                                </div>
                        </div>
                                <!--  ########################################################## -->
                            </ContentTemplate>
                        </AjaxASP:TabPanel>
                        <AjaxASP:TabPanel ID="tab2" HeaderText="test2" runat="server" OnDemandMode="Always">
                            <HeaderTemplate>
                                <img src="./Images/TAB_TimeReview.png"  width="200px" height="50px" />
                            </HeaderTemplate>
                            <ContentTemplate>
                                <!--  ####################   TIME REVIEW TAB    ############### -->
                                <div id="tab2_header" class="tab_header">
                            <div id="tab2_headerleft" class="tab_headerleft">
                                <asp:Label ID="Label5" runat="server" Text="Time Review" Font-Names="Segoe UI" Font-Size="20px" />
                            </div>
                            <div id="tab2_headerright" class="tab_headerright">
                                
                            </div>
                        </div>
                                <div id="tab2_body" class="tab2_body">
                          
                            
                        </div>
                                <div id="tab2_footer" class="tab_footer">
                            <div id="tab2_footercenter" class="tab_footercenter">
                                
                                
                            </div>
                            <div id="tab2_footerright" class="tab_footerright">
                                <img id="img1" src="Images/icon-drawer-processing-inactive.png" />
                                </div>
                        </div>
                                <!--  ########################################################## -->
                            </ContentTemplate>
                        </AjaxASP:TabPanel>
                        <AjaxASP:TabPanel ID="tab3" HeaderText="test2" runat="server">
                            <HeaderTemplate>
                                <img src="./Images/TAB_Accounting.png"  width="200px" height="50px"/>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <!--  ####################   ACCOUNTING TAB    ############### -->
                                <div id="tab3_header" class="tab_header">
                            <div id="tab3_headerleft" class="tab_headerleft">
                                <asp:Label ID="Label22" runat="server" Text="Accounting" Font-Names="Segoe UI" Font-Size="20px" />
                            </div>
                            <div id="tab3_headerright" class="tab_headerright">

                            </div>
                        </div>    
                                <!--  ######################################################## -->                    
                            </ContentTemplate>
                        </AjaxASP:TabPanel>
                        <AjaxASP:TabPanel ID="tab4" HeaderText="test2" runat="server">
                            <HeaderTemplate>
                                <img src="./Images/TAB_Configuration.png"  width="200px" height="50px"/>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <!--  ####################   CONFIGURATION TAB    ############### -->
                                <div id="tab4_header" class="tab_header">
                                    <div id="tab4_headerleft" class="tab_headerleft">
                                        <asp:Label ID="Label21" runat="server" Text="Configuration" Font-Names="Segoe UI" Font-Size="20px" ForeColor="#666666" />
                                    </div>
                                    <div id="tab4_headerright" class="tab_headerright">

                                    </div>
                                </div>
                                <div id="tab4_body" class="tab2_body">
                                    <div class="config_tabs">
                                        <div class="config_tab">
                                            <input type="radio" id="tab-1" name="tab-group-1" checked="checked">
                                            <label for="tab-1">Users</label>
                                            <div class="content">
                                                <div id="tab2_BodyTop">
                                                    <div id="tab2_BodyTopLeft">
                                                        <asp:Label ID="Label6" runat="server" Text="Service Manager Users"  />
                                                        <br /><br />
                                                        <asp:ListBox ID="lstSMUsers" runat="server" Width="200px" Height="250px"></asp:ListBox>
                                                    </div>
                                                    <div id="tab2_BodyTopMiddle">
                                                        <asp:ImageButton ID="btnAddUser" runat="server" ImageUrl="~/Images/button-next.png" OnClick="btnAddUser_Click" CausesValidation="false"/>
                                                        <br /><br />
                                                        <asp:ImageButton ID="btnRemoveUser" runat="server" ImageUrl="~/Images/button-back.png" OnClick="btnRemoveUser_Click" CausesValidation="false" />
                                                    </div>
                                                    <div id="tab2_BodyTopRight">
                                                        <asp:Label ID="Label7" runat="server" Text="Time Capture Users"  />
                                                        <br /><br />
                                                        <asp:ListBox ID="lstCurrentUsers" runat="server" Width="200px" Height="250px" OnSelectedIndexChanged="lstCurrentUsers_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                    </div>
                                                </div>
                                                <div id="tab2_BodyBottom">
                                                    <asp:Panel ID="pnlUserDetailsHeader" runat="server">
                                                        <asp:Label ID="lblUserDetailsHeader" runat="server" width="1100px"/>
                                                        <asp:Image ID="imgUserDetails" runat="server" ImageAlign="Right" ImageUrl="~/Images/deploy_chevrondown.png" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlUserDetailsBody" runat="server">
                                                        <asp:Label ID="Label8" runat="server" Text="ID" CssClass="labelgrid" />
                                                        <asp:TextBox ID="tbServiceManagerID" runat="server" CssClass="textboxgrid" /><br />
                                                        <asp:Label ID="Label9" runat="server" Text="Firstname" CssClass="labelgrid"/>
                                                        <asp:TextBox ID="tbFirstName" runat="server" CssClass="textboxgrid"/><br />
                                                        <asp:Label ID="Label10" runat="server" Text="Lastname" CssClass="labelgrid"/>
                                                        <asp:TextBox ID="tbLastName" runat="server" CssClass="textboxgrid"/><br />
                                                        <asp:Label ID="Label11" runat="server" Text="Username" CssClass="labelgrid"/>
                                                        <asp:TextBox ID="tbUsername" runat="server" CssClass="textboxgrid"/><br />
                                                        <asp:Label ID="Label12" runat="server" Text="Password" CssClass="labelgrid"/>
                                                        <asp:TextBox ID="tbPassword" runat="server" CssClass="textboxgrid"/><br />
                                                        <asp:Label ID="Label13" runat="server" Text="Domain" CssClass="labelgrid"/>
                                                        <asp:TextBox ID="tbDomain" runat="server" CssClass="textboxgrid"/><br />
                                                        <asp:Label ID="Label14" runat="server" Text="Display Name" CssClass="labelgrid"/>
                                                        <asp:TextBox ID="tbDisplayName" runat="server" CssClass="textboxgrid"/><br />
                                                        <asp:Label ID="Label15" runat="server" Text="Email address" CssClass="labelgrid"/>
                                                        <asp:TextBox ID="tbEmail" runat="server" CssClass="textboxgrid"/><br />
                                                    </asp:Panel>
                                                    <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" CollapseControlID="pnlUserDetailsHeader" Collapsed="False" CollapsedImage="Images/deploy_chevrondown.png" ExpandedImage="../Images/deploy_chevronup.png" 
                                                        CollapsedText="User Details" ExpandControlID="pnlUserDetailsHeader" ExpandedText="User Details" TargetControlID="pnlUserDetailsBody" 
                                                        TextLabelID="lblUserDetailsHeader" ImageControlID="imgUserDetails" Enabled="True">
                                                    </AjaxASP:CollapsiblePanelExtender>
                                                    <asp:Panel ID="pnlUserAccessHeader" runat="server">
                                                        <asp:Label ID="lblUserAccessHeader" runat="server" width="1100px"/><asp:Image ID="imgUserAccess" runat="server" ImageAlign="Right" ImageUrl="~/Images/deploy_chevrondown.png" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlUserAccessBody" runat="server">
                                                        <asp:CheckBox ID="cbTab1" runat="server" />
                                                        <asp:Label ID="Label16" runat="server" Text="Time Capture tab"/><br />
                                                        <asp:CheckBox ID="cbTab2" runat="server" />
                                                        <asp:Label ID="Label17" runat="server" Text="User Management tab"/><br />
                                                        <asp:CheckBox ID="cbTab3" runat="server" />
                                                        <asp:Label ID="Label18" runat="server" Text="tab 3"/><br />
                                                        <asp:CheckBox ID="cbTab4" runat="server" />
                                                        <asp:Label ID="Label19" runat="server" Text="tab 4"/><br />
                                                        <asp:CheckBox ID="cbTab5" runat="server" />
                                                        <asp:Label ID="Label20" runat="server" Text="tab 5"/><br />
                                                    </asp:Panel>
                                                    <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" CollapseControlID="pnlUserAccessHeader" Collapsed="True" CollapsedImage="Images/deploy_chevrondown.png" ExpandedImage="../Images/deploy_chevronup.png" 
                                                        CollapsedText="User Access" ExpandControlID="pnlUserAccessHeader" ExpandedText="User Access" TargetControlID="pnlUserAccessBody" 
                                                        TextLabelID="lblUserAccessHeader" ImageControlID="imgUserAccess" Enabled="True">
                                                    </AjaxASP:CollapsiblePanelExtender>
                                                </div>
                                            </div> 
                                        </div>
                                        <div class="config_tab">
                                            <input type="radio" id="tab-2" name="tab-group-1">
                                            <label for="tab-2">Companies</label>
                                            <div class="content">
                                                <div id="ertert" style="position:absolute;top:0px;left:0px;right:0px;bottom:0px">
                                                    stuff for companies
                                                </div>
                                            </div> 
                                        </div>
                                    </div>
                                </div>
                                <div id="tab4_footer" class="tab_footer">
                                    <div id="tab4_footercenter" class="tab_footercenter">
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon-behavior-save-text.png" CssClass="labelgeneric" CausesValidation="false"  />
                                    </div>
                                    <div id="tab4_footerright" class="tab_footerright">
                                        <img id="img2" src="Images/icon-drawer-processing-inactive.png" />
                                    </div>
                                </div>
                                <!--  ######################################################## -->                    
                            </ContentTemplate>
                        </AjaxASP:TabPanel>
                    </AjaxASP:TabContainer>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" >
                        <progresstemplate>
                            <div id="ProgressBackground" />
                            </div>
                            <div id="progressimage1" class="tab_footer">
                                <div id="progressimage2" class="tab_footerright">
                                    <img src="Images/icon-drawer-processing-active.gif" />
                                </div>
                            </div>
                        </progresstemplate>
                    </asp:UpdateProgress>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
