<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminCategorias.ascx.cs" Inherits="Lives.AdminCategorias1" %>
<%@ Register src="AdminCategoriasSubcategorias.ascx" tagname="AdminCategoriasSubcategorias" tagprefix="uc1" %>
<asp:Label ID="lblCategorias" runat="server" Text="Categorias"></asp:Label>

<asp:DropDownList id="DropDownList1" runat="server">
	<asp:ListItem>Actividade</asp:ListItem>
	<asp:ListItem>Local</asp:ListItem>
	<asp:ListItem>Emoção</asp:ListItem>
	<asp:ListItem>Tempo</asp:ListItem>
	<asp:ListItem>Evento</asp:ListItem>
	<asp:ListItem>Pessoas</asp:ListItem>
</asp:DropDownList>
<div>
    <uc1:AdminCategoriasSubcategorias ID="AdminCategoriasSubcategorias1" 
        runat="server" />
</div>


<p>
    &nbsp;</p>



