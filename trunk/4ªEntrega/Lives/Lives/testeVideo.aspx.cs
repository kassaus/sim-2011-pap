using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lives
{
    public partial class testeVideo : System.Web.UI.Page
    {
        private string UrlYouTube = "http://youtu.be/tnBccuIDTTw";
        protected void Page_Load(object sender, EventArgs e)
        {

            int height = 188;
            int width = 300;
            Literal1.Text = "<object style='height: " + height + "px; width: " + width + "px'>" +
                "<param name='movie' value='" + UrlYouTube + "'>" +
                "<param name='allowFullScreen' value='true'>" +
                "<param name='allowScriptAccess' value='always'>" +
                "<embed src='" + UrlYouTube + "' type='application/x-shockwave-flash'" +
                " allowfullscreen='true' allowScriptAccess='always' width='" + width + "' height='" + height + "'></object>";
        }
    }
}