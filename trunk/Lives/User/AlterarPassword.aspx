<%@ Page Title="" Language="C#" MasterPageFile="~/UserLayout.master" AutoEventWireup="true"
    CodeBehind="AlterarPassword.aspx.cs" Inherits="Lives.User.AlterarPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="corpo" runat="server">
    <div class="corpoInterior">
        <h3 class="LetraAmarelaGrande">
            Alterar Password</h3>
        <div class="changePassword">
            <span style="color: #666; font-weight: bold">Preenche o formulário para alterar a password.<br />
                Password necessita de ter
                <%= Membership.MinRequiredPasswordLength %>
                carateres no mínimo. </span>
        </div>
        <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/User/HomeUser.aspx"
            EnableViewState="false" RenderOuterTable="false" SuccessPageUrl="~/User/UserSucessoPassword.aspx" 
            ChangePasswordFailureText="Password incorreta ou  a nova password não respeita as políticas de segurança do site!">
            <ChangePasswordTemplate>
                <div class="changePassword">
                    <p>
                        &nbsp&nbsp&nbsp&nbsp&nbsp<asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword"
                            CssClass="labelChangePassword">Password Antiga:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="BoxForm" 
                            TextMode="Password" Width="146px"></asp:TextBox>
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
