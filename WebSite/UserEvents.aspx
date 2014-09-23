<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UserEvents.aspx.cs" Inherits="Events" %>

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
            <asp:Button ID="NewEventButton" runat="server" Height="85px" OnClick="NewEventButton_Click" Text="Create a new event" Width="192px" />
            &nbsp;
            <asp:Image ID="Image1" runat="server" Height="79px" Width="672px" />
        </asp:Panel>
    
        <br />
        <asp:Panel ID="Panel2" runat="server" Height="479px" CssClass="panel">
            My events:<br />
            <br />
            <asp:Panel ID="Panel3" runat="server" Height="209px" ScrollBars="Vertical">
            </asp:Panel>
            <br />
            <asp:Button ID="EventBrowseButton" runat="server" Height="36px" OnClick="EventBrowseButton_Click" Text="Browse events near you" Width="166px" />
            &nbsp;<asp:Button ID="EventSearchButton" runat="server" Height="36px" OnClick="EventSearchButton_Click" Text="Search for an event" Width="166px" />
            &nbsp;<asp:Button ID="EventInviteButton" runat="server" Height="36px" Text="Invite people to my events" Width="166px" OnClick="EventInviteButton_Click" />
        
        
            <br />
            <br />
        
        
        </asp:Panel>
    </div>
    
    

</asp:Content>

