<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="HomeTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  
     <div style="width: 520px;" class="div">
           <asp:Panel ID="Panel1" runat="server" CssClass="panelHead" Width="500px">
                    Login:
                </asp:Panel>
            
         <asp:Panel ID="Panel2" runat="server" Width="500px" CssClass="panel" Height="319px">
               
                <br />
                Email address:
                <asp:TextBox ID="EmailText" runat="server" MaxLength="100" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="EmailText" ErrorMessage="Please enter a valid email address.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="EmailText" ErrorMessage="Please enter a valid email address." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                
                <br />
                
                Password:
                <asp:TextBox ID="PasswordText" runat="server" MaxLength="16" TextMode="Password" CssClass="textbox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" ControlToValidate="PasswordText" ErrorMessage="Please enter a passowrd.">*</asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Button ID="LoginButton" runat="server" OnClick="LoginButton_Click" Text="Login" />
                <p style="width: 266px">Not signed up? <a href="Register.aspx">register here</a></p>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" Height="52px" />
                <asp:Label ID="LabelErr" runat="server" Text="Label" Visible="False"></asp:Label>
                <br />
                <br />
                
            </asp:Panel>
            
            <br />
            <br />
            
         &nbsp;<br />  
        </div>
             
</asp:Content>
