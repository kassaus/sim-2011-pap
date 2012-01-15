<%@ Page Title="" Language="C#" MasterPageFile="~/TopoAutenticacao.Master" AutoEventWireup="true"
    CodeBehind="Error.aspx.cs" Inherits="Lives.ErrorPages.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ERRO</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="corpo_user" runat="server">
    <div class="error">
        <p class="errorMessage">
            Ocorreu um erro na aplicação.<br />Por favor tente mais tarde.
        </p>
        <p class="errorNote">
            Se o problema persistir, contacte um administrador do site. Obrigado.
        </p>
    </div>
</asp:Content>
