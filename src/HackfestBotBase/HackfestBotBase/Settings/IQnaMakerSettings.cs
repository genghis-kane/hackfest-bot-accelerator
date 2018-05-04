namespace HackfestBotBase.Settings
{
    public interface IQnaMakerSettings
    {
        string SubscriptionKey { get; }
        string KnowledgeBaseId { get; }
        double ScoreThreshold { get; }
    }
}