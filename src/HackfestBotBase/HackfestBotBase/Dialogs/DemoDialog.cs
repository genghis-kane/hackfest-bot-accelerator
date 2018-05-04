using System;
using System.Threading.Tasks;
using HackfestBotBase.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;

namespace HackfestBotBase.Dialogs
{
    /// <summary>
    /// This is an example conversation.
    /// </summary>
    [Serializable]
    public class DemoDialog : IDialog<object>
    {
        private readonly IDialogBuilder _dialogBuilder;
        private readonly IMessageService _messageService;

        public DemoDialog(IMessageService messageService, IDialogBuilder dialogBuilder)
        {
            SetField.NotNull(out _messageService, nameof(messageService), messageService);
            SetField.NotNull(out _dialogBuilder, nameof(dialogBuilder), dialogBuilder);
        }

        public Task StartAsync(IDialogContext context)
        {
            // 1. Wait for a user to send a message.
            context.Wait(MessageReceivedAsync); 
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            /*
             * If you care about the initial message content from the user, then you can use the following to extract the message.
             *
             * string message = (await result).Text
             */

            // 2. Post a message to the user
            await _messageService.PostAsync("Hello!\nI'm just a demo bot!\nAll I do is ask for your name, but you can make me smarter!");

            // 3. Resolve dialog through Autofac
            NameDialog nameDialog = _dialogBuilder.BuildNameDialog(context.Activity.AsMessageActivity());

            // 4. Navigate to the created dialog
            context.Call(nameDialog, AfterNameDialog);
        }

        private async Task AfterNameDialog(IDialogContext context, IAwaitable<string> result)
        {
            // 11. Extract the value returned by the NameDialog when context.Done(...) is called within the NameDialog.
            string name = await result; 

            await context.PostAsync($"I've got your name saved as {name}.");

            ShowSuggestedButtons(context);
        }

        private void ShowSuggestedButtons(IDialogContext context)
        {
            // 12. Show prompt (first string), and then give the rest of the parameters as suggested buttons.
            var showSuggestedActionsDialog = _dialogBuilder.BuildShowSuggestedActionsDialog(context.Activity.AsMessageActivity(),
                            "Type (or click) 'Lets go!' to get started.", "Let's go!", "Lets....not(?) go!");

            context.Call(showSuggestedActionsDialog, AfterShowingSuggestedActions);
        }

        private async Task AfterShowingSuggestedActions(IDialogContext context, IAwaitable<object> result)
        {
            await result;
            
            // 13. Wait for the next message from the user after calling the dialog to show suggested actions.
            context.Wait(Resume); 
        }

        private async Task Resume(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            string message = (await result)?.Text;

            // 14. Check the message sent, and either exit the dialog or show the options again.
            if (message == "Let's go!")
            {
                await context.PostAsync("Nice! You did it.");
                context.Done<object>(null);
                return;
            }

            await context.PostAsync("That's the wrong message!");
            ShowSuggestedButtons(context);
        }
    }
}