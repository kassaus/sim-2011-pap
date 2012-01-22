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
                Response.Redirect("~/Home.aspx");
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
                MultiViewVideos.ActiveViewIndex = int.Parse(Request.Params["view"]);
            }

            if (MultiViewVideos.ActiveViewIndex == 1 || MultiViewVideos.ActiveViewIndex == 2)
            {
                panelFiltros.Visible = false;
                TagInserirVideoRepeater.DataSource = listaEtiquetas;

            }


        }



        #region Listagens de Vídeos

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
            //Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaId(int.Parse(ddlSubcategorias.SelectedValue));

            //GridViewListaVideos.DataSource = gestorVideos.obterTodosVideosSubcategoria(subcategoria.id);
            //GridViewListaVideos.DataBind();
            //lblSubtitulo.Text = "Listagem de vídeos da Subcategoria " + subcategoria.nome;
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

        protected void lbtnEditarVideo_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as LinkButton).NamingContainer;
            idVideoToEdit.Value = ((GridView)row.NamingContainer).DataKeys[row.RowIndex].Value.ToString();
            MultiViewVideos.ActiveViewIndex = 1;
            filtroVideos.Visible = false;
            filtroVideos.SelectedIndex = 0;
            ddlCategorias.ClearSelection();
            ddlSubcategorias.ClearSelection();
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
        protected void labelEditarVideoClickEventHandler(object sender, EventArgs e)
        {
            LinkButton etiqueta = (LinkButton)sender;
            Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaNome(etiqueta.Text);
            gestorVideos.obterVideo(int.Parse(idVideoToEdit.Value)).Subcategorias.Remove(subcategoria);
            etiqueta.Parent.DataBind();
        }

        protected void labelInserirVideoClickEventHandler(object sender, EventArgs e)
        {
            LinkButton etiqueta = (LinkButton)sender;
            Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaNome(etiqueta.Text);
            listaEtiquetas.Remove(subcategoria);
            TagInserirVideoRepeater.DataBind();
        }

        protected void btnInserirSubcategoria_Click(object sender, EventArgs e)
        {
            Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaId(int.Parse(ddlSubcategorias.SelectedValue));
            listaEtiquetas.Add(subcategoria);
            TagInserirVideoRepeater.DataBind();

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }



        #endregion





















    }
}