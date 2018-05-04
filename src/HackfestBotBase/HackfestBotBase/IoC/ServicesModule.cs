using Autofac;
using HackfestBotBase.Services;
using Microsoft.Bot.Builder.Internals.Fibers;

namespace HackfestBotBase.IoC
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<BotDataService>()
                .Keyed<IBotDataService>(FiberModule.Key_DoNotSerialize)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<MessageService>()
                .Keyed<IMessageService>(FiberModule.Key_DoNotSerialize)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}