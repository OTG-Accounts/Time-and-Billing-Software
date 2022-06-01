<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimeReview.aspx.cs" Inherits="TimeCapture.TimeReview" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>
<%@ Register Assembly="BulkEditGridView" Namespace="BulkEditGridView" TagPrefix="MyAsp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
    <script type="text/javascript">
        function updateEditStatus(ClientID) {
            var hiddenEditControl = document.getElementById(ClientID);
            if (hiddenEditControl != null) {
                hiddenEditControl.value = "true";
            }
		}
		function ShowPopup(ClientID, ClientID2) {


			var EntityChangeLogID = document.getElementById(ClientID2);
			var txtOrginEntityID = document.getElementById(ClientID);
			if (EntityChangeLogID != null) {
				txtOrginEntityID.value = EntityChangeLogID.value;
			}
			else {
				txtOrginEntityID.value = 'test';
			}
			var modalPopupBehaviorCtrl = $find('mpeNegativeTimeBehaviour');
			modalPopupBehaviorCtrl.show();


		}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="navigator">
            <div id="PanelHeader">
                <table align="center" cellpadding="0" cellspacing="2">
                    <tr>
                        <td><asp:TextBox ID="TextBox1" runat="server" Width="80px" Text="Incident" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:TextBox ID="txtIncidentID" runat="server" CssClass="textboxgrid" Width="150px" /></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox10" runat="server" Width="80px" Text="Comments" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:TextBox ID="txtSearchComment" runat="server" CssClass="textboxgrid" Width="150px" /></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox7" runat="server" Width="80px" Text="Start Date" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:TextBox ID="txtStartDate" runat="server" CssClass="TextBoxClientOverviewRight" Width="60px" />
                                <AjaxASP:CalendarExtender ID="StartDateCalendar" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy" />
                                <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  /></td>
                        <td>&nbsp;&nbsp;</td>
                        <td>&nbsp;&nbsp;</td>
                        <td>&nbsp;&nbsp;</td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox12" runat="server" Width="80px" Text="Completed" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:DropDownList ID="ddlCompleted" runat="server" CssClass="textboxgrid" Width="60px">
                                <asp:ListItem Text="Yes" Value="1" />    
                                <asp:ListItem Text="All" Value="%" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList></td>
                        
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="TextBox2" runat="server" Width="80px" Text="Analyst" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:DropDownList ID="ddlEnteredBy" runat="server" CssClass="textboxgrid" Width="158px" /></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox9" runat="server" Width="80px" Text="Title" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:TextBox ID="txtSearchTitle" runat="server" CssClass="textboxgrid" Width="150px"/></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox8" runat="server" Width="80px" Text="End Date" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:TextBox ID="txtEndDate" runat="server" CssClass="TextBoxClientOverviewRight" Width="60px" />
                                <AjaxASP:CalendarExtender ID="EndDateCalendar" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy" />
                                <AjaxASP:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtEndDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  /></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox4" runat="server" Width="80px" Text="On Site" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:DropDownList ID="ddlOnsite" runat="server" CssClass="textboxgrid" Width="60px">
                                <asp:ListItem Text="All" Value="%" />
                                <asp:ListItem Text="Yes" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox13" runat="server" Width="80px" Text="Peer Review" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:DropDownList ID="ddlPeerReview" runat="server" CssClass="textboxgrid" Width="60px">
                                <asp:ListItem Text="Yes" Value="1" />    
                                <asp:ListItem Text="All" Value="%" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="TextBox11" runat="server" Width="80px" Text="Company" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:DropDownList ID="ddlCompany" runat="server" CssClass="textboxgrid" Width="158px" /></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox14" runat="server" Width="80px" Text="Sub Category" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="textboxgrid" Width="155px" Enabled="False">
                            </asp:DropDownList></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox3" runat="server" Width="80px" Text="Category" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:DropDownList ID="ddlCategory" runat="server" CssClass="textboxgrid" Width="65px">
                            </asp:DropDownList></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox5" runat="server" Width="80px" Text="Billable" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:DropDownList ID="ddlBillable" runat="server" CssClass="textboxgrid" Width="60px">
                                <asp:ListItem Text="All" Value="%" />
                                <asp:ListItem Text="Yes" Value="1" />
                                <asp:ListItem Text="No" Value="0" />
                            </asp:DropDownList></td>
                        <td>&nbsp;&nbsp;</td>
                        <td><asp:TextBox ID="TextBox6" runat="server" Width="80px" Text="Reviewed" CssClass="TextBoxClientOverviewLeft" /></td>
                        <td><asp:DropDownList ID="ddlReviewed" runat="server" CssClass="textboxgrid" Width="60px">
                                <asp:ListItem Text="No" Value="0" />
                                <asp:ListItem Text="Yes" Value="1" />
                                <asp:ListItem Text="All" Value="%" />
                            </asp:DropDownList>
                            
                            </td>
                    </tr>
                </table>
            <asp:TextBox ID="txtSortExpression" runat="server" Visible="false" Text="ORDER BY EnteredDate, StartTime, EntityChangeLogId DESC" />
            
            </div>
        </div>   
    
                  
        <div id="gridheader">
            		<table cellspacing="0" cellpadding="4" id="ContentPlaceHolder1_GridView2" style="color:#333333;width:100%;border-collapse:collapse;">
			            <tr style="color:#EEEEEE;background-color:#6B7B8D;">
				            <th align="left" scope="col" style="width:50px;white-space:nowrap;">&nbsp;</th>
                            <th class="hiddencol" scope="col">&nbsp;</th>
                            <th class="hiddencol" scope="col">&nbsp;</th>
                            <th class="hiddencol" scope="col">&nbsp;</th>
                            <th scope="col" style="width:50px;">
                                <asp:Button ID="btnIncidentSort" runat="server" CausesValidation="false" OnClick="btnIncidentSort_Click" Text="Incident" BorderStyle="None" BorderColor="Transparent" BackColor="Transparent" Font-Names="Segoe UI" ForeColor="#EEEEEE" Font-Bold="true"  />
                            </th>
                            <th scope="col" style="width:140px;">
                                <asp:Button ID="btnCompanySort" runat="server" CausesValidation="false" OnClick="btnCompanySort_Click" Text="Company" BorderStyle="None" BorderColor="Transparent" BackColor="Transparent" Font-Names="Segoe UI" ForeColor="#EEEEEE" Font-Bold="true"  />
                            </th>
                            <th scope="col" style="width:100px;white-space:nowrap;">
                                <asp:Button ID="btnAnalystSort" runat="server" CausesValidation="false" OnClick="btnAnalystSort_Click" Text="Analyst" BorderStyle="None" BorderColor="Transparent" BackColor="Transparent" Font-Names="Segoe UI" ForeColor="#EEEEEE" Font-Bold="true"  />
                            </th>
                            <th class="MinWidth50" scope="col" style="font-family:Segoe UI;width:80px;white-space:nowrap;">
                                <asp:Button ID="btnEnteredDateSort" runat="server" CausesValidation="false" OnClick="btnEnteredDateSort_Click" Text="Date" BorderStyle="None" BorderColor="Transparent" BackColor="Transparent" Font-Names="Segoe UI" ForeColor="#EEEEEE" Font-Bold="true"  />
                            </th>
                            <th class="MinWidth50" align="center" scope="col" style="font-family:Segoe UI;width:50px;white-space:nowrap;">
                                <asp:Button ID="btnStartSort" runat="server" CausesValidation="false" OnClick="btnStartSort_Click" Text="Start" BorderStyle="None" BorderColor="Transparent" BackColor="Transparent" Font-Names="Segoe UI" ForeColor="#EEEEEE" Font-Bold="true"  />
                            </th>
                            <th scope="col" style="font-family:Segoe UI;width:45px;white-space:nowrap;">
                                <asp:Button ID="btnMinutesSort" runat="server" CausesValidation="false" OnClick="btnMinutesSort_Click" Text="Mins" BorderStyle="None" BorderColor="Transparent" BackColor="Transparent" Font-Names="Segoe UI" ForeColor="#EEEEEE" Font-Bold="true"  />
                            </th>
                            <th scope="col" style="font-family:Segoe UI;width:75px;white-space:nowrap;">
                                <asp:Button ID="btnCategorySort" runat="server" CausesValidation="false" OnClick="btnCategorySort_Click" Text="Category" BorderStyle="None" BorderColor="Transparent" BackColor="Transparent" Font-Names="Segoe UI" ForeColor="#EEEEEE" Font-Bold="true"  />
                            </th>
                            <th scope="col" style="font-family:Segoe UI;width:75px;white-space:nowrap;">
                                <asp:Button ID="btnSubCategorySort" runat="server" CausesValidation="false" OnClick="btnSubCategorySort_Click" Text="Sub" BorderStyle="None" BorderColor="Transparent" BackColor="Transparent" Font-Names="Segoe UI" ForeColor="#EEEEEE" Font-Bold="true"  />
                            </th>
                            <th scope="col" style="font-family:Segoe UI;width:50px;white-space:nowrap;"> On Site </th>
                            <th scope="col" style="font-family:Segoe UI;width:40px;white-space:nowrap;"> AHS </th>
                            <th scope="col" style="font-family:Segoe UI;width:15%;white-space:nowrap;"> Incident Title </th>
                            <th scope="col" style="font-family:Segoe UI;width:30%;white-space:nowrap;"> Comments </th>
                            <th scope="col" style="font-family:Segoe UI;width:3%;white-space:nowrap;"> NTBI </th>
                            <th scope="col" style="font-family:Segoe UI;width:3%;white-space:nowrap;"> Bill </th>
                            <th scope="col" style="font-family:Segoe UI;width:3%;white-space:nowrap;">
                                <asp:Button ID="btnDoneAll" runat="server" CausesValidation="false" OnClick="btnDoneAll_Click" Text="Done" BorderStyle="None" BorderColor="Transparent" BackColor="Transparent" Font-Names="Segoe UI" ForeColor="#EEEEEE" Font-Bold="true"  />
                            </th>
							<th scope="col"  style="font-family:Segoe UI;width:80px;white-space:nowrap;">&nbsp;</th>
			            </tr>
                     </table>
            </div>
    <div id="gridmain">
		<div id="MessageBox">
                <input id="dummy2" type="button" style="display: none" runat="server" />
                <AjaxASP:ModalPopupExtender ID="mpeMessageBox" runat="server" PopupControlID="pnlMessageBox" TargetControlID="dummy2" BackgroundCssClass="modalBackground" BehaviorID="mpeMessageBoxBehaviour" />
                <asp:Panel ID="pnlMessageBox" runat="server" CssClass="modalPopup">
                    <asp:Label ID="lblMessageBox" runat="server" />
                    <br />
                    <asp:Button ID="btnMessageBoxOK" runat="server" Text="OK" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false" OnClick="btnMessageBoxOK_Click" />
                </asp:Panel>
			<input id="dummy3" type="button" style="display: none" runat="server" />
                <AjaxASP:ModalPopupExtender ID="mpeConfirm" runat="server" PopupControlID="pnlConfirm" TargetControlID="dummy3" BackgroundCssClass="modalBackground" BehaviorID="mpeConfirmBehaviour" />
                <asp:Panel ID="pnlConfirm" runat="server" CssClass="modalPopup">
                    <table align="center">
                        <tr>
							<td colspan="4">
								<asp:Label ID="lblConfirmLine1" runat="server" />
							</td>
                        </tr>
                        <tr>
                            <td colspan="4"><asp:Label ID="lblConfirmLine2" runat="server" /></td>
                        </tr>
                        <tr><td colspan="4"><asp:Label ID="DelID" runat="server" Visible="false" /></td></tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td><asp:Button ID="btnOK" runat="server" Text="OK" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false" OnClick="btnOK_Click" /></td>
                            <td>&nbsp;</td>
                            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false" OnClick="btnCancel_Click" /></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
                    </table>
                    
                    <asp:TextBox ID="txtOrginEntityID" runat="server" Enabled="false" Width="0px" Height="0px" BorderStyle="None" />
                </asp:Panel>
        </div>
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"  
                OnRowDataBound="GridView2_RowDataBound" EmptyDataText="No entries returned" HeaderStyle-BackColor="#6B7B8D" HeaderStyle-ForeColor="#eeeeee" OnRowCommand="GridView2_RowCommand" Width="100%" ShowHeader="false" >
                <EmptyDataTemplate>No data available for the selected day</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="50px">
                            <HeaderStyle HorizontalAlign="Left" Wrap="False" Width="50px" />
                            <itemtemplate >
                                <asp:Label ID="EntityChangeLogId" runat="server" Text='<%#Bind("EntityChangeLogId") %>' CssClass="labelgrid" Width="50px" />
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                            <itemtemplate>
                                <asp:Label ID="lblCompleted" runat="server" Text='<%#Bind("Completed") %>'/>
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                            <itemtemplate>
                                <asp:Label ID="lblPeerReview" runat="server" Text='<%#Bind("PeerReview") %>'/>
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                            <itemtemplate>
                                <asp:Label ID="lblAccountsLock" runat="server" Text='<%#Bind("AccountsLock") %>'/>
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Incident" SortExpression="IncidentID" HeaderStyle-Width="50px" >
                            <itemtemplate>
                                <asp:Label ID="lblIncidentID" runat="server" Text='<%#Bind("IncidentID") %>' CssClass="labelgrid" />
                            </itemtemplate>
                            <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="140px" HeaderText=" Company " SortExpression="Company" HeaderStyle-Width="140px">
                            <itemtemplate>
                                <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("Company") %>' CssClass="labelgrid" />
                            </itemtemplate>
                            <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Created By " SortExpression="EnteredBy" HeaderStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Names="Segoe UI" HeaderStyle-Wrap="false" >
                            <itemtemplate>
                                <asp:Label ID="lblEnteredBy" runat="server" Text='<%#Bind("EnteredBy") %>' CssClass="labelgrid" />
                            </itemtemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Date " SortExpression="EnteredDate" HeaderStyle-Width="80px" ItemStyle-Width="80px">
                            <itemtemplate>
                                <asp:TextBox ID="txtDate" runat="server" Text='<%#Bind("EnteredDate") %>' CssClass="centertextboxgrid"  />
                                <AjaxASP:CalendarExtender ID="MyCalendar" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy" />
                                <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                            </itemtemplate>
                            <headerstyle wrap="False" Font-Names="Segoe UI" CssClass="MinWidth50"/>
                            <itemstyle horizontalalign="Left" CssClass="MinWidth50"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Start " HeaderStyle-Width="50px" ItemStyle-Width="50px">
                            <itemtemplate>
                                <asp:TextBox ID="txtStartTime" runat="server" Text='<%#Bind("StartTime") %>' CssClass="centertextboxgrid" />
                                <AjaxASP:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartTime" Mask="99:99:99" MaskType="Time" />
                            </itemtemplate>
                            <headerstyle wrap="False" HorizontalAlign="Center" Font-Names="Segoe UI" CssClass="MinWidth50"/>
                            <itemstyle horizontalalign="Center" CssClass="MinWidth50" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Mins " HeaderStyle-Width="45px" ItemStyle-Width="45px">
                            <itemtemplate>
                                <asp:TextBox ID="txtTimeInMinutes" runat="server" Text='<%#Bind("TimeInMinutes") %>' CssClass="centertextboxgrid" />
                            </itemtemplate>
                            <headerstyle wrap="True" Font-Names="Segoe UI"/>
                            <itemstyle horizontalalign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Cat " HeaderStyle-Width="75px" ItemStyle-Width="75px">
                            <itemtemplate>
                                <asp:TextBox ID="txtCategory" runat="server" Text='<%#Bind("Category") %>' CssClass="centertextboxgrid" ReadOnly="true" />
                            </itemtemplate>
                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            <itemstyle horizontalalign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Sub " HeaderStyle-Width="75px" ItemStyle-Width="75px">
                            <itemtemplate>
                                <asp:TextBox ID="txtSubCategory" runat="server" Text='<%#Bind("SubCategory") %>' CssClass="centertextboxgrid" ReadOnly="true" />
                            </itemtemplate>
                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            <itemstyle horizontalalign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" On Site " HeaderStyle-Width="50px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <itemtemplate>
                                <asp:CheckBox ID="cbOnsite" runat="server" Checked='<%#Bind("OnSite") %>' />
                            </itemtemplate>
                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" AHS " HeaderStyle-Width="40px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                            <itemtemplate>
                                <asp:CheckBox ID="cbAHS" runat="server" Checked='<%#Bind("AHS") %>' />
                            </itemtemplate>
                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Incident Title " HeaderStyle-Width="15%" ItemStyle-Width="15%" >
                            <itemtemplate>
                                <asp:TextBox ID="txtTitle" runat="server" Text='<%#Bind("Title") %>' CssClass="lefttextboxgrid"  TextMode="MultiLine"  BorderStyle="Solid" BorderWidth="1px" BorderColor="#e8e8ec" Wrap="true" ReadOnly="true" />
                            </itemtemplate>
                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            <itemstyle horizontalalign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Comments " HeaderStyle-Width="30%" ItemStyle-Width="30%" >
                            <itemtemplate>
                                <asp:TextBox Wrap="true" ID="txtComment" runat="server" Text='<%#Bind("Comment") %>' CssClass="lefttextboxgrid" TextMode="MultiLine"  BorderStyle="Solid" BorderWidth="1px" BorderColor="#e8e8ec" />
                            </itemtemplate>
                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                            <itemstyle horizontalalign="Left"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" NTBI " HeaderStyle-Width="3%" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" >
                            <itemtemplate>
                                <asp:CheckBox ID="cbNTBI" runat="server" Checked='<%#Bind("NotToBeInvoiced") %>'  />
                            </itemtemplate>
                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" Bill " HeaderStyle-Width="3%" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center" >
                            <itemtemplate>
                                <asp:CheckBox ID="cbBillable" runat="server" Checked='<%#Bind("Billable") %>' />
                            </itemtemplate>
                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                        </asp:TemplateField>    
                        <asp:TemplateField HeaderText=" Done " HeaderStyle-Width="3%" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                            <itemtemplate>
                                <asp:CheckBox ID="cbManagementReview" runat="server" Checked='<%#Bind("ManagementReview") %>'  />
                            </itemtemplate>
                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                        </asp:TemplateField>
						<asp:TemplateField HeaderText="" HeaderStyle-Width="2%" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" >
							<itemtemplate>
								<asp:ImageButton ID="imgBtnSplitLine" runat="server" ImageUrl="~/Images/split-vertical.png" Height="20px" Width="20px" CommandName="Split" CausesValidation="false" CommandArgument="<%#((GridViewRow)Container).RowIndex %>" />
							</itemtemplate>
							<headerstyle wrap="False" Font-Names="Segoe UI"/>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="" HeaderStyle-Width="2%" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" >
							<itemtemplate>
								<asp:ImageButton ID="imgBtnDeleteLine" runat="server" ImageUrl="~/Images/deleteBin.png" Height="20px" Width="20px" CommandName="Del" CausesValidation="false" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"/>
							</itemtemplate>
							<headerstyle wrap="False" Font-Names="Segoe UI"/>
						</asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                 <asp:HiddenField runat="server" ID="hdnEditStatus" Value='' EnableViewState="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="White" BorderStyle="Solid" BorderColor="#cccccc" BorderWidth="1px" />
                
            </asp:GridView>
            
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" >
                <progresstemplate>
                    <div id="ProgressBackground" />
                </progresstemplate>
            </asp:UpdateProgress>
        </div>
        <div id="footer">
            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/icon-behavior-save-text.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnSave_Click"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/icon-behavior-retry-text.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnRefresh_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnReview" runat="server" ImageUrl="~/Images/Review.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnReview_Click" />
            <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" >
                <progresstemplate>
                    <div id="UpdateFooterRight" class="footerright">
                        <img src="Images/icon-drawer-processing-active.gif" />
                    </div>
                </progresstemplate>
            </asp:UpdateProgress>
        </div>
    
</asp:Content>
