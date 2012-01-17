﻿using System;
using System.Web.Security;

namespace Lives
{
    public partial class Home : System.Web.UI.Page
    {
        private string UrlYouTube = "http://youtube.com/v/tnBccuIDTTw";
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
            int height = 390;
            int width = 640;
            Literal1.Text = "<object style='height: " + height + "px; width: " + width + "px'>" +
                "<param name='movie' value='" + UrlYouTube + "'>" +
                "<param name='allowFullScreen' value='true'>" +
                "<param name='allowScriptAccess' value='always'>" +
                "<embed src='" + UrlYouTube + "' type='application/x-shockwave-flash'" +
                " allowfullscreen='true' allowScriptAccess='always' width='" + width + "' height='" + height + "'></embed></object>";
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);
            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            try
            {
                MembershipUser user = Membership.GetUser(RegisterUser.UserName);
                Roles.AddUserToRole(user.UserName, "user");
            }
            catch { }
            if (string.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/Home.aspx";
            }
            Response.Redirect(continueUrl);
        }


    }
}