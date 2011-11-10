<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLayout.master" AutoEventWireup="true" CodeBehind="AdminListagensVideos.aspx.cs" Inherits="Lives.AdminListagensVideos1" %>
<%@ Register src="AdminListagensVideos.ascx" tagname="AdminListagensVideos" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="corpo" runat="server">
<div class="corpoInterior">
<h3 class="LetraAmarelaGrande">Listagens Aprovação</h3>
    <asp:Panel ID="Panel1" runat="server">
        <uc1:AdminListagensVideos ID="AdminListagensVideos1" runat="server" />
        <uc1:AdminListagensVideos ID="AdminListagensVideos2" runat="server" />

    </asp:Panel>
</div>
</asp:Content>
