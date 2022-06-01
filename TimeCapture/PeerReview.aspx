<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PeerReview.aspx.cs" Inherits="TimeCapture.PeerReview" %>
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="navigator">
        <div id="WeekHeader">
            <div id="WeekHeaderDate">
                <asp:Label ID="lblPeer" runat="server" ForeColor="DarkRed" />&nbsp;&nbsp;&nbsp;
                <asp:Label ID="CurrentDate" runat="server"  />
                
            </div>
            <div id="WeekHeaderButton1">
                <asp:ImageButton ID="btnNextWeek" runat="server" ImageUrl="~/Images/NextWeek.png" CausesValidation="false" OnClick="btnNextWeek_Click" />
            </div>
            <div id="WeekHeaderButton2">
                <asp:ImageButton ID="btnToday" runat="server" ImageUrl="~/Images/Today.png" CausesValidation="false" OnClick="btnToday_Click"  />
            </div>
                <div id="WeekHeaderButton3">
                    <asp:ImageButton ID="btnPreviousWeek" runat="server" ImageUrl="~/Images/PreviousWeek.png" CausesValidation="false" OnClick="btnPreviousWeek_Click" />
                </div>
        </div>
        <div id="WeekHeaderMain">
            <div id="Monday" runat="server" class="MondayDefault">
                <div id="MondayTime" class="DivDailyTime">
                    <asp:Label ID="lbMondayTime" CssClass="LabelDailyTimeNotEnough" runat="server"/>
                </div>
                <div id="MondayTimeRedDot" class="DivRedDot">
                    <asp:Image runat="server" ID="MondayRedDot" ImageUrl="~/Images/RedDot.png" />
                </div>
                <asp:ImageButton ID="btnMonday" runat="server" ImageUrl="~/Images/MondayButton.png" CausesValidation="false" OnClick="btnMonday_Click"/>
            </div>
            <div id="Tuesday" runat="server" class="TuesdayDefault">
                <div id="TuesdayTime" class="DivDailyTime">
                    <asp:Label ID="lbTuesdayTime" CssClass="LabelDailyTimeNotEnough" runat="server"/>
                </div>
                <div id="TuesdayTimeRedDot" class="DivRedDot">
                    <asp:Image runat="server" ID="TuesdayRedDot" ImageUrl="~/Images/RedDot.png" />
                </div>
                <asp:ImageButton ID="btnTuesday" runat="server" ImageUrl="~/Images/TuesdayButton.png" CausesValidation="false" OnClick="btnTuesday_Click"/>
            </div>
            <div id="Wednesday" runat="server" class="WednesdayDefault">
                <div id="WednesdayTime" class="DivDailyTime">
                    <asp:Label ID="lbWednesdayTime" CssClass="LabelDailyTimeNotEnough" runat="server"/>
                </div>
                <div id="WednesdayTimeRedDot" class="DivRedDot">
                    <asp:Image runat="server" ID="WednesdayRedDot" ImageUrl="~/Images/RedDot.png" />
                </div>
                <asp:ImageButton ID="btnWednesday" runat="server" ImageUrl="~/Images/WednesdayButton.png" CausesValidation="false" OnClick="btnWednesday_Click" />
            </div>
            <div id="Thursday" runat="server" class="ThursdayDefault">
                <div id="ThursdayTime" class="DivDailyTime">
                    <asp:Label ID="lbThursdayTime" CssClass="LabelDailyTimeNotEnough" runat="server"/>
                </div>
                <div id="ThursdayTimeRedDot" class="DivRedDot">
                    <asp:Image runat="server" ID="ThursdayRedDot" ImageUrl="~/Images/RedDot.png" />
                </div>
                <asp:ImageButton ID="btnThursday" runat="server" ImageUrl="~/Images/ThursdayButton.png" CausesValidation="false" OnClick="btnThursday_Click" />
            </div>
            <div id="Friday" runat="server" class="FridayDefault"> 
                <div id="FridayTime" class="DivDailyTime">
                    <asp:Label ID="lbFridayTime" CssClass="LabelDailyTimeNotEnough" runat="server"/>
                </div>
                <div id="FridayTimeRedDot" class="DivRedDot">
                    <asp:Image runat="server" ID="FridayRedDot" ImageUrl="~/Images/RedDot.png" />
                </div>
                <asp:ImageButton ID="btnFriday" runat="server" ImageUrl="~/Images/FridayButton.png" CausesValidation="false" OnClick="btnFriday_Click"/>
            </div>
            <div id="Saturday" runat="server" class="SaturdayDefault">
                <div id="SaturdayTime" class="DivDailyTime">
                    <asp:Label ID="lbSaturdaytime" CssClass="LabelDailyTime" runat="server"/>
                </div>
                <div id="SaturdayTimeRedDot" class="DivRedDot">
                    <asp:Image runat="server" ID="SaturdayRedDot" ImageUrl="~/Images/RedDot.png" />
                </div>
                <asp:ImageButton ID="btnSaturday" runat="server" ImageUrl="~/Images/SaturdayButton.png" CausesValidation="false" OnClick="btnSaturday_Click" />
            </div>
            <div id="Sunday" runat="server" class="SundayDefault">
                <div id="SundayTime" class="DivDailyTime">
                    <asp:Label ID="lbSundayTime" CssClass="LabelDailyTime" runat="server"/>
                </div>
                <div id="SundayTimeRedDot" class="DivRedDot">
                    <asp:Image runat="server" ID="SundayRedDot" ImageUrl="~/Images/RedDot.png" />
                </div>
                <asp:ImageButton ID="btnSunday" runat="server" ImageUrl="~/Images/SundayButton.png" CausesValidation="false" OnClick="btnSunday_Click"/>
            </div>
            <div id="Week" runat="server" class="WeekDefault">
                <div id="WeekTime" class="DivWeeklyTime">
                    <asp:Label ID="lbWeekTime" CssClass="LabelDailyTimeNotEnough" runat="server"/>
                </div>
                <asp:ImageButton ID="btnWeek" runat="server" ImageUrl="~/Images/WeekButton.png" CausesValidation="false" OnClick="btnWeek_Click" />
            </div>
        </div>
        <div id="FilterImage">
           <asp:DropDownList ID="ddlCompleted" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompleted_SelectedIndexChanged" CssClass="textboxgrid" >
                <asp:ListItem Text="No" Value="0" />
                <asp:ListItem Text="Yes" Value="1" />
                <asp:ListItem Text="All" Value="%" />
            </asp:DropDownList>
        </div>
            <asp:TextBox ID="txtSortExpression" runat="server" Visible="false" Text="ORDER BY EnteredDate, StartTime DESC" />
            <asp:TextBox ID="txtCurrentDayOfWeek" runat="server" Visible="false" Text="" />
            <asp:TextBox ID="txtDaySelected" runat="server" Visible="false" Text="" />
    </div>
    <div id="main">
        <div id="MessageBox">
            <input id="dummy2" type="button" style="display: none" runat="server" />
            <AjaxASP:ModalPopupExtender ID="mpeMessageBox" runat="server" PopupControlID="pnlMessageBox" TargetControlID="dummy2" BackgroundCssClass="modalBackground" />
            <asp:Panel ID="pnlMessageBox" runat="server" CssClass="modalPopup">
                <asp:Label ID="lblMessageBox" runat="server" />
                <br />
                <asp:Button ID="btnMessageBoxOK" runat="server" Text="OK" CssClass="button_header" Width="50px" Height="20px" CausesValidation="false" OnClick="btnMessageBoxOK_Click" />
            </asp:Panel>
        </div>
        <br />
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowUpdating="GridView2_RowUpdating" CssClass="GridLayout" 
            AllowSorting="True" OnSorting="GridView2_Sorting" OnRowDataBound="GridView2_RowDataBound" HeaderStyle-BackColor="#DDDDDD" HeaderStyle-ForeColor="#465C71" Width="98%" >
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                    <itemtemplate>
                        <asp:TextBox ID="lblEntityChangeLogId" runat="server" Text='<%#Bind("EntityChangeLogId") %>' />
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                    <itemtemplate>
                        <asp:Label ID="lblManagementReview" runat="server" Text='<%#Bind("ManagementReview") %>'/>
                    </itemtemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                    <itemtemplate>
                        <asp:Label ID="lblCompleted" runat="server" Text='<%#Bind("Completed") %>'/>
                    </itemtemplate>
                </asp:TemplateField >
                <asp:TemplateField HeaderText="Incident" SortExpression="IncidentID" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                    <itemtemplate>
                        <asp:Label ID="lblIncidentID" runat="server" Text='<%#Bind("IncidentID") %>' CssClass="labelgrid" />
                    </itemtemplate>
                    <headerstyle wrap="False" />
                    <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Company " SortExpression="Company" HeaderStyle-Width="140px" ItemStyle-Width="140px"  >
                    <itemtemplate>
                        <asp:Label ID="lblCompany" runat="server" Text='<%#Bind("Company") %>' CssClass="labelgrid" />
                    </itemtemplate>
                    <headerstyle wrap="False" />
                    <itemstyle horizontalalign="Center" Font-Names="Segoe UI" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date" SortExpression="EnteredDate" HeaderStyle-Width="80px" ItemStyle-Width="80px" >
                    <itemtemplate>
                        <asp:Textbox ID="txtDate" runat="server" Text='<%#Bind("EnteredDate") %>' CssClass="centertextboxgrid" ReadOnly="true"  />
                        <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                    </itemtemplate>
                    <headerstyle wrap="False" Font-Names="Segoe UI" CssClass="MinWidth50"/>
                    <itemstyle horizontalalign="Left" CssClass="MinWidth50"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Time" HeaderStyle-Width="50px" ItemStyle-Width="50px" >
                    <itemtemplate>
                        <asp:Label ID="txtStartTime" runat="server" Text='<%#Bind("StartTime") %>' CssClass="centertextboxgrid" />
                    </itemtemplate>
                    <headerstyle wrap="False" HorizontalAlign="Center" Font-Names="Segoe UI" CssClass="MinWidth50"/>
                    <itemstyle horizontalalign="Center" CssClass="MinWidth50" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mins" HeaderStyle-Width="50px" ItemStyle-Width="50px" >
                    <itemtemplate>
                        <asp:Label ID="txtTimeInMinutes" runat="server" Text='<%#Bind("TimeInMinutes") %>' CssClass="centertextboxgrid" ReadOnly="true" />
                    </itemtemplate>
                    <headerstyle wrap="False" Font-Names="Segoe UI"/>
                    <itemstyle horizontalalign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Cat " HeaderStyle-Width="75px" ItemStyle-Width="75px" >
                    <itemtemplate>
                        <asp:Label ID="txtCategory" runat="server" Text='<%#Bind("Category") %>' CssClass="centertextboxgrid" ReadOnly="true" Visible="true" />
                    </itemtemplate>
                    <headerstyle wrap="False" Font-Names="Segoe UI"/>
                    <itemstyle horizontalalign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" Sub " HeaderStyle-Width="75px" ItemStyle-Width="75px" >
                    <itemtemplate>
                        <asp:Label ID="txtSubCategory" runat="server" Text='<%#Bind("SubCategory") %>' CssClass="centertextboxgrid" ReadOnly="true" Visible="true" />
                    </itemtemplate>
                    <headerstyle wrap="False" Font-Names="Segoe UI"/>
                    <itemstyle horizontalalign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="On Site" HeaderStyle-Width="50px" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" >
                    <itemtemplate>
                        <asp:CheckBox ID="cbOnsite" runat="server" Checked='<%#Bind("OnSite") %>' Enabled="false" />
                    </itemtemplate>
                    <headerstyle wrap="False" Font-Names="Segoe UI"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Incident Title" HeaderStyle-Width="20%" ItemStyle-Width="20%" >
                    <itemtemplate>
                        <asp:TextBox ID="txtTitle" Wrap="true" runat="server" Text='<%#Bind("Title") %>' CssClass="lefttextboxgrid" ReadOnly="true" TextMode="MultiLine"  BorderStyle="Solid" BorderWidth="1px" BorderColor="#e8e8ec" />
                    </itemtemplate>
                    <headerstyle wrap="False" Font-Names="Segoe UI"/>
                    <itemstyle horizontalalign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comments" HeaderStyle-Width="35%" ItemStyle-Width="35%" >
                    <itemtemplate>
                        <asp:TextBox Wrap="true" ID="txtComment" runat="server" Text='<%#Bind("Comment") %>' CssClass="lefttextboxgrid" TextMode="MultiLine"  BorderStyle="Solid" BorderWidth="1px" BorderColor="#e8e8ec" ReadOnly="true" />
                    </itemtemplate>
                    <headerstyle wrap="False" Font-Names="Segoe UI"/>
                    <itemstyle horizontalalign="Left"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Done" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" >
                    <itemtemplate>
                        <asp:CheckBox ID="cbPeerReview" runat="server" Checked='<%#Bind("PeerReview") %>' />
                    </itemtemplate>
                    <headerstyle wrap="False" Font-Names="Segoe UI"/>
                </asp:TemplateField>  
                <asp:TemplateField>
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
