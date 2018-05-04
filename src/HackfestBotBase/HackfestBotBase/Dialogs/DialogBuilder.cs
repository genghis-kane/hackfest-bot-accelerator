using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;

namespace HackfestBotBase.Dialogs
{
    public class DialogBuilder : IDialogBuilder
    { 
        public ShowSuggestedActionsDialog BuildShowSuggestedActionsDialog(IMessageActivity message, string prompt, List<string> options)
        {
            return CreateDialog(message, scope => scope.Resolve<ShowSuggestedActionsDialog>(TypedParameter.From(prompt),
                TypedParameter.From(options)));
        }

        public ShowSuggestedActionsDialog BuildShowSuggestedActionsDialog(IMessageActivity message, string prompt, params string[] options)
        {
            return BuildShowSuggestedActionsDialog(message, prompt, options.ToList());
        }

        public NameDialog BuildNameDialog(IMessageActivity message)
        {
            return CreateDialog(message, scope => scope.Resolve<NameDialog>());
        }
        
        /*
         * For a new dialog registered with Autofac, use the private CreateDialog method below.
         *
         * To pass parameters into a constructor when resolving an instance through Autofac, use TypedParameter, NamedParameter, or PositionalParameter.
         * See examples in IoC/ApplicationDialogsModule.cs for how to register.
         */

        private T CreateDialog<T>(IMessageActivity message, Func<ILifetimeScope, T> func)
        {
            using (var scope = CreateDialogLifetimeScope(message))
            {
                return func(scope);
            }
        }

        private static ILifetimeScope CreateDialogLifetimeScope(IMessageActivity message)
        {
            return DialogModule.BeginLifetimeScope(Conversation.Container, message);
        }
    }
}