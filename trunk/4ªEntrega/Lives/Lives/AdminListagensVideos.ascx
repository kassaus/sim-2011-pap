<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminListagensVideos.ascx.cs"
    Inherits="Lives.AdminListagensVideos" %>
<link href="Estilo.css" rel="stylesheet" type="text/css" />
<div class="contentorListagemVideosAdmin">
    <div class="hrLinha"><hr /></div>
    <div class="imagemUserAprovacao">
        <div style="position:absolute;">
        <asp:LinkButton ID="btnNome" runat="server" CssClass="letraSiteMedia" Font-Overline="false">&nbsp;Eterno Diabo</asp:LinkButton><br />
        <asp:Image ID="imgUser" runat="server" Width="80px" ImageAlign="Top" ImageUrl="~/images/Diabo.png" />
        </div>
        <asp:LinkButton ID="AceitarVideo" CssClass="botaoListagemAprovacao" runat="server">Aceitar</asp:LinkButton>
        <asp:LinkButton ID="RemoverVideo" CssClass="botaoListagemremover" runat="server">Remover</asp:LinkButton>
                
        <div class="filmeUseraprovacao">        
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <div class="drListCategoriaAprovacao">
            <strong>Categorias:</strong><br />
            <asp:DropDownList ID="drlCategoria" runat="server">
                <asp:ListItem>actividade</asp:ListItem>
                <asp:ListItem>Local</asp:ListItem>
                <asp:ListItem>Emoção</asp:ListItem>
                <asp:ListItem>Evento</asp:ListItem>
                <asp:ListItem>Pessoas</asp:ListItem>
            </asp:DropDownList>
            </div>
            <div class="drListSubCategoriaAprovacao">
            <strong>SubCategorias:</strong><br />
            
            <asp:DropDownList ID="drlSub_Categorias" runat="server">
                <asp:ListItem>Cantar</asp:ListItem>
                <asp:ListItem>Andar</asp:ListItem>
                <asp:ListItem>Estudar</asp:ListItem>
                <asp:ListItem>Comer</asp:ListItem>
                <asp:ListItem>Jogar</asp:ListItem>
            </asp:DropDownList>
        </div>
            </div>
            
        <div class="entulho">&nbsp;</div>
    </div>
</div>
