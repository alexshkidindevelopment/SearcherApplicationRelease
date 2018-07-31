using Autofac;
using SearcherApplication.SearchEngine.SearcherFactory;

namespace SearcherApplication.DAL.Infrastructure
{
    public class DalDependenciesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SearcherFactory>()
                .As<AbstractSearcherFactory>()
                .AsImplementedInterfaces()
                .InstancePerRequest();
        }
    }
}