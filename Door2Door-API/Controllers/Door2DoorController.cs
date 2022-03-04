using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;
using GeoJSON.Net;
using NpgsqlTypes;

namespace Door2Door_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Door2DoorController : ControllerBase
    {
        private readonly IFactory<Room> roomFactory;
        // TODO: DELETE THIS!
        private const string CONNECTIONSTRING =
            "Server=192.168.1.102;Port=5432;Database=door2door;User Id=postgres;Password=12345;";
        public Door2DoorController(IConfiguration configuration)
        {
            roomFactory = new RoomFactory();
            // connectionString = @"User id=postgres; Password=12345; Database" ;
            // Console.WriteLine("connection: " + connectionString);

            // if (connectionString == null)
                // Console.WriteLine("No connectionstring");
        }

        [HttpGet(Name = "GetRoute")]
        public List<string> GetRoute()
        {
            List<string> routeGroup = new List<string>();
            try
            {
                using(NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
                {
                    using (NpgsqlCommand pgSqlCommand = new NpgsqlCommand())
                    {
                        pgSqlCommand.CommandText = "SELECT ST_AsGeoJSON(n.geom) from network as n " +
                                                    "join (select * from pgr_dijkstra('select id, source, target, distance as cost from network', 1, 12, false)) as route " +
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

        [HttpGet("GetRoomById", Name = "GetRoom")]
        public Room GetRoomById(int id)
        {
            using var connection = new NpgsqlConnection(CONNECTIONSTRING);
            using var command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = $@"SELECT * FROM get_room_by_id({id})";
            command.Connection.Open();
            command.Prepare();
            using var reader = command.ExecuteReader();
            // return reader.ReadFirstOrDefault(r => roomFactory.Build(r));
            // return reader.ReadFirstOrDefault
            var rooms = new List<Room>();
            while (reader.Read())
            {
                return roomFactory.Build(reader);
            }

            return null!;
        }
    }
}
