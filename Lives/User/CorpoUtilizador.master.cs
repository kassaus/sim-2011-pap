using System;
using System.Web.Security;

namespace Lives
{
    public partial class UserLayout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Home.aspx");

            }

        }
    }
}