using System;
using System.Web.Security;

namespace Lives
{
	public partial class Home : System.Web.UI.Page
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
					painelRegisto.Visible = false;
					painelRecuperacaoPassword.Visible = false;
					break;
			}

			Video.Text = "<object data='data:application/x-silverlight-2,' type='application/x-silverlight-2'>" +
			"<param name='source' value='ClientBin/SilverlightApplication.xap' />" +
			"<param name='onError' value='onSilverlightError' />" +
			"<param name='background' value='white' />" +
			"<param name='minRuntimeVersion' value='4.0.60310.0' />" +
			"<param name='autoUpgrade' value='true' />" +
			"<param name='windowless' value='true'/>" +
			"<a href='http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.60310.0' style='text-decoration: none'>" +
				"<img src='http://go.microsoft.com/fwlink/?LinkId=161376' alt='Get Microsoft Silverlight'" +
					"style='border-style: none' />" +
			"</a>" +
			"</object>" +
			"<iframe id='_sl_historyFrame' style='visibility: hidden; height: 0px; width: 0px;" +
			"border: 0px'></iframe>";
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