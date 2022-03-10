using System.Data;
using Door2Door_API.ExceptionTypes;
using Door2Door_API.Factories.Interfaces;
using Route = Door2Door_API.Models.Route;

namespace Door2Door_API.Factories;

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

        if (geometries.Count == 0)
        {
            throw new RouteBuildingException(
                "The procedure did not return any geometries, which means a route could not be generated."
                );
        }

        return new Route(geometries);
    }
}