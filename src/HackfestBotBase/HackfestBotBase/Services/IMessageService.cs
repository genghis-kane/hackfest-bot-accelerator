using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace HackfestBotBase.Services
{
    public interface IMessageService
    {
        IEnumerable<IMessageActivity> CreateMessages(string text);
        Task PostAsync(IEnumerable<IMessageActivity> messages);
        Task PostAsync(string text);
    }
}