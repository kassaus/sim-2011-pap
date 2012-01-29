using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI.WebControls;
using BLL;
using BO;

namespace Lives.Admin
{
	public partial class HomeAdmin : System.Web.UI.Page
	{
		private VideoBO gestorVideos { get; set; }

		private SubcategoriaBO gestorSubcategorias { get; set; }

		private CategoriaBO gestorCategorias { get; set; }

		private UserMembershipBO gestorUsers { get; set; }

		private static List<Subcategoria> listaEtiquetas = null;
		private string DIRETORIO_VIDEOS = "/Videos";
		private static Video novoVideo = new Video();
		private static Video videoOriginal = null;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Request.IsAuthenticated)
			{
				Response.Redirect("~/", true);
			}

			lblNome.Text = Membership.GetUser().UserName;
			lblOnline.Text = Membership.GetNumberOfUsersOnline().ToString();


			ODSObterSubcategoriasCategoria.DataBind();

			if (listaEtiquetas == null)
			{
				listaEtiquetas = new List<Subcategoria>();
			}

			if (gestorSubcategorias == null)
			{
				gestorSubcategorias = new SubcategoriaBO();
			}

			if (gestorCategorias == null)
			{
				gestorCategorias = new CategoriaBO();
			}

			if (gestorVideos == null)
			{
				gestorVideos = new VideoBO();
			}

			if (gestorUsers == null)
			{
				gestorUsers = new UserMembershipBO();
			}

			if (filtroVideos.SelectedItem == null)
			{
				filtroVideos.SelectedIndex = 0;
				FiltroVideos_OnSelectedIndexChanged(filtroVideos.SelectedItem, null);
			}

			if (!IsPostBack)
			{
				try
				{
					MultiViewVideos.ActiveViewIndex = int.Parse(Request.Params["view"]);
				}
				catch
				{
					MultiViewVideos.ActiveViewIndex = 0;
				}

				if (MultiViewVideos.ActiveViewIndex == 1)
				{
					novoVideo = new Video();
					listaEtiquetas = new List<Subcategoria>();
				}
			}


			if (MultiViewVideos.ActiveViewIndex != 0)
			{
				panelFiltros.Visible = false;
			}

			if (MultiViewVideos.ActiveViewIndex == 3)
			{
				GridViewUser.DataSource = Membership.GetAllUsers();
				GridViewUser.DataBind();
			}
		}

		protected void ddlCategoriasEditUploadVideo_OnDataBound(object sender, EventArgs e)
		{
			DropDownList ddl = ((DropDownList)sender);
			if (ddl.Items.FindByValue(null) == null)
			{
				ddl.Items.Insert(0, new ListItem(null, null));
			}
		}

		private void reproduzVideo(string url, Literal literal)
		{
			int height = 375;
			int width = 500;

			url = DIRETORIO_VIDEOS + "/" + url;
			literal.Text = "<object classid='clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95' width='" + width + "' height='" + height + "' " +
				"codebase='http://www.microsoft.com/Windows/MediaPlayer/'>" +
				   "<param name='allowFullScreen' value='true'>" +
				   "<param name='allowScriptAccess' value='always'>" +
				   "<param name='Filename' value='" + url + "'>" +
						"<param name='AutoStart' value='true'>" +
						"<param name='ShowControls' value='true'>" +
						"<param name='BufferingTime' value='2'>" +
						"<param name='ShowStatusBar' value='false'>" +
						"<param name='AutoSize' value='true'>" +
						"<param name='InvokeURLs' value='false'>" +

						"<embed src='" + url + "' type='application/x-mplayer2' autostart='0'" +
							"enabled='1' showstatusbar='0' showdisplay='1' showcontrols='1' pluginspage='http://www.microsoft.com/Windows/MediaPlayer/'" +
							"codebase='http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,0,0'" +
							"width='" + width + "' height='" + height + "'>" + "</embed>" + "</object>";
			literal.DataBind();
		}

		#region //Listagem dos videos

		protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
		{
			OdsSubcategorias.SelectMethod = "obterTodasSubCategoriasCategoria";
			OdsSubcategorias.SelectParameters.Clear();
			OdsSubcategorias.SelectParameters.Add("cat", TypeCode.Int32, ddlCategorias.SelectedValue);
			ddlSubcategorias.Enabled = true;
			ddlSubcategorias.DataBind();
		}

		protected void ddlSubcategorias_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaId(int.Parse(ddlSubcategorias.SelectedValue));

			GridViewListaVideos.DataSource = gestorVideos.obterTodosVideosSubcategoria(subcategoria.id);
			GridViewListaVideos.DataBind();
			lblSubtitulo.Text = "Listagem de vídeos da Subcategoria " + subcategoria.nome;
		}

		protected void GridViewListaVideos_OnRowDataBound(object sender, GridViewRowEventArgs e)
		{
			GridViewRow row = e.Row;

			if (row.RowType == DataControlRowType.DataRow)
			{
				Video video = row.DataItem as Video;
				Label label = row.FindControl("lblUser") as Label;

				label.Text = Membership.GetUser(video.id_user).UserName;
			}
		}

		protected void aprovarVideo_check(object sender, EventArgs e)
		{
			GridViewRow row = (GridViewRow)(sender as CheckBox).NamingContainer;
			idVideoAprovacao.Value = ((GridView)row.NamingContainer).DataKeys[row.RowIndex].Value.ToString();

			CheckBox aprovar = (CheckBox)sender;
			if (aprovar.Checked)
			{
				gestorVideos.aprovar(int.Parse(idVideoAprovacao.Value));
				filtroVideos.SelectedIndex = 1;
				FiltroVideos_OnSelectedIndexChanged(filtroVideos.SelectedItem, null);
				MultiViewVideos.ActiveViewIndex = 0;
			}
			else
			{
				gestorVideos.desaprova(int.Parse(idVideoAprovacao.Value));
				filtroVideos.SelectedIndex = 0;
				FiltroVideos_OnSelectedIndexChanged(filtroVideos.SelectedItem, null);
				MultiViewVideos.ActiveViewIndex = 0;
			}
		}

		protected void lbtnEditarVideo_Click(object sender, EventArgs e)
		{
			GridViewRow row = (GridViewRow)(sender as LinkButton).NamingContainer;
			idVideoAprovacao.Value = ((GridView)row.NamingContainer).DataKeys[row.RowIndex].Value.ToString();
			MultiViewVideos.ActiveViewIndex = 1;
			panelFiltros.Visible = false;
			filtroVideos.SelectedIndex = 0;
			ddlCategorias.ClearSelection();
			videoOriginal = gestorVideos.obterVideo(int.Parse(idVideoAprovacao.Value));
			TextBoxWatermarkExtenderTituloEditarVideo.WatermarkText = videoOriginal.titulo;
			TextBoxWatermarkExtenderDescricaoEditarVideo.WatermarkText = videoOriginal.descricao;
			reproduzVideo(videoOriginal.url, LiteralVisualizaEditarVideo);
			novoVideo = new Video();

			Repeater tagsRepeater = MultiViewVideos.Views[1].FindControl("RepeaterTagEditarVideo") as Repeater;
			Subcategoria[] aux = new Subcategoria[videoOriginal.Subcategorias.Count];
			videoOriginal.Subcategorias.CopyTo(aux, 0);
			listaEtiquetas = new List<Subcategoria>(aux);

			tagsRepeater.DataSource = listaEtiquetas;
			tagsRepeater.DataBind();
		}

		protected void lbtnApagarVideo_Click(object sender, EventArgs e)
		{
			LinkButton apagar = sender as LinkButton;
			GridViewRow row = (GridViewRow)apagar.NamingContainer;
			GridView Videos = (GridView)row.NamingContainer;

			string VideoId = Convert.ToString(Videos.DataKeys[row.RowIndex].Value);

			VideoBO videoBO = new VideoBO();
			bool teste = videoBO.removeVideo(int.Parse(VideoId));

			Videos.DataBind();
			filtroVideos.SelectedIndex = 0;
			FiltroVideos_OnSelectedIndexChanged(filtroVideos.SelectedItem, null);
		}

		protected void FiltroVideos_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			switch (filtroVideos.SelectedIndex)
			{
				case 0:
				default:
					GridViewListaVideos.DataSource = ODSObterVideosPorAprovar;
					lblSubtitulo.Text = "Listagem de vídeos Por Aprovar";
					break;
				case 1:
					GridViewListaVideos.DataSource = ODSObterVideosAprovados;
					lblSubtitulo.Text = "Listagem de Vídeos Aprovados";
					break;
				case 2:
					GridViewListaVideos.DataSource = ODSObterTodosVideos;
					lblSubtitulo.Text = "Listagem de Todos Vídeos";
					break;
			}

			GridViewListaVideos.DataBind();
		}

		#endregion //Listagem dos videos

		#region //Editar Vídeos

		protected void btnConfirmarEdicaoVideo_Click(object sender, EventArgs e)
		{
			if (novoVideo.url == null)
			{
				if (gestorVideos.modificaVideo(txtBoxDescricaoEditarVideo.Text, txtBoxTituloEditarVideo.Text, videoOriginal.url, int.Parse(idVideoAprovacao.Value), listaEtiquetas))
				{
					Response.Redirect("?view=0", true);
				}
				else
				{
					LabelerroEditarVideo.Visible = true;
					LabelerroEditarVideo.Text = "Não foi possivel atualizar a base de dados, tente novamente!";
				}
			}
			else
			{
				string videoAnterior = videoOriginal.url;
				if (gestorVideos.modificaVideo(txtBoxDescricaoEditarVideo.Text, txtBoxTituloEditarVideo.Text, novoVideo.url, int.Parse(idVideoAprovacao.Value), listaEtiquetas))
				{
					apagaFicheiroDiretorioVideos(videoAnterior);
					Response.Redirect("./?view=0", true);
				}
				else
				{
					LabelerroEditarVideo.Visible = true;
					LabelerroEditarVideo.Text = "Não foi possivel atualizar a basde de dados, tente novamente!";
				}
			}
		}

		protected void ButtonCancelarEdicaoVideo_Click(object sender, EventArgs e)
		{
			if (novoVideo.url != null)
			{
				apagaFicheiroDiretorioVideos(novoVideo.url);
			}
			Response.Redirect("./?view=0", true);
		}

		protected void labelClickEventHandler(object sender, EventArgs e)
		{
			LinkButton etiqueta = (LinkButton)sender;
			Subcategoria subCat = gestorSubcategorias.obterSubCategoriaNome(etiqueta.Text);

			listaEtiquetas.Remove(subCat);
			RepeaterTagEditarVideo.DataSource = listaEtiquetas;
			RepeaterTagEditarVideo.DataBind();
		}

		protected void btnInserirSubcategoria_Click(object sender, EventArgs e)
		{
			PainelAdicionarSubcategoriaEditarVideo.Visible = !PainelAdicionarSubcategoriaEditarVideo.Visible;
			ddlCategoriasEditarVideo.ClearSelection();
			ddlSubcategoriasEditarVideo.ClearSelection();
			ddlSubcategoriasEditarVideo.Enabled = false;
		}

		protected void ddlCategoriasEditVideo_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList ddl = ((DropDownList)sender);
			DropDownList subcat = ddl.FindControl("ddlSubcategoriasEditarVideo") as DropDownList;

			if (ddl.SelectedIndex == 0)
			{
				subcat.Enabled = false;
				subcat.ClearSelection();
			}
			else
			{
				OdsSubcategorias.SelectMethod = "obterTodasSubCategoriasCategoria";
				OdsSubcategorias.SelectParameters.Clear();
				OdsSubcategorias.SelectParameters.Add("cat", TypeCode.Int32, ddl.SelectedValue);

				subcat.Enabled = true;
				subcat.DataBind();
			}
		}

		protected void ddlSubcategoriasEditarVideo_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList subcat = (DropDownList)sender;

			Subcategoria subCat = gestorSubcategorias.obterSubCategoriaId(int.Parse(subcat.SelectedValue));

			if (!listaEtiquetas.Contains(subCat))
			{
				listaEtiquetas.Add(subCat);

				((DropDownList)PainelAdicionarSubcategoriaEditarVideo.FindControl("ddlCategoriasEditarVideo")).ClearSelection();

				RepeaterTagEditarVideo.DataSource = listaEtiquetas;
				RepeaterTagEditarVideo.DataBind();
				LabelerroEditarVideo.Visible = false;
				PainelAdicionarSubcategoriaEditarVideo.Visible = false;
			}
			else
			{
				LabelerroEditarVideo.Visible = true;
				LabelerroEditarVideo.Text = "Já inseriu essa subcategoria!";
			}
		}

		protected void ddlEditarVideo_OnDataBound(object sender, EventArgs e)
		{
			DropDownList ddl = ((DropDownList)sender);
			if (ddl.Items.FindByValue(null) == null)
			{
				ddl.Items.Insert(0, new ListItem(null, null));
			}
		}



		#endregion //Editar Vídeos

		#region //Editar Subcategorias

		protected void btnNovaSubcategoria_Click(object sender, EventArgs e)
		{
			if (txtBoxNovaSubcategoria.Text == "")
			{
				lblErro.Visible = true;
				lblErro.Text = "Primeiro escreva a nova Subcategoria!";
			}
			else
			{
				gestorSubcategorias.criarSubCategoria(txtBoxNovaSubcategoria.Text, int.Parse(categoriasDropBox.SelectedItem.Value));
				RepeaterTag.DataBind();
				txtBoxNovaSubcategoria.Text = "";
				lblErro.Visible = false;
			}
		}

		protected void labelSubCatEditClickEventHandler(object sender, EventArgs e)
		{
			LinkButton etiqueta = (LinkButton)sender;
			if (gestorSubcategorias.removeSubcategoria(etiqueta.Text))
			{
				etiqueta.Parent.DataBind();
			}
			else
			{
				throw new NotImplementedException();
			}
		}



		#endregion //Editar Subcategorias

		#region //Gerir Users

		protected void imgbtnBloquearUser_Click(object sender, EventArgs e)
		{
			ImageButton imgbtnApagarUser = sender as ImageButton;
			GridViewRow row = (GridViewRow)imgbtnApagarUser.NamingContainer;
			Guid userId = (Guid)(GridViewUser.DataKeys[row.RowIndex].Value);
			string userName = (String)GridViewUser.DataKeys[row.RowIndex].Values[1];
			actualizaEstadoLockUser(userName, userId, true);
		}

		protected void imgbtnDesbloquearUser_Click(object sender, EventArgs e)
		{
			ImageButton imgbtnApagarUser = sender as ImageButton;
			GridViewRow row = (GridViewRow)imgbtnApagarUser.NamingContainer;
			Guid userId = (Guid)(GridViewUser.DataKeys[row.RowIndex].Value);
			string userName = (String)GridViewUser.DataKeys[row.RowIndex].Values[1];
			actualizaEstadoLockUser(userName, userId, false);
		}

		private void actualizaEstadoLockUser(string userName, Guid userId, bool estado)
		{
			MembershipUser user = Membership.GetUser(userName);

			if (user != null && user.IsApproved)
			{
				gestorUsers.modificaEstadoLock(userId, estado);
			}
			GridViewUser.DataBind();
			Response.Redirect("./?view=3", false);
		}

		protected void imgbtnApagarUser_Click(object sender, EventArgs e)
		{
			ImageButton imgbtnApagarUser = sender as ImageButton;
			GridViewRow row = (GridViewRow)imgbtnApagarUser.NamingContainer;
			Guid UserId = (Guid)(GridViewUser.DataKeys[row.RowIndex].Value);
			string UserName = (String)GridViewUser.DataKeys[row.RowIndex].Values[1];

			List<Video> videos = gestorVideos.obterVideosUser(UserId);

			if (videos.Count > 0)
			{
				foreach (Video video in videos)
				{
					gestorVideos.removeVideo(video.id);
				}
				Membership.DeleteUser(UserName);
			}
			else
			{
				Membership.DeleteUser(UserName);
			}
			GridViewUser.DataBind();
			Response.Redirect("./?view=3", true);
		}

		protected void imgbtnAlterarPasswordUser_Click(object sender, EventArgs e)
		{
			string novaPassword = null;
			string email = null;
			ImageButton imgbtnApagarUser = sender as ImageButton;
			GridViewRow row = (GridViewRow)imgbtnApagarUser.NamingContainer;
			Guid userId = (Guid)(GridViewUser.DataKeys[row.RowIndex].Value);
			string userName = (String)GridViewUser.DataKeys[row.RowIndex].Values[1];

			MembershipUser user = Membership.GetUser(userName);
			email = user.Email;
			if (user != null && user.IsApproved)
			{
				try
				{
					novaPassword = user.ResetPassword();
				}
				catch { }
				if (novaPassword != null)
				{
					enviaEmailNovaPass(userName, Server.HtmlEncode(novaPassword), email);
					lblSucessoAlterarPass.Visible = true;
				}
				else
				{
					lblErroResetPassword.Visible = true;
					lblErroResetPassword.Text = "Não foi possivel satisfazer o seu pedido";
				}
			}
		}

		protected void enviaEmailNovaPass(string username, string password, string email)
		{
			string to = email;
			string subject = "Password";

			MailDefinition md = new MailDefinition();
			md.BodyFileName = "~/Resources/PasswordRecoveryEmail.txt";
			md.IsBodyHtml = true;
			MailMessage ms = md.CreateMailMessage(to, new Dictionary<string, string>() { { "<%UserName%>", username }, { "<%Password%>", password } }, this);

			SendEmail.EnviarEmail(to, subject, ms.Body);
		}

		#endregion //Gerir Users

		protected string findControloTextBoxRepeater(Repeater repeater, string id)
		{
			string valor = null;
			foreach (RepeaterItem item in repeater.Items)
			{
				if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
				{
					TextBox controlo = (TextBox)item.FindControl(id);
					valor = controlo.Text;
				}
			} return valor;
		}

		private void apagaFicheiroDiretorioVideos(string nome_ficheiro)
		{
			if (System.IO.File.Exists(Server.MapPath(DIRETORIO_VIDEOS) + "\\" + nome_ficheiro))
			{
				try
				{
					System.IO.File.Delete(Server.MapPath(DIRETORIO_VIDEOS) + "\\" + nome_ficheiro);
				}
				catch { }
			}
		}
	}
}