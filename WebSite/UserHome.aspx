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
        <h1><asp:Literal ID="litHeader" runat="server" /></h1>
        
        <div style="float:left;">
            <asp:Panel ID="Panel1" runat="server" CssClass="panel" Height="469px" Width="604px">
                <br />
                Update status:<br />
                <asp:TextBox ID="StatusUpdateText" runat="server" Width="463px" Height="30px" CssClass="textbox"></asp:TextBox>
                &nbsp;<asp:Button ID="SubmitStatusButton" runat="server" Text="Submit" OnClick="SubmitStatusButton_Click" />
                <br />
                <asp:Label ID="LabelErr" runat="server" Text="Label" Visible="false"></asp:Label>
                <br />
                Upload a photo:<br />
                <asp:FileUpload ID="FileUpload2" runat="server" />
                &nbsp;<asp:Button ID="SubmitPhotoButton0" runat="server" OnClick="SubmitPhotoButton_Click" Text="Submit" style="height: 26px" />
                <br />
                <br />
                <br />
                <br />
                <asp:TextBox ID="CurrentStatusText" runat="server" Height="227px" Width="551px" CssClass="textbox2" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
            </asp:Panel>
        </div>

        <div style="float:right; width: 259px; position:relative; z-index:0; top: 0px; left: 0px;" id="UserNavDiv">
            <asp:Panel ID="Panel2"  runat="server"  CssClass="panel" Width="236px">
                <%--<img id= "userProfileImg" alt="user profile picture" class="border" dir="ltr" src="Images/DefaultProfile_03.gif"/>--%>
                <asp:Image ID ="userProfileImage" runat="server" Height="150px" Width="200px"/>
                &nbsp;&nbsp;
                <asp:Label ID="SelectPhotoLabel" runat="server" Font-Size="XX-Small" Text="Select a photo: " Visible="False"></asp:Label>
                <asp:Button ID="ChangeProfile" runat="server" Font-Size="XX-Small" Height="17px" OnClick="ChangeProfile_Click" Text="Change photo" Width="81px" />
                &nbsp;<asp:FileUpload ID="ProfileImageUpload" runat="server" Visible="False" />
                <br />
                <asp:Button ID="SubmitProfileImageButton" runat="server" Height="18px" OnClick="SubmitPhotoButton_Click" Text="Save photo" Visible="False" Width="88px" />
                <br />
                <br />
                <br />
                
                <asp:TextBox ID="UserAboutMeText" runat="server" CssClass="textbox2" Height="120px" ReadOnly="True" TextMode="MultiLine" Width="178px">About me</asp:TextBox>
                &nbsp;
                <asp:Button ID="EditAboutMe" runat="server" Font-Size="XX-Small" Height="17px" OnClick="EditAboutMe_Click" Text="Edit" Width="29px" />
                <asp:Label ID="LabelErr2" runat="server" Font-Size="XX-Small" Text="Label" Visible="False"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="AboutMeSubmit" runat="server" Font-Size="XX-Small" OnClick="AboutMeSubmit_Click" Text="Submit" Visible="False" Width="43px" />
                <br />
                <br />
                
                <br />
                <br />
            </asp:Panel>
        </div> 
    &nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="NewFriendRequestsButton" runat="server" visible="false" Height="50px" OnClick="ViewFriendRequestsButton_Click" style="margin-right: 0px" Width="50px" ImageUrl="http://pixabay.com/static/uploads/photo/2012/05/07/13/47/baby-48509_640.png" />
    &nbsp;<asp:Button ID="LogOutButton" runat="server" OnClick="LogOutButton_Click" Text="Log out" />
    </div>
       
</asp:Content>

