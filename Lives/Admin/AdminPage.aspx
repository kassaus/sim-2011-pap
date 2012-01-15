<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/CorpoAdministrador.master"
	AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Lives.AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="corpo" runat="server">
	<asp:HiddenField ID="idVideoAprovacao" runat="server" />
	<style type="text/css">
		html
		{
			overflow: auto;
		}
	</style>
	<div id="ContentorCorpoInterior">
		<div class="corpoInterior">
			<h3 class="titulo">
				Gestão de publicações</h3>
			<hr style="margin: 0; padding: 0;" />
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
				<div style="position: absolute; height: 50%; width: 20%; top: 20%; left: 580px;">
					<asp:RadioButtonList CssClass="filtro" ID="filtroVideos" runat="server" OnSelectedIndexChanged="FiltroVideos_OnSelectedIndexChanged"
						AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="0">
						<asp:ListItem Text="Por aprovar" Value=" Por Aprovar" />
						<asp:ListItem Text="Aprovados" Value=" Aprovados" />
						<asp:ListItem Text="Todos" Value="" />
					</asp:RadioButtonList>
				</div>
			</div>
			<asp:MultiView ID="MultiViewVideos" runat="server" ActiveViewIndex="0">
				<asp:View ID="View1" runat="server">
					<div style="padding-bottom: 10px; border-top-style: solid; border-top-width: thin;
						border-top-color: #666; border-bottom-style: solid; border-bottom-width: thin;
						border-bottom-color: #666;">
						<h3 class="subtitulo">
							Listagem de Vídeos<%= filtroVideos.SelectedItem.Value %></h3>
					</div>
					<asp:GridView ID="ListaVideos" runat="server" AutoGenerateColumns="False" GridLines="None"
						ShowHeader="False" DataKeyNames="id" OnRowDataBound="ListaVideos_OnRowDataBound">
						<Columns>
							<asp:TemplateField>
								<ItemTemplate>
									<div style="position: relative; margin-left: 10px; padding-bottom: 10px; padding-top: 10px;
										width: 500px">
										<strong>User:</strong>&nbsp
										<asp:Label ID="lblUser" runat="server" Font-Bold="false"></asp:Label><br />
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
													<span style="font-weight: bold">Aprovado?<asp:CheckBox ID="chkbAprovado" OnCheckedChanged="aprovarVideo_check"
														AutoPostBack="true" runat="server" Checked='<%# (int) Eval("Estado.id") == 2 %>' /><br />
													</span>
												</div>
												<div style="position: relative; top: 20px">
													<asp:LinkButton ID="lbtnEditar" CssClass="botaoLogin" Font-Underline="false" Width="80px"
														runat="server" OnClick="lbtnEditar_Click">Editar</asp:LinkButton>
												</div>
												<div style="position: relative; top: 70px">
													<asp:LinkButton ID="lbtnApagar" CssClass="botaoLogin" Font-Underline="false" ForeColor="red"
														Width="80px" runat="server" OnClick="lbtnApagarVideo_Click">Remover</asp:LinkButton>
												</div>
											</div>
										</div>
									</div>
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
						<EmptyDataTemplate>
							<p class="informacao">
								Não existem vídeos<%= filtroVideos.SelectedItem.Value.ToLower() %>.</p>
						</EmptyDataTemplate>
					</asp:GridView>
				</asp:View>
				<asp:View ID="View3" runat="server">
				</asp:View>
				<asp:View ID="View4" runat="server">
				</asp:View>
			</asp:MultiView>
		</div>
	</div>
	<asp:ObjectDataSource ID="ODSObterVideosPorAprovar" runat="server" SelectMethod="obterVideosPorAprovar"
		TypeName="BLL.VideoBO" />
	<asp:ObjectDataSource ID="ODSObterTodosVideos" runat="server" SelectMethod="obterTodosVideos"
		TypeName="BLL.VideoBO" />
	<asp:ObjectDataSource ID="ODSObterVideosAprovados" runat="server" SelectMethod="obterVideosAprovados"
		TypeName="BLL.VideoBO" />
</asp:Content>
