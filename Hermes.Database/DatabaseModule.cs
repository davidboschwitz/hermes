using Autofac;

namespace Hermes.Database
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DatabaseController())
                .As<DatabaseController>();
        }
    }
}
