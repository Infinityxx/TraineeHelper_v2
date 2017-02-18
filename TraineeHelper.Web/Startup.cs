using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TraineeHelper.Web.Startup))]

namespace TraineeHelper.Web
{
    public partial class Startup
    {
       public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddSwaggerGen();
        //}
    }
}
