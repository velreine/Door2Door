using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Net;
using Door2Door_API.Models;
using Door2Door_API.Models.Interfaces;
using RouteModel = Door2Door_API.Models.Route;

namespace Door2Door_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository roomRepository;
        private readonly IFactory<Room> roomFactory;

        private readonly string connectionString;
        
        // TODO: DELETE THIS!
        // For testing only. 
        private const string CONNECTIONSTRING =
            "Server=192.168.1.102;Port=5432;Database=door2door;User Id=postgres;Password=12345;";
        public RoomController(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
            connectionString = Environment.GetEnvironmentVariable("foo");
            roomFactory = new RoomFactory();

            if (connectionString == null)
                Console.WriteLine("No connectionstring");
            NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite();
        }
        
        [HttpGet("GetAllRooms", Name = "GetAllRooms")]
        public async Task<ActionResult> GetAllRooms()
        {
            try
            {
                var rooms = await roomRepository.GetAllAsync();
                
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
        
        [HttpGet("GetRoomById", Name = "GetRoomById")]
        public async Task<ActionResult> GetRoomById(long id)
        {
            
            try
            {
                var room = await roomRepository.GetByIdAsync((int)id);
                
                if (room is null) return NotFound(new { message = $"The room with id: \"{id}\" was not found." });

                return Ok(room);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new { message = "Something went wrong on our side." });
            }
            
            
        }
        
    }
}
