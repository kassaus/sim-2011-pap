using System;
using System.Web.UI.WebControls;
using BLL;
using BO;
using System.Web.Security;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;
using AjaxControlToolkit;

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


            ODSObterSubcategoriasCategoria.DataBind();

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
                GridViewUser.DataSource = Membership.GetAllUsers();
                GridViewUser.DataBind();
            }

            //if (MultiViewVideos.ActiveViewIndex == 0)
            //{
            //    GridViewListaVideos.FindControl("SubCats").DataBind();
            //    filtroVideos.SelectedIndex = 0;
            //    FiltroVideos_OnSelectedIndexChanged(filtroVideos.SelectedItem, null);
            //}



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

        protected void ddlSubcategorias_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Subcategoria subcategoria = gestorSubcategorias.obterSubCategoriaId(int.Parse(ddlSubcategorias.SelectedValue));

            GridViewListaVideos.DataSource = gestorVideos.obterTodosVideosSubcategoria(subcategoria.id);
            GridViewListaVideos.DataBind();
            lblSubtitulo.Text = "Listagem de vídeos da Subcategoria " + subcategoria.nome;

        }

        #region //Listagem dos videos


        #endregion

        protected void labelClickEventHandler(object sender, EventArgs e)
        {
            LinkButton etiqueta = (LinkButton)sender;

            if (gestorVideos.desassociaEtiqueta(int.Parse(idVideoAprovacao.Value), etiqueta.Text))
            {
                etiqueta.Parent.DataBind();
            }
            else
            {
                throw new NotImplementedException();
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
        }

        protected void lbtnEditarVideo_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(sender as LinkButton).NamingContainer;
            idVideoAprovacao.Value = ((GridView)row.NamingContainer).DataKeys[row.RowIndex].Value.ToString();
            MultiViewVideos.ActiveViewIndex = 1;
            filtroVideos.Visible = false;
            ddlCategorias.ClearSelection();
            ddlSubcategorias.ClearSelection();
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
                RepeaterTag.DataBind();
                txtBoxNovaSubcategoria.Text = "";
                lblErro.Visible = false;


            }
        }

        protected void btnInserirSubcategoria_Click(object sender, EventArgs e)
        {

            if (gestorVideos.associaEtiqueta(int.Parse(idVideoAprovacao.Value), int.Parse(ddlSubcategorias.SelectedValue)))
            {
                RepeaterTag.DataBind();

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

        protected void btnConfirmarEdicaoVideo_Click(object sender, EventArgs e)
        {
            string conteudoBox = null;
            conteudoBox = findControloTextBoxRepeater(RepeaterVideoDetails);
            Subcategoria subCat = gestorSubcategorias.obterSubCategoriaId(int.Parse(ddlSubcategorias.SelectedValue));
            //gestorVideos.modificaVideo(int.Parse(idVideoAprovacao.Value)).Subcategorias.Remove(subcategoria);

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
            Response.Redirect("~/Admin/AdminPage.aspx?view=3", false);
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
            Response.Redirect("~/Admin/AdminPage.aspx?view=3", true);
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

        protected string findControloTextBoxRepeater(Repeater repeater)
        {
            string valor = null;
            foreach (RepeaterItem item in repeater.Items)
            {
                if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
                {
                    TextBox controlo = (TextBox)item.FindControl("txtBoxTitulo");
                    valor = controlo.Text;
                }
            } return valor;
        }




    }
}