using System.Configuration;

namespace HackfestBotBase.Settings
{
    public class BotSettings : ILuisSettings, IQnaMakerSettings
    {

        public BotSettings()
        {
            AppId = ConfigurationManager.AppSettings["LuisAppId"];
            ApiKey = ConfigurationManager.AppSettings["LuisAPIKey"];
            ApiHostName = ConfigurationManager.AppSettings["LuisAPIHostName"];
            UseStaging = ConfigurationManager.AppSettings["LuisUseStaging"].ToBool();
            MenuScoreThreshold = ConfigurationManager.AppSettings["LuisMenuScoreThreshold"].ToDouble();
            ScorableScoreThreshold = ConfigurationManager.AppSettings["LuisScorableScoreThreshold"].ToDouble();

            SubscriptionKey = ConfigurationManager.AppSettings["QnAMakerSubscriptionKey"];
            KnowledgeBaseId = ConfigurationManager.AppSettings["QnAMakerKnowledgeBaseId"];
            ScoreThreshold = ConfigurationManager.AppSettings["QnAMakerScoreThreshold"].ToDouble();
        }

        public string AppId { get; }
        public string ApiKey { get; }
        public string ApiHostName { get; }
        public bool UseStaging { get; }
        public double MenuScoreThreshold { get; }
        public double ScorableScoreThreshold { get; }
        public string SubscriptionKey { get; }
        public string KnowledgeBaseId { get; }
        public double ScoreThreshold { get; }
    }
}