using System.Data;
using GeoJSON.Net;
using Npgsql;

namespace Door2Door_API;

public class RoomFactory : IFactory<Room>
{
    public Room Build(IDataRecord record)
    {
        return new Room
        {
            Id = (int)record.GetInt64(0),
            Geometry = record.GetValue(1),
            RoomType = (RoomType)record.GetValue(2),
            RoomName = record.GetString(3)
        };
    }
}