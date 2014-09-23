<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="Events" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="div" >
        <div style="float:left;">
            <asp:Panel ID="Panel1" runat="server" CssClass="panel" Height="258px" Width="581px">
                <asp:Image ID="Image1" runat="server" Height="247px" Width="540px" />
        
            </asp:Panel>
            <br />
    
            <asp:Panel ID="Panel2" runat="server" CssClass="panel" Height= "322px" Width= "577px">
                <asp:TextBox ID="EventNameText" runat="server" Height="32px" Width="562px" CssClass="textbox2"></asp:TextBox>
                <br />
                <br />
                <asp:TextBox ID="EventDateText" runat="server" Height="24px" Width="277px" CssClass="textbox2"></asp:TextBox>
                <br />
                <br />
                <asp:TextBox ID="EventPlaceText" runat="server" Height="24px" Width="277px" CssClass="textbox2"></asp:TextBox>
                <br />
                <br />
                <asp:TextBox ID="DetailsText" runat="server" Height="129px" Width="560px" CssClass="textbox2"></asp:TextBox>
            </asp:Panel>    
        </div>
        
        <div style="float:right; height: 633px; margin-left: 0px;">
            
            <asp:Panel ID="Panel3" runat="server" width= "252px" height= "422px" CssClass="panel" Font-Size="Medium">
                <asp:Button ID="JoinEventButton" runat="server" Height="38px" OnClick="JoinEventButton_Click" Text="I'm going" Width="70px" />
                &nbsp; Going:&nbsp;
                <asp:TextBox ID="EventJoined" runat="server" Height="22px" Width="46px"></asp:TextBox>
                <br />
                <br />
                <asp:Panel ID="Panel5" runat="server" Height="360px">
                </asp:Panel>
            </asp:Panel>
            <br />

            <asp:Panel ID="Panel4" runat="server" width= "251px" height= "150px" CssClass="panel">
                <asp:Button ID="InviteButton" runat="server" Text="Invite people" Height="65px" Width="97px"  />
            </asp:Panel>
        </div>
        
        <br />

    </div>
    

</asp:Content>

