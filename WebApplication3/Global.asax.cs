using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApplication3
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // アプリケーションのスタートアップで実行するコードです
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        //void Application_OnBeginRequest(object sender, EventArgs e)
        //{
        //    if (Request.FilePath != "~/login.aspx")
        //    {
        //        Uri refuri = Request.UrlReferrer;

        //        if (refuri == null)
        //        {
        //            Response.Redirect("~/login.aspx");
        //        }
        //        else
        //        {
        //            if (!refuri.AbsoluteUri.StartsWith("~/WebForm1.aspx"))
        //            {
        //                Response.Redirect("~/login.aspx");
        //            }
        //        }

        //    }
        //}


    }
}