<%@ Page Title="" Language="C#" MasterPageFile="~/User/CorpoUtilizador.master" AutoEventWireup="true"
    CodeBehind="HomeUser.aspx.cs" Inherits="Lives.Users.HomeUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="corpo" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager" runat="Server">
    </asp:ToolkitScriptManager>
    <style type="text/css">
        html
        {
            overflow: auto;
        }
    </style>
    <asp:HiddenField ID="idUserHide" runat="server" />
    <asp:HiddenField ID="idVideoToEdit" runat="server" />
    <div id="ContentorCorpoInterior">
        <div class="corpoInterior">
            <h3 class="titulo">
                Gestão de Publicações</h3>
            <hr />
            <div style="position: relative; width: 100%; height: 40px;">
                <div style="position: absolute; top: 0px; margin-left: 50px">
                    <p class="letraCinzentoMedia">
                        Categorias:
                        <asp:DropDownList ID="ddlCategorias" runat="server" DataSourceID="OdsCategorias"
                            DataTextField="nome" DataValueField="id" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="OdsCategorias" runat="server" SelectMethod="obterTodas"
                            TypeName="BLL.CategoriaBO"></asp:ObjectDataSource>
                    </p>
                </div>
                <div style="position: absolute; top: 0px; left: 300px">
                    <p class="letraCinzentoMedia">
                        Subcategorias:
                        <asp:DropDownList ID="ddlSubcategorias" runat="server" Width="150px" AutoPostBack="false"
                            DataSourceID="OdsSubcategorias" DataTextField="nome" DataValueField="id">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="OdsSubcategorias" runat="server" SelectMethod="obterTodasSubCategoriasCategoria"
                            TypeName="BLL.SubcategoriaBO">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlCategorias" Name="cat" PropertyName="SelectedValue"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </p>
                </div>
                <div style="position: absolute; top: 0px; left: 580px">
                    <p class="letraCinzentoMedia">
                        <asp:CheckBox ID="CheckBoxVideosAprovados" runat="server" AutoPostBack="True" Text="Vídeos aprovados" />&nbsp&nbsp&nbsp&nbsp
                        <asp:CheckBox ID="CheckBoxVideosPorAprovar" runat="server" AutoPostBack="True" Text="Vídeos por aprovar" />&nbsp&nbsp&nbsp&nbsp
                        <asp:CheckBox ID="CheckBoxTodosVideos" runat="server" AutoPostBack="True" Text="Mostrar todos" />&nbsp&nbsp&nbsp&nbsp
                    </p>
                </div>
            </div>
            <asp:MultiView ID="MultiViewVideos" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <div style="padding-bottom: 10px; border-top-style: solid; border-top-width: thin;
                        border-top-color: #666; border-bottom-style: solid; border-bottom-width: thin;
                        border-bottom-color: #666;">
                        <h3 class="subtitulo">
                            Vídeos</h3>
                    </div>
                    <asp:GridView ID="ListaVideos" runat="server" AutoGenerateColumns="False" GridLines="None"
                        OnRowDataBound="ListaVideos_RowDataBound" ShowHeader="False" DataKeyNames="id"
                        DataSourceID="ODSListaVideos">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div style="position: relative; margin-left: 10px; padding-bottom: 10px; padding-top: 10px;
                                        width: 500px">
                                        <strong>Título:</strong>&nbsp
                                        <asp:Label ID="Label1" runat="server" Font-Bold="false" Text='<%# Eval("titulo") %>'></asp:Label><br />
                                        <div style="position: relative; margin-top: 5px; border-bottom-style: solid; border-bottom-width: thin;
                                            border-bottom-color: #666">
                                            <object classid="clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95" width="252" height="189"
                                                codebase="http://www.microsoft.com/Windows/MediaPlayer/">
                                                <param name="Filename" value='<%# Eval("url") %>'>
                                                <param name="AutoStart" value="false">
                                                <param name="ShowControls" value="true">
                                                <param name="BufferingTime" value="2">
                                                <param name="ShowStatusBar" value="false">
                                                <param name="AutoSize" value="true">
                                                <param name="InvokeURLs" value="false">
                                                <embed src='<%# "/Videos/" + Eval("url") %>' type="application/x-mplayer2" autostart="0"
                                                    enabled="1" showstatusbar="0" showdisplay="1" showcontrols="1" pluginspage="http://www.microsoft.com/Windows/MediaPlayer/"
                                                    codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,0,0"
                                                    width="252" height="189"></embed>
                                            </object>
                                            <div style="position: relative; top: 5px; padding-bottom: 20px">
                                                <span style="font-weight: bold">Publicado:</span>
                                                <asp:Label ID="Label38" runat="server" Text='<%# Eval("data") %>'></asp:Label><br />
                                                <span style="font-weight: bold">Descrição:</span>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("descricao") %>'></asp:Label><br />
                                            </div>
                                            <div style="position: absolute; top: 0px; left: 267px; width: 200px;">
                                                <span style="font-weight: bold">Etiquetas:</span>
                                                <asp:Repeater runat="server" DataSource='<%# Eval("Subcategorias") %>' ID="SubCats">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("nome") + "  " %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <div>
                                                    <span style="font-weight: bold">Aprovado?<asp:CheckBox ID="chkbAprovado" runat="server"
                                                        Text="" Checked='<%# (int) Eval("Estado.id") == 2 %>' Enabled="False" /><br />
                                                    </span>
                                                </div>
                                                <div style="position: relative; top: 20px">
                                                    <asp:LinkButton ID="lbtnEditar" CssClass="botaoLogin" Font-Underline="false" Width="80px"
                                                        runat="server" OnClick="lbtnEditar_Click">Editar</asp:LinkButton>
                                                </div>
                                                <div style="position: relative; top: 70px">
                                                    <asp:LinkButton ID="lbtnApagar" CssClass="botaoLogin" Font-Underline="false" ForeColor="red"
                                                        Width="80px" runat="server" OnClick="lbtnApagarVideo_Click">Apagar</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ODSListaVideos" runat="server" SelectMethod="obterPorUser"
                        TypeName="BLL.VideoBO">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="idUserHide" DbType="Guid" Name="idUser" PropertyName="Value" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="VideoEditView" runat="server">
                    <asp:ObjectDataSource ID="ODSVideoToEdit" runat="server" SelectMethod="obterVideo"
                        TypeName="BLL.VideoBO">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="idVideoToEdit" DbType="Int32" Name="id" ConvertEmptyStringToNull="true"
                                PropertyName="Value" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:Repeater ID="VideoDetailsView" runat="server" DataSourceID="ODSVideoToEdit">
                        <ItemTemplate>
                            <div style="padding-bottom: 10px; border-top-style: solid; border-top-width: thin;
                                border-top-color: #666; border-bottom-style: solid; border-bottom-width: thin;
                                border-bottom-color: #666;">
                                <h3 class="subtitulo">
                                    Editar Vídeos</h3>
                            </div>
                            <div style="position: relative; bottom: 1.9em;">
                                <div style="margin-left: auto; margin-right: auto; width: 300px">
                                    <asp:Label ID="lblErro" CssClass="redError" runat="Server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div style="position: relative; margin-left: 10px; padding-bottom: 10px; padding-top: 10px;">
                                <div style="position: relative; top: 0; left: 0; margin-left: 10px; padding-bottom: 10px;">
                                    <div>
                                        <p class="letraCinzentoMedia" style="font-weight: bold">
                                            Título:
                                            <asp:TextBox ID="txtBoxTitulo" runat="server" Columns="50" Width="200px"></asp:TextBox></p>
                                        <asp:TextBoxWatermarkExtender ID="txtBoxTitulo_TextBoxWatermarkExtender" runat="server"
                                            Enabled="True" TargetControlID="txtBoxTitulo" WatermarkText='<%# Eval("titulo") %>'>
                                        </asp:TextBoxWatermarkExtender>
                                    </div>
                                    <div style="position: absolute; top: 0; left: 220px">
                                        <span style="margin-left: 50px; font-weight: bold">Etiquetas: </span>
                                        <asp:Repeater ID="TagRepeater" runat="server" DataSource='<%# Eval("Subcategorias") %>'>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" Text='<%# Eval("nome") %>' OnClick="labelClickEventHandler"
                                                    SkinID="EtiquetaRemovivel" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <span style="margin-left: 50px; padding: 10px"></span>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnCategorizar" CssClass="botaoLogin" Height="40px" Width="150px"
                                            runat="server" Text="Classificar" OnClick="btnCategorizar_Click" />
                                        <span style="margin-left: 50px;"></span>
                                        <asp:Button ID="btnApagarSubcat" CssClass="botaoLogin" Height="40px" Width="150px"
                                            runat="server" Text="Apagar Etiquetas" OnClick="btnApagarSubcat_Click" />
                                    </div>
                                    <div style="position: relative; top: 20px; width: 500px; height: 300px; background-color: Green">
                                    </div>
                                    <div style="position: relative; top: 10px; width: 500px; height: 40px; background-color: red">
                                        <div style="position: absolute; top: 5px;">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                        </div>
                                        <div style="position: absolute; top: 5px; right: 120px">
                                            <asp:Button ID="Button1" runat="server" CssClass="botaoLogin" Height="20px" Width="100px"
                                                Text="Cancelar" />
                                        </div>
                                        <div style="position: absolute; top: 5px; right: 0px">
                                            <asp:Button ID="Button2" runat="server" CssClass="botaoLogin" Height="20px" Width="100px"
                                                Text="Confirmar" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:View>
                <asp:View ID="View3" runat="server">
                </asp:View>
                <asp:View ID="View4" runat="server">
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>
