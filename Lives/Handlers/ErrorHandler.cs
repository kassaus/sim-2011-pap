using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lives.Handlers
{
    public class ErrorHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.RewritePath("~/ErrorPages/Error.aspx");
        }
    }
}