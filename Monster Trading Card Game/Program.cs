using Npgsql;
using System;


namespace Monster_Trading_Card_Game
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("Hello World!");

            var cs = "Host=localhost;Username=postgres;Password=Rainbowdash1!;Database=MTCG";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            var sql = "SELECT version()";

            using var cmd = new NpgsqlCommand(sql, con);

            var version = cmd.ExecuteScalar().ToString();
            Console.WriteLine($"PostgreSQL version: {version}");
        }
    }
}
