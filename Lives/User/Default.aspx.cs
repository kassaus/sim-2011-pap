using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using BLL;
using BO;
using System.Collections.Generic;

namespace Lives.Users
{
    public partial class HomeUser : System.Web.UI.Page
    {
        private VideoBO gestorVideos { get; set; }
        private SubcategoriaBO gestorSubcategorias { get; set; }
        private CategoriaBO gestorCategorias { get; set; }
        private List<Subcategoria> listaEtiquetas = new List<Subcategoria>();

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
                TagInserirVideoRepeater.DataSource = listaEtiquetas;
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

        protected void ddlCategoriasEditarVideo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = ((DropDownList)sender);
            DropDownList subcat = ddl.FindControl("ddlSubcategoriasEditarVideo") as DropDownList;

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

        protected void ddlSubcategoriasEditarVideo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList subcat = ((DropDownList)sender).FindControl("ddlSubcategoriasEditarVideo") as DropDownList;


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
                        erro = (Label)item.FindControl("lblErroEditarVideos");
                    }
                }
                if (erro != null)
                {
                    erro.Visible = true;
                    erro.Text = "Já inseriu essa subcategoria!";
                }
            }

        }

        protected void ddlCategoriasEditarVideo_OnDataBound(object sender, EventArgs e)
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
            string url = null;
            FileUpload fileUpload = null;
            titulo = findControloTextBoxRepeater(RepeaterVideoDetails, "txtBoxTituloEditarVideo");
            descricao = findControloTextBoxRepeater(RepeaterVideoDetails, "txtBoxDescricaoEditarVideo");
            fileUpload = findControloFileUploadRepeater(RepeaterVideoDetails, "FileUpload1");




            gestorVideos.modificaVideo(descricao, titulo, null, int.Parse(idVideoToEdit.Value));
            Response.Redirect("?view=0", true);
        }

        protected void labelClickEventHandler(object sender, EventArgs e)
        {
            LinkButton etiqueta = (LinkButton)sender;

            if (gestorVideos.desassociaEtiqueta(int.Parse(idVideoToEdit.Value), etiqueta.Text))
            {
                etiqueta.Parent.DataBind();
            }
            else
            {
                throw new NotImplementedException();
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

















    }
}