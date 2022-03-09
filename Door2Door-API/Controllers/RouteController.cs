using System.Net;
using Door2Door_API.ExceptionTypes;
using Door2Door_API.Models;
using Door2Door_API.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RouteModel = Door2Door_API.Models.Route;

namespace Door2Door_API.Controllers;

[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{
    private readonly IRouteRepository _routeRepository;
    
    public RouteController(IRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }
    
    [HttpGet("GetRoute", Name = "GetRoute")]
    public async Task<ActionResult<RouteModel>> GetRoute(long destinationRoomId)
    {
        try
        {
            var route = await _routeRepository.GetRouteTo(destinationRoomId);

            return Ok(route);
        }
        catch (RouteBuildingException exception)
        {
            return StatusCode((int)HttpStatusCode.UnprocessableEntity, new { message = exception.Message });
        }
        catch (Exception exception)
        {
            //return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "Internal Server Error" });
            return StatusCode((int)HttpStatusCode.InternalServerError, new { message = exception.Message });
        }
        
    }
    
    [HttpGet("GetStartingPoint", Name = "GetStartingPoint")]
    public async Task<ActionResult<string>> GetStartingPoint(long id)
    {
        try
        {
            var startingPoint = await _routeRepository.GetStartingPoint(id);

            return Ok(startingPoint);
        }
        catch (RouteBuildingException exception)
        {
            return StatusCode((int)HttpStatusCode.UnprocessableEntity, new { message = exception.Message });
        }
        catch (Exception exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new { message = exception.Message });
        }
        
    }
    
/*    [HttpGet("GetStartingPoint", Name = "GetStartingPoint")]
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
    */
}