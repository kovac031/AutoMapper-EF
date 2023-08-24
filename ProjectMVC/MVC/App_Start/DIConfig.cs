using Autofac;
using Autofac.Integration.Mvc;
using DAL;
using Repository;
using Repository.Common;
using Service;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Project.App_Start
{
    public class DIConfig
    {
        public static void Configure()
        {    
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<EFContext>().AsSelf();

            builder.RegisterType<EFContext>().AsSelf();
            builder.RegisterType<StudentService>().As<IService>();
            builder.RegisterType<StudentRepository>().As<IRepository>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}