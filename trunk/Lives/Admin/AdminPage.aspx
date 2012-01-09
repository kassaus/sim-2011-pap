<%@ Page Title="" Language="C#" MasterPageFile="~/AdminLayout.master" AutoEventWireup="true"
    CodeBehind="AdminPage.aspx.cs" Inherits="Lives.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="corpo" runat="server">
    <div class="corpoInterior">
        <asp:MultiView ID="AdminMultiView" runat="server" ActiveViewIndex="0">
            <asp:View runat="server" ID="default">
            olá António!!!!!!!!!!
                <asp:GridView ID="VideosToApproveGridView" runat="server" AutoGenerateColumns="False"
                    DataSourceID="VideosDataSource" GridLines="None" ShowHeader="False" DataKeyNames="id">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div style="float: left; padding-bottom: 10px;">
                                    <div>
                                        <object classid="clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95" width="252" height="189"
                                            codebase="http://www.microsoft.com/Windows/MediaPlayer/">
                                            <param name="Filename" value='<%# Eval("url") %>'>
                                            <param name="AutoStart" value="false">
                                            <param name="ShowControls" value="true">
                                            <param name="BufferingTime" value="2">
                                            <param name="ShowStatusBar" value="false">
                                            <param name="AutoSize" value="true">
                                            <param name="InvokeURLs" value="false">
                                            <embed src='<%# "/Videos/" + Eval("url") %>' type="application/x-mplayer2" autostart="0" enabled="1"
                                                showstatusbar="0" showdisplay="1" showcontrols="1" pluginspage="http://www.microsoft.com/Windows/MediaPlayer/"
                                                codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,0,0"
                                                width="252" height="189"></embed>
                                        </object>
                                    </div>
                                </div>
                                <div style="float: left; padding-left: 10px; padding-top: 10px;">
									<asp:Label ID="Label99" runat="server" Text='<%# Eval("descricao") %>'></asp:Label><br />
									<asp:Label ID="Label9" runat="server" Text='Subcategorias:'></asp:Label><br />
                                    <div style="font-size: x-small">
                                        <asp:Repeater runat="server" DataSource='<%# Eval("Subcategorias") %>' ID="SubCats">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("nome") + ", " %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div style="font-size: x-small">Introduzido a: 
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("data") %>'></asp:Label><br />
                                    </div>
                                    <hr />
								</div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <p>
                            Não existem videos por aprovar.</p>
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:ObjectDataSource ID="VideosDataSource" runat="server" SelectMethod="obterVideosPorAprovar"
                    TypeName="BLL.VideoBO"></asp:ObjectDataSource>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
