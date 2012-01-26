﻿using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using BLL;
using BO;
using System.Collections.Generic;
using System.Text;

namespace Lives.Users
{
	public partial class HomeUser : System.Web.UI.Page
	{
		private VideoBO gestorVideos { get; set; }
		private SubcategoriaBO gestorSubcategorias { get; set; }
		private CategoriaBO gestorCategorias { get; set; }
		private List<Subcategoria> listaEtiquetas = new List<Subcategoria>();
		private string DIRETORIO_VIDEOS = "/Videos";
		private string nome_video = null;

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
			}

			if (MultiViewVideos.ActiveViewIndex == 1 || MultiViewVideos.ActiveViewIndex == 2)
			{
				panelFiltros.Visible = false;
				RepeaterNewTag.DataSource = listaEtiquetas;
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
		}

		protected void lbtnEditarVideo_Click(object sender, EventArgs e)
		{
			GridViewRow row = (GridViewRow)(sender as LinkButton).NamingContainer;
			idVideoToEdit.Value = ((GridView)row.NamingContainer).DataKeys[row.RowIndex].Value.ToString();
			MultiViewVideos.ActiveViewIndex = 1;
			filtroVideos.Visible = false;
			filtroVideos.SelectedIndex = 0;
			ddlCategoriasListagens.ClearSelection();
			ddlSubcategoriasListagens.ClearSelection();
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

		#endregion

		#region Editar Vídeos

		protected void btnInserirSubcategoria_Click(object sender, EventArgs e)
		{
			Panel p = ((ImageButton)sender).FindControl("PainelAdicionarSubcategoria") as Panel;
			p.Visible = !p.Visible;
		}

		protected void ddlCategoriasEditUploadVideo_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList ddl = ((DropDownList)sender);
			DropDownList subcat = ddl.FindControl("ddlSubcategoriasEditUploadVideo") as DropDownList;

			if (ddl.SelectedIndex == 0)
			{
				subcat.Enabled = false;
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

		protected void ddlSubcategoriasEdituploadVideo_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList subcat = ((DropDownList)sender).FindControl("ddlSubcategoriasEditUploadVideo") as DropDownList;
			Panel subcategorias_panel = (Panel)subcat.Parent.FindControl("PainelAdicionarSubcategoria");

			if (MultiViewVideos.ActiveViewIndex == 1)
			{
				if (gestorVideos.associaEtiqueta(int.Parse(idVideoToEdit.Value), int.Parse(subcat.SelectedValue)))
				{
					RepeaterVideoDetails.DataBind();
				}
				else
				{
					Label erro = null;
					foreach (RepeaterItem item in RepeaterVideoDetails.Items)
					{
						if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
						{
							erro = (Label)item.FindControl("lblErro");
						}
					}
					if (erro != null)
					{
						erro.Visible = true;
						erro.Text = "Já inseriu essa subcategoria!";
					}
				}
			}

			if (MultiViewVideos.ActiveViewIndex == 2)
			{
				Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaId(int.Parse(subcat.SelectedValue));
				listaEtiquetas.Add(subcategoria);
				RepeaterNewTag.DataBind();
			}

			subcategorias_panel.Visible = false;

		}

		protected void ddlCategoriasEditUploadVideo_OnDataBound(object sender, EventArgs e)
		{
			DropDownList ddl = ((DropDownList)sender);
			if (ddl.Items.FindByValue(null) == null)
			{
				ddl.Items.Insert(0, new ListItem(null, null));
			}
		}

		protected void btnConfirmarEdicaoVideo_Click(object sender, EventArgs e)
		{
			string titulo = null;
			string descricao = null;
			FileUpload filme = null;
			Label msg = null;

			if (MultiViewVideos.ActiveViewIndex == 1)
			{
				titulo = findControloTextBoxRepeater(RepeaterVideoDetails, "txtBoxTituloVideo");
				descricao = findControloTextBoxRepeater(RepeaterVideoDetails, "txtBoxDescricaoVideo");
				filme = findControloFileUploadRepeater(RepeaterVideoDetails, "VideoUpload");
				msg = findControloLabelRepeater(RepeaterVideoDetails, "lblErro");
				if (filme.PostedFile.ContentLength > 0)
				{
					nome_video = uploadVideo(filme, msg);
					if (nome_video != null)
					{
						if (gestorVideos.modificaVideo(descricao, titulo, nome_video, int.Parse(idVideoToEdit.Value)))
						{
							msg.Text = "Video Atualizado!";
							RepeaterVideoDetails.DataBind();
						}
						else
						{
							apagaFicheiroDiretorioVideos(nome_video);
							msg.Visible = true;
							msg.Text = "Não foi possivel atualizar a basde de dados, tente novamente!";

						}
					}
				}
				else
				{
					Response.Redirect("?view=0", true);
				}
			}
			if (MultiViewVideos.ActiveViewIndex == 2)
			{
				if (nome_video != null)
				{
					Guid idUser = Guid.Parse(idUserHide.Value);
					if (gestorVideos.criarVideo(txtBoxDescricaoVideo.Text, idUser, txtBoxTituloVideo.Text, nome_video))
					{
						lblErro.Text = "Video Atualizado!";
						// RESOLVER A QUESTÂO DE COLOCAR LOGO O VIDEO......
						//RepeaterVideoDetails.DataBind();
					}
					else
					{
						apagaFicheiroDiretorioVideos(nome_video);
						lblErro.Visible = true;
						lblErro.Text = "Não foi possivel atualizar a base de dados, tente novamente!";

					}
				}

			}

		}

		protected void btnAnexarVideo_Click(object sender, EventArgs e)
		{
			int height = 375;
			int width = 500;
			string url = null;
			nome_video = uploadVideo(VideoUpload, lblErro);

			if (nome_video != null)
			{
				btnConfirmarEdicaoVideo.Visible = false;
				url = DIRETORIO_VIDEOS + "/" + nome_video;

				Literal1.Text = "<object classid='clsid:22D6F312-B0F6-11D0-94AB-0080C74C7E95' width='" + width + "' height='" + height + "' " +
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













			}
		}


		protected void labelClickEventHandler(object sender, EventArgs e)
		{
			LinkButton etiqueta = (LinkButton)sender;
			if (MultiViewVideos.ActiveViewIndex == 1)
			{
				if (gestorVideos.desassociaEtiqueta(int.Parse(idVideoToEdit.Value), etiqueta.Text))
				{
					etiqueta.Parent.DataBind();
				}
				else
				{
					throw new NotImplementedException();
				}
			}
			if (MultiViewVideos.ActiveViewIndex == 2)
			{
				Subcategoria etiqueta_Remover = gestorSubcategorias.obterSubCategoriaNome(etiqueta.Text);
				listaEtiquetas.Remove(etiqueta_Remover);
				RepeaterNewTag.DataBind();
			}


		}

		#endregion

		#region Upload Vídeo

		#endregion

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

		private string uploadVideo(FileUpload filme, Label msg)
		{
			string url = null;
			string ficheiroVideo = null;
			string nomeAleatorio = null;
			string SaveLocation = null;

			if ((filme.PostedFile != null) && (filme.PostedFile.ContentLength > 0))
			{
				if ((filme.PostedFile.ContentLength / 1024) < 20480)
				{
					nomeAleatorio = criaNomeVideo(18);
					ficheiroVideo = System.IO.Path.GetFileName(filme.PostedFile.FileName);
					string novoNome = nomeAleatorio + ficheiroVideo.Substring(ficheiroVideo.Length - 4);
					SaveLocation = Server.MapPath(DIRETORIO_VIDEOS) + "\\" + novoNome;
					try
					{
						filme.PostedFile.SaveAs(SaveLocation);
						url = novoNome;

					}
					catch
					{
						msg.Visible = true;
						msg.Text = "Não foi possivel carregar o ficheiro, tente de novo.";
						return null;
					}
				}
				else
				{
					msg.Visible = true;
					msg.Text = "O Tamanho do ficheiro deve ser inferior a 20 MB";
					return null;
				}
			}
			else
			{
				msg.Visible = true;
				msg.Text = "Primeiro escolha o ficheiro.";
				return null;

			}
			return url;
		}




		public string criaNomeVideo(int tamanho)
		{
			string caracteres = "abcoqsujhkvy246890";
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