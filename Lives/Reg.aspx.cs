using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Lives
{
	public partial class Reg : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];

			int panel = -1;

			try
			{
				panel = int.Parse(Request.QueryString["p"]);
			}
			catch { }

			switch (panel)
			{
				case 0:
					painelRegisto.Visible = true;
					painelRecuperacaoPassword.Visible = false;
					break;
				case 1:
					painelRecuperacaoPassword.Visible = true;
					painelRegisto.Visible = false;
					break;
				default:
					Response.Redirect("~/ErrorPages/Error.aspx", true);
					break;
			}

		}

		protected void RegisterUser_CreatedUser(object sender, EventArgs e)
		{
			FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false);

			string continueUrl = RegisterUser.ContinueDestinationPageUrl;

			try
			{
				MembershipUser user = Membership.GetUser(RegisterUser.UserName);
				Roles.AddUserToRole(user.UserName, "user");
			}
			catch { }

			if (string.IsNullOrEmpty(continueUrl))
			{
				continueUrl = "~/HomeAdmin.aspx";
			}

			Response.Redirect(continueUrl);
		}
	}
}