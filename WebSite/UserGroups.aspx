<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserGroups.aspx.cs" Inherits="UserGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .panel {
            height: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="div">
        <asp:Panel ID="Panel1" runat="server" CssClass="panel" Height="86px" Width="880px">
            <asp:Button ID="NewGroupButton" runat="server" Height="85px" OnClick="NewGroupButton_Click" Text="Create a new group" Width="192px" />
            &nbsp;
            <asp:Image ID="Image1" runat="server" Height="79px" Width="672px" />
        </asp:Panel>
    
        <br />
        <asp:Panel ID="Panel2" runat="server" Height="479px" CssClass="panel">
            My groups:<br />
            <br />
            <asp:Panel ID="Panel3" runat="server" Height="209px" ScrollBars="Vertical">
            </asp:Panel>
            <br />
            <asp:Button ID="GroupBrowseButton" runat="server" Height="36px" OnClick="GroupBrowseButton_Click" Text="Browse groups near you" Width="166px" />
            &nbsp;<asp:Button ID="GroupSearchButton" runat="server" Height="36px" OnClick="GroupSearchButton_Click" Text="Search for a group" Width="166px" />
            &nbsp;<asp:Button ID="GroupInviteButton" runat="server" Height="36px" Text="Invite people to my groups" Width="166px" OnClick="GroupInviteButton_Click" />
        
        
            <br />
            <br />
        
        
        </asp:Panel>
    </div>
    
    

</asp:Content>

