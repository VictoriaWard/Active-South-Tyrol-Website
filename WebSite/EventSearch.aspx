<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EventSearch.aspx.cs" Inherits="EventSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class=" div">
    <div class="div" style="float:left; height:344px; width:390px;">
        <asp:Panel ID ="EventSearchPanel" runat ="server" CssClass="panelHead" Width="370px">
            Search by event name:
        </asp:Panel>

        <asp:Panel ID="Panel1" runat="server" Height="103px" CssClass="panel" Width="370px" Font-Size="Smaller">
            <br />
            Event name:
            <br />
            <asp:TextBox ID="EventNameText" runat="server" CssClass="textbox" Width="265px"></asp:TextBox>
            &nbsp;
            <asp:Button ID="EventNameSearchButton" runat="server" OnClick="EventNameSearchButton_Click" Text="Search" />
            <br />
            <br />
            <br />
            &nbsp;

        </asp:Panel>
        <br/>
            
        <asp:Panel ID ="NameSearch" runat ="server" CssClass="panelHead" Width="370px">
        </asp:Panel>
            
        <asp:Panel ID="Panel4" runat="server" CssClass="panel" Height="103px" Width="370px" Font-Size="Smaller">
            <br />
            &nbsp;
        </asp:Panel>
    </div>

    <div class="div" style="float:right; height:343px; width:490px;">
        <asp:Panel ID="Panel2" runat="server" Height="323px" Width="470px" CssClass="panel" ScrollBars="Vertical">
            <asp:Label ID="LabelNotFound" runat="server" Text="Label" Visible="False"></asp:Label>
        </asp:Panel>

    </div>
        </div>

</asp:Content>

