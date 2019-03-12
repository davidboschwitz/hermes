using Autofac;

namespace Hermes.Capability.Chat
{
    public class ChatModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ChatController())
                .As<IChatController>()
                .SingleInstance();
        }
    }
}
