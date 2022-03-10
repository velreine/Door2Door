using System.Net;
using Door2Door_API.Models;
using Door2Door_API.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Door2Door_API.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomTypeController : ControllerBase
{
    private readonly IRoomTypeRepository roomTypeRepository;

    public RoomTypeController(IRoomTypeRepository roomTypeRepository)
    {
        this.roomTypeRepository = roomTypeRepository;
    }

    [HttpGet("GetAllRoomTypes", Name = "GetAllRoomTypes")]
    public async Task<ActionResult<IEnumerable<RoomType>>> GetAllRoomTypes()
    {
        try
        {
            var roomtypes = await roomTypeRepository.GetAllAsync();
            return Ok(roomtypes);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Task failed with message: {e.Message}", e);
            return StatusCode((int)HttpStatusCode.InternalServerError, new
            {
                message = "INTERNAL SERVER ERROR"
            });
        }
    }

}