using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FinancePlanner.Database
{
    public class RemoteDb
    {
        public static void connectToAzure()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.ConnectionString = "Server=tcp:plannerdbserver.database.windows.net,1433;" +
                    "Initial Catalog=Planner_db_2021-11-13T10-06Z;" +
                    "Persist Security Info=False;" +
                    "User ID=simonblnt;" +
                    "Password={your_password};" +
                    "MultipleActiveResultSets=False;" +
                    "Encrypt=True;" +
                    "TrustServerCertificate=False;" +
                    "Connection Timeout=30;";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    String sql = "SELECT name, collation_name FROM sys.databases";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}