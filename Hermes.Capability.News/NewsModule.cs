using Autofac;
using Hermes.Database;

namespace Hermes.Capability.News
{
    public class NewsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new NewsController(c.Resolve<DatabaseController>()))
                .As<INewsController>()
                .SingleInstance();
        }
    }
}
