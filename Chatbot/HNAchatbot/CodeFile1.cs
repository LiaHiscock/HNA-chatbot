using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HNAchatbot {

    //[BotAuthentication]
    public class MessageResponse {

        public static string SendMessage(string message)
        {
            if (message.Contains("hi") || message.Contains("hello"))
            {
                return "Welcome to the HNA Chatbot!\nSome things you can ask me are:\nWhat's the schedule today?" +
                "\nWhat's the lunch menu today?\nWhat sports games are today?\nAre there any dances coming up? Type \"END\" to stop at anytime.";
            }

            if (message.Contains("calendar"))
            {
                return "sah dude";
            }

            else if (message.Contains("lunch"))
            {
                return "i love food";
            }

            else if (message.Contains("dance"))
            {
                return "goteem";
            }

            else if (message.Contains("game"))
            {
                return "sport";
            }

            else
            {
                return "pls try again";
            }

        }
    }
}