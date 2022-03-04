using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using NpgsqlTypes;

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
            
            // Add spatial data to Npgsql's mapping.
            NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite();
            
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


        [HttpGet("/nicky", Name = "nicky")]
        public List<string> Nicky()
        {

            var tempConnString = "User Id=postgres; Database=door2door;Password=12345;Server=192.168.1.102;";

            var conn = new NpgsqlConnection(tempConnString);
            
            conn.Open();

            //var cmd = new NpgsqlCommand("get_room_by_id(:p1,:p2,:p3)", conn);
            //var cmd = new NpgsqlCommand("nicky(?, ?, ?)", conn);
            var cmd = new NpgsqlCommand("get_room_by_id", conn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@room_id", NpgsqlDbType.Integer, 1);
            cmd.Prepare();

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int debug = 2;

    
                var id = reader.GetInt64("id");
                var geometry = reader.GetValue("geom");
                var roomType = reader.GetInt64("room_type");
                var roomName = reader.GetString("room_name");

            }
            
            
            
            
            return new List<string>() { "foo", "bar" };
        }
    }
}
