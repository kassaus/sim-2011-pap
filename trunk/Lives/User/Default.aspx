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
			<asp:ControlParameter ControlID="ddlCategoriasListagens" Name="cat" PropertyName="SelectedValue"
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
			<asp:DropDownList ID="ddlCategoriasListagens" runat="server" DataSourceID="OdsCategorias"
				DataTextField="nome" DataValueField="id" Width="150px" AutoPostBack="True" OnDataBound="ddlCategoriasListagens_OnDataBound"
				OnSelectedIndexChanged="ddlCategoriasListagens_SelectedIndexChanged">
			</asp:DropDownList>
		</div>
		<div class="colunaFiltro">
			Subcategorias:
			<asp:DropDownList ID="ddlSubcategoriasListagens" runat="server" Width="150px" AutoPostBack="true"
				DataSourceID="OdsSubcategorias" DataTextField="nome" DataValueField="id" OnDataBound="ddlSubcategoriasListagens_OnDataBound"
				OnSelectedIndexChanged="ddlSubcategoriasListagens_OnSelectedIndexChanged">
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
										<asp:Label ID="Label38" runat="server" Text='<%# string.Format("{0:yyyy-MM-dd HH:mm}", Eval("data")) %>'></asp:Label>
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
		<asp:View ID="ViewVideoEdit" runat="server">
			<div class="subtitulo">
				Editar Vídeos
			</div>
			<div class="row left infoTopoPagina">
				<asp:Label ID="LabelerroEditarVideo" CssClass="redError" runat="Server" Visible="false"></asp:Label>
			</div>
			<div class='editPanel'>
				<div class='row left'>
					<div class="infoImage">
						<asp:Image ID="Image5" runat="server" ImageUrl="~/images/informacao.png" ToolTip="Para repor o título original basta limpar a caixa de texto." />
					</div>
					<div class="letraCinzentoMedia field">
						Título:
						<asp:TextBox ID="txtBoxTituloEditarVideo" runat="server" Columns="50" Width="200px"></asp:TextBox>
					</div>
					<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderTituloEditarVideo" runat="server"
						Enabled="True" TargetControlID="txtBoxTituloEditarVideo">
					</asp:TextBoxWatermarkExtender>
				</div>
				<div class='row left'>
					<div class="infoImage">
						<asp:Image ID="Image9" runat="server" ImageUrl="~/images/informacao.png" ToolTip="Para repor a descrição original basta limpar a caixa de texto." />
					</div>
					<div class="letraCinzentoMedia field">
						Descrição:
						<asp:TextBox ID="txtBoxDescricaoEditarVideo" runat="server" Columns="255" Width="200px"></asp:TextBox></div>
					<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderDescricaoEditarVideo" runat="server"
						Enabled="True" TargetControlID="txtBoxDescricaoEditarVideo">
					</asp:TextBoxWatermarkExtender>
				</div>
				<div class="row left">
					<asp:Literal ID="LiteralVisualizaEditarVideo" runat="server"></asp:Literal>
				</div>
				<div class='column left'>
					<div class="column left">
						<asp:Image ID="Image3" CssClass="infoImage" AlternateText="Informação do controlo"
							runat="server" ImageUrl="~/images/informacao.png" ToolTip="Para remover uma Tag clique na respetiva tag!"
							ImageAlign="AbsMiddle" />&nbsp
						<asp:ImageButton ID="btnInserirSubcategoriaEditarVideo" runat="server" ImageAlign="Middle"
							ImageUrl="~/images/add.png" OnClick="btnInserirSubcategoria_Click" />
						Etiquetas:
					</div>
					<div class='column left painelEtiquetasActuais'>
						<asp:Repeater ID="RepeaterTagEditarVideo" runat="server">
							<ItemTemplate>
								<asp:LinkButton ID="LinkButtonTagEditarVideo" runat="server" Text='<%# Eval("nome") %>'
									OnClick="labelClickEventHandler" />
							</ItemTemplate>
						</asp:Repeater>
					</div>
					<div class="row right extraTopSpace">
						<asp:Panel ID="PainelAdicionarSubcategoriaEditarVideo" runat="server" Visible="false">
							<div class='row right'>
								Categorias:
								<asp:DropDownList ID="ddlCategoriasEditarVideo" runat="server" DataSourceID="OdsCategorias"
									OnDataBound="ddlCategoriasEditUploadVideo_OnDataBound" DataTextField="nome" DataValueField="id"
									Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoriasEditVideo_SelectedIndexChanged">
								</asp:DropDownList>
							</div>
							<div class='row right'>
								Subcategorias:
								<asp:DropDownList ID="ddlSubcategoriasEditarVideo" OnDataBound="ddlCategoriasEditUploadVideo_OnDataBound"
									runat="server" Width="150px" AutoPostBack="true" DataSourceID="OdsSubcategorias"
									Enabled="false" DataTextField="nome" DataValueField="id" OnSelectedIndexChanged="ddlSubcategoriasEditarVideo_OnSelectedIndexChanged">
								</asp:DropDownList>
							</div>
						</asp:Panel>
					</div>
				</div>
				<div class="row left extraTopSpace">
					Máximo 20MB
					<asp:FileUpload CssClass="fileUpload" ID="UploadEditarVideo" runat="server" />
				</div>
				<div class="column left extraTopSpace">
					<asp:Button ID="ButtonAnexarEditarVideo" CssClass="botaoLogin" runat="server" Text="Anexar"
						OnClick="ButtonAnexarEditarVideo_Click" />
				</div>
				<div class="row left extraTopSpace painelBotoes">
					<div class="column right">
						<asp:Button ID="ButtonCancelaredicaoVideo" CssClass="botaoLogin" runat="server" Text="Cancelar"
							OnClick="ButtonCancelarEdicaoVideo_Click" />
					</div>
					<div class="column right">
						<asp:Button ID="ButtonConfirmarEditarVideo" CssClass="botaoLogin" runat="server"
							Text="Confirmar" OnClick="btnConfirmarEdicaoVideo_Click" />
					</div>
				</div>
			</div>
		</asp:View>
		<asp:View ID="UploadVideos" runat="server">
			<div class="subtitulo">
				Upload Vídeo
			</div>
			<div class="row left infoTopoPagina">
				<asp:Label ID="LabelerroUploadVideo" CssClass="redError" runat="Server" Visible="false"></asp:Label>
				<asp:ValidationSummary CssClass="redError" ID="ValidationSummary1" runat="server"
					ForeColor="Red" ValidationGroup="grupo1" />
			</div>
			<div class='editPanel'>
				<div class='row left'>
					<div class="infoImage">
						<asp:Image ID="Image1" runat="server" ImageUrl="~/images/informacao.png" ToolTip="Para repor o título original basta limpar a caixa de texto." />
					</div>
					<div class="letraCinzentoMedia field">
						Título:
						<asp:TextBox ID="txtBoxTituloUploadVideo" runat="server" Columns="50" Width="200px"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBoxTituloUploadVideo"
							ErrorMessage="Campo obrigatório" ForeColor="Red" ValidationGroup="grupo1">*</asp:RequiredFieldValidator>
						<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderTituloUploadVideo" runat="server"
							Enabled="True" TargetControlID="txtBoxTituloUploadVideo" WatermarkText="Escreva aqui o título">
						</asp:TextBoxWatermarkExtender>
					</div>
				</div>
				<div class='row left'>
					<div class="infoImage">
						<asp:Image ID="Image2" runat="server" ImageUrl="~/images/informacao.png" ToolTip="Para repor a descrição original basta limpar a caixa de texto." />
					</div>
					<div class="letraCinzentoMedia field">
						Descrição:
						<asp:TextBox ID="txtBoxDescricaoUploadVideo" runat="server" Columns="255" Width="200px"></asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBoxDescricaoUploadVideo"
							ErrorMessage="Campo obrigatório" ForeColor="Red" ValidationGroup="grupo1">*</asp:RequiredFieldValidator>
						<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtenderDescricaoUploadVideo" runat="server"
							Enabled="True" TargetControlID="txtBoxDescricaoUploadVideo" WatermarkText="Escreva aqui a descrição">
						</asp:TextBoxWatermarkExtender>
					</div>
				</div>
				<div class="row left videoPlaceHolder">
					<asp:Literal ID="LiteralVisualizaUploadVideo" runat="server"></asp:Literal>
				</div>
				<div class='column left'>
					<div class="column left">
						<asp:Image ID="Image4" CssClass="infoImage" AlternateText="Informação do controlo"
							runat="server" ImageUrl="~/images/informacao.png" ToolTip="Para remover uma Tag clique na respetiva tag!"
							ImageAlign="AbsMiddle" />&nbsp
						<asp:ImageButton ID="btnInserirSubcategoriaUploadVideo" runat="server" ImageAlign="Middle"
							ImageUrl="~/images/add.png" OnClick="btnInserirSubcategoriaUpload_Click" />
						Etiquetas:
					</div>
					<div class='column left painelEtiquetasActuais'>
						<asp:Repeater ID="RepeaterTagUploadVideo" runat="server">
							<ItemTemplate>
								<asp:LinkButton ID="LinkButtonTagEditarVideo" runat="server" Text='<%# Eval("nome") %>'
									OnClick="labelClickUploadVideoEventHandler" />
							</ItemTemplate>
						</asp:Repeater>
					</div>
					<div class="row right extraTopSpace">
						<asp:Panel ID="PainelAdicionarSubcategoriaUploadVideo" runat="server" Visible="false">
							<div class='row right'>
								Categorias:
								<asp:DropDownList ID="ddlCategoriasUploadVideo" runat="server" DataSourceID="OdsCategorias"
									OnDataBound="ddlCategoriasEditUploadVideo_OnDataBound" DataTextField="nome" DataValueField="id"
									Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoriasUploadVideo_SelectedIndexChanged">
								</asp:DropDownList>
							</div>
							<div class='row right'>
								Subcategorias:
								<asp:DropDownList ID="ddlSubcategoriasUploadVideo" OnDataBound="ddlCategoriasEditUploadVideo_OnDataBound"
									runat="server" Width="150px" AutoPostBack="true" DataSourceID="OdsSubcategorias"
									Enabled="false" DataTextField="nome" DataValueField="id" OnSelectedIndexChanged="ddlSubcategoriasUploadVideo_OnSelectedIndexChanged">
								</asp:DropDownList>
							</div>
						</asp:Panel>
					</div>
				</div>
				<div class="row left extraTopSpace">
					Máximo 20MB
					<asp:FileUpload CssClass="fileUpload" ID="UploadVideo" runat="server" />
				</div>
				<div class="column left extraTopSpace">
					<asp:Button ID="ButtonAnexarUploadVideo" CssClass="botaoLogin" runat="server" Text="Anexar"
						OnClick="ButtonAnexarUploadVideo_Click" />
				</div>
				<div class="row left extraTopSpace painelBotoes">
					<div class="column right">
						<asp:Button ID="ButtonCancelarUploadVideo" CssClass="botaoLogin" runat="server" Text="Cancelar"
							OnClick="ButtonCancelarUploadVideo_Click" />
					</div>
					<div class="column right">
						<asp:Button ID="ButtonConfirmaruploadVideo" CssClass="botaoLogin" runat="server"
							Text="Confirmar" OnClick="btnConfirmarUploadVideo_Click" ValidationGroup="grupo1" />
					</div>
				</div>
			</div>
		</asp:View>
	</asp:MultiView>
</asp:Content>