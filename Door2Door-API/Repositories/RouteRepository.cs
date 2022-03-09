using System.Data;
using Dapper;
using Door2Door_API.Models.Interfaces;

namespace Door2Door_API.Models;

public class RouteRepository : BaseRepository,IRouteRepository
{

    private readonly IFactory<Route> _routeFactory;

    public RouteRepository(
    IFactory<Route> routeFactory,
    IDbConnection dbConnection
    ) : base(dbConnection)
    {
        this._routeFactory = routeFactory;
    }

    public async Task<Route> GetRouteTo(long roomId)
    {
        const string query = "SELECT * FROM GenerateRoute(:roomId)";
        using var reader = await Connection.ExecuteReaderAsync(query, new {roomId});
        
        
        
        return _routeFactory.Build(reader);
    }

    public async Task<string?> GetStartingPoint(long id)
    {
        var startPoint = "";
        const string query = "SELECT * FROM getstartingpoint(:id)";
        using var reader = await Connection.ExecuteReaderAsync(query, new {id});
        while (reader.Read())
        {
            startPoint = reader["geom"].ToString();
        }

        return startPoint;
    }
}