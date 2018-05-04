using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;

namespace HackfestBotBase.Services
{
    public class MessageService : IMessageService
    {
        private readonly IBotToUser _botToUser;

        public MessageService(IBotToUser botToUser)
        {
            SetField.NotNull(out _botToUser, nameof(botToUser), botToUser);
        }

        public IEnumerable<IMessageActivity> CreateMessages(string text)
        {
            return text.Split('\n')
                .Where(t => !string.IsNullOrWhiteSpace(t)).Select(t =>
                {
                    IMessageActivity message = _botToUser.MakeMessage();
                    message.Text = t;
                    message.Type = ActivityTypes.Message;
                    message.TextFormat = TextFormatTypes.Markdown;
                    return message;
                });
        }

        public async Task PostAsync(IEnumerable<IMessageActivity> messages)
        {
            foreach (IMessageActivity message in messages)
            {
                await _botToUser.PostAsync(message);
            }
        }

        public async Task PostAsync(string text)
        {
            IEnumerable<IMessageActivity> messageActivities = CreateMessages(text);
            await PostAsync(messageActivities);
        }
    }
}