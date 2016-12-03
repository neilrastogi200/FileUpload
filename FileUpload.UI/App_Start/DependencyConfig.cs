using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using FileUpload.BLL.Dependency;
using FileUpload.Common.Dependency;
using FileUpload.Repository.Dependency;

namespace FileUpload.UI
{
    [ExcludeFromCodeCoverage]
    public sealed class DependencyConfig
    {
        private static IContainer _container;

        public static IContainer Container => _container ?? RegisterDependencyResolvers();

        public static IContainer RegisterDependencyResolvers()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterAutofacFramework(containerBuilder);
            RegisterApplicationDependencies(containerBuilder);

            _container = containerBuilder.Build();

            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));

            return _container;
        }

        private static void RegisterApplicationDependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterModule(new BusinessDependencyModule());
            builder.RegisterModule(new RepositoryDependencyModule());
            builder.RegisterModule(new CommonDependencyModule());

        }

        private static void RegisterAutofacFramework(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof (MvcApplication).Assembly);
            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterModule(new AutofacWebTypesModule());
        }
    }
}