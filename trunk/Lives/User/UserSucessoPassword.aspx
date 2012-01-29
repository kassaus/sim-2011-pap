<%@ Page Title="" Language="C#" MasterPageFile="~/Authenticated.Master" AutoEventWireup="true"
    CodeBehind="UserSucessoPassword.aspx.cs" Inherits="Lives.User.UserSucessoPassword" %>

<asp:Content ID="Menu" ContentPlaceHolderID="menu" runat="Server">
    <div class="userState">
        <h3>
            Olá&nbsp;<asp:Label ID="lblNome" runat="server"></asp:Label>
        </h3>
        <asp:Image ID="imagemLogo" runat="server" CssClass="imagemLogo" ImageUrl="~/images/user.png"
            Width="100px" />
    </div>
    <div class="listaMenu">
        <h3>
            Menu</h3>
        <li>
            <asp:Image ID="ImgAlterarPass" runat="server" AlternateText="AlterarPass" Height="40px"
                ImageAlign="Middle" ImageUrl="~/images/id.png" />
            <a href="AlterarPassword.aspx" rel="nofollow" class="style1">Alterar Password</a></li>
        <li>
            <asp:Image ID="ImgGerirVideos" runat="server" AlternateText="Users" Height="40px"
                ImageAlign="Middle" ImageUrl="~/images/pencil.png" />
            <a href="?view=0" rel="nofollow" class="style1">Gerir Vídeos</a></li>
        <li>
            <asp:Image ID="ImgUploadFile" runat="server" AlternateText="Categorias" Height="40px"
                ImageAlign="Middle" ImageUrl="~/images/file_upload.png" />
            <a class="style1" href="?view=2" rel="nofollow">Upload Vídeos</a></li>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div class="editPanel">
        <div class="passAlteradaSucesso">
            <strong>Password alterada com sucesso, da proxima vez que efectuar login utilize a nova
                password!</strong>
        </div>
    </div>
</asp:Content>
