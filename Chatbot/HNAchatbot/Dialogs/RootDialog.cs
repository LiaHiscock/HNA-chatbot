using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HNAchatbot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            string WelcomeText = "Welcome to the HNA Chatbot!\nSome things you can ask me are:\nWhat's the schedule today?" +
                "\nWhat's the lunch menu today?\nWhat sports games are today?\nAre there any dances coming up? Type \"END\" to stop at anytime.";
            await context.PostAsync(WelcomeText);

            while (activity.Text != "END") {
                //send activity.Text to our message response class

            }

            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }   
    }
}