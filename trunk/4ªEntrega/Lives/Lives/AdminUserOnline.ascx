<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminUserOnline.ascx.cs"
    Inherits="Lives.AdminUserOnline" %>
<link href="Estilo.css" rel="stylesheet" type="text/css" />
<div class="hrLinha"><hr /></div>
<div style="position: relative; text-align: left; margin-left: 10px; margin-top: 20px;
    margin-bottom: 10px;">
    
    <asp:Image ID="imgAmigo" runat="server" Width="80px" ImageAlign="Top" ImageUrl="~/images/Diabo.png" />
    &nbsp;
    <asp:LinkButton ID="btnAmigo" runat="server" CssClass="letraSiteMedia" Font-Overline="false">Eterno Diabo</asp:LinkButton>
    <div style="position: inherit;">
        <asp:Label ID="Label1" runat="server" Text="Ultima publicação:" Style="font-weight: 700"></asp:Label>
        &nbsp;
        <asp:Label ID="lblUltimaPublicacao" runat="server" Text="Data da ultima publicação"></asp:Label>
    </div>
    
        <asp:Button ID="btnAlterarPwd" CssClass="botaoRemoverUser" runat="server" Text="Remover User" />
    
    
        <asp:Button ID="btnApagar" CssClass="botaoPassUser" runat="server" Height="26px" OnClick="Button2_Click" Text="Alterar Pass" />
    
</div>
