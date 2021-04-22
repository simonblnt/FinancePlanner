using System;
using Npgsql;

namespace FinancePlanner.Database
{
    public class PgStatus
    {
        public static void GetVersion()
        {
            var cs = "Host=localhost;" +
                     "Username=postgres;" +
                     "Password=postgres;" +
                     "Database=finance_planner";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT version()";

            using var cmd = new NpgsqlCommand(sql, con);

            var version = cmd.ExecuteScalar().ToString();
            Console.WriteLine($"PostgreSQL version: {version}");
        }
    }
}