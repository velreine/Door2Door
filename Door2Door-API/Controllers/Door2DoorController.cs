using Microsoft.AspNetCore.Mvc;
using Npgsql;
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
            Console.WriteLine("connection: " + connectionString);

            if (connectionString == null)
                Console.WriteLine("No connectionstring");
        }

        [HttpGet(Name = "GetRoute")]
        public List<string> GetRoute()
        {
            List<string> routeGroup = new List<string>();
            try
            {
                using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    using (NpgsqlCommand pgSqlCommand = new NpgsqlCommand())
                    {
                        pgSqlCommand.CommandText = "SELECT ST_AsGeoJSON(n.geom) from shitroute as n " +
                                                    "join (select * from pgr_dijkstra('select id, source, target, cost from shitroute', 1, 12, false)) as route " +
                                                    "on n.id = route.edge";
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
