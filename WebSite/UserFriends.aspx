<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserFriends.aspx.cs" Inherits="UserFriends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .panel {
            height: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="div">
        <asp:Panel ID="Panel1" runat="server" CssClass="panel" Height="120px" Width="880px">
            Friend requests:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="LogOutButton" runat="server" OnClick="Button1_Click" Text="Log out" />
            <br />
            <asp:Panel ID="Panel3" runat="server" Height="76px" ScrollBars="Vertical">
            </asp:Panel>
        </asp:Panel>
    
        <br />
        <asp:Panel ID="Panel2" runat="server" Height="479px" CssClass="panel">
            My friends:<br />
            <br />
            <asp:Panel ID="Panel4" runat="server" Height="209px" ScrollBars="Vertical">
            </asp:Panel>
            <br />
            <asp:Button ID="FriendSearchButton" runat="server" Height="36px" OnClick="FriendSearchButton_Click" Text="Search for a friend" Width="166px" />
            &nbsp;<asp:Button ID="FriendInviteButton" runat="server" Height="36px" Text="Invite a friend " Width="166px" OnClick="FriendInviteButton_Click" />
        
        
            <br />
            <br />
        
        
        </asp:Panel>
    </div>
    
    

</asp:Content>

