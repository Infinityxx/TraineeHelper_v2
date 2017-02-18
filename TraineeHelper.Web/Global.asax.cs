using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TraineeHelper.Web.CustomModelBinders;

namespace TraineeHelper.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //// Register the BsonObjectIdBinder custom model binder
            ModelBinders.Binders.Add(typeof(ObjectId), new BsonObjectIdBinder());
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
        }
    }
}
