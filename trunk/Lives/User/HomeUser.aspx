<%@ Page Title="" Language="C#" MasterPageFile="~/UserLayout.master" AutoEventWireup="true"
    CodeBehind="HomeUser.aspx.cs" Inherits="Lives.Users.HomeUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="corpo" runat="server">
    <style type="text/css">
        html
        {
            overflow: auto;
        }
    </style>
    <asp:HiddenField ID="idUserHide" runat="server" />
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
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Text="Vídeos aprovados" />&nbsp&nbsp&nbsp&nbsp
                    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Text="Vídeos reprovados" />&nbsp&nbsp&nbsp&nbsp
                    <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="True" Text="Mostrar todos" />&nbsp&nbsp&nbsp&nbsp
                </p>
            </div>
        </div>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <hr />
                <h3 class="subtitulo">
                    Vídeos</h3>
                <asp:GridView ID="ListaVideos" runat="server" AutoGenerateColumns="False" GridLines="None"
                    OnRowDataBound="ListaVideos_RowDataBound" ShowHeader="False" DataSourceID="ODSListaVideos">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="border-top-style: solid; border-top-width: thin; border-top-color: #666;">
                                    <div style="float: left; margin-left: 10px; padding-bottom: 10px;">
                                        <strong>Título:</strong>&nbsp
                                        <asp:Label ID="Label1" runat="server" Font-Bold="false" Text='<%# Eval("titulo") %>'></asp:Label><br />
                                        <div style="margin-top: 5px">
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
                                        </div>
                                        <div style="margin-top: 5px">
                                            <span style="font-weight: bold">Publicado:</span>
                                            <asp:Label ID="Label38" runat="server" Text='<%# Eval("data") %>'></asp:Label><br />
                                            <span style="font-weight: bold">Descrição:</span>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("descricao") %>'></asp:Label><br />
                                        </div>
                                    </div>
                                    <div style="float: left; padding-left: 10px; padding-top: 20px">
                                        <span style="font-weight: bold">Chaves:</span>
                                        <asp:Repeater runat="server" DataSource='<%# Eval("Subcategorias") %>' ID="SubCats">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("nome") + "  " %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:Repeater>
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
            <asp:View ID="View2" runat="server">
            </asp:View>
            <asp:View ID="View3" runat="server">
            </asp:View>
            <asp:View ID="View4" runat="server">
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
