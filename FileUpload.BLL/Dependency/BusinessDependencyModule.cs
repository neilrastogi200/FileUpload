using Autofac;
using FileUpload.BLL.Services.TransactionService;
using FileUpload.BLL.Services.ValidationService;

namespace FileUpload.BLL.Dependency
{
    public class BusinessDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileManager>()
                .As<IFileManager>()
                .InstancePerRequest();

            builder.RegisterType<ValidationService>().As<IValidationService>().InstancePerRequest();

            builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerRequest();
        }
    }
}