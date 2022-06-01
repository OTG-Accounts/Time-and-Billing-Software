<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TimeCapture.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="./Styles/StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmPasswordChange" runat="server">
                                                <div id="ChangePasswordHeader">
                                            <asp:Label ID="Label24" runat="server" Text="CHANGE PASSWORD" />
                                        </div>
                                        <div id="ChangePasswordBody">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:50%">
                                                        <asp:Label ID="Label23" runat="server" Text="Old Password" CssClass="labelgrid" />
                                                    </td>
                                                    <td style="width:50%">
                                                        <asp:TextBox ID="tbOldPassword" runat="server" CssClass="textboxgrid" TextMode="Password" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:50%">
                                                        <asp:Label ID="Label25" runat="server" Text="New Password" CssClass="labelgrid" />
                                                    </td>
                                                    <td style="width:50%">
                                                        <asp:TextBox ID="tbNewPassword" runat="server" CssClass="textboxgrid" TextMode="Password" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:50%">
                                                        <asp:Label ID="Label26" runat="server" Text="Confirm Password" CssClass="labelgrid" />
                                                    </td>
                                                    <td style="width:50%">
                                                        <asp:TextBox ID="tbConfirmPassword" runat="server" CssClass="textboxgrid" TextMode="Password" />
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </div>
                                        <div id="ChangePasswordFooter">
                                            <asp:Button ID="btnChangePasswordCancel" runat="server" Text="Cancel" CssClass="button_header" Width="50px" Height="20px" />
                                            <asp:Button ID="btnChangePassword" runat="server" Text="OK" CssClass="button_header" Width="50px" Height="20px" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbOldPassword" ErrorMessage="Old password is required.">*</asp:RequiredFieldValidator>
                                            
                                        </div>

    </form>
</body>
</html>
