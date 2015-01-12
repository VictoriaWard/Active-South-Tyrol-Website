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
        
        <asp:Panel ID="PanelRegister" runat="server" CssClass="panel" Width="500px" Height="349px" >
            
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
            <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="EmailText" ErrorMessage="Please enter a valid email address." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Size="X-Small"></asp:RegularExpressionValidator>
            
            <br />
            <br />
            Password:&nbsp;<asp:TextBox ID="PasswordText" runat="server" CssClass="textbox" MaxLength="16" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" ControlToValidate="PasswordText" ErrorMessage="Please enter a password.">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PasswordText" ErrorMessage="Passwords must be between 8 and 16 characters and include at least one upper case letter and one numeric character" Font-Size="X-Small" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,16}$"></asp:RegularExpressionValidator>
            <br />
            Re-enter password:
            <asp:TextBox ID="PasswordReenter" runat="server" CssClass="textbox" MaxLength="16" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator0" runat="server" ControlToValidate="PasswordText" ErrorMessage="Please enter a password.">*</asp:RequiredFieldValidator>
            <asp:Label ID="PasswordErrLabel" runat="server" Text="Label" Visible="False" Font-Size="X-Small"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Submitbtn" runat="server" OnClick="Submitbtn_Click" Text="Submit" />
            &nbsp;
            <p>Already signed up? <a href="Login.aspx">login here</a></p>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="35px" Width="346px" Font-Size="Small" />
            <asp:Label ID="LabelErr" runat="server" Text="Label" Visible="False"></asp:Label>
        </asp:Panel>
    </div>
        
</asp:Content>
