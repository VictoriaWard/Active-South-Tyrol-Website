<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddNewProfileImage.aspx.cs" Inherits="AddNewProfileImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="155px" HorizontalAlign="Center" Width="953px">
        <br />
        Upload your photo:<br />
        <br />
        <asp:FileUpload ID="ProfileImageUpload" runat="server" Height="26px" Width="174px"  />
        <br />
        <br />
        <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Text="Submit" />
    </asp:Panel>
</asp:Content>

