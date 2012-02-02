﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Authenticated.Master" AutoEventWireup="true"
	EnableEventValidation="false" CodeBehind="Default.aspx.cs" Inherits="Lives.Admin.HomeAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Menu" ContentPlaceHolderID="menu" runat="Server">
	<div class="userState">
		<h3>
			Olá&nbsp;<asp:Label ID="lblNome" runat="server" Text="Paulo Luís"></asp:Label>
		</h3>
		<asp:Image ID="imagemLogo" runat="server" CssClass="imagemLogo" ImageUrl="~/images/user.png"
			Width="100px" />
	</div>
	<div class="listaMenu">
		<div class="labelOnline">
			<asp:Label ID="lblOnline" runat="server" Text="1"></asp:Label>
		</div>
		<h3>
			Administração</h3>
		<li>
			<asp:Image ID="Image4" runat="server" AlternateText="Users" Height="40px" ImageAlign="Middle"
				ImageUrl="~/images/online.png" /><b>Online</b></li>
		<li>
			<asp:Image ID="Image1" runat="server" AlternateText="Users" Height="40px" ImageAlign="Middle"
				ImageUrl="~/images/users.png" />
			<a href="Default.aspx?view=3" rel="nofollow" class="style1">Users</a></li>
		<li>
			<asp:Image ID="Image2" runat="server" AlternateText="Categorias" Height="40px" ImageAlign="Middle"
				ImageUrl="~/images/categorias.png" />
			<a class="style1" href="Default.aspx?view=2" rel="nofollow">Subcategorias</a></li>
		<li>
			<asp:Image ID="Image3" runat="server" AlternateText="Listagens" Height="40px" ImageAlign="Middle"
				ImageUrl="~/images/listagens.png" />
			<a href="Default.aspx?view=0" class="style1">Listagens</a></li>
	</div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
	<asp:ObjectDataSource ID="OdsCategorias" runat="server" SelectMethod="obterTodas"
		TypeName="BLL.CategoriaBO"></asp:ObjectDataSource>
	<asp:ObjectDataSource ID="OdsSubcategorias" runat="server" SelectMethod="obterTodasSubCategoriasCategoria"
		TypeName="BLL.SubcategoriaBO">
		<SelectParameters>
			<asp:ControlParameter ControlID="ddlCategorias" Name="cat" PropertyName="SelectedValue"
				Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="obterTodas"
		TypeName="BLL.CategoriaBO"></asp:ObjectDataSource>
	<asp:ObjectDataSource ID="ODSVideoToEdit" runat="server" SelectMethod="obterVideo"
		TypeName="BLL.VideoBO">
		<SelectParameters>
			<asp:ControlParameter ControlID="idVideoAprovacao" DbType="Int32" Name="id" ConvertEmptyStringToNull="true"
				PropertyName="Value" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:ObjectDataSource ID="ODSObterVideosPorAprovar" runat="server" SelectMethod="obterVideosPorAprovar"
		TypeName="BLL.VideoBO" />
	<asp:ObjectDataSource ID="ODSObterTodosVideos" runat="server" SelectMethod="obterTodosVideos"
		TypeName="BLL.VideoBO" />
	<asp:ObjectDataSource ID="ODSObterVideosAprovados" runat="server" SelectMethod="obterVideosAprovados"
		TypeName="BLL.VideoBO" />
	<asp:ObjectDataSource ID="ODSObterSubcategoriasCategoria" runat="server" SelectMethod="obterTodasSubCategoriasCategoria"
		TypeName="BLL.SubcategoriaBO">
		<SelectParameters>
			<asp:ControlParameter ControlID="categoriasDropBox" Name="cat" PropertyName="SelectedValue"
				Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<asp:ToolkitScriptManager ID="ScriptManager" runat="Server">
	</asp:ToolkitScriptManager>
	<asp:HiddenField ID="idVideoAprovacao" runat="server" />
	<h3 class="titulo">
		Administração</h3>
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
		<asp:View ID="ViewListagensVideos" runat="server">
			<div class="subtitulo">
				<asp:Label ID="lblSubtitulo" runat="server" Text=""></asp:Label>
			</div>
			<asp:GridView ID="GridViewListaVideos" runat="server" AutoGenerateColumns="False"
				GridLines="None" ShowHeader="False" DataKeyNames="id" OnRowDataBound="GridViewListaVideos_OnRowDataBound">
				<Columns>
					<asp:TemplateField>
						<ItemTemplate>
							<div class="infoListagem">
								<div class="painelInfoListagemVideos">
									<div class="linhaInfoListagemVideos">
										<strong>User: </strong>
										<asp:Label ID="lblUser" runat="server" Font-Bold="false"></asp:Label><br />
									</div>
									<div class="linhaInfoListagemVideos">
										<strong>Título: </strong>
										<asp:Label ID="Label1" runat="server" Font-Bold="false" Text='<%# Eval("titulo") %>'></asp:Label>
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
										<strong>Aprovado?<asp:CheckBox ID="chkbAprovado" OnCheckedChanged="aprovarVideo_check"
											AutoPostBack="true" runat="server" Checked='<%# (int) Eval("Estado.id") == 2 %>' /><br />
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
					<p class="informacao">
						Não existem vídeos<%= filtroVideos.SelectedItem.Value.ToLower() %>.</p>
				</EmptyDataTemplate>
			</asp:GridView>
		</asp:View>
		<asp:View ID="ViewEditVideo" runat="server">
			<div class="subtitulo">
				Editar Vídeos
			</div>
			<div class="row left infoTopoPagina">
				<asp:Label ID="LabelerroEditarVideo" CssClass="redError" runat="Server" Visible="false"></asp:Label>
			</div>
			<div class='editPanel'>
				<div class='row left'>
					<div class="infoImage">
						<asp:Image runat="server" ImageUrl="~/images/informacao.png" ToolTip="Para repor o título original basta limpar a caixa de texto." />
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
						<asp:Image ID="Image5" CssClass="infoImage" AlternateText="Informação do controlo"
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
		<asp:View ID="ViewEditarsubCategorias" runat="server">
			<div class="subtitulo">
				Editar Subcategorias</h3>
			</div>
			<div class="row left erroTopoPagina">
				<asp:Label ID="lblErro" CssClass="redError" runat="Server" Visible="false"></asp:Label>
			</div>
			<div class="editPanel">
				<div class="row left extraRightSpace">					
					<div class="letraCinzentoMedia field">
						Categorias:
						<asp:DropDownList ID="categoriasDropBox" runat="server" DataSourceID="OdsCategorias"
							DataTextField="nome" DataValueField="id" Width="150px" AutoPostBack="True">
						</asp:DropDownList>
					</div>
				</div>
				<div class="column left">
					<div class='row left'>
						<div class="letraCinzentoMedia field">
							<asp:ImageButton ID="lbtnInserirSubcategoria" runat="server" ImageAlign="AbsMiddle"
								ImageUrl="~/images/add.png" OnClick="btnNovaSubcategoria_Click" />
							Nova Subcategoria:
							<asp:TextBox ID="txtBoxNovaSubcategoria" runat="server" Columns="30" Width="150px"></asp:TextBox></p>
							<asp:TextBoxWatermarkExtender ID="txtBoxNovaSubcategoria_TextBoxWatermarkExtender"
								runat="server" Enabled="True" TargetControlID="txtBoxNovaSubcategoria" WatermarkText="Nova subcategoria!">
							</asp:TextBoxWatermarkExtender>
						</div>
					</div>
				</div>
				<div class="row left infoImageExtraTopoButtom">
					<div class="infoImage">
						<asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Para remover uma subcategoria clique na subcategoria." ImageUrl="~/images/informacao.png" />
					</div>					
				</div>
				<div class="column left">
					<div class='row left  extraTopSpace'>
					<div class="letraCinzentoMedia field">
						Etiquetas:
					</div>
						<asp:Repeater ID="RepeaterTag" runat="server" DataSourceID="ODSObterSubcategoriasCategoria">
							<ItemTemplate>
								<asp:LinkButton runat="server" Text='<%# Eval("nome") %>' OnClick="labelSubCatEditClickEventHandler" />
							</ItemTemplate>
						</asp:Repeater>
					</div>
				</div>
			</div>
		</asp:View>
		<asp:View ID="ViewUsers" runat="server">
			<div class="subtitulo">
				Users</div>
			<div class="row left infoTopoPagina">
				<asp:Label ID="Label4" CssClass="redError" runat="Server" Visible="false"></asp:Label>
				<asp:Label ID="lblErroResetPassword" CssClass="redError" runat="server" Visible="false"
					Text="Label"></asp:Label>
				<asp:Label ID="lblSucessoAlterarPass" runat="server" CssClass="passAlteradaSucesso"
					Visible="false" Text="Password alterada com sucesso!"></asp:Label>
			</div>
			<div class="row">
				<asp:GridView runat="server" ID="GridViewUser" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#FAFAFA"
					EmptyDataText="---" ShowFooter="true" FooterStyle-BackColor="#4e4a47" Font-Size="1em"
					RowStyle-VerticalAlign="Middle" DataKeyNames="ProviderUserKey, UserName" RowStyle-Height="3em"
					HeaderStyle-ForeColor="White" GridLines="None" HeaderStyle-HorizontalAlign="Center"
					HeaderStyle-BackColor="#4e4a47" HeaderStyle-Height="2em" FooterStyle-Height="0.1em"
					RowStyle-HorizontalAlign="Left" RowStyle-CssClass="gridViewLineUsers">
					<Columns>
						<asp:BoundField DataField="UserName" HeaderText="Utilizador" ItemStyle-Font-Bold="true"
							ItemStyle-Width="6em" />
						<asp:BoundField DataField="Email" HeaderText="Email" DataFormatString="<a href='mailto:{0}'>{0}</a>"
							HtmlEncode="false" />
						<asp:TemplateField>
							<HeaderTemplate>
								<b>Bloqueado</b>
							</HeaderTemplate>
							<ItemTemplate>
								<div class='celulaCentrada'>
									<asp:CheckBox ID="chkbUserBloqueado" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.IsLockedOut") %>'
										Enabled="False" />
								</div>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:BoundField DataField="CreationDate" HeaderText="Data Registo" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
						<asp:BoundField DataField="LastLoginDate" HeaderText="Ultima Sessão" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
						<asp:TemplateField ItemStyle-CssClass="celulaCentrada">
							<HeaderTemplate>
								Comandos
							</HeaderTemplate>
							<ItemTemplate>
								<asp:ImageButton CssClass="imagemBotaoTabela" ID="btnApagarUser" runat="server" ImageUrl="~/images/deleteUser.png"
									AlternateText="Remover User" OnClick="imgbtnApagarUser_Click" />
								<asp:ImageButton CssClass="imagemBotaoTabela" ID="btnBloquearUser" runat="server"
									ImageUrl="~/images/locked.png" AlternateText="Bloquear User" OnClick="imgbtnBloquearUser_Click"
									Visible='<%# !((bool)DataBinder.Eval(Container, "DataItem.IsLockedOut")) %>' />
								<asp:ImageButton CssClass="imagemBotaoTabela" ID="btndesbloquearUser" runat="server"
									ImageUrl="~/images/unlocked.png" AlternateText="Desbloquear User" OnClick="imgbtnDesbloquearUser_Click"
									Visible='<%#DataBinder.Eval(Container, "DataItem.IsLockedOut")%>' />
								<asp:ImageButton CssClass="imagemBotaoTabela" ID="btndesAlterarPass" runat="server"
									ImageUrl="~/images/changePassword.png" AlternateText="Alterar Pass User" OnClick="imgbtnAlterarPasswordUser_Click" />
							</ItemTemplate>
							<HeaderStyle />
						</asp:TemplateField>
					</Columns>
					<EmptyDataTemplate>
						<b>Não existem utilizadores!</b>
					</EmptyDataTemplate>
				</asp:GridView>
			</div>
		</asp:View>
	</asp:MultiView>
</asp:Content>