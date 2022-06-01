<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="TimeCapture.Logon" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <title>TMS Login</title>
    <link href="./Styles/LoginStyle.css" rel="stylesheet" type="text/css" />
</head>

<body >
	<div class="login">
		<div class="image">
			<img src="Images/.IT-logo%20(836x385).png" width="300px"/>
		</div>
		<span>Time Management System</span>

		<form id="Form1" runat="server">
			<div id="u" class="form-group">
				<input id="username" spellcheck=false class="form-control" name="username" type="text" size="18" alt="login" required="" runat="server"/>
				<span class="form-highlight"></span>
				<span class="form-bar"></span>
				<label for="username" class="float-label">Username</label>
				<erroru>
  					Username is required
  					<i>		
						<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
							<path d="M0 0h24v24h-24z" fill="none"/>
							<path d="M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z"/>
						</svg>
  					</i>
				</erroru>
			</div>
			<div id="p" class="form-group">
				<input id="passwd" class="form-control" spellcheck=false name="password" type="password" size="18" alt="login" required="" runat="server">
				<span class="form-highlight"></span>
				<span class="form-bar"></span>
				<label for="password" class="float-label">Password</label>
				<errorp>
  					Password is required
  					<i>		
						<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
							<path d="M0 0h24v24h-24z" fill="none"/>
							<path d="M1 21h22l-11-19-11 19zm12-3h-2v-2h2v2zm0-4h-2v-4h2v4z"/>
						</svg>
  					</i>
				</errorp>
			</div>
			<div class="form-group">
				<asp:Button ID="cmdLogin" runat="server" onclick="cmdLogin_Click" Text="Sign in" CssClass="button buttonBlue"  />
			</div>	</form>	
			<footer><asp:Label ID="lblVersion" runat="server">Version 2.1.6701.37826</asp:Label></footer>
		


	</div>


		




    



</body>
</html>
