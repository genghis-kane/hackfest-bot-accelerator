using System.Collections.Generic;
using Autofac;
using HackfestBotBase.Dialogs;
using HackfestBotBase.Services;
using Microsoft.Bot.Builder.Internals.Fibers;

namespace HackfestBotBase.IoC
{
    public class ApplicationDialogsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<DialogBuilder>()
                .Keyed<IDialogBuilder>(FiberModule.Key_DoNotSerialize)
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<DemoDialog>().AsSelf().InstancePerDependency();
            builder.RegisterType<NameDialog>().AsSelf().InstancePerDependency();

            builder.Register((c, p) =>
                new ShowSuggestedActionsDialog(
                    p.TypedAs<string>(),
                    p.TypedAs<List<string>>(),
                    c.Resolve<IMessageService>()))
                .AsSelf()
                .InstancePerDependency();

            /*
            How to parse parameters into the constructor. See the DialogBuilder for example usages.
            
            builder.Register((c, p) =>
                new PhoneNumberDialog(
                    c.Resolve<IBotDataService>(),
                    p.Named<string>("prompt")))
                .AsSelf()
                .InstancePerDependency();

            builder.Register((c, p) =>
                new LocationDialog(
                    p.Named<string>("apiKey"),
                    p.Named<string>("channelId"),
                    p.Named<string>("prompt"),
                    p.TypedAs<LocationOptions>(),
                    p.TypedAs<LocationRequiredFields>(),
                    countryCode: p.Named<string>("countryCode")))
                .AsSelf()
                .InstancePerDependency();

            builder.Register((c, p) =>
                new QuestionDialog(
                    p.TypedAs<bool>(),
                    c.Resolve<IDialogBuilder>(),
                    c.Resolve<IQnaMakerSettings>(),
                    c.Resolve<IMessageService>()))
                .AsSelf()
                .InstancePerDependency();

            Example registration for a scorable.
            
            builder
                .RegisterType<MenuScorable>()
                .Keyed<MenuScorable>(FiberModule.Key_DoNotSerialize)
                .AsImplementedInterfaces()
                .InstancePerMatchingLifetimeScope(DialogModule.LifetimeScopeTag);
            */
        }
    }
}