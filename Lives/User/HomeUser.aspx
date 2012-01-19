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
			<hr style="margin: 0; padding: 0;" />
			<asp:Panel ID="panelFiltros" runat="server">
				<div style="position: relative; width: 100%; height: 40px; border-bottom-style: solid;
					border-bottom-width: thin; border-bottom-color: #666;">
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
							<asp:DropDownList ID="ddlSubcategorias" runat="server" Width="150px" AutoPostBack="true"
								DataSourceID="OdsSubcategorias" DataTextField="nome" DataValueField="id" OnSelectedIndexChanged="ddlSubcategorias_OnSelectedIndexChanged">
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
					<div style="position: absolute; height: 50%; width: 30%; top: 20%; left: 560px;">
						<asp:RadioButtonList CssClass="filtro" ID="filtroVideos" runat="server" OnSelectedIndexChanged="FiltroVideos_OnSelectedIndexChanged"
							AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="0">
							<asp:ListItem Text="Por aprovar" Value=" Por Aprovar" />
							<asp:ListItem Text="Aprovados" Value=" Aprovados" />
							<asp:ListItem Text="Todos" Value="" />
						</asp:RadioButtonList>
					</div>
				</div>
			</asp:Panel>
			<asp:MultiView ID="MultiViewVideos" runat="server" ActiveViewIndex="0">
				<asp:View ID="View1" runat="server">
					<div style="padding-bottom: 10px; border-top-style: solid; border-top-width: thin;
						border-top-color: #666; border-bottom-style: solid; border-bottom-width: thin;
						border-bottom-color: #666;">
						<h3>
							<asp:Label ID="lblSubtitulo" CssClass="subtitulo" runat="server" Text=""></asp:Label></h3>
					</div>
					<asp:GridView ID="ListaVideos" runat="server" AutoGenerateColumns="False" GridLines="None"
						OnRowDataBound="ListaVideos_RowDataBound" ShowHeader="False" DataKeyNames="id"
						DataSourceID="ODSVideosByUser">
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
						<EmptyDataTemplate>
							Não tem vídeos publicados.
						</EmptyDataTemplate>
					</asp:GridView>
					<asp:ObjectDataSource ID="ODSVideosByUser" runat="server" SelectMethod="GetByUser"
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
							<div style="position: relative; width: 100%; padding-bottom: 10px; padding-left: 10px;">
								<h3 class="subtitulo">
									Editar Vídeos</h3>
							</div>
							<div style="position: relative; top: 0px; height: 530px; width: 100%; border-top-style: solid;
								border-top-width: thin; border-top-color: #666;">
								<div style="position: absolute; float: none; top: 0px; width: 100%">
									<div style="position: relative; float: left; top: 0px; height: 530px; width: 44%;">
										<div style="position: relative; float: left; width: 60%;">
											<p class="letraCinzentoMedia" style="font-weight: bold">
												<asp:Image ID="Image1" AlternateText="Informação do controlo" runat="server" ImageUrl="~/images/informacao.png" />&nbsp
												Título:
												<asp:TextBox ID="txtBoxTitulo" runat="server" Columns="50" Width="200px"></asp:TextBox></p>
											<asp:TextBoxWatermarkExtender ID="txtBoxTitulo_TextBoxWatermarkExtender" runat="server"
												Enabled="True" TargetControlID="txtBoxTitulo" WatermarkText='<%# Eval("titulo") %>'>
											</asp:TextBoxWatermarkExtender>
										</div>
										<div style="position: relative; float: left; width: 100%; height: 380px;">
											<object classid="clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95" width="500" height="375"
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
													width="500" height="375"></embed>
											</object>
										</div>
										<div style="position: relative; float: right; margin-right: 5px; height: 20px; width: 400px;
											text-align: right">
											<span class="letraCinzentoPeq">Máximo 20Mb &nbsp&nbsp&nbsp<asp:FileUpload ID="FileUpload1"
												runat="server" Font-Bold="True" Height="20px" Width="250px" ForeColor="#000666"
												Font-Size="X-Small" BorderWidth="1" /></span>
										</div>
										<div style="position: relative; float: left; width: 100%; margin-top: 10px">
											<asp:Button ID="btnApagarSubcat" CssClass="botaoLogin" Height="40px" Width="133px"
												runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
											<span style="margin-left: 10px;">
												<asp:Button ID="btnConfirmarEdicaoVideo" CssClass="botaoLogin" Height="40px" Width="133px"
													runat="server" Text="Confirmar" /></span>
										</div>
									</div>
									<div style="position: relative; float: right; top: 0px; height: 530px; width: 56%;">
										<div style="position: relative; float: left; top: 48px; width: 100%;">
											<asp:Image ID="Image2" AlternateText="Informação do controlo" runat="server" ImageUrl="~/images/informacao.png"
												ImageAlign="AbsMiddle" />&nbsp Etiquetas:
											<asp:Repeater ID="TagRepeater" runat="server" DataSource='<%# Eval("Subcategorias") %>'>
												<ItemTemplate>
													<asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("nome") %>' OnClick="labelEditarVideoClickEventHandler" />
												</ItemTemplate>
											</asp:Repeater>
										</div>
									</div>
								</div>
						</ItemTemplate>
					</asp:Repeater>
				</asp:View>
				<asp:View ID="UploadVideos" runat="server">
					<div style="position: relative; width: 100%; padding-bottom: 10px; padding-left: 10px;">
						<h3 class="subtitulo">
							Upload Vídeos</h3>
					</div>
					<div style="position: relative; top: 0px; height: 530px; width: 100%; border-top-style: solid;
						border-top-width: thin; border-top-color: #666;">
						<div style="position: absolute; float: none; top: 0px; width: 100%">
							<div style="position: relative; float: left; top: 0px; height: 530px; width: 44%;">
								<div style="position: relative; float: left; width: 60%;">
									<p class="letraCinzentoMedia" style="font-weight: bold">
										<asp:Image ID="Image1" AlternateText="Informação do controlo" runat="server" ImageUrl="~/images/informacao.png" />&nbsp
										Título:
										<asp:TextBox ID="txtBoxTitulo" runat="server" Columns="50" Width="200px"></asp:TextBox></p>
									<asp:TextBoxWatermarkExtender ID="txtBoxTitulo_TextBoxWatermarkExtender" runat="server"
										Enabled="True" TargetControlID="txtBoxTitulo" WatermarkText="Escreva o título!">
									</asp:TextBoxWatermarkExtender>
								</div>
								<div style="position: relative; float: left; width: 100%; height: 380px;">
									<object classid="clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95" width="500" height="375"
										codebase="http://www.microsoft.com/Windows/MediaPlayer/">
										<!--<param name="Filename" value='<%# Eval("url") %>'>-->
										<param name="AutoStart" value="false">
										<param name="ShowControls" value="true">
										<param name="BufferingTime" value="2">
										<param name="ShowStatusBar" value="false">
										<param name="AutoSize" value="true">
										<param name="InvokeURLs" value="false">
										<!--<embed src='<%# "/Videos/" + Eval("url") %>' type="application/x-mplayer2" autostart="0"
													enabled="1" showstatusbar="0" showdisplay="1" showcontrols="1" pluginspage="http://www.microsoft.com/Windows/MediaPlayer/"
													codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,0,0"
													width="500" height="375"></embed>-->
									</object>
								</div>
								<div style="position: relative; float: right; margin-right: 5px; height: 20px; width: 400px;
									text-align: right">
									<span class="letraCinzentoPeq">Máximo 20Mb &nbsp&nbsp&nbsp<asp:FileUpload ID="FileUpload1"
										runat="server" Font-Bold="True" Height="20px" Width="250px" ForeColor="#000666"
										Font-Size="X-Small" BorderWidth="1" /></span>
								</div>
								<div style="position: relative; float: left; width: 100%; margin-top: 10px">
									<asp:Button ID="btnApagarSubcat" CssClass="botaoLogin" Height="40px" Width="133px"
										runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
									<span style="margin-left: 10px;">
										<asp:Button ID="btnConfirmarEdicaoVideo" CssClass="botaoLogin" Height="40px" Width="133px"
											runat="server" Text="Confirmar" /></span>
								</div>
							</div>
							<div style="position: relative; float: right; top: 0px; height: 530px; width: 56%;">
								<div style="position: relative; float: left; top: 48px; width: 100%;">
									<asp:Image ID="Image2" AlternateText="Informação do controlo" runat="server" ImageUrl="~/images/informacao.png"
										ImageAlign="AbsMiddle" />&nbsp Etiquetas:
									<!--<asp:Repeater ID="TagRepeater" runat="server" DataSource='<%# Eval("Subcategorias") %>'>
												<ItemTemplate>
													<asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("nome") %>' OnClick="labelEditarVideoClickEventHandler" />
												</ItemTemplate>
											</asp:Repeater>-->
								</div>
							</div>
						</div>
				</asp:View>
			</asp:MultiView>
		</div>
	</div>
</asp:Content>
