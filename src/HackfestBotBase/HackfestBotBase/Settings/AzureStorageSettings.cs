using System.Configuration;

namespace HackfestBotBase.Settings
{
    public class AzureStorageSettings : IAzureStorageSettings
    {
        public string ConnectionString { get; }

        public AzureStorageSettings()
        {
            ConnectionString = ConfigurationManager.AppSettings["AzureWebJobsStorage"];
        }
    }
}