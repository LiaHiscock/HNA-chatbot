using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace AddChatbotData
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "holynamesacademy.database.windows.net";
                builder.UserID = "hna-admin";
                builder.Password = "CharityAndWisdom1";
                builder.InitialCatalog = "hna-db";
                String path = @".\data\data.csv";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    String sql = "INSERT INTO HNAEvents";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (var reader = new StreamReader(path))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine().Trim();
                                var values = line.Split(',');
                                Console.WriteLine(values[0]);

                            }
                            Console.ReadLine();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}