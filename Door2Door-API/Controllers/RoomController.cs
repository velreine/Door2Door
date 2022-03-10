using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Net;
using Door2Door_API.Models;
using Door2Door_API.Repositories.Interfaces;
using RouteModel = Door2Door_API.Models.Route;

namespace Door2Door_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        public RoomController(IRoomRepository roomRepository)
        {
            this._roomRepository = roomRepository;
        }
        
        [HttpGet("GetAllRooms", Name = "GetAllRooms")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRooms()
        {
            try
            {
                var rooms = await _roomRepository.GetAllAsync();
                
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
        public async Task<ActionResult<Room>> GetRoomById(long id)
        {
            
            try
            {
                var room = await _roomRepository.GetByIdAsync(id);
                
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
