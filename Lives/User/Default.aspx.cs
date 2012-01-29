using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;
using BLL;
using BO;

namespace Lives.Users
{
	public partial class HomeUser : System.Web.UI.Page
	{
		private VideoBO gestorVideos { get; set; }

		private SubcategoriaBO gestorSubcategorias { get; set; }

		private CategoriaBO gestorCategorias { get; set; }

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

			MembershipUser user;
			user = Membership.GetUser();
			Guid idUser = new Guid(user.ProviderUserKey.ToString());
			idUserHide.Value = idUser.ToString();

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

				if (MultiViewVideos.ActiveViewIndex == 1 || MultiViewVideos.ActiveViewIndex == 2)
				{
					novoVideo = new Video();
					listaEtiquetas = new List<Subcategoria>();
				}
			}

			if (MultiViewVideos.ActiveViewIndex != 0)
			{
				panelFiltros.Visible = false;
			}
		}


		#region Listagens de Vídeos

		protected void ddlCategoriasListagens_OnDataBound(object sender, EventArgs e)
		{
			if (ddlCategoriasListagens.Items.FindByValue(null) == null)
			{
				ddlCategoriasListagens.Items.Insert(0, new ListItem(null, null));
			}
		}

		protected void ddlCategoriasListagens_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlCategoriasListagens.SelectedIndex == 0)
			{
				ddlSubcategoriasListagens.Enabled = false;
			}
			else
			{
				OdsSubcategorias.SelectMethod = "obterTodasSubCategoriasCategoria";
				OdsSubcategorias.SelectParameters.Clear();
				OdsSubcategorias.SelectParameters.Add("cat", TypeCode.Int32, ddlCategoriasListagens.SelectedValue);
				ddlSubcategoriasListagens.Enabled = true;
				ddlSubcategoriasListagens.DataBind();
			}
		}


		protected void ddlSubcategoriasListagens_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaId(int.Parse(ddlSubcategoriasListagens.SelectedValue));

			GridViewListaVideos.DataSource = gestorVideos.obterTodosVideosSubcategoriaUser(subcategoria.id, Guid.Parse(idUserHide.Value));
			GridViewListaVideos.DataBind();
			lblSubtitulo.Text = "Listagem de vídeos da Subcategoria " + subcategoria.nome;
		}

		protected void ddlSubcategoriasListagens_OnDataBound(object sender, EventArgs e)
		{
			if (ddlSubcategoriasListagens.Items.FindByValue(null) == null)
			{
				ddlSubcategoriasListagens.Items.Insert(0, new ListItem(null, null));
			}
		}

		protected void GridViewListaVideos_OnRowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				CheckBox aprovado = (CheckBox)e.Row.FindControl("chkbAprovado");
				LinkButton apagar = (LinkButton)e.Row.FindControl("lbtnApagarVideo");
				LinkButton editar = (LinkButton)e.Row.FindControl("lbtnEditarVideo");
				if (aprovado.Checked)
				{
					apagar.Visible = false;
					editar.Visible = false;
				}
			}
		}

		protected void lbtnApagarVideo_Click(object sender, EventArgs e)
		{
			LinkButton apagar = sender as LinkButton;

			GridViewRow row = (GridViewRow)apagar.NamingContainer;
			GridView Videos = (GridView)row.NamingContainer;

			string VideoId = Convert.ToString(Videos.DataKeys[row.RowIndex].Value);

			VideoBO videoBO = new VideoBO();
			bool teste = videoBO.removeVideo(int.Parse(VideoId));
			GridViewListaVideos.DataBind();
			Response.Redirect("?view=0", true);
		}

		protected void lbtnEditarVideo_Click(object sender, EventArgs e)
		{
			GridViewRow row = (GridViewRow)(sender as LinkButton).NamingContainer;
			idVideoToEdit.Value = ((GridView)row.NamingContainer).DataKeys[row.RowIndex].Value.ToString();
			MultiViewVideos.ActiveViewIndex = 1;
			panelFiltros.Visible = false;
			filtroVideos.SelectedIndex = 0;
			ddlCategoriasListagens.ClearSelection();
			videoOriginal = gestorVideos.obterVideo(int.Parse(idVideoToEdit.Value));
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

		protected void FiltroVideos_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			switch (filtroVideos.SelectedIndex)
			{
				case 0:
				default:
					GridViewListaVideos.DataSource = ODSPorAprovar;
					lblSubtitulo.Text = "Listagem de vídeos Por Aprovar";
					break;
				case 1:
					GridViewListaVideos.DataSource = ODSObterAprovados;
					lblSubtitulo.Text = "Listagem de Vídeos Aprovados";
					break;
				case 2:
					GridViewListaVideos.DataSource = ODSObterTodos;
					lblSubtitulo.Text = "Listagem de Todos Vídeos";
					break;
			}
			GridViewListaVideos.DataBind();
		}

		#endregion Listagens de Vídeos

		#region Editar Vídeos

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

		protected void labelClickEventHandler(object sender, EventArgs e)
		{
			LinkButton etiqueta = (LinkButton)sender;
			Subcategoria subCat = gestorSubcategorias.obterSubCategoriaNome(etiqueta.Text);

			listaEtiquetas.Remove(subCat);
			RepeaterTagEditarVideo.DataSource = listaEtiquetas;
			RepeaterTagEditarVideo.DataBind();
		}

		protected void ButtonCancelarEdicaoVideo_Click(object sender, EventArgs e)
		{
			if (novoVideo.url != null)
			{
				apagaFicheiroDiretorioVideos(novoVideo.url);
			}
			Response.Redirect("?view=0", true);
		}

		protected void btnConfirmarEdicaoVideo_Click(object sender, EventArgs e)
		{
			if (novoVideo.url == null)
			{
				if (gestorVideos.modificaVideo(txtBoxDescricaoEditarVideo.Text, txtBoxTituloEditarVideo.Text, videoOriginal.url, int.Parse(idVideoToEdit.Value), listaEtiquetas))
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
				if (gestorVideos.modificaVideo(txtBoxDescricaoEditarVideo.Text, txtBoxTituloEditarVideo.Text, novoVideo.url, int.Parse(idVideoToEdit.Value), listaEtiquetas))
				{
					apagaFicheiroDiretorioVideos(videoAnterior);
					Response.Redirect("?view=0", true);
				}
				else
				{
					LabelerroEditarVideo.Visible = true;
					LabelerroEditarVideo.Text = "Não foi possivel atualizar a basde de dados, tente novamente!";
				}
			}
		}


		protected void ButtonAnexarEditarVideo_Click(object sender, EventArgs e)
		{
			if ((UploadEditarVideo.PostedFile != null) && (UploadEditarVideo.PostedFile.ContentLength > 0))
			{
				if (novoVideo.url != null)
				{
					apagaFicheiroDiretorioVideos(novoVideo.url);
				}

				uploadVideo(UploadEditarVideo, LabelerroEditarVideo);
				reproduzVideo(novoVideo.url, LiteralVisualizaEditarVideo);
			}
			else
			{
				LabelerroEditarVideo.Visible = true;
				LabelerroEditarVideo.Text = "Primeiro escolha o ficheiro.";
			}
		}

		#endregion Editar Vídeos


		#region Upload vídeo

		protected void btnInserirSubcategoriaUpload_Click(object sender, EventArgs e)
		{
			PainelAdicionarSubcategoriaUploadVideo.Visible = !PainelAdicionarSubcategoriaUploadVideo.Visible;
			ddlCategoriasUploadVideo.ClearSelection();
			ddlSubcategoriasUploadVideo.ClearSelection();
			ddlSubcategoriasUploadVideo.Enabled = false;
		}

		protected void ddlCategoriasUploadVideo_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList ddl = ((DropDownList)sender);
			DropDownList subcat = ddl.FindControl("ddlSubcategoriasUploadVideo") as DropDownList;

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

		protected void ddlSubcategoriasUploadVideo_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList subcat = (DropDownList)sender;

			Subcategoria subCat = gestorSubcategorias.obterSubCategoriaId(int.Parse(subcat.SelectedValue));


			if (!listaEtiquetas.Contains(subCat))
			{
				listaEtiquetas.Add(subCat);

				((DropDownList)PainelAdicionarSubcategoriaUploadVideo.FindControl("ddlCategoriasUploadVideo")).ClearSelection();

				RepeaterTagUploadVideo.DataSource = listaEtiquetas;
				RepeaterTagUploadVideo.DataBind();
				LabelerroUploadVideo.Visible = false;
				PainelAdicionarSubcategoriaUploadVideo.Visible = false;
			}
			else
			{
				LabelerroUploadVideo.Visible = true;
				LabelerroUploadVideo.Text = "Já inseriu essa subcategoria!";
			}
		}


		protected void labelClickUploadVideoEventHandler(object sender, EventArgs e)
		{
			LinkButton etiqueta = (LinkButton)sender;
			Subcategoria subCat = gestorSubcategorias.obterSubCategoriaNome(etiqueta.Text);

			listaEtiquetas.Remove(subCat);
			RepeaterTagUploadVideo.DataSource = listaEtiquetas;
			RepeaterTagUploadVideo.DataBind();
		}


		protected void ButtonCancelarUploadVideo_Click(object sender, EventArgs e)
		{
			if (novoVideo.url != null)
			{
				apagaFicheiroDiretorioVideos(novoVideo.url);
			}
			listaEtiquetas = null;
			Response.Redirect("?view=0", true);
		}


		protected void btnConfirmarUploadVideo_Click(object sender, EventArgs e)
		{
			Video videoMaisRecente = null;
			if (novoVideo.url != null)
			{
				if (listaEtiquetas.Count > 0)
				{
					if (gestorVideos.criarVideo(txtBoxDescricaoUploadVideo.Text, Guid.Parse(idUserHide.Value), txtBoxTituloUploadVideo.Text, novoVideo.url))
					{
						videoMaisRecente = gestorVideos.obterVideoMaisRecenteUser(Guid.Parse(idUserHide.Value));

						if (gestorVideos.modificaVideo(null, null, null, videoMaisRecente.id, listaEtiquetas))
						{
							Response.Redirect("?view=0", true);
						}
						else
						{
							LabelerroUploadVideo.Visible = true;
							LabelerroUploadVideo.Text = "Não foi possivel atualizar a base de dados, tente novamente!";
						}
					}
					else
					{
						LabelerroUploadVideo.Visible = true;
						LabelerroUploadVideo.Text = "Não foi possivel atualizar a base de dados, tente novamente!";
					}
				}
				else
				{
					LabelerroUploadVideo.Visible = true;
					LabelerroUploadVideo.Text = "Precisa de adicionar etiquetas!!";
				}
			}
			else
			{
				LabelerroUploadVideo.Visible = true;
				LabelerroUploadVideo.Text = "Primeiro precisa de anexar o vídeo!!";
			}
		}

		protected void ButtonAnexarUploadVideo_Click(object sender, EventArgs e)
		{
			novoVideo = new Video();
			if ((UploadVideo.PostedFile != null) && (UploadVideo.PostedFile.ContentLength > 0))
			{
				if (novoVideo.url != null)
				{
					LabelerroUploadVideo.Visible = false;
					apagaFicheiroDiretorioVideos(novoVideo.url);
				}

				uploadVideo(UploadVideo, LabelerroUploadVideo);
				reproduzVideo(novoVideo.url, LiteralVisualizaUploadVideo);
			}
			else
			{
				LabelerroUploadVideo.Visible = true;
				LabelerroUploadVideo.Text = "Primeiro escolha o ficheiro.";
			}
		}

		#endregion Upload vídeo


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

		private void uploadVideo(FileUpload filme, Label msg)
		{
			string ficheiroVideo = null;
			string nomeAleatorio = null;
			string SaveLocation = null;

			if ((filme.PostedFile.ContentLength / 1024) < 20480)
			{
				nomeAleatorio = criaNomeVideo(18);
				ficheiroVideo = System.IO.Path.GetFileName(filme.PostedFile.FileName);
				string novoNome = nomeAleatorio + ficheiroVideo.Substring(ficheiroVideo.Length - 4);
				SaveLocation = Server.MapPath(DIRETORIO_VIDEOS) + "\\" + novoNome;
				try
				{
					filme.PostedFile.SaveAs(SaveLocation);
					novoVideo.url = novoNome;
				}
				catch
				{
					msg.Visible = true;
					msg.Text = "Não foi possivel carregar o ficheiro, tente de novo.";
				}
			}
			else
			{
				msg.Visible = true;
				msg.Text = "O Tamanho do ficheiro deve ser inferior a 20 MB";
			}
		}


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

		protected FileUpload findControloFileUploadRepeater(Repeater repeater, string id)
		{
			FileUpload controlo = null;
			foreach (RepeaterItem item in repeater.Items)
			{
				if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
				{
					controlo = (FileUpload)item.FindControl(id);
				}
			} return controlo;
		}

		protected Label findControloLabelRepeater(Repeater repeater, string id)
		{
			Label controlo = null;
			foreach (RepeaterItem item in repeater.Items)
			{
				if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
				{
					controlo = (Label)item.FindControl(id);
				}
			} return controlo;
		}

		protected Button findControloButtonRepeater(Repeater repeater, string id)
		{
			Button controlo = null;
			foreach (RepeaterItem item in repeater.Items)
			{
				if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
				{
					controlo = (Button)item.FindControl(id);
				}
			} return controlo;
		}

		public string criaNomeVideo(int tamanho)
		{
			string caracteres = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWYXZ0123456789";
			int valormaximo = caracteres.Length;

			Random random = new Random(DateTime.Now.Millisecond);

			StringBuilder nome = new StringBuilder(tamanho);

			for (int indice = 0; indice < tamanho; indice++)
				nome.Append(caracteres[random.Next(0, valormaximo)]);

			return nome.ToString();
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