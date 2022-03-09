using System.Data;
using Door2Door_API.Models.Interfaces;

namespace Door2Door_API.Models;

public class RoomTypeFactory : IFactory<RoomType>
{
    public RoomType Build(IDataReader record)
    {
        var res = new RoomType
        {
            Id = (long)record["id"],
            Type = record["room_type"].ToString()!
        };
        return res;
    }
}