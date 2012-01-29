<%@ Page Title="" Language="C#" MasterPageFile="~/Authenticated.Master" AutoEventWireup="true"
    CodeBehind="AlterarPassword.aspx.cs" Inherits="Lives.User.AlterarPassword" %>

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
            <a href="./?view=0" rel="nofollow" class="style1">Gerir Vídeos</a></li>
        <li>
            <asp:Image ID="ImgUploadFile" runat="server" AlternateText="Categorias" Height="40px"
                ImageAlign="Middle" ImageUrl="~/images/file_upload.png" />
            <a class="style1" href="./?view=2" rel="nofollow">Upload Vídeos</a></li>
    </div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div class="corpoInterior">
        <h3 class="LetraAmarelaGrande">
            Alterar Password</h3>
        <div class="changePassword">
            <span style="color: #666; font-weight: bold">Preenche o formulário para alterar a password.<br />
                Password necessita de ter
                <%= Membership.MinRequiredPasswordLength %>
                carateres no mínimo. </span>
        </div>
        <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/User/"
            EnableViewState="false" RenderOuterTable="false" SuccessPageUrl="~/User/UserSucessoPassword.aspx" 
            ChangePasswordFailureText="Password incorreta ou  a nova password não respeita as políticas de segurança do site!">
            <ChangePasswordTemplate>
                <div class="changePassword">
                    <p>
                        &nbsp&nbsp&nbsp&nbsp&nbsp<asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword"
                            CssClass="labelChangePassword">Password Antiga:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="BoxForm" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                            CssClass="failureNotification" ErrorMessage="Insira a antiga Password." ToolTip="Old Password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword"
                            CssClass="labelChangePassword">Nova Password:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="BoxForm" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                            CssClass="failureNotification" ErrorMessage="Nova password é necessária." ToolTip="New Password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword"
                            CssClass="labelChangePassword">Confirmar Password:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="BoxForm" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirme a nova password."
                            ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                            ControlToValidate="ConfirmNewPassword" CssClass="failureNotification" Display="Dynamic"
                            ErrorMessage="As passwords têm de ser iguais!" ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                    </p>
                    <p>
                        <span style="position:relative;margin-left:183px">
                            <asp:Button ID="CancelPushButton" runat="server" CssClass="botaoRegisto" CausesValidation="False"
                                CommandName="Cancel" Text="Cancel" /></span> <span style="position:relative;margin-left:4px">
                                    <asp:Button ID="ChangePasswordPushButton" runat="server" CssClass="botaoRegisto"
                                        CommandName="ChangePassword" Text="Alterar" ValidationGroup="ChangeUserPasswordValidationGroup" /></span>
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification"
                        ValidationGroup="ChangeUserPasswordValidationGroup" />
                </div>
            </ChangePasswordTemplate>
        </asp:ChangePassword>
    </div>
</asp:Content>
