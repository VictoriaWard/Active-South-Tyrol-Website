<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateNewEvent.aspx.cs" Inherits="CreateNewEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    
    <asp:Panel ID="Panel1" runat="server" CssClass="panel" Height="356px" Width="782px">
        Create new event:
        <br />
        <br />
        Event name:
        <asp:TextBox ID="EventNameText" runat="server" Width="447px" CssClass="textbox"></asp:TextBox>
        <br />
        <br />
        Date:
        <asp:TextBox ID="EventDateText" runat="server" Width="171px" CssClass="textbox"></asp:TextBox>
        <br />
        Time:
        <asp:TextBox ID="TextBox3" runat="server" Width="170px" CssClass="textbox"></asp:TextBox>
        <br />
        Place:
        <asp:TextBox ID="EventPlaceText" runat="server" Width="167px" CssClass="textbox"></asp:TextBox>
        <br />
        <br />
        Details:
        <asp:TextBox ID="EventDetailsText" runat="server" Height="104px" Width="621px" TextMode="MultiLine" CssClass="textbox"></asp:TextBox>
        <br />
        <br />
        <br />
        Add picture:
        <asp:FileUpload ID="FileUpload1" runat="server" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" Width="70px" OnClick="SubmitButton_Click" />
        &nbsp;
        <asp:Label ID="LabelErr" runat="server" Text="Label" Visible="False"></asp:Label>
    </asp:Panel>

    
    
    </asp:Content>

