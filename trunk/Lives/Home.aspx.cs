using System;
using System.Web.Security;

namespace Lives
{
	public partial class Home : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{


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

	}
}