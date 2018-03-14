using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace AddChatbotData
{
    class Program
    {
        static void Main(string[] args)
        {
            CSVtoDatabase();
        }

        static void CSVtoDatabase()
        {         
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "holynamesacademy.database.windows.net";
                builder.UserID = "hna-admin";
                builder.Password = "CharityAndWisdom1";
                builder.InitialCatalog = "hna-db";
                String path = @".\data\data.csv";
                path = Path.Combine("..","..","data.csv");

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();
                    StringBuilder sb = new StringBuilder();

                    using (var reader = new StreamReader(path))
                    {
                        //downloads the table and deletes all existing objects
                        String sqlDelete = "DELETE FROM HNAEvents";
                        SqlCommand deleteCommand = new SqlCommand(sqlDelete, connection);
                        deleteCommand.ExecuteNonQuery();

                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine().Trim();
                            var values = line.Split(',');
                            
                            String sql = $"INSERT INTO HNAEvents(EventId, Name, DateTime, Location, Type, ExtraNotes) VALUES ('{values[0]}', '{values[1]}', '{values[2]}', '{values[3]}', '{values[4]}', '{values[5]}')";

                            SqlCommand insertCommand = new SqlCommand(sql, connection);
                            var rowsAffected = insertCommand.ExecuteNonQuery();
                            Console.WriteLine(rowsAffected);


                            //successful query -- String sql = "INSERT INTO HNAEvents(EventId, Name, DateTime, Location, Type, ExtraNotes) 
                            //                                  VALUES(1, 'Deck the Dome', '2017-10-28 08:00', 'holy names', 'auction', 'none')";                           
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static List<CalendarEntry> AddDataToList()
        {
            List<CalendarEntry> calendarEvents = new List<CalendarEntry>();

            String iCal = @".\data\hna-calendar.ics";
            using (var reader = new StreamReader(iCal))
            {
                while (!reader.EndOfStream)
                {
                    Dictionary<String, String> singleEvent = new Dictionary<String, String>();

                    var line = reader.ReadLine().Trim();
                    if (line.Equals("BEGIN:VEVENT"))
                    {
                        //Regex eventSplitter = new Regex("([A - Z\\-] +)[;:](.+)");

                        MatchCollection event1;
                        event1 = Regex.Matches(line, "([A - Z\\-] +)[;:](.+)");
                          
                        //MatchCollection event = Regex.Matches(line,eventSplitter);

                        calendarEvents.Add(ParseEntry(singleEvent));
                    }

                }
                
            }
            return calendarEvents;
        }

         static void AddDataToCSV()
         { 
            // String dataFile = @".\data\data.csv";
            
          }

        static CalendarEntry ParseEntry(List<String> singleEvent)
        {
            
            CalendarEntry c1 = new CalendarEntry();
            return c1;
        }
    }
}