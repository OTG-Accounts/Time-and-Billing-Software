<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" Inherits="_Default" Codebehind="Default.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>

 <%@ Register Assembly="BulkEditGridView" Namespace="BulkEditGridView" TagPrefix="MyAsp" %> 


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">




</script>
    
    <style type="text/css">
        .style2
        {
            width: 65px;
        }
        .style3
        {
            width: 72px;
        }
        .style11
        {
            width: 479px;
        }
        .style14
        {
            width: 85px;
        }
        .style19
        {
            width: 474px;
        }
    </style>
    
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


        <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    
    
<div class="testdiv">

    <AjaxASP:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" UseVerticalStripPlacement="true" CssClass="tabs">
        <AjaxASP:TabPanel runat="server" HeaderText="Time Capture" ID="TabPanel1"  >
        <HeaderTemplate>
Time Capture
            
</HeaderTemplate>
        



<ContentTemplate>
        

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

      <div class="tabdiv">

<table width="100%">
    <tr>
        <td class="style2">
            
        </td>
        <td class="style3">
            
        </td>
        <td>
        
        
        </td>
        <td align="right">

            <asp:Panel ID="PnlFilterHeader" runat="server" Font-Names="Calibri" 
                Height="16px" Width="173px">

        <asp:Label ID="lblFilterHeader" runat="server" />
    </asp:Panel>
    
        <asp:Panel ID="pnlFilterBody" runat="server" Height="0px" overflow="hidden" 
                Width="177px">
        <table>
            <tr>
                <td>Completed</td>
                <td><asp:DropDownList ID="ddlCompleted" runat="server" AutoPostBack="True" 
                        CssClass="input_ddl" 
                        onselectedindexchanged="ddlCompleted_SelectedIndexChanged">
            <asp:ListItem Text="No" Value="0" />
            <asp:ListItem Text="Yes" Value="1" />
        </asp:DropDownList></td>
            </tr>
            <tr>
                <td>From date
                </td>
                <td>
                <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="True" 
                        CssClass="input_txt" ontextchanged="txtFromDate_TextChanged" 
                        Width="70px" />
                    <AjaxASP:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtFromDate" 
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                    </AjaxASP:MaskedEditExtender>
            <AjaxASP:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="txtFromDate" Enabled="True" />
                </td>
            </tr>
            <tr>
                <td>To date
                </td>
                <td>
            <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="True" CssClass="input_txt" 
                        ontextchanged="txtToDate_TextChanged" Width="70px" />
            <AjaxASP:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="txtToDate" Enabled="True" />    
                </td>
            </tr>

        </table>    
            
        </asp:Panel>

    <AjaxASP:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" 
                CollapseControlID="pnlFilterHeader" Collapsed="True" 
                CollapsedText="Show Filters" ExpandControlID="pnlFilterHeader" 
                ExpandedText="Hide Filter" TargetControlID="pnlFilterBody" 
                TextLabelID="lblFilterHeader" Enabled="True">
    </AjaxASP:CollapsiblePanelExtender>
        </td>

    </tr>
</table>            
        
            
               

               <MyAsp:BulkEditGridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                BulkEdit="False" OnPageIndexChanging="grdComment_PageIndexChanging" 
            CellPadding="4" ForeColor="#333333" GridLines="None" 
            OnRowUpdating="grdComment_RowUpdating" CssClass="GridLayout" 
            AllowSorting="True" OnSorting="GridView1_Sorting" BorderColor="#F0F3F4" 
            BorderStyle="Solid" BorderWidth="1px"  
             >
                   <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="EntityChangeLogId" ReadOnly="True" >
                    <HeaderStyle HorizontalAlign="Left" Width="1px" Wrap="False" />
                    <ItemStyle ForeColor="LightGray" HorizontalAlign="Left" Width="1px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Incident" SortExpression="IncidentID" >
                        <itemtemplate>
                            <asp:Label ID="lblIncidentID" runat="server" Text='<%#Bind("IncidentID") %>' />
                        </itemtemplate>
                        <headerstyle wrap="False" Width="50px" />
                        <itemstyle horizontalalign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" SortExpression="EnteredDate">
                        <itemtemplate>
                            <asp:TextBox ID="txtDate" runat="server" Text='<%#Bind("EnteredDate") %>' CssClass="input_txt" Width="70px" />
                            <AjaxASP:CalendarExtender ID="MyCalendar" runat="server" TargetControlID="txtDate" Format="dd/MM/yyyy" />
                            <AjaxASP:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDate" UserDateFormat="DayMonthYear" Mask="99/99/9999" MaskType="Date"  />
                        </itemtemplate>
                        <headerstyle wrap="False" Width="50px"/>
                        <itemstyle horizontalalign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Time">
                        <itemtemplate>
                             <asp:TextBox ID="txtStartTime" runat="server" Text='<%#Bind("StartTime") %>' CssClass="input_txt" Width="50px"/>
                             <AjaxASP:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartTime" Mask="99:99:99" MaskType="Time" />
                        </itemtemplate>
                        <headerstyle wrap="False" HorizontalAlign="Center" Width="50px" />
                        <itemstyle horizontalalign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mins">
                        <itemtemplate>
                            <asp:TextBox ID="txtTimeInMinutes" runat="server" Text='<%#Bind("TimeInMinutes") %>' CssClass="input_txt" Width="30px" />
                        </itemtemplate>
                        <headerstyle wrap="False" Width="30px"/>
                        <itemstyle horizontalalign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="O">
                        <itemtemplate>
                            <asp:CheckBox ID="cbOnsite" runat="server" Checked='<%#Bind("OnSite") %>' Width="20px"/>
                        </itemtemplate>
                        <headerstyle wrap="False" Width="20px"/>
                        <itemstyle horizontalalign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Incident Title">
                        <itemtemplate>
                            <asp:TextBox ID="txtTitle" runat="server" Text='<%#Bind("Title") %>' Width="300px" TextMode="MultiLine" CssClass="input_Comment" Font-Names="calibri" ReadOnly="true"/>
                        </itemtemplate>
                        <headerstyle wrap="False" Width="300px"/>
                        <itemstyle horizontalalign="Left"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments">
                        <itemtemplate>
                            <asp:TextBox Wrap="true" ID="txtComment" runat="server" Text='<%#Bind("Comment") %>' Width="590px" TextMode="MultiLine" CssClass="input_Comment" Font-Names="calibri"/>
                        </itemtemplate>
                        <headerstyle wrap="False" Width="590px"/>
                        <itemstyle horizontalalign="Left"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="D">
                        <itemtemplate>
                            <asp:CheckBox ID="cbCompleted" runat="server" Checked='<%#Bind("Completed") %>' Width="20px"/>
                        </itemtemplate>
                        <headerstyle wrap="False" Width="20px"/>
                        <itemstyle horizontalalign="Left" />
                    </asp:TemplateField>                    
                                        
                                        
                                        
                                        
                                        
                                        
                                        
                </Columns>
                   <EditRowStyle BackColor="#999999" />
                   <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                   <HeaderStyle Font-Bold="True" BorderColor="#465767" BorderStyle="None" 
                       CssClass="HeaderBackGround" ForeColor="#465C71" />
                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                   <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                   <SortedAscendingCellStyle BackColor="#E9E7E2" />
                   <SortedAscendingHeaderStyle BackColor="#506C8C" />
                   <SortedDescendingCellStyle BackColor="#FFFDF8" />
                   <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </MyAsp:BulkEditGridView>


    
        
     


         
           </div>     

        








        
        
</ContentTemplate>
            
        



</AjaxASP:TabPanel>
        <AjaxASP:TabPanel runat="server" HeaderText="Admin" ID="TabPanel2">
        <ContentTemplate>
        <div class="tabdiv">
            <asp:Panel ID="Panel1" runat="server" Width="300px" Height="300px">
            </asp:Panel>
            </div>
            
</ContentTemplate>
            
        



</AjaxASP:TabPanel>
    </AjaxASP:TabContainer>
        
    
  </div>
    
  <div class="footer">
  <table width="100%" height="100%">
  <tr>
    <td class="style19">
    </td>
    <td class="style14">
    </td>
    <td class="style14">
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/icon-behavior-save-text.png" onclick="btnUpdate_Click" />
    </td>
    <td class="style14">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/icon-behavior-retry-text.png"  onclick="btnRefresh_Click"/>
    </td>
    <td class="style14">
        &nbsp;</td>
    <td align="right">
    <asp:UpdateProgress ID="UpdateProgress" runat="server" DisplayAfter="0" 
                DynamicLayout="False">
                                    <progresstemplate>
                                        <img src="Images/icon-drawer-processing-active.gif" />
                                        
                                    </progresstemplate>
                                </asp:UpdateProgress>
    </td>
  </tr>
  </table>
</div>
      
      
      
      </ContentTemplate>
</asp:UpdatePanel>  
</asp:Content>
