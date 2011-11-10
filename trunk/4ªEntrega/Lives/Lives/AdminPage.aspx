<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLayout.master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Lives.AdminPage" %>
<%@ Register src="AdminUserOnline.ascx" tagname="AdminUserOnline" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="corpo" runat="server">
    
    <div class="corpoInterior">
        <asp:Panel ID="Panel1" runat="server">
        <H3 class="LetraAmarelaGrande">User Online</H3>
            <uc1:AdminUserOnline ID="AdminUserOnline1" runat="server" />

        </asp:Panel>
        
    </div>
</asp:Content>
