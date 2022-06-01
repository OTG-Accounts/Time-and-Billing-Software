<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForDesign.aspx.cs" Inherits="TimeCapture.ForDesign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxASP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 89px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    

        
        
                <AjaxASP:RoundedCornersExtender ID="rceCheckEntriesReviewed" runat="server" TargetControlID="pnlCheckEntriesReviewed" Corners="All" Radius="6" BorderColor="#c0c0c0" />
                <asp:Panel ID="pnlCheckEntriesReviewed" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="#c0c0c0" Width="600px" Height="79px" HorizontalAlign="Center" Style="margin-left:40%;" Visible="false" Enabled="false">
                    <table align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td rowspan="3" class="auto-style1">
                                <asp:Image ID="image1" runat="server" ImageUrl="~/Images/Warning_XL.png" />
                            </td>
                            <td colspan="2"><asp:TextBox ID="txt123" runat="server" Text="Records were found that are not reviewed in the selected period" BorderStyle="None" Width="505" style="text-align:center;font-size:16px" Height="30px" /></td>
                        </tr>
                        <tr>
                            <td colspan="2"><hr /></td>
                            
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="cbEntriesNotReviewedACK" runat="server" Text="Acknowledge warning" /></td>
                            <td><asp:CheckBox ID="cbIncludeEntriesNotReviewed" runat="server" Text="Include entries in export" /></td>
                        </tr>
                    </table>
                    
                    
                </asp:Panel>
                <AjaxASP:RoundedCornersExtender ID="rceAllEntriesReviewed" runat="server" TargetControlID="pnlAllEntriesReviewed" Corners="All" Radius="6" BorderColor="#c0c0c0" />
                <asp:Panel ID="pnlAllEntriesReviewed" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="#c0c0c0" Width="600px" Height="79px" HorizontalAlign="Center" Style="margin-left:40%;" Visible="false" Enabled="false">
                    <table align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td rowspan="3" class="auto-style1">
                                <asp:Image ID="image3" runat="server" ImageUrl="~/Images/OK_XL.png" />
                            </td>
                            <td colspan="2"><asp:TextBox ID="TextBox21" runat="server" Text="All records in the selected period are reviewed" BorderStyle="None" Width="505" style="text-align:center;font-size:16px" Height="30px" /></td>
                        </tr>
                        <tr>
                            <td colspan="2"><hr /></td>
                            
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    
                    
                </asp:Panel>
        
            <AjaxASP:RoundedCornersExtender ID="rceEntriesFromPreviousPeriods" runat="server" TargetControlID="pnlEntriesFromPreviousPeriods" Corners="All" Radius="6" BorderColor="#c0c0c0" />
                <asp:Panel ID="pnlEntriesFromPreviousPeriods" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="#c0c0c0" Width="600px" Height="79px" HorizontalAlign="Center" Style="margin-left:40%;" Visible="false" Enabled="false">
                    <table align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td rowspan="3" class="auto-style1">
                                <asp:Image ID="image2" runat="server" ImageUrl="~/Images/Warning_XL.png" />
                            </td>
                            <td colspan="2"><asp:TextBox ID="TextBox20" runat="server" Text="Records were found from previous periods" BorderStyle="None" Width="505" style="text-align:center;font-size:16px" Height="30px"/></td>
                        </tr>
                        <tr>
                            <td colspan="2"><hr /></td>
                            
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="CheckBox1" runat="server" Text="Acknowledge warning" /></td>
                            <td><asp:CheckBox ID="CheckBox2" runat="server" Text="Include entries in export" /></td>
                        </tr>
                    </table>
                    
                    
                </asp:Panel>
                        <AjaxASP:RoundedCornersExtender ID="rceNoEntriesInPreviousPeriods" runat="server" TargetControlID="pnlNoEntriesInPreviousPeriods" Corners="All" Radius="6" BorderColor="#c0c0c0" />
                <asp:Panel ID="pnlNoEntriesInPreviousPeriods" runat="server" BorderStyle="Solid" BorderWidth="1px" BorderColor="#c0c0c0" Width="600px" Height="79px" HorizontalAlign="Center" Style="margin-left:40%;" Visible="false" Enabled="false">
                    <table align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td rowspan="3" class="auto-style1">
                                <asp:Image ID="image4" runat="server" ImageUrl="~/Images/OK_XL.png" />
                            </td>
                            <td colspan="2"><asp:TextBox ID="TextBox22" runat="server" Text="No entries found in previous periods" BorderStyle="None" Width="505" style="text-align:center;font-size:16px" Height="30px" /></td>
                        </tr>
                        <tr>
                            <td colspan="2"><hr /></td>
                            
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    
                    
                </asp:Panel>

    </div>
    </form>
</body>
</html>
