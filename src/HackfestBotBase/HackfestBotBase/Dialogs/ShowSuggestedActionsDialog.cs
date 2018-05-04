using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackfestBotBase.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;

namespace HackfestBotBase.Dialogs
{
    [Serializable]
    public class ShowSuggestedActionsDialog : IDialog
    {
        private readonly IMessageService _messageService;
        private readonly List<string> _options;
        private readonly string _prompt;

        public ShowSuggestedActionsDialog(string prompt, List<string> options, IMessageService messageService)
        {
            SetField.NotNull(out _messageService, nameof(messageService), messageService);
            SetField.NotNull(out _options, nameof(options), options);
            SetField.NotNull(out _prompt, nameof(prompt), prompt);
        }

        public async Task StartAsync(IDialogContext context)
        {
            await SendMessage(context);
        }

        private async Task SendMessage(IDialogContext context, string name = "")
        {
            string text = string.Format(_prompt, name);

            IEnumerable<IMessageActivity> activities = _messageService.CreateMessages(text).ToList();

            AddOptionsToLastMessage(activities);

            await _messageService.PostAsync(activities);
            context.Done<object>(null);
        }

        private void AddOptionsToLastMessage(IEnumerable<IMessageActivity> activities)
        {
            IMessageActivity lastMessage = activities.Last();

            List<CardAction> cardActions = new List<CardAction>();
            cardActions.AddRange(_options.Select(t => new CardAction()
            {
                Title = t,
                Type = ActionTypes.ImBack,
                Value = t,
                DisplayText = t
            }));

            lastMessage.SuggestedActions = new SuggestedActions()
            {
                Actions = cardActions
            };
        }
    }
}