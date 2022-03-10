using System.Data;
using Door2Door_API.Factories.Interfaces;
using Door2Door_API.Models;

namespace Door2Door_API.Factories;

public class RoomTypeFactory : IFactory<RoomType>
{
    public RoomType Build(IDataReader record)
    {
        var res = new RoomType
        {
            Id = (int)record["room_type_id"],
            Name = record["type"].ToString()!
        };
        return res;
    }
}