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

            AddDataToCSV( AddDataToList() );
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
                String path = @"data.csv";
                //path = Path.Combine("..","..","data.csv");

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

            String iCal = @"hna-calendar.ics";
            using (var reader = new StreamReader(iCal))
            {
                while (!reader.EndOfStream)
                {
                    Dictionary<String, String> singleEvent = new Dictionary<String, String>();

                    var line = reader.ReadLine().Trim();

                     if (line.Equals("BEGIN:VEVENT"))
                    {
                        //Regex eventSplitter = new Regex("([A - Z\\-] +)[;:](.+)");

                        while (!line.Equals("END:VEVENT"))
                        {
                            //var event1 = Regex.Matches(line, "([A-Z\\-]+)[;:](.+)");
                            Regex r1 = new Regex(@"([A-Z\\-]+)[;:](.+)");
                            var m1 = r1.Match(line);

                            //if (event1.Count > 1)
                            if (m1.Success)
                            {
                                //singleEvent.Add(event1[0].Value, event1[1].Value);
                                singleEvent.Add(m1.Groups[1].Value, m1.Groups[2].Value);
                            }

                           line = reader.ReadLine();        //reads the next line
                        }

                        //MatchCollection event = Regex.Matches(line,eventSplitter);

                        calendarEvents.Add(ParseEntry(singleEvent));        //turns the dictionary of strings into a CalendarEvent object
                    }
                }                
            }
            return calendarEvents;
        }

         static void AddDataToCSV(List<CalendarEntry> l1)
         {
            //create a streamwriter (requires a file name)
            StreamWriter s1 = new StreamWriter(@"data.csv");

            //loop through list of calendar entries 
                //for each object, make it a string that represents the row 
                //write each string to the StreamWriter
               
            for(int i = 0; i < l1.Count; i++)
            {
                StringBuilder myBuilder = new StringBuilder();
                myBuilder.Append((i + 1) + ',');
                CalendarEntry c1 = l1[i];
           
                String value = c1.getSummary();
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                {
                    myBuilder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                }
                 
                else
                {
                    myBuilder.Append(value);
                }
                myBuilder.Append(',');

                value = c1.getDStart();
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                {
                    myBuilder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                }

                else
                {
                    myBuilder.Append(value);
                }
                myBuilder.Append(',');

                value = c1.getLocation();
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                {
                    myBuilder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                }

                else
                {
                    myBuilder.Append(value);
                }
                myBuilder.Append(',');

                value = c1.getCategories();
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                {
                    myBuilder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                }

                else
                {
                    myBuilder.Append(value);
                }
                myBuilder.Append(',');

                value = c1.getDescription();
                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                {
                    myBuilder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                }

                else
                {
                    myBuilder.Append(value);
                }

                String row = myBuilder.ToString();
                s1.WriteLine(row);
            }        
         }

        static CalendarEntry ParseEntry(Dictionary<String, String> singleEvent)
        {
            String summary = "";
            singleEvent.TryGetValue("SUMMARY", out summary);

            String dStart = "";
            singleEvent.TryGetValue("DSTART", out dStart);

            String location = "";
            singleEvent.TryGetValue("LOCATION", out location);

            String categories = "";
            singleEvent.TryGetValue("CATEGORIES", out categories);

            String description = "";
            singleEvent.TryGetValue("DESCRIPTION", out description);

            CalendarEntry c1 = new CalendarEntry(summary, dStart, location, categories, description);
            return c1;  
        }
    }
}