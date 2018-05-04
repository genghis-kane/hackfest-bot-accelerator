namespace HackfestBotBase.Settings
{
    public interface ILuisSettings
    {
        string AppId { get; }
        string ApiKey { get; }
        string ApiHostName { get; }
        bool UseStaging { get; }
        double MenuScoreThreshold { get; }
        double ScorableScoreThreshold { get; }
    }
}