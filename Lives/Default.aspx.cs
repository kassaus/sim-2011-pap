using System;

namespace Lives
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("Home.aspx");
            }

            if (User.IsInRole("admin"))
            {

                Response.Redirect("~/Admin/AdminPage.aspx?view=0", true);
            }

            if (User.IsInRole("user"))
            {
                Response.Redirect("~/User/HomeUser.aspx", true);
            }
        }
    }
}