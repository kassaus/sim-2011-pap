<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/CorpoAdministrador.master"
	AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AdminPage.aspx.cs"
	Inherits="Lives.AdminPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="corpo" runat="server">
	<link href="../Estilo.css" rel="stylesheet" type="text/css" />
	<asp:ToolkitScriptManager ID="ScriptManager" runat="Server">
	</asp:ToolkitScriptManager>
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
				Administração</h3>
			<hr style="margin: 0; padding: 0;" />
			<asp:Panel ID="panelFiltros" runat="server">
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
					<div style="padding-bottom: 10px; padding-left:10px; border-top-style: solid; border-top-width: thin;
						border-top-color: #666; border-bottom-style: solid; border-bottom-width: thin;
						border-bottom-color: #666;">
						<h3>
							<asp:Label ID="lblSubtitulo" CssClass="subtitulo" runat="server" Text=""></asp:Label></h3>
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
														<asp:Label ID="Label3" runat="server" Text='<%# Eval("nome") + "  " %>'></asp:Label></ItemTemplate>
												</asp:Repeater>
												<div>
													<span style="font-weight: bold">Aprovado?<asp:CheckBox ID="chkbAprovado" OnCheckedChanged="aprovarVideo_check"
														AutoPostBack="true" runat="server" Checked='<%# (int) Eval("Estado.id") == 2 %>' /><br />
													</span>
												</div>
												<div style="position: relative; top: 20px">
													<asp:LinkButton ID="lbtnEditar" CssClass="botaoLogin" Font-Underline="false" Width="80px"
														runat="server" OnClick="lbtnEditar_Click">Editar</asp:LinkButton></div>
												<div style="position: relative; top: 70px">
													<asp:LinkButton ID="lbtnApagar" CssClass="botaoLogin" Font-Underline="false" ForeColor="red"
														Width="80px" runat="server" OnClick="lbtnApagarVideo_Click">Remover</asp:LinkButton></div>
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
				<asp:View ID="VideoEditSubcategoriasView" runat="server">
					<asp:ObjectDataSource ID="ODSVideoToEdit" runat="server" SelectMethod="obterVideo"
						TypeName="BLL.VideoBO">
						<SelectParameters>
							<asp:ControlParameter ControlID="idVideoAprovacao" DbType="Int32" Name="id" ConvertEmptyStringToNull="true"
								PropertyName="Value" />
						</SelectParameters>
					</asp:ObjectDataSource>
					<asp:Repeater ID="VideoDetailsView" runat="server" DataSourceID="ODSVideoToEdit">
						<ItemTemplate>
							<div style="padding-bottom: 10px; padding-left:10px; border-top-style: solid; border-top-width: thin;
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
												<asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("nome") %>' OnClick="labelClickEventHandler" />
											</ItemTemplate>
										</asp:Repeater>
									</div>
									<div>
										<asp:Button ID="btnCategorizar" CssClass="botaoLogin" Height="40px" Width="150px"
											runat="server" Text="Classificar" OnClick="btnCategorizar_Click" />
										<span style="margin-left: 50px;"></span>
										<asp:Button ID="btnApagarSubcat" CssClass="botaoLogin" Height="40px" Width="150px"
											runat="server" Text="Apagar Etiquetas" OnClick="btnApagarSubcat_Click" />
									</div>
									<div style="position: relative; top: 20px; width: 500px; height: 375px;">
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
								</div>
							</div>
						</ItemTemplate>
					</asp:Repeater>
				</asp:View>
				<asp:View ID="EditarsubCategoriasView" runat="server">
					<div style="position: relative; padding-bottom: 10px; padding-left:10px; border-top-style: solid; border-top-width: thin;
						border-top-color: #666; border-bottom-style: solid; border-bottom-width: thin;
						border-bottom-color: #666;">
						<h3 class="subtitulo">
							Editar Subcategorias</h3>
						<div style="position: absolute; top: 20px; left: 320px">
							<div style="margin-left: auto; margin-right: auto; width: 300px">
								<asp:Label ID="lblErro" CssClass="redError" runat="Server" Visible="false"></asp:Label>
							</div>
						</div>
					</div>
					<div style="position: relative; top: 0px; padding-left: 50px">
						<p class="letraCinzentoMedia">
							Categorias:
							<asp:DropDownList ID="categoriasDropBox" runat="server" DataSourceID="OdsCategorias"
								DataTextField="nome" DataValueField="id" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="CategoriasDropBox_SelectedIndexChanged">
							</asp:DropDownList>
							<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="obterTodas"
								TypeName="BLL.CategoriaBO"></asp:ObjectDataSource>
						</p>
						<div class="letraCinzentoMedia" style="position: absolute; top: 3px; height: 200px;
							left: 370px; margin: 0px;">
							<span style="font-weight: bold">Etiquetas: </span>
							<asp:Repeater ID="TagRepeater" runat="server" DataSourceID="ODSObterSubcategoriasCategoria">
								<ItemTemplate>
									<asp:LinkButton runat="server" Text='<%# Eval("nome") %>' OnClick="labelSubCatEditClickEventHandler" />
								</ItemTemplate>
							</asp:Repeater>
						</div>
						<div style="position: absolute; top: 3px; left: 350px;">
							<asp:ImageButton ID="ImageButton2" runat="server" ImageAlign="Middle" ImageUrl="~/images/informacao.png" />
						</div>
						<div style="position: relative; top: 10; left: 0px">
							<p class="letraCinzentoMedia" style="font-weight: bold">
								Nova Subcategoria:
								<asp:TextBox ID="txtBoxNovaSubcategoria" runat="server" Columns="30" Width="150px"></asp:TextBox></p>
							<asp:TextBoxWatermarkExtender ID="txtBoxNovaSubcategoria_TextBoxWatermarkExtender"
								runat="server" Enabled="True" TargetControlID="txtBoxNovaSubcategoria" WatermarkText="Nova subcategoria!">
							</asp:TextBoxWatermarkExtender>
							<div style="position: absolute; top: 0; left: 290px;">
								<asp:ImageButton ID="lbtnInserirSubcategoria" runat="server" ImageAlign="Middle"
									ImageUrl="~/images/add.png" OnClick="btnNovaSubcategoria_Click" />
							</div>
						</div>
						<div style="position: relative; height: 100px;">
						</div>
						<div style="position: absolute; top: 33px; left: 30px;">
							<asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="Middle" ImageUrl="~/images/informacao.png" />
						</div>
					</div>
				</asp:View>
				<asp:View ID="usersView" runat="server">
					<div style="position: relative; padding-bottom: 10px; padding-left:10px; border-bottom-style: solid;
						border-bottom-width: thin; border-bottom-color: #666;">
						<h3 class="subtitulo">
							Users</h3>
						<div style="position: absolute; top: 20px; left: 320px">
							<div style="margin-left: auto; margin-right: auto; width: 300px">
								<asp:Label ID="Label4" CssClass="redError" runat="Server" Visible="false"></asp:Label>
							</div>
						</div>
					</div>
					<div style="position: relative; margin-top: 20px; margin-left: 20px; margin-bottom: 20px;">
						<table border="0" cellspacing="0" cellpadding="0" width="100%" align="center">
							<tr>
								<td>
									<asp:GridView runat="server" ID="gridViewUser" AllowSorting="True" AutoGenerateColumns="False"
										Font-Size="X-Small" HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"
										DataKeyNames="ProviderUserKey, UserName" RowStyle-Height="40px" EditRowStyle-HorizontalAlign="Center"
										EditRowStyle-VerticalAlign="Middle" HeaderStyle-ForeColor="White" GridLines="None"
										HeaderStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#666666">
										<Columns>
											<asp:TemplateField ItemStyle-CssClass="gridViewLineUsers">
												<HeaderTemplate>
													<b>Utilizador</b>
												</HeaderTemplate>
												<ItemTemplate>
													<%#DataBinder.Eval(Container, "DataItem.UserName")%>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField ItemStyle-CssClass="gridViewLineUsers">
												<HeaderTemplate>
													<b>Email</b>
												</HeaderTemplate>
												<ItemTemplate>
													<%#DataBinder.Eval(Container, "DataItem.Email")%>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField ItemStyle-CssClass="gridViewLineUsers">
												<HeaderTemplate>
													<b>Aprovado</b>
												</HeaderTemplate>
												<ItemTemplate>
													<asp:CheckBox ID="chkbUserAprovado" runat="server" Text="" Checked='<%#DataBinder.Eval(Container, "DataItem.IsApproved")%>'
														Enabled="False" />
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField ItemStyle-CssClass="gridViewLineUsers">
												<HeaderTemplate>
													<b>Bloqueado</b>
												</HeaderTemplate>
												<ItemTemplate>
													<asp:CheckBox ID="chkbUserBloqueado" runat="server" Text="" Checked='<%#DataBinder.Eval(Container, "DataItem.IsLockedOut")%>'
														Enabled="False" />
												</ItemTemplate>
												<ItemStyle HorizontalAlign="Center" Width="70px" />
											</asp:TemplateField>
											<asp:TemplateField ItemStyle-CssClass="gridViewLineUsers">
												<HeaderTemplate>
													<b>Data Registo</b>
												</HeaderTemplate>
												<ItemTemplate>
													<%#DataBinder.Eval(Container, "DataItem.CreationDate")%>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField ItemStyle-CssClass="gridViewLineUsers">
												<HeaderTemplate>
													<b>Ultima Sessão</b>
												</HeaderTemplate>
												<ItemTemplate>
													<%#DataBinder.Eval(Container, "DataItem.LastLoginDate")%>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField ItemStyle-CssClass="gridViewLineUsers">
												<HeaderTemplate>
													Comandos
												</HeaderTemplate>
												<ItemTemplate>
													<asp:ImageButton ID="btnApagarUser" runat="server" ImageUrl="~/images/deleteUser.png"
														AlternateText="Remover User" OnClick="imgbtnApagarUser_Click"></asp:ImageButton>&nbsp&nbsp
													<asp:ImageButton ID="btnBloquearUser" runat="server" ImageUrl="~/images/locked.png"
														AlternateText="Bloquear User" OnClick="imgbtnBloquearUser_Click"></asp:ImageButton>&nbsp&nbsp
													<asp:ImageButton ID="btndesbloquearUser" runat="server" ImageUrl="~/images/unlocked.png"
														AlternateText="Desbloquear User" OnClick="imgbtnDesbloquearUser_Click"></asp:ImageButton>&nbsp&nbsp
													<asp:ImageButton ID="btndesAlterarPass" runat="server" ImageUrl="~/images/changePassword.png"
														AlternateText="Alterar Pass User" OnClick="imgbtnAlterarPasswordUser_Click">
													</asp:ImageButton>
												</ItemTemplate>
												<HeaderStyle />
												<ItemStyle HorizontalAlign="Center" Width="130px" />
											</asp:TemplateField>
										</Columns>
										<EmptyDataTemplate>
											<b>Não existem utilizadores!</b></EmptyDataTemplate>
									</asp:GridView>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="lblErroResetPassword" CssClass="error" runat="server" Visible="false"
										Text="Label"></asp:Label>
								</td>
							</tr>
						</table>
						<br />
						<br />
						<div style="margin-left: 30%; margin-right: 30%">
							<strong>
								<asp:Label ID="lblSucessoAlterarPass" runat="server" CssClass="passAlteradaSucesso"
									Visible="false" Text="Password alterada com sucesso!"></asp:Label></strong>
						</div>
					</div>
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
	<asp:ObjectDataSource ID="ODSObterSubcategoriasCategoria" runat="server" SelectMethod="obterTodasSubCategoriasCategoria"
		TypeName="BLL.SubcategoriaBO">
		<SelectParameters>
			<asp:ControlParameter ControlID="categoriasDropBox" Name="cat" PropertyName="SelectedValue"
				Type="Int32" />
		</SelectParameters>
	</asp:ObjectDataSource>
</asp:Content>
