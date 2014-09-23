<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewUserProfile.aspx.cs" Inherits="ViewUserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
    <style type="text/css">
        #userProfileImg {
        height: 203px;
        width: 181px;
          
        }
    </style>
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id= "UserHomeDiv1" class="div">
        <asp:TextBox ID="UserNameText" runat="server" Height="30px" Width="880px" ReadOnly="True" CssClass="panelHead" Font-Bold="False">User Name</asp:TextBox>
        
        <div style="float:left;">
            <asp:Panel ID="Panel1" runat="server" CssClass="panel" Height="469px" Width="604px">
                <asp:Button ID="AddFriendButton" runat="server" OnClick="AddFriendButton_Click" Text="Add friend" Width="150px" />
                &nbsp;<asp:Button ID="AcceptFriendRequestButton" runat="server" OnClick="AcceptFriendRequestButton_Click" Text="Accept friend request" Visible="False" Width="150px" />
                <br />
                <br />
                <asp:TextBox ID="CurrentStatusText" runat="server" Height="40px" Width="551px" CssClass="textbox2" ReadOnly="True"></asp:TextBox>
            </asp:Panel>
        </div>

        <div style="float:right; width: 259px;">
            <asp:Panel ID="Panel2"  runat="server"  CssClass="panel" Width="236px">
                <img id= "userProfileImg" alt="user profile picture" class="border" dir="ltr" src="Images/DefaultProfile_03.gif"/>
                &nbsp;
                <br />
                <br />
                
                <asp:TextBox ID="UserAboutMeText" runat="server" CssClass="textbox2" Height="120px" ReadOnly="True" TextMode="MultiLine" Width="178px">About me</asp:TextBox>
                &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <br />
                
                <br />
                <br />
                <asp:Button ID="Photos" runat="server" OnClick="Photos_Click" Text="Photos" />
                <asp:Button ID="Friends" runat="server" OnClick="Friends_Click" Text="Friends" />
            </asp:Panel>
        </div> 
    </div>
       
</asp:Content>


