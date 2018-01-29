using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace HNAchatbot {

    //[BotAuthentication]
    public class MessageResponse {

        public string SendMessage(string message)
        {
            if (message == "calendar")
            {
                return "sah dude";
            }

            else if (message == "lunch")
            {
                return "i love food";
            }

            else if (message == "dance")
            {
                return "goteem";
            }

            else
            {
                return "pls try again";
            }

        }
    }
}