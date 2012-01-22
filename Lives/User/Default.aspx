<%@ Page Title="" Language="C#" MasterPageFile="~/Authenticated.Master" AutoEventWireup="true"
	CodeBehind="Default.aspx.cs" Inherits="Lives.Users.HomeUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
			<a href="?view=0" rel="nofollow" class="style1">Gerir Vídeos</a></li>
		<li>
			<asp:Image ID="ImgUploadFile" runat="server" AlternateText="Categorias" Height="40px"
				ImageAlign="Middle" ImageUrl="~/images/file_upload.png" />
			<a class="style1" href="?view=2" rel="nofollow">Upload Vídeos</a></li>
	</div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
	<asp:HiddenField ID="idVideoToEdit" runat="server" />
	<asp:ToolkitScriptManager ID="ScriptManager" runat="Server">
	</asp:ToolkitScriptManager>
	<asp:ObjectDataSource ID="ODSVideoToEdit" runat="server" SelectMethod="obterVideo"
		TypeName="BLL.VideoBO">
		<SelectParameters>
			<asp:ControlParameter ControlID="idVideoToEdit" DbType="Int32" Name="id" ConvertEmptyStringToNull="true"
				PropertyName="Value" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:ObjectDataSource ID="OdsCategorias" runat="server" SelectMethod="obterTodas"
		TypeName="BLL.CategoriaBO"></asp:ObjectDataSource>
	<asp:ObjectDataSource ID="OdsSubcategorias" runat="server" SelectMethod="obterTodasSubCategoriasCategoria"
		TypeName="BLL.SubcategoriaBO">
		<SelectParameters>
			<asp:ControlParameter ControlID="ddlCategorias" Name="cat" PropertyName="SelectedValue"
				Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:ObjectDataSource ID="ODSVideosByUser" runat="server" SelectMethod="GetByUser"
		TypeName="BLL.VideoBO">
		<SelectParameters>
			<asp:ControlParameter ControlID="idUserHide" DbType="Guid" Name="idUser" PropertyName="Value" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:ObjectDataSource ID="ODSObterAprovados" runat="server" SelectMethod="obterVideosAprovadosUser"
		TypeName="BLL.VideoBO">
		<SelectParameters>
			<asp:ControlParameter ControlID="idUserHide" DbType="Guid" Name="idUser" PropertyName="Value" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:ObjectDataSource ID="ODSObterTodos" runat="server" SelectMethod="obterVideosUser"
		TypeName="BLL.VideoBO">
		<SelectParameters>
			<asp:ControlParameter ControlID="idUserHide" DbType="Guid" Name="idUser" PropertyName="Value" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:ObjectDataSource ID="ODSPorAprovar" runat="server" SelectMethod="obterVideosPorAprovadosUser"
		TypeName="BLL.VideoBO">
		<SelectParameters>
			<asp:ControlParameter ControlID="idUserHide" DbType="Guid" Name="idUser" PropertyName="Value" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:HiddenField ID="idUserHide" runat="server" />
	<h3 class="titulo">
		Gestão de Publicações</h3>
	<asp:Panel ID="panelFiltros" CssClass="painelFiltros" runat="server">
		<div class="colunaFiltro">
			Categorias:
			<asp:DropDownList ID="ddlCategorias" runat="server" DataSourceID="OdsCategorias"
				DataTextField="nome" DataValueField="id" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
			</asp:DropDownList>
		</div>
		<div class="colunaFiltro">
			Subcategorias:
			<asp:DropDownList ID="ddlSubcategorias" runat="server" Width="150px" AutoPostBack="true"
				DataSourceID="OdsSubcategorias" DataTextField="nome" DataValueField="id" OnSelectedIndexChanged="ddlSubcategorias_OnSelectedIndexChanged">
			</asp:DropDownList>
		</div>
		<div class="colunaFiltro">
			<asp:RadioButtonList ID="filtroVideos" runat="server" OnSelectedIndexChanged="FiltroVideos_OnSelectedIndexChanged"
				AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="0">
				<asp:ListItem Text="Por aprovar" Value=" Por Aprovar" />
				<asp:ListItem Text="Aprovados" Value=" Aprovados" />
				<asp:ListItem Text="Todos" Value="" />
			</asp:RadioButtonList>
		</div>
	</asp:Panel>
	<asp:MultiView ID="MultiViewVideos" runat="server" ActiveViewIndex="0">
		<asp:View ID="View1" runat="server">
			<div class="subtitulo">
				<asp:Label ID="lblSubtitulo" CssClass="subtitulo" runat="server" Text=""></asp:Label>
			</div>
			<asp:GridView ID="GridViewListaVideos" runat="server" AutoGenerateColumns="False"
				GridLines="None" OnRowDataBound="GridViewListaVideos_OnRowDataBound" ShowHeader="False"
				DataKeyNames="id">
				<Columns>
					<asp:TemplateField>
						<ItemTemplate>
							<div class="infoListagem">
								<div class="painelInfoListagemVideos">
									<div class="linhaInfoListagemVideos">
										<strong>Título: </strong>
										<asp:Label ID="Label4" runat="server" Font-Bold="false" Text='<%# Eval("titulo") %>'></asp:Label>
									</div>
								</div>
								<div class="videoListagem">
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
								<div class="painelLateralInfoListagemVideos">
									<div class="linhaLateralInfoListagemVideos">
										<strong>Etiquetas:</strong>
										<asp:Repeater runat="server" DataSource='<%# Eval("Subcategorias") %>' ID="SubCats">
											<ItemTemplate>
												<asp:Label ID="Label3" runat="server" Text='<%# Eval("nome") + "  " %>'></asp:Label></ItemTemplate>
										</asp:Repeater>
									</div>
									<div class="linhaLateralInfoListagemVideos">
										<strong>Aprovado?<asp:CheckBox ID="chkbAprovado" Enabled="false" runat="server" Checked='<%# (int) Eval("Estado.id") == 2 %>' /><br />
										</strong>
									</div>
									<div class="botoesLateralInfoListagemVideos">
										<div class="linhaLateralInfoListagemVideos botao editBorder">
											<asp:LinkButton ID="lbtnEditarVideo" CssClass="botao" runat="server" OnClick="lbtnEditarVideo_Click">Editar</asp:LinkButton></div>
										<div class="linhaLateralInfoListagemVideos botao removeBorder">
											<asp:LinkButton ID="lbtnApagarVideo" CssClass="botao" runat="server" OnClick="lbtnApagarVideo_Click">Remover</asp:LinkButton></div>
									</div>
								</div>
								<div class="painelInfoListagemVideos">
									<div class="linhaInfoListagemVideos">
										<strong>Descrição: </strong>
										<asp:Label ID="Label2" runat="server" Text='<%# Eval("descricao") %>'></asp:Label><br />
									</div>
									<div class="linhaInfoListagemVideos">
										<strong>Publicado: </strong>
										<asp:Label ID="Label38" runat="server" Text='<%# Eval("data") %>'></asp:Label>
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
		</asp:View>
		<asp:View ID="VideoEditView" runat="server">
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
								<asp:TextBox ID="txtBoxTitulo" runat="server" Columns="50" Width="200px"></asp:TextBox>
								<asp:TextBoxWatermarkExtender ID="txtBoxTitulo_TextBoxWatermarkExtender" runat="server"
									Enabled="True" TargetControlID="txtBoxTitulo" WatermarkText="Escreva o título!">
								</asp:TextBoxWatermarkExtender>
								Descrição:
								<asp:TextBox ID="txtBoxDescricao" runat="server" Columns="50" Width="200px"></asp:TextBox></p>
							<asp:TextBoxWatermarkExtender ID="TextBoxDescricao_WatermarkExtender" runat="server"
								Enabled="True" TargetControlID="txtBoxDescricao" WatermarkText="Insira uma descrição">
							</asp:TextBoxWatermarkExtender>
						</div>
						<div style="position: relative; float: left; width: 100%; height: 380px; background-color: Menu">
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
						<div style="position: relative; float: left; top: 65px; width: 100%;">
							<asp:Image ID="Image2" AlternateText="Informação do controlo" runat="server" ImageUrl="~/images/informacao.png"
								ImageAlign="AbsMiddle" />&nbsp
							<asp:ImageButton ID="btnInserirSubcategoria" runat="server" ImageAlign="Middle" ImageUrl="~/images/add.png"
								OnClick="btnInserirSubcategoria_Click" />
							&nbsp Etiquetas:
							<asp:Repeater ID="TagInserirVideoRepeater" runat="server">
								<ItemTemplate>
									<asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("nome") %>' OnClick="labelInserirVideoClickEventHandler" />
								</ItemTemplate>
							</asp:Repeater>
						</div>
					</div>
				</div>
		</asp:View>
	</asp:MultiView>
</asp:Content>
