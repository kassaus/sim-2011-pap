<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLayout.master" AutoEventWireup="true" CodeBehind="AdminCategorias.aspx.cs" Inherits="Lives.AdminCategorias" %>
<%@ Register src="AdminCategorias.ascx" tagname="AdminCategorias" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="corpo" runat="server">

<div class="corpoInterior">
<h3 class="LetraAmarelaGrande">Listagens Aprovação</h3>
    <uc1:AdminCategorias ID="AdminCategorias1" runat="server" />
    </div>
</asp:Content>
