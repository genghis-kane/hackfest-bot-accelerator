using System;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using HackfestBotBase.IoC;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;

namespace HackfestBotBase
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Conversation.UpdateContainer(Update);

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Conversation.Container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void Update(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new ReflectionSurrogateModule());
            containerBuilder.RegisterModule(new DialogModule());
            
            containerBuilder.RegisterModule<CommonModule>();
            containerBuilder.RegisterModule<ApplicationDialogsModule>();
            containerBuilder.RegisterModule<ServicesModule>();
            containerBuilder.RegisterModule<DataStorageModule>();
        }
    }
}
