using System;
using System.Web.UI.WebControls;
using BLL;
using BO;
using System.Web.Security;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;
using AjaxControlToolkit;
using System.IO;
using System.Net.Mail;

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
                Response.Redirect("~/Home.aspx", true);
            }

            lblNome.Text = Membership.GetUser().UserName;
            lblOnline.Text = Membership.GetNumberOfUsersOnline().ToString();


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

            if (!IsPostBack)
            {
                MultiViewVideos.ActiveViewIndex = int.Parse(Request.Params["view"]);
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
            ddlCategorias.ClearSelection();
            ddlSubcategorias.ClearSelection();
            panelFiltros.Visible = false;
            MultiViewVideos.ActiveViewIndex = 1;
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

        #endregion

        #region Editar Vídeos

        protected void btnConfirmarEdicaoVideo_Click(object sender, EventArgs e)
        {
            string titulo = null;
            string descricao = null;
            titulo = findControloTextBoxRepeater(RepeaterVideoDetails, "txtBoxTituloEditarVideo");
            descricao = findControloTextBoxRepeater(RepeaterVideoDetails, "txtBoxDescricaoEditarVideo");
            gestorVideos.modificaVideo(descricao, titulo, null, int.Parse(idVideoAprovacao.Value));
            Response.Redirect("AdminPage.aspx?view=0", true);

        }

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


            if (gestorVideos.associaEtiqueta(int.Parse(idVideoAprovacao.Value), int.Parse(subcat.SelectedValue)))
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


        #endregion

        #region Editar Subcategorias

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



        #endregion

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



    }
}