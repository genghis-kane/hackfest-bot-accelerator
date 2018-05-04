using System;
using System.Linq;
using System.Threading.Tasks;
using HackfestBotBase.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;

namespace HackfestBotBase.Dialogs
{
    /// <summary>
    /// This dialog is responsible for determining what to call the user.
    /// If it is through a Channel like Facebook Messenger, it will confirm if the bot should call the user by the user name provided by Facebook.
    /// If it is through a web-chat, it will ask the user for their name.
    /// The name will be saved to the user data store, and if the value already exists in the data store, it will return immediately without asking the user again.
    /// </summary>
    [Serializable]
    public class NameDialog : IDialog<string>
    {
        private string _suggestedName;
        private readonly IBotDataService _botDataService;

        public NameDialog(IBotDataService botDataService)
        {
            SetField.NotNull(out _botDataService, nameof(botDataService), botDataService);
        }

        public Task StartAsync(IDialogContext context)
        {
            // 5. Check if the name exists in the bot data store. Return name if saved.
            string name = _botDataService.GetPreferredName(context);
            if (!string.IsNullOrWhiteSpace(name))
            {
                context.Done(name);
                return Task.CompletedTask;
            }

            // 6. Try extract name from a channel (eg. Facebook will return the user name)
            string fromName = context.Activity.From.Name;

            _suggestedName = fromName.Split(' ').First();

            if (_suggestedName.ToLower().Contains("user"))
            {
                // 7. Webchat has no name, default is "user". Request real name from the user.
                PromptDialog.Text(context, ResumeAfterNameFilled, "What should I call you?", "Sorry I didn't get that - try again! What should I call you?");
                return Task.CompletedTask;
            }

            PromptDialog.Confirm(context, ResumeAfterConfirmation, $"Should I call you {_suggestedName}?", $"Sorry I don't understand - try again! Should I call you {_suggestedName}?");
            return Task.CompletedTask;
        }

        private async Task ResumeAfterNameFilled(IDialogContext context, IAwaitable<string> result)
        {
            // 8. Wait for the name 
            string filledName = await result;

            // 9. Save the name in bot data store
            _botDataService.SetPreferredName(context, filledName);

            // 10. End the dialog, and return the filledName to caller dialog (DemoDialog).
            context.Done(filledName);
        }

        private async Task ResumeAfterConfirmation(IDialogContext context, IAwaitable<bool> result)
        {
            bool confirmation = await result;

            switch (confirmation)
            {
                case true:
                    _botDataService.SetPreferredName(context, _suggestedName);
                    context.Done(_suggestedName);
                    break;
                default:
                    PromptDialog.Text(context, ResumeAfterNameFilled, "Okay, what should I call you?", "Sorry I didn't get that - try again! What should I call you?");
                    break;
            }
        }
    }
}