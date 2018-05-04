using Microsoft.Bot.Builder.Dialogs.Internals;

namespace HackfestBotBase.Services
{
    public interface IBotDataService
    {
        void SetPreferredName(IBotData botData, string name);
        string GetPreferredName(IBotData botData);
    }
}