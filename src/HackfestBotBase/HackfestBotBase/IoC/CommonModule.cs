using Autofac;
using HackfestBotBase.Settings;
using Module = Autofac.Module;

namespace HackfestBotBase.IoC
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            BotSettings botSettings = new BotSettings();
            AzureStorageSettings azureStorageSettings = new AzureStorageSettings();

            builder.Register(c => botSettings).As<ILuisSettings, IQnaMakerSettings>().SingleInstance();
            builder.Register(c => azureStorageSettings).As<IAzureStorageSettings>().SingleInstance();
        }
    }
}