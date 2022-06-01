<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectList.aspx.cs" Inherits="TimeCapture.ProjectList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>
<%@ Register Assembly="BulkEditGridView" Namespace="BulkEditGridView" TagPrefix="MyAsp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

            function ShowPopup(ClientID,ClientID2) {

          
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="navigator">

    </div>
    <div id="main">

        <div id="MessageBox">
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
                                <asp:DropDownList runat="server" ID="ddlUserList" CssClass="textboxgrid" Width="150px"/>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label ID="EntityID" runat="server" Visible="false" />
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
                                <asp:GridView ID="GridProjectRates" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-BackColor="#DDDDDD" HeaderStyle-ForeColor="#465C71" EmptyDataText="No specific rates setup" OnRowCommand="GridProjectRates_RowCommand">
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
                                <asp:Label ID="lblDetails" Text="" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        
                        
                        
                    </asp:table>




                    
                    <br />
                    <asp:Button ID="btnCloseDetails" runat="server" Text="Close" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false" OnClick="btnCloseDetails_Click"/>
                </asp:Panel>

        </div>


        <asp:GridView ID="GridProjectList" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#DDDDDD" HeaderStyle-ForeColor="#465C71" Width="98%" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="GridProjectList_RowDataBound" OnRowCommand="GridProjectList_RowCommand">
            <Columns>
                <asp:CommandField SelectText="Select" ShowSelectButton="true" Visible="false" />
                <asp:TemplateField HeaderText=" Project ID " HeaderStyle-Width="140px" ItemStyle-Width="140px"  >
                    <itemtemplate>
                        <asp:Label ID="lblIncidentID" runat="server" Text='<%#Bind("IncidentID") %>' CssClass="labelgrid" />
                
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Title "  >
                    <itemtemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%#Bind("Title") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="left" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Status " >
                    <itemtemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Company "  >
                    <itemtemplate>
                        <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("CompanyName") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Owner "  >
                    <itemtemplate>
                        <asp:Label ID="lblOwner" runat="server" Text='<%#Bind("DisplayName") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Project Type "  >
                    <itemtemplate>
                        <asp:Label ID="lblProjectType" runat="server" Text='<%#Bind("SubCategory") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Hours Allocated "  >
                    <itemtemplate>
                        <asp:Label ID="lblTotalHoursAllocated" runat="server" Text='<%#Bind("TotalHoursAllocated") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText=" Hours Spent "  >
                    <itemtemplate>
                        <asp:Label ID="lblHoursSpent" runat="server" Text="0" CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Estimated Revenue "  >
                    <itemtemplate>
                        <asp:Label ID="lblEstimatedRevenue" runat="server" Text='<%#Bind("EstimatedRevenue") %>' CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Actual Revenue "  >
                    <itemtemplate>
                        <asp:Label ID="lblActualRevenue" runat="server" Text="$0" CssClass="labelgrid" />
                    </itemtemplate>
                        <headerstyle wrap="False" />
                        <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Actual Cost "  >
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
        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/Images/icon-behavior-retry-text.png" CssClass="labelgeneric" CausesValidation="false" OnClick="btnRefresh_Click"/>
        
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="0" >
            <progresstemplate>
                <div id="UpdateFooterRight" class="footerright">
                    <img src="Images/icon-drawer-processing-active.gif" />
                </div>
            </progresstemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
