﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lives.Admin
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPaginaInicial_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}