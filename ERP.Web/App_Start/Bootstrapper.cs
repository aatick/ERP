//using UcasPortfolio.Data.Repository;

using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Common.Data;
using Common.Data.CommonRepository;
using Common.Data.Infrastructure;
using Common.Service;
using ERP.Web.Helpers;
using ERP.Web.Mappings;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ERP.Web
{
    public static class Bootstrapper
    {
        //This method will be called when the application runs for the first time....        
        public static void Run()
        {
            SetAutofacContainer();
            //Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }


        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(x => x.FullName.StartsWith("UCAS"));
            foreach (var assembly in assemblies)
            {
                builder.RegisterControllers(assembly);
            }

            //builder
            //    .RegisterGeneric(typeof(UnitOfWorkCodeFirst<>))
            //    .As(typeof(IUnitOfWorkCodeFirst<>))
            //    .InstancePerDependency();

            builder
                .RegisterGeneric(typeof(DatabaseFactoryCodeFirst<>))
                .As(typeof(IDatabaseFactoryCodeFirst<>))
                .InstancePerDependency();

            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyTypes(assembly)
                  .Where(t => t.Name.EndsWith("Repository"))
                  .AsImplementedInterfaces().InstancePerRequest();

                builder.RegisterAssemblyTypes(assembly)
                  .Where(t => t.Name.EndsWith("Service"))
                  .AsImplementedInterfaces().InstancePerRequest();
            }

            builder.RegisterType<Logger>().As<ILogger>().InstancePerRequest();
            builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new UserManagementEntities())))
                .As<UserManager<ApplicationUser>>().InstancePerRequest();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}