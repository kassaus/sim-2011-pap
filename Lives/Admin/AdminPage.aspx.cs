using System;
using System.Web.UI.WebControls;
using BLL;
using BO;
using System.Web.Security;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;

namespace Lives
{
    public partial class AdminPage : System.Web.UI.Page
    {
        private VideoBO gestorVideos { get; set; }
        private SubcategoriaBO gestorSubcategorias { get; set; }
        private CategoriaBO gestorCategorias { get; set; }
        private UserMembershipBO gestorUsers { get; set; }

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

            string view = Request.Params["view"];

            if (view != null)
            {
                MultiViewVideos.ActiveViewIndex = int.Parse(view);
            }

            if (MultiViewVideos.ActiveViewIndex == 2 || MultiViewVideos.ActiveViewIndex == 3 || MultiViewVideos.ActiveViewIndex == 4)
            {
                panelFiltros.Visible = false;

            }

            if (MultiViewVideos.ActiveViewIndex == 3)
            {
                gridViewUser.DataSource = Membership.GetAllUsers();
                gridViewUser.DataBind();
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

        protected void CategoriasDropBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblErro.Visible = false;
        }


        protected void labelClickEventHandler(object sender, EventArgs e)
        {
            LinkButton etiqueta = (LinkButton)sender;
            Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaNome(etiqueta.Text);
            gestorVideos.obterVideo(int.Parse(idVideoAprovacao.Value)).Subcategorias.Remove(subcategoria);
            etiqueta.Parent.DataBind();
        }

        protected void labelSubCatEditClickEventHandler(object sender, EventArgs e)
        {
            LinkButton etiqueta = (LinkButton)sender;
            gestorSubcategorias.removeSubcategoria(etiqueta.Text);
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
            panelFiltros.Visible = false;
            MultiViewVideos.ActiveViewIndex = 1;
            filtroVideos.SelectedIndex = 0;
            ddlCategorias.ClearSelection();
            ddlSubcategorias.ClearSelection();
        }

        protected void btnCategorizar_Click(object sender, EventArgs e)
        {

        }

        protected void btnApagarSubcat_Click(object sender, EventArgs e)
        {

        }

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
                TagRepeater.DataBind();
                txtBoxNovaSubcategoria.Text = "";
                lblErro.Visible = false;


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

        protected void ListaVideos_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;

            if (row.RowType == DataControlRowType.DataRow)
            {
                Video video = row.DataItem as Video;
                Label label = row.FindControl("lblUser") as Label;

                label.Text = Membership.GetUser(video.id_user).UserName;
            }
        }


        protected void imgbtnBloquearUser_Click(object sender, EventArgs e)
        {
            ImageButton imgbtnApagarUser = sender as ImageButton;
            GridViewRow row = (GridViewRow)imgbtnApagarUser.NamingContainer;
            Guid userId = (Guid)(gridViewUser.DataKeys[row.RowIndex].Value);
            string userName = (String)gridViewUser.DataKeys[row.RowIndex].Values[1];
            actualizaEstadoLockUser(userName, userId, true);

        }

        protected void imgbtnDesbloquearUser_Click(object sender, EventArgs e)
        {
            ImageButton imgbtnApagarUser = sender as ImageButton;
            GridViewRow row = (GridViewRow)imgbtnApagarUser.NamingContainer;
            Guid userId = (Guid)(gridViewUser.DataKeys[row.RowIndex].Value);
            string userName = (String)gridViewUser.DataKeys[row.RowIndex].Values[1];
            actualizaEstadoLockUser(userName, userId, false);
        }

        private void actualizaEstadoLockUser(string userName, Guid userId, bool estado)
        {
            MembershipUser user = Membership.GetUser(userName);

            if (user != null && user.IsApproved)
            {
                gestorUsers.modificaEstadoLock(userId, estado);

            }
            gridViewUser.DataBind();
            Response.Redirect("~/Admin/AdminPage.aspx?view=3", false);
        }

        protected void imgbtnApagarUser_Click(object sender, EventArgs e)
        {
            ImageButton imgbtnApagarUser = sender as ImageButton;
            GridViewRow row = (GridViewRow)imgbtnApagarUser.NamingContainer;
            Guid UserId = (Guid)(gridViewUser.DataKeys[row.RowIndex].Value);
            string UserName = (String)gridViewUser.DataKeys[row.RowIndex].Values[1];

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
            gridViewUser.DataBind();
            Response.Redirect("~/Admin/AdminPage.aspx?view=3", true);
        }

        protected void imgbtnAlterarPasswordUser_Click(object sender, EventArgs e)
        {
            string novaPassword = null;
            string email = null;
            ImageButton imgbtnApagarUser = sender as ImageButton;
            GridViewRow row = (GridViewRow)imgbtnApagarUser.NamingContainer;
            Guid userId = (Guid)(gridViewUser.DataKeys[row.RowIndex].Value);
            string userName = (String)gridViewUser.DataKeys[row.RowIndex].Values[1];

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


        protected void enviaEmailNovaPass(string userName, string password, string email)
        {
            SendEmail email_pass = new SendEmail();
            StringBuilder strData = new StringBuilder(string.Empty);
            string from = "lives@gmail.com";
            string to = email;
            string bcc = null;
            string cc = "pppluis@gmail.com";
            string subject = "Nova Password";
            string body = null;

            strData.Append("<h4>Olá " + userName + " bem vindo ao Lives!</h4>");
            strData.Append("Dados da sua conta:</br>");
            strData.Append("<span style=" + @"""font-weight: bold;""><p>Nova Password: </span>" + password + "</ br>");
            strData.Append("<p>Carregue na ligação para voltar ao Lives.</p><p><a href=" + @"""http://lives.pt"" target=" + @"""_blank"">http://lives.pt</a></p>");
            strData.Append("<p style=" + @"""font-weight: bold;"">Até breve! </p>");

            body = strData.ToString();
            email_pass.EnviarEmail(from, to, bcc, cc, subject, body);
        }



    }
}