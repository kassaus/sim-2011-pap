using System;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("Home.aspx");
            }
            MembershipUser user;
            user = Membership.GetUser();
            Guid idUser = new Guid(user.ProviderUserKey.ToString());
            idUserHide.Value = idUser.ToString();

            if (gestorSubcategorias == null)
            {
                gestorSubcategorias = new SubcategoriaBO();
            }

            if (gestorVideos == null)
            {
                gestorVideos = new VideoBO();
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

        protected void ListaVideos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkbAprovado = (CheckBox)e.Row.FindControl("chkbAprovado");
                LinkButton lbtnApagar = (LinkButton)e.Row.FindControl("lbtnApagar");
                LinkButton lbtnEditar = (LinkButton)e.Row.FindControl("lbtnEditar");
                if (chkbAprovado.Checked)
                {
                    lbtnApagar.Visible = false;
                    lbtnEditar.Visible = false;
                }
            }

        }

        protected void lbtnEditar_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as LinkButton).NamingContainer;
            idVideoToEdit.Value = ((GridView)row.NamingContainer).DataKeys[row.RowIndex].Value.ToString();

            MultiViewVideos.ActiveViewIndex = 1;
            if (CheckBoxVideosAprovados.Checked || CheckBoxVideosAprovados.Checked || CheckBoxVideosAprovados.Checked)
            {
                CheckBoxVideosAprovados.Checked = false;
                CheckBoxVideosPorAprovar.Checked = false;
                CheckBoxTodosVideos.Checked = false;
            }

            CheckBoxVideosAprovados.Enabled = false;
            CheckBoxVideosPorAprovar.Enabled = false;
            CheckBoxTodosVideos.Enabled = false;
        }

        protected void labelClickEventHandler(object sender, EventArgs e)
        {
            LinkButton etiqueta = (LinkButton)sender;
            Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaNome(etiqueta.Text);
            gestorVideos.obterVideo(int.Parse(idVideoToEdit.Value)).Subcategorias.Remove(subcategoria);
            etiqueta.Parent.DataBind();
        }

        protected void lbtnApagarVideo_Click(object sender, EventArgs e)
        {
            LinkButton apagar = sender as LinkButton;

            GridViewRow row = (GridViewRow)apagar.NamingContainer;
            GridView Videos = (GridView)row.NamingContainer;

            string VideoId = Convert.ToString(Videos.DataKeys[row.RowIndex].Value);

            VideoBO videoBO = new VideoBO();
            bool teste = videoBO.removeVideo(int.Parse(VideoId));

            ListaVideos.DataBind();

        }




        protected void btnCategorizar_Click(object sender, EventArgs e)
        {
            string subCat = null;


            SubcategoriaBO subCatBO = new SubcategoriaBO();
            subCat = subCatBO.obterSubCategoriaId(Convert.ToInt32(ddlSubcategorias.SelectedValue)).nome;
            Button button = (Button)sender;
            Label erro = (Label)button.Parent.FindControl("lblErro");
            erro.Visible = true;
            erro.Text = "É necessário escolher uma categoria!";







        }





        protected void btnApagarSubcat_Click(object sender, EventArgs e)
        {

        }

    }
}