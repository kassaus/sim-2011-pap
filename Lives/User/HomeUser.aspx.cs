using System;
using System.Text;
using System.Web.Security;
using System.Web.UI.WebControls;
using BLL;
using BO;

namespace Lives.Users
{
    public partial class HomeUser : System.Web.UI.Page
    {
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
                    lbtnApagar.Enabled = false;
                    lbtnEditar.Enabled = false;
                }
            }

        }

        protected void lbtnEditar_Click(object sender, EventArgs e)
        {

            Video videoEditar;
            LinkButton editar = sender as LinkButton;
            GridViewRow row = (GridViewRow)editar.NamingContainer;
            GridView Videos = (GridView)row.NamingContainer;
            string VideoId = Convert.ToString(Videos.DataKeys[row.RowIndex].Value);
            StringBuilder chaves = new StringBuilder();



            MultiViewVideos.ActiveViewIndex = 1;
            if (CheckBox1.Checked || CheckBox1.Checked || CheckBox1.Checked)
            {
                CheckBox1.Checked = false;
                CheckBox2.Checked = false;
                CheckBox3.Checked = false;

            }
            CheckBox1.Enabled = false;
            CheckBox2.Enabled = false;
            CheckBox3.Enabled = false;

            VideoBO videoBO = new VideoBO();
            videoEditar = videoBO.obterVideo(int.Parse(VideoId));
            txtBoxTitulo_TextBoxWatermarkExtender.WatermarkText = videoEditar.titulo;

            foreach (Subcategoria subCat in videoEditar.Subcategorias)
            {
                chaves.Append(", ");
                chaves.Append(subCat.nome);

            }
            if (chaves.Length > 0)
            {
                chaves.Remove(0, 1);
            }

            lblChaves.Text = chaves.ToString();


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
            lblErro.Visible = true;
            lblErro.Text = "É necessário escolher uma categoria!";







        }





        protected void btnApagarSubcat_Click(object sender, EventArgs e)
        {

        }

    }
}