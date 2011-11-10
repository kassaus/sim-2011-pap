<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLayout.master" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="Lives.AdminUsers1" %>
<%@ Register src="AdminUserOnline.ascx" tagname="AdminUserOnline" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="corpo" runat="server">
<div class="corpoInterior">
<H3 class="LetraAmarelaGrande">Users</H3>
        <asp:Panel ID="Panel1" runat="server">
        <uc1:AdminUserOnline ID="AdminUserOnline2" runat="server" />
            <uc1:AdminUserOnline ID="AdminUserOnline1" runat="server" />
            
        </asp:Panel>
</div>
      
</asp:Content>
