using System;
using System.Web.UI.WebControls;
using BLL;
using BO;
using System.Web.Security;

namespace Lives
{
    public partial class AdminPage : System.Web.UI.Page
    {
        private VideoBO gestorVideos { get; set; }
        private SubcategoriaBO gestorSubcategorias { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("Home.aspx");
            }

            if (gestorSubcategorias == null)
            {
                gestorSubcategorias = new SubcategoriaBO();
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
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            OdsSubcategorias.SelectMethod = "obterTodasSubCategoriasCategoria";
            OdsSubcategorias.SelectParameters.Clear();
            OdsSubcategorias.SelectParameters.Add("cat", TypeCode.Int32, ddlCategorias.SelectedValue);
            ddlSubcategorias.Enabled = true;
            ddlSubcategorias.DataBind();

            if (ddlSubcategorias.Items.Count == 0)
            {
                ddlSubcategorias.Enabled = false;
            }
            ddlSubcategorias.DataBind();
        }


        protected void labelClickEventHandler(object sender, EventArgs e)
        {
            LinkButton etiqueta = (LinkButton)sender;
            Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaNome(etiqueta.Text);
            gestorVideos.obterVideo(int.Parse(idVideoAprovacao.Value)).Subcategorias.Remove(subcategoria);
            etiqueta.Parent.DataBind();
        }

        protected void aprovarVideo_check(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as CheckBox).NamingContainer;
            idVideoAprovacao.Value = ((GridView)row.NamingContainer).DataKeys[row.RowIndex].Value.ToString();

            CheckBox aprovar = (CheckBox)sender;
            if (aprovar.Checked)
            {
                gestorVideos.aprovar(int.Parse(idVideoAprovacao.Value));
            }
            else
            {
                gestorVideos.desaprova(int.Parse(idVideoAprovacao.Value));
            }

            FiltroVideos_OnSelectedIndexChanged(filtroVideos.SelectedItem, null);
        }

        protected void lbtnEditar_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as LinkButton).NamingContainer;
            idVideoAprovacao.Value = ((GridView)row.NamingContainer).DataKeys[row.RowIndex].Value.ToString();

            MultiViewVideos.ActiveViewIndex = 1;
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

            Videos.DataBind();
        }

        protected void FiltroVideos_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            switch (filtroVideos.SelectedIndex)
            {
                case 0:
                default:
                    ListaVideos.DataSource = ODSObterVideosPorAprovar;
                    break;
                case 1:
                    ListaVideos.DataSource = ODSObterVideosAprovados;
                    break;
                case 2:
                    ListaVideos.DataSource = ODSObterTodosVideos;
                    break;
            }

            ListaVideos.DataBind();
        }

        public void ListaVideos_OnRowDataBound(object sender, EventArgs e)
        {
            GridView grid = sender as GridView;
            GridViewRow row = grid.SelectedRow;
            Video video = row.DataItem as Video;

            Label label = row.FindControl("lblUser") as Label;

            label.Text = Membership.GetUser(video.id_user).UserName;
        }
    }
}