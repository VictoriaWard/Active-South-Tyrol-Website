<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="UserHome.aspx.cs" Inherits="UserHome" %>

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
        <h1><asp:Literal ID="litHeader" runat="server" />
        <asp:ImageButton ID="NewFriendRequestsButton" runat="server" visible="false" Height="50px" OnClick="ViewFriendRequestsButton_Click" style="margin-right: 0px" Width="50px" ImageUrl="http://pixabay.com/static/uploads/photo/2012/05/07/13/47/baby-48509_640.png" />
            <asp:Button ID="LogOutButton" runat="server" OnClick="LogOutButton_Click" Text="Log out" />
        </h1>
        
        <div style="float:left;">
            <asp:Panel ID="Panel1" runat="server" CssClass="panel" Height="469px" Width="604px">
                <asp:Label ID="LabelErr" runat="server" Text="Label" Visible="false"></asp:Label>
                <br />
                Update status:<br />
                <asp:TextBox ID="StatusUpdateText" runat="server" CssClass="textbox" Height="30px" Width="463px"></asp:TextBox>
                &nbsp;<asp:Button ID="SubmitStatusButton" runat="server" OnClick="SubmitStatusButton_Click" Text="Submit" />
                <br />
                <br />
                Upload a photo:<br />
                <asp:FileUpload ID="NewsUpload" runat="server" />
                &nbsp;<asp:Button ID="SubmitPhotoButton0" runat="server" OnClick="SubmitPhotoButton_Click" Text="Submit" style="height: 26px" />
                <br />
                <br />
                <asp:Panel ID="NewsPanel" runat="server" Height="277px">
                </asp:Panel>
                <br />
                <br />
            </asp:Panel>
        </div>

        <div style="float:right; width: 288px; position:relative; z-index:0; top: 0px; left: -29px;" id="UserNavDiv">
            <asp:Panel ID="Panel2"  runat="server"  CssClass="panel" Width="281px">
                <%--<img id= "userProfileImg" alt="user profile picture" class="border" dir="ltr" src="Images/DefaultProfile_03.gif"/>--%>
                <asp:Image ID ="userProfileImage" runat="server" Height="214px" Width="276px"/>
                &nbsp;&nbsp;&nbsp;<br /> Change picture:<asp:FileUpload ID="ProfileImageUpload" runat="server"  Width="141px" />
                <asp:Button ID="SubmitProfileImageButton" runat="server" Height="24px" OnClick="SubmitProfileImageButton_Click" Text="Save picture" Width="94px" />
                &nbsp;<br />
                <br />
                <asp:Label ID="LabelErr2" runat="server" Font-Size="XX-Small" Text="Label" Visible="False"></asp:Label>
                <br />
                <br />
                <asp:TextBox ID="UserAboutMeText" runat="server" CssClass="textbox2" Height="120px" ReadOnly="True" TextMode="MultiLine" Width="178px">About me</asp:TextBox>
                &nbsp;
                <asp:Button ID="EditAboutMe" runat="server" Font-Size="XX-Small" Height="17px" OnClick="EditAboutMe_Click" Text="Edit" Width="29px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="AboutMeSubmit" runat="server" Font-Size="XX-Small" OnClick="AboutMeSubmit_Click" Text="Submit" Visible="False" Width="43px" />
                <br />
                <br />
                
                <br />
                <br />
            </asp:Panel>
        </div> 
    &nbsp;&nbsp;&nbsp;
        &nbsp;</div>
       
</asp:Content>

