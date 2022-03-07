using System.Data;
using Door2Door_API.Models.Interfaces;
using GeoJSON.Net;
using NetTopologySuite.Geometries;
using Npgsql;

namespace Door2Door_API.Models;

public class RoomFactory : IFactory<Room>
{
    public Room Build(NpgsqlDataReader record)
    {
        
        var res =  new Room
        {
            Id = (long)record["id"],
            Geometry = (string)record["geom"],
            //Geom = (GeoJSONObject)record["geom"],
            Type = (long)record["room_type"],
            Name = record["room_name"].ToString()
        };

        return res;
    }
}