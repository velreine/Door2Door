using System.Data;
using Door2Door_API.Models.Interfaces;
using NetTopologySuite.Geometries;
using Npgsql;

namespace Door2Door_API.Models;

public class RoomFactory : IFactory<IRoom>
{
    public IRoom Build(NpgsqlDataReader record)
    {
        return new Room
        {
            Id = (long)record["id"],
            Geometry = (Geometry)record["geom"],
            RoomType = (long)record["room_type"],
            RoomName = record["room_name"].ToString()
        };
    }
}