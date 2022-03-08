using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System.Data;
using System.Net;
using Door2Door_API.Models;
using Door2Door_API.Models.Interfaces;
using GeoJSON.Net;
using GeoJSON.Net.Converters;
using Newtonsoft.Json;
using NpgsqlTypes;

namespace Door2Door_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Door2DoorController : ControllerBase
    {
        private readonly IFactory<Room> roomFactory;

        private readonly string connectionString;
        
        // TODO: DELETE THIS!
        // For testing only. 
        private const string CONNECTIONSTRING =
            "Server=192.168.1.102;Port=5432;Database=door2door;User Id=postgres;Password=12345;";
        public Door2DoorController()
        {
            connectionString = Environment.GetEnvironmentVariable("connectionstring");
            roomFactory = new RoomFactory();

            if (connectionString == null)
                Console.WriteLine("No connectionstring");
            NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite();
        }


        [HttpGet("GetStartingPoint", Name = "GetStartingPoint")]
        public string GetStartingPoint()
        {
            string startPoint = "";
            try
            {
                using(NpgsqlConnection connection = new NpgsqlConnection(CONNECTIONSTRING))
                {
                    using (NpgsqlCommand pgSqlCommand = new NpgsqlCommand())
                    {
                        pgSqlCommand.CommandText = "SELECT * FROM starting_point WHERE section = 'B'";

                        pgSqlCommand.Connection = connection;
                        pgSqlCommand.Connection.Open();

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
                        pgSqlCommand.Connection.Open();

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


        //TODO: DELETE THIS
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

        [HttpGet("GetRoomById", Name = "GetRoom")]
        public Room? GetRoomById(long id)
        {
            try
            {
                using var connection = new NpgsqlConnection(CONNECTIONSTRING);
                using var command = new NpgsqlCommand("get_room_by_id", connection);
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(":room_id", id);
                command.Connection.Open();
                command.Prepare();
                using var reader = command.ExecuteReader();
                var result = reader.ReadFirstOrDefault(r => roomFactory.Build(r));
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    $"The program done goofed! with error: {e.Message}"
                    ,e);
                throw;
            }

        }


        [HttpGet("GetAllRooms", Name = "GetAllRooms")]
        public async Task<ActionResult> GetAllRooms()
        {

            List<Room> rooms;
            
            try
            {
                using var connection = new NpgsqlConnection(CONNECTIONSTRING);
                using var command = new NpgsqlCommand("get_all_rooms", connection);
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.Connection.Open();
                command.Prepare();
                using var reader = command.ExecuteReader();

                // Initialize list.
                rooms = new List<Room>((int)reader.Rows);
                
                while (reader.Read())
                {
                    rooms.Add(roomFactory.Build(reader));
                }

                // Id => id,
                
                return Ok(rooms);
            }
            catch (Exception e)
            {
                Console.WriteLine($"The program done goofed! with error: {e.Message}" ,e);
                return StatusCode((int)HttpStatusCode.InternalServerError, new
                {
                    message = "INTERNAL SERVER ERROR"
                });
            }
        }
        
        [HttpGet("GetRoomByIdCoolVersion", Name = "GetRoomCool")]
        public async Task<ActionResult> GetRoomByIdCoolVersion(long id)
        {

            Console.WriteLine("Was the endpoint invoked???");
            
            var room = this.GetRoomById(id);

            if (room != null)
            {
                return Ok(room);
            }
            
            return NotFound(new { message = $"The room with id: \"{id}\" was not found."});
        }
        
    }
}
