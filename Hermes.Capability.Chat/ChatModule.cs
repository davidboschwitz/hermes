using Autofac;
using Hermes.Database;

namespace Hermes.Capability.Chat
{
    public class ChatModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ChatController(c.Resolve<DatabaseController>()))
                   .As<ICapabilityController>()
                   .As<IChatController>()
                   .SingleInstance();
        }
    }
}
