<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true"
    CodeBehind="Home.aspx.cs" Inherits="Lives.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="corpo_user" runat="server">
    <div class="home_backoffice">
        <div class="home_backoffice_video">
            <div style="text-align: center; width: 100%">
                <p class="letraCinzentoGrande">
                    Life in a day...</p>
            </div>
            <div style="text-align: center; width: 100%; height: 390px">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </div>
        </div>
        <asp:Panel ID="painelRegisto" runat="server" Visible="false">
            <div class="home_backoffice_registo">
                <div style="text-align: left; width: 100%">
                    <p class="letraCinzentoGrande">
                        Regista-te</p>
                    <div style="margin-top: 2em; border-top: thin solid #4e4a47; border-bottom: thin solid #4e4a47;
                        padding: 1em 0 1em 0">
                        <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" OnCreatedUser="RegisterUser_CreatedUser">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
                                <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <WizardSteps>
                                <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
                                    <ContentTemplate>
                                        <table style="padding-right: 0px; margin-right: 0px; width: 21em">
                                            <tr>
                                                <td colspan="2">
                                                    Preenche o formulário para te registares.<br />
                                                    Password necessita de ter
                                                    <%= Membership.MinRequiredPasswordLength %>
                                                    carateres no mínimo.
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="middle">
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Style="font-weight: bold">Utilizador:</asp:Label>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" Display="Dynamic"
                                                        ControlToValidate="UserName" CssClass="failureNotification" ErrorMessage="User Name is required."
                                                        ToolTip="User Name is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="middle">
                                                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="middle">
                                                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" Style="font-weight: bold">E-mail:</asp:Label>
                                                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" Display="Dynamic" ControlToValidate="Email"
                                                        CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
                                                        ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="middle">
                                                    <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="middle">
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Style="font-weight: bold">Password:</asp:Label>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" Display="Dynamic"
                                                        ControlToValidate="Password" CssClass="failureNotification" ErrorMessage="Password is required."
                                                        ToolTip="Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td valign="middle">
                                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="middle">
                                                    <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword"
                                                        Style="font-weight: bold">Confirmar Password:</asp:Label>
                                                    <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification"
                                                        Display="Dynamic" ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired"
                                                        runat="server" ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                                        ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic"
                                                        ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                                                </td>
                                                <td valign="middle">
                                                    <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <span class="failureNotification">
                                                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                                                        <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ValidationGroup="RegisterUserValidationGroup" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="right" valign="middle">
                                                    <span>
                                                        <asp:Button ID="CreateUserButton" runat="server" CssClass="botaoRegisto" CommandName="MoveNext"
                                                            Text="Registar" ValidationGroup="RegisterUserValidationGroup" />
                                                    </span>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <CustomNavigationTemplate>
                                    </CustomNavigationTemplate>
                                </asp:CreateUserWizardStep>
                            </WizardSteps>
                        </asp:CreateUserWizard>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="painelRecuperacaoPassword" runat="server" Visible="true">
            <div class="home_backoffice_registo">
                <div style="text-align: left; width: 100%">
                    <p class="letraCinzentoGrande">
                        Recuperar Password</p>
                    <div style="margin-top: 2em; border-top: thin solid #4e4a47; border-bottom: thin solid #4e4a47;
                        padding: 1em 0 1em 0; width: 21em">
                        <asp:PasswordRecovery runat="server" UserNameLabelText="Utilizador:" SubmitButtonStyle-CssClass="botaoRegisto"
                            SubmitButtonText="Enviar" UserNameTitleText="Esqueceste-te da tua password?"
                            UserNameRequiredErrorMessage="Nome de utilizador obrigatório" UserNameInstructionText="Escreva o seu nome de utilizador para receber uma nova password."
                            UserNameFailureText="Não foi possível aceder às tuas informações. Por favor tenta mais tarde."
                            RenderOuterTable="False" SuccessText="A sua nova password foi-lhe enviada." MailDefinition-IsBodyHtml="True" MailDefinition-BodyFileName="~/Resources/PasswordRecoveryEmail.txt">
                        </asp:PasswordRecovery>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
