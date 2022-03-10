using System.Data;
using Door2Door_API.Models.Interfaces;

namespace Door2Door_API.Models;

public class RoomTypeFactory : IFactory<RoomType>
{
    public RoomType Build(IDataReader record)
    {
        var res = new RoomType
        {
            Id = (int)record["room_type_id"],
            Type = record["type"].ToString()!
        };
        return res;
    }
}