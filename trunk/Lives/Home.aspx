<%@ Page Title="" Language="C#" MasterPageFile="~/user_topo.Master" AutoEventWireup="true"
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
        <div class="home_backoffice_registo">
            <div style="text-align: left; width: 100%">
                <p class="letraCinzentoGrande">
                    Regista-te</p>
                <hr style="width: 100%; color: #666; margin: 0px" />
                <div style="margin-top: 20px;">
                    <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" OnCreatedUser="RegisterUser_CreatedUser">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
                            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <WizardSteps>
                            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
                                <ContentTemplate>
                                    <table style="padding-right: 0px; margin-right: 0px;">
                                        <tr>
                                            <td colspan="2">
                                                <span style="color: #666; font-weight: bold">Preenche o formulário para te registares.<br />
                                                    Password necessita de ter
                                                    <%= Membership.MinRequiredPasswordLength %>
                                                    carateres no mínimo. </span>
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
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Utilizador:</asp:Label>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" Display="Dynamic"
                                                    ControlToValidate="UserName" CssClass="failureNotification" ErrorMessage="User Name is required."
                                                    ToolTip="User Name is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td valign="middle">
                                                <asp:TextBox ID="UserName" runat="server" CssClass="boxLogin"></asp:TextBox>
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
                                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" Display="Dynamic" ControlToValidate="Email"
                                                    CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
                                                    ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td valign="middle">
                                                <asp:TextBox ID="Email" runat="server" CssClass="boxLogin"></asp:TextBox>
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
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" Display="Dynamic"
                                                    ControlToValidate="Password" CssClass="failureNotification" ErrorMessage="Password is required."
                                                    ToolTip="Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td valign="middle">
                                                <asp:TextBox ID="Password" runat="server" CssClass="boxLogin" TextMode="Password"></asp:TextBox>
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
                                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirar Password:</asp:Label>
                                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification"
                                                    Display="Dynamic" ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired"
                                                    runat="server" ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                                    ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic"
                                                    ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                                            </td>
                                            <td valign="middle">
                                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="boxLogin" TextMode="Password"></asp:TextBox>
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
                                    <hr style="width: 100%; color: #666; margin-top: 10px" />
                                </ContentTemplate>
                                <CustomNavigationTemplate>
                                </CustomNavigationTemplate>
                            </asp:CreateUserWizardStep>
                        </WizardSteps>
                    </asp:CreateUserWizard>
                </div>
            </div>
        </div>
    </div>
    <div class="contentorRodaPe">
        <div class="rodaPe">
            <p class="letraCinzentoPeq">
                Lifes &copy 2012 SIM</p>
        </div>
    </div>
</asp:Content>
