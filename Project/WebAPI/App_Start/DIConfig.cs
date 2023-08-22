using Autofac.Integration.WebApi;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using DataAccessLayer;
using Service;
using Service.Common;
using Repository;
using Repository.Common;

namespace Project.WebAPI.App_Start
{
    public class DIConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<EFContext>().AsSelf();
            builder.RegisterType<StudentService>().As<IService>();
            builder.RegisterType<StudentRepository>().As<IRepository>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}