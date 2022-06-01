<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectManagement.aspx.cs" Inherits="TimeCapture.ProjectManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>
<%@ Register Assembly="BulkEditGridView" Namespace="BulkEditGridView" TagPrefix="MyAsp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=icon-chevron-collapsed]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/icon-chevron-expanded.png");
        });
        $("[src*=icon-chevron-expanded]").live("click", function () {
            $(this).attr("src", "images/icon-chevron-collapsed.png");
            $(this).closest("tr").next().remove();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="navigator">
        <div id="PanelHeader">
            <asp:table runat="server" align="center">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="TextBox1" runat="server" Width="80px" Text="Status" CssClass="TextBoxClientOverviewLeft" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="textboxgrid" Width="155px">
                                <asp:ListItem Text="Active" Value="Active" />    
                                <asp:ListItem Text="Closed" Value="Closed" />
                                <asp:ListItem Text="Pending" Value="Pending" />
                                <asp:ListItem Text="All" Value="%" />
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell>
                        &nbsp;&nbsp;
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TextBox2" runat="server" Width="80px" Text="Company" CssClass="TextBoxClientOverviewLeft" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlCompany" runat="server" CssClass="textboxgrid" Width="155px" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="TextBox3" runat="server" Width="80px" Text="Owner" CssClass="TextBoxClientOverviewLeft" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlOwner" runat="server" CssClass="textboxgrid" Width="155px" />
                    </asp:TableCell>
                    <asp:TableCell>
                        &nbsp;&nbsp;
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="TextBox4" runat="server" Width="80px" Text="Project Type" CssClass="TextBoxClientOverviewLeft" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="textboxgrid" Width="155px">
                                <asp:ListItem Text="All" Value="PS%" />
                                <asp:ListItem Text="T&M Monthly" Value="PS - T&M Monthly" />    
                                <asp:ListItem Text="T&M End" Value="PS - T&M End" />
                                <asp:ListItem Text="T&M PrePaid" Value="PS - T&M PrePaid" />
                                <asp:ListItem Text="Fixed Price" Value="PS - Fixed Price" />
                                <asp:ListItem Text="Internal" Value="PS - Internal" />
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="TextBox5" runat="server" Width="80px" Text="Excl. Internal" CssClass="TextBoxClientOverviewLeft" />
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:CheckBox ID="cbExcludeInternal" runat="server" Checked="true"/>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:table>
                        
                            
        </div>
    </div>
    <div id="main">

        <div id="MessageBox">
                <input id="dummy3" type="button" style="display: none" runat="server" />
                <AjaxASP:ModalPopupExtender ID="mpeNewProject" runat="server" PopupControlID="pnlNewProject" TargetControlID="dummy3" BackgroundCssClass="modalBackground" BehaviorID="mpeConfirmBehaviour" />
                <asp:Panel ID="pnlNewProject" runat="server" CssClass="modalPopup">
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
                            <td><asp:Button ID="btnOK" runat="server" Text="OK" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false" /></td>
                            <td>&nbsp;</td>
                            <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false"  /></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
                    </table>
                    
                    <asp:TextBox ID="txtOrginEntityID" runat="server" Enabled="false" Width="0px" Height="0px" BorderStyle="None" />
                </asp:Panel>

            <input id="DummyRates" type="button" style="display: none" runat="server" />
                <AjaxASP:ModalPopupExtender ID="mpeMessageBox" runat="server" PopupControlID="pnlMessageBox" TargetControlID="DummyRates" BackgroundCssClass="modalBackground" BehaviorID="mpeMessageBoxBehaviour" />
                <asp:Panel ID="pnlMessageBox" runat="server" CssClass="modalPopup">
                    <asp:table runat="server" align="center">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="4">
                                <asp:Label ID="lblMessageBoxTitle" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="lblHeaderUser" Text="User" Width="150px" CssClass="labelgrid"/>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblHeaderOnsite" runat="server" Text="Onsite" Width="60px" CssClass="labelgrid"/>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="lblHeaderOffsite" runat="server" Text="Offsite" Width="60px" CssClass="labelgrid"/>
                            </asp:TableCell>
                            <asp:TableCell>
                                
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:DropDownList runat="server" ID="ddlUserList" CssClass="textboxgrid" Width="150px"/>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="EntityID" runat="server" Visible="false" />
                                <asp:Label ID="MyLabelIncidentID" runat="server" Visible="false" />
                                <asp:TextBox ID="txtOnsiteRate" runat="server" CssClass="textboxgrid" Width="60px"/>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtOffsiteRate" runat="server" CssClass="textboxgrid" Width="60px"/>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Button ID="btnAddUser" runat="server" Text="Add" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false" OnClick="btnAddUser_Click"/>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell  ColumnSpan="4">
                                <asp:GridView ID="GridProjectRates" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-BackColor="#DDDDDD" HeaderStyle-ForeColor="#465C71" EmptyDataText="No specific rates setup" OnRowCommand="GridProjectRates_RowCommand" RowStyle-Font-Names="Segoe UI">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Users" HeaderStyle-Width="300px" ItemStyle-Width="300px">
                                            <itemtemplate >
                                                <asp:Label ID="lblId" runat="server" Text='<%#Bind("Id") %>' Visible="false" />
                                                <asp:Label ID="DisplayName" runat="server" Text='<%#Bind("DisplayName") %>'/>
                                            </itemtemplate>
                                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                                            <itemstyle horizontalalign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Onsite" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                                            <itemtemplate>
                                                <asp:Label ID="OnsiteRate" runat="server" Text='<%#Bind("OnsiteRate") %>'/>
                                            </itemtemplate>
                                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                                            <itemstyle horizontalalign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Offsite" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                                            <itemtemplate>
                                                <asp:Label ID="OffsiteRate" runat="server" Text='<%#Bind("OffsiteRate") %>'/>
                                            </itemtemplate>
                                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                                            <itemstyle horizontalalign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="30px" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" >
                                            <itemtemplate>
						                        <asp:ImageButton ID="imgBtnDeleteLine" runat="server" ImageUrl="~/Images/deleteBin.png" Height="20px" Width="20px" CommandName="Del" CausesValidation="false" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"/>
                                            </itemtemplate>
                                            <headerstyle wrap="False" Font-Names="Segoe UI"/>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:TableCell>
                        </asp:TableRow>
                        
                    </asp:table>




                    
                    <br />
                    <asp:Button ID="btnMessageBoxOK" runat="server" Text="Close" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false" OnClick="btnMessageBoxOK_Click"/>
                </asp:Panel>

                <input id="DummyDetails" type="button" style="display: none" runat="server" />
                <AjaxASP:ModalPopupExtender ID="mpeProjectDetails" runat="server" PopupControlID="pnlProjectDetails" TargetControlID="DummyDetails" BackgroundCssClass="modalBackground" BehaviorID="mpeProjectDetailsBehaviour" />
                <asp:Panel ID="pnlProjectDetails" runat="server" CssClass="modalPopup">
                    <asp:table runat="server" align="center">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="lblIncident" runat="server" Visible="false" />
                                <asp:Label ID="lblDetails" Text="" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label1" Text="Budget" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtEstimatedRevenue" runat="server" CssClass="textboxgrid" Width="100px"/>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label2" Text="Hours Allocated" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtHoursAllocated" runat="server" CssClass="textboxgrid" Width="100px"/>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label3" Text="Invoiced" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtInvoiced" runat="server" CssClass="textboxgrid" Width="100px"/>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:table>
                    
                    <br />
                    <asp:Button ID="btnCloseDetails" runat="server" Text="Close" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false" OnClick="btnCloseDetails_Click"/>
                    <asp:Button ID="btnSaveDetails" runat="server" Text="Save" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false" OnClick="btnSaveDetails_Click"/>
                </asp:Panel>


        </div>


        <asp:GridView ID="GridProjectList" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#DDDDDD" HeaderStyle-ForeColor="#465C71" Width="98%" CellPadding="4" ForeColor="#333333" AlternatingRowStyle-BackColor="#f8f7f6" GridLines="None" OnRowCommand="GridProjectList_RowCommand" OnRowDataBound="GridProjectList_RowDataBound">
            <Columns>
                <asp:CommandField SelectText="Select" ShowSelectButton="true" Visible="false" />

                <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-Width="20px" HeaderStyle-Font-Names="Segoe UI" >
                    <ItemTemplate>
                        <img alt = "" style="cursor: pointer" src="images\icon-chevron-collapsed.png" />
                        <asp:Panel ID="pnlEntries" runat="server" Style="display: none">
                            <asp:GridView ID="gvEntries" runat="server" AutoGenerateColumns="false" Width="80%" HeaderStyle-BackColor="#465C71" HeaderStyle-ForeColor="#DDDDDD" ForeColor="#333333" AlternatingRowStyle-BackColor="#f8f7f6" GridLines="None" 
                                HeaderStyle-Font-Names="Segoe UI" HeaderStyle-Font-Size="10px" HeaderStyle-Font-Bold="false" RowStyle-Font-Names="Segoe UI" RowStyle-Font-Size="10px" AlternatingRowStyle-Font-Names="Segoe UI" AlternatingRowStyle-Font-Size="10px">
                                <Columns>
                                    <asp:TemplateField HeaderText=" Entered By " HeaderStyle-Width="140px" ItemStyle-Width="140px">
                                        <itemtemplate>
                                            <asp:Label ID="lblEnteredBy" runat="server" Text='<%#Bind("EnteredBy") %>' />
                                        </itemtemplate>
                                            <headerstyle wrap="False" />
                                            <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Date " HeaderStyle-Width="80px" ItemStyle-Width="80px" >
                                        <itemtemplate>
                                            <asp:label ID="lblEnteredDate" runat="server" Text='<%#ReformatDate(Eval("EnteredDate", "{0}")) %>'  />
                                        </itemtemplate>
                                            <headerstyle wrap="False" />
                                            <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Mins " HeaderStyle-Width="45px" ItemStyle-Width="45px"  >
                                        <itemtemplate>
                                            <asp:Label ID="lblTimeInMinutes" runat="server" Text='<%#Bind("TimeInMinutes") %>' />
                                        </itemtemplate>
                                            <headerstyle wrap="False" />
                                            <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Comment "   HeaderStyle-HorizontalAlign="Left" >
                                        <itemtemplate>
                                            <asp:Label ID="lblComment" runat="server" Text='<%#Bind("Comment") %>' />
                                        </itemtemplate>
                                            <headerstyle wrap="False" />
                                            <itemstyle horizontalalign="left" Font-Names="Segoe UI" />
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:TemplateField>





                <asp:TemplateField HeaderText=" Project ID " HeaderStyle-Width="140px" ItemStyle-Width="140px" HeaderStyle-Font-Names="Segoe UI" >
                    <itemtemplate>
                        <asp:Label ID="lblIncidentID" runat="server" Text='<%#Bind("IncidentID") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Title " HeaderStyle-Font-Names="Segoe UI"   >
                    <itemtemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Bind("Title") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Status "  HeaderStyle-Font-Names="Segoe UI" >
                    <itemtemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Company "  HeaderStyle-Font-Names="Segoe UI"  >
                    <itemtemplate>
                        <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("CompanyName") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Owner "   HeaderStyle-Font-Names="Segoe UI" >
                    <itemtemplate>
                        <asp:Label ID="lblOwner" runat="server" Text='<%#Bind("DisplayName") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Project Type "   HeaderStyle-Font-Names="Segoe UI" >
                    <itemtemplate>
                        <asp:Label ID="lblProjectType" runat="server" Text='<%#Bind("SubCategory") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Hrs Allocated " HeaderStyle-Font-Names="Segoe UI"   >
                    <itemtemplate>
                        <asp:Label ID="lblTotalHoursAllocated" runat="server" Text='<%#Bind("TotalHoursAllocated") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText=" Hrs Billed " ItemStyle-Width="100px" ItemStyle-Wrap="false"  HeaderStyle-Font-Names="Segoe UI" >
                    <itemtemplate>
                        <asp:Label ID="lblHoursBilled" runat="server" Text="0" CssClass="labelgrid" Width="100px" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" Wrap="false" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Hrs Logged " ItemStyle-Width="100px" ItemStyle-Wrap="false"  HeaderStyle-Font-Names="Segoe UI" >
                    <itemtemplate>
                        <asp:Label ID="lblHoursLogged" runat="server" Text="0" CssClass="labelgrid" Width="100px" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" Wrap="false" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Budgeted "  HeaderStyle-Font-Names="Segoe UI"  >
                    <itemtemplate>
                        <asp:Label ID="lblEstimatedRevenue" runat="server" Text='<%#Bind("EstimatedRevenue","{0:$0}") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Invoiced "  HeaderStyle-Font-Names="Segoe UI"  >
                    <itemtemplate>
                        <asp:Label ID="lblActualRevenue" runat="server" Text="$0" CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Costs "  HeaderStyle-Font-Names="Segoe UI"  >
                    <itemtemplate>
                        <asp:Label ID="lblActualCost" runat="server" Text="$0" CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" HeaderStyle-Width="2%" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" >
                    <itemtemplate>
                        <asp:Button ID="btnDetails" runat="server" Text="Details" CssClass="button_header" Width="50px" Height="20px" CommandName="DisplayDetails" CausesValidation="false" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"/>
                    </itemtemplate>
                    <headerstyle wrap="False" Font-Names="Segoe UI"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" HeaderStyle-Width="2%" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" >
                    <itemtemplate>
                        <asp:Button ID="btnRates" runat="server" Text="Rates" CssClass="button_header" Width="50px" Height="20px" CommandName="DisplayRates" CausesValidation="false" CommandArgument="<%#((GridViewRow)Container).RowIndex %>"/>
                    </itemtemplate>
                    <headerstyle wrap="False" Font-Names="Segoe UI"/>
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
        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/icon-behavior-retry-text.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnRefresh_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/AddProject.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnAdd_Click" />
        
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" >
            <progresstemplate>
                <div id="UpdateFooterRight" class="footerright">
                    <img src="Images/icon-drawer-processing-active.gif" />
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
    </div>

</asp:Content>
