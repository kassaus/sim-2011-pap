using System;

namespace Lives
{
    public partial class Default : System.Web.UI.Page
    {
        // Boolean teste;
        protected void Page_Load(object sender, EventArgs e)
        {
            // teste = Membership.ValidateUser("pauloluis", "123456");

            if (!Request.IsAuthenticated)
            {
                Response.Redirect("Home.aspx");
                // "~/Account/Login.aspx"
            }

            if (User.IsInRole("admin"))
            {

                Response.Redirect("~/Admin/AdminPage.aspx?view=0", true);
            }

            if (User.IsInRole("user"))
            {
                Response.Redirect("~/user/HomeUser.aspx", true);
            }


        }
    }
}