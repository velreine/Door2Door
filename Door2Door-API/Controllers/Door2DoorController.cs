using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace Door2Door_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Door2DoorController : ControllerBase
    {
        private readonly string connectionString;

        public Door2DoorController(IConfiguration configuration)
        {
            connectionString = Environment.GetEnvironmentVariable("foo");
            //connectionString = Environment.GetEnvironmentVariable("connectionstring");

            if (connectionString == null)
                Console.WriteLine("No connectionstring");
        }

        [HttpGet("GetStartingPoint", Name = "GetStartingPoint")]
        public string GetStartingPoint()
        {
            string startPoint = "";
            try
            {
                using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand pgSqlCommand = new NpgsqlCommand())
                    {
                        pgSqlCommand.CommandText = "SELECT * FROM starting_point WHERE section = 'B'";

                        pgSqlCommand.Connection = connection;
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (NpgsqlDataReader reader = pgSqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader.GetValue(0).ToString());
                                startPoint = reader.GetValue(0).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The program made an oopsie :(");
                Console.WriteLine(e.Message);
            }
            return startPoint;
        }

        [HttpGet("GetRoute", Name = "GetRoute")]
        public List<string> GetRoute(string roomDestination)
        {
            List<string> routeGroup = new List<string>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand pgSqlCommand = new NpgsqlCommand())
                    {
                        pgSqlCommand.CommandText = "SELECT * FROM GenerateRoute(@Room)";
                        pgSqlCommand.Parameters.Add("@Room", NpgsqlDbType.Varchar).Value = roomDestination;

                        pgSqlCommand.Connection = connection;
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        using (NpgsqlDataReader reader = pgSqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader.GetValue(0).ToString());
                                Console.WriteLine();
                                routeGroup.Add(reader.GetValue(0).ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The program made an oopsie :(");
                Console.WriteLine(e.Message);
            }
            return routeGroup;
        }
    }
}
