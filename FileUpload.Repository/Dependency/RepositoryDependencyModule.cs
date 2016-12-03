using Autofac;
using FileUpload.Repository.Infrastructure;
using FileUpload.Repository.Repositories;

namespace FileUpload.Repository.Dependency
{
    public class RepositoryDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionRepository>()
                .As<ITransactionRepository>()
                .InstancePerRequest();

            builder.RegisterType<ConnectionFactory>()
                .As<IConnectionFactory>()
                .InstancePerRequest();

            builder.RegisterType<CurrencyCodeRepository>().As<ICurrencyCodeRepository>().InstancePerRequest();
        }
    }
}