using System;
using System.Web.Security;

namespace Lives.User
{
    public partial class AlterarPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/", true);
            }

            lblNome.Text = Membership.GetUser().UserName;

        }
    }
}