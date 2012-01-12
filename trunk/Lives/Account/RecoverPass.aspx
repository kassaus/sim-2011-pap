<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="RecoverPass.aspx.cs" Inherits="Lives.Account.RecoverPass" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">


    <asp:PasswordRecovery runat="server" UserNameLabelText="Utilizador:">
    </asp:PasswordRecovery>

</asp:Content>
