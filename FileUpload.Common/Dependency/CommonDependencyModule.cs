using Autofac;
using FileUpload.Common.Logging;

namespace FileUpload.Common.Dependency
{
    public class CommonDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
        }
    }
}