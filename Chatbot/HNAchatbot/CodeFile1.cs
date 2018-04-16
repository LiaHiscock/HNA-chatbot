using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using AddChatbotData;
using System.Data.SqlClient;

namespace HNAchatbot {

    //[BotAuthentication]
    public class MessageResponse {

        public static string SendMessage(string message)
        {
            //String sql = $"INSERT INTO HNAEvents(EventId, Name, DateTime, Location, Type, ExtraNotes) VALUES ('{values[0]}', '{values[1]}', '{values[2]}', '{values[3]}', '{values[4]}', '{values[5]}')";
            String sql = "SELECTS * from HNAEvents";

            String queryString = "";
            String connectionString = "";

            using (SqlConnection connection = new SqlConnection(connectionString)) {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("", "");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("", "", ""));
                    }
                }
                finally {
                    reader.Close(); 
                }
            }

            if (message.Contains("hi") || message.Contains("hello"))
            {
                return "Welcome to the HNA Chatbot!\nSome things you can ask me are:\nWhat's the schedule today?" +
                "\nWhat's the lunch menu today?\nWhat sports games are today?\nAre there any dances coming up? Type \"END\" to stop at anytime.";
            }

            if (message.Contains("calendar"))
            {  
                sql = $"SELECT eventname from table_name";
                return "cal";
            }

            else if (message.Contains("lunch"))
            {
                return "food";
            }

            else if (message.Contains("dance"))
            {
                return "funky fresh";
            }

            else if (message.Contains("game"))
            {
                return "sportball";
            }

            else
            {
                return "pls try again";
            }

        }
    }
}