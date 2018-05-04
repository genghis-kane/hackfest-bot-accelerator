using System.Reflection;
using Autofac;
using HackfestBotBase.Settings;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using Module = Autofac.Module;

namespace HackfestBotBase.IoC
{
    public class DataStorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));

            string serviceName = "noncached";
            builder.Register(c =>
                {
                    IAzureStorageSettings settings = c.Resolve<IAzureStorageSettings>();
                    IBotDataStore<BotData> store;
                    if (string.IsNullOrWhiteSpace(settings.ConnectionString))
                    {

                        store = new InMemoryDataStore();
                    }
                    else
                    {
                        store = new TableBotDataStore(settings.ConnectionString);
                    }
                    return store;
                })
                .Named(serviceName, typeof(IBotDataStore<BotData>))
                .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                .AsSelf()
                .SingleInstance();

            builder.Register(c =>
                    new CachingBotDataStore(c.ResolveNamed<IBotDataStore<BotData>>(serviceName),
                        CachingBotDataStoreConsistencyPolicy.ETagBasedConsistency))
                .As<IBotDataStore<BotData>>()
                .AsSelf()
                .InstancePerLifetimeScope();

        }
    }
}