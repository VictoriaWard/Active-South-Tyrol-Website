<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #registerDiv
        {
            width:520px;
        }

        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div" id="registerDiv">
        <asp:Panel ID="Panel1" runat="server" Height="21px" CssClass="panelHead" Width="500px">
                Register new user:
            </asp:Panel>
        
        <asp:Panel ID="PanelRegister" runat="server" CssClass="panel" Width="500px" >
            
            <br />
            User name:
            <asp:TextBox ID="UserNameText" runat="server" MaxLength="16" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="UserNameValidator" runat="server" ControlToValidate="UserNameText" ErrorMessage="Please enter a user name.">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="UserNameExpressionValidator" runat="server" ControlToValidate="UserNameText" ValidationExpression="^.{1,16}"></asp:RegularExpressionValidator>
            <br />
            Password:
            <asp:TextBox ID="PasswordText" runat="server" MaxLength="16" TextMode="Password" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" ControlToValidate="PasswordText" ErrorMessage="Please enter a passowrd.">*</asp:RequiredFieldValidator>
            <br />
            <br />
            First name:
            <asp:TextBox ID="FirstNameText" runat="server" MaxLength="50" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="FirstNameValidator" runat="server" ControlToValidate="FirstNameText" ErrorMessage="First name is required.">*</asp:RequiredFieldValidator>
            <br />
            Last name:
            <asp:TextBox ID="LastNameText" runat="server" MaxLength="50" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="LastNameValidator" runat="server" ControlToValidate="LastNameText" ErrorMessage="Last name is required.">*</asp:RequiredFieldValidator>
            <br />
            Email address:
            <asp:TextBox ID="EmailText" runat="server" MaxLength="100" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="EmailText" ErrorMessage="Please enter a valid email address.">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="EmailText" ErrorMessage="Please enter a valid email address." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            
            <br />
            
            <br />
            
            <asp:Button ID="Submitbtn" runat="server" OnClick="Submitbtn_Click" Text="Submit" />
            &nbsp;
            <asp:Button ID="GotoLoginButton" runat="server" OnClick="GotoLoginButton_Click" Text="Already registered? Login here" Width="200px" />
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="35px" Width="346px" />
            <br />
            <asp:Label ID="LabelErr" runat="server" Text="Label" Visible="False"></asp:Label>
        </asp:Panel>
    </div>
        
</asp:Content>
