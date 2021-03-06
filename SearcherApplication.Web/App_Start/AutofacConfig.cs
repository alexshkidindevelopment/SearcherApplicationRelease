﻿using Autofac;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Compilation;
using System.Linq;

namespace SearcherApplication.Web.App_Start
{
    public class AutofacConfig
    {
        public static IDependencyResolver GetResolver()
        {
            var builder = new ContainerBuilder();

            var assemblies = BuildManager.GetReferencedAssemblies()
                .Cast<Assembly>()
                .Where(a => a.FullName.Contains("SearcherApplication"))
                .ToArray();

            builder.RegisterControllers(assemblies).InstancePerRequest();

            builder.RegisterAssemblyTypes(assemblies)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerRequest();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyModules(assemblies);

            var container = builder.Build();

            return new AutofacDependencyResolver(container);
        }
    }
}