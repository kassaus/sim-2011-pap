<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true"
    CodeBehind="Error.aspx.cs" Inherits="Lives.ErrorPages.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ERRO</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="corpo_user" runat="server">
    <div class="error">
        <div class="errorMessage">
            Ocorreu um erro na aplicação.<br />
            Por favor tente mais tarde.
        </div>
        <div class="errorNote">
            Se o problema persistir, por favor contacte um administrador do site.<br />Obrigado.
        </div>
    </div>
</asp:Content>
