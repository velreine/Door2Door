using System.Data;
using Door2Door_API.Models.Interfaces;
using GeoJSON.Net;
using NetTopologySuite.Geometries;
using Npgsql;

namespace Door2Door_API.Models;

public class RouteFactory : IFactory<Route>
{
    public Route Build(IDataReader reader)
    {
        var geometries = new List<string>();
        
        while (reader.Read())
        { 
            var geoData = (string)reader["st_asgeojson"];
            geometries.Add(geoData);
        }

        return new Route(geometries);
    }
}