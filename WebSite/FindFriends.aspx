<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FindFriends.aspx.cs" Inherits="FindFriends" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class=" div">
    <div class="div" style="float:left; height:344px; width:390px;">
        <asp:Panel ID ="EmailSearch" runat ="server" CssClass="panelHead" Width="370px">
            Search by email address:
        </asp:Panel>

        <asp:Panel ID="Panel1" runat="server" Height="103px" CssClass="panel" Width="370px" Font-Size="Smaller">
            <br />
            Email address:
            <br />
            <asp:TextBox ID="EmailText" runat="server" CssClass="textbox" Width="265px" OnTextChanged="EmailText_TextChanged"></asp:TextBox>
            &nbsp;
            <asp:Button ID="EmailSearchButton" runat="server" OnClick="EmailSearchButton_Click" Text="Search" />
            <br />
            <br />
            <br />
            &nbsp;

        </asp:Panel>
        <br/>
            
        <asp:Panel ID ="NameSearch" runat ="server" CssClass="panelHead" Width="370px">
            Search by name:
        </asp:Panel>
            
        <asp:Panel ID="Panel4" runat="server" CssClass="panel" Height="103px" Width="370px" Font-Size="Smaller">
            <br />
            First name:
            <br />
            <asp:TextBox ID="FirstNameText" runat="server" CssClass="textbox" OnTextChanged="FirstNameText_TextChanged" Width="265px"></asp:TextBox>
            <br />
            Last name:
            <br />
            <asp:TextBox ID="LastNameText" runat="server" CssClass="textbox" OnTextChanged="LastNameText_TextChanged" Width="265px"></asp:TextBox>
            &nbsp;
            <asp:Button ID="NameSearchButton0" runat="server" OnClick="NameSearchButton_Click" Text="Search" />
        </asp:Panel>
    </div>

    <div class="div" style="float:right; height:343px; width:490px;">
        <asp:Panel ID="Panel2" runat="server" Height="323px" Width="470px" CssClass="panel" ScrollBars="Vertical">
            <asp:Label ID="LabelNotFound" runat="server" Text="Label" Visible="False"></asp:Label>
        </asp:Panel>

    </div>
        </div>

</asp:Content>

