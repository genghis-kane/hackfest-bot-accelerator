using System.Collections.Generic;
using Microsoft.Bot.Connector;

namespace HackfestBotBase.Dialogs
{
    public interface IDialogBuilder
    {
        ShowSuggestedActionsDialog BuildShowSuggestedActionsDialog(IMessageActivity message, string prompt, List<string> options);
        
        ShowSuggestedActionsDialog BuildShowSuggestedActionsDialog(IMessageActivity message, string prompt, params string[] options);

        NameDialog BuildNameDialog(IMessageActivity message);
    }
}