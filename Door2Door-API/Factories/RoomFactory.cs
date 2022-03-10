using System.Data;
using Door2Door_API.Models.Interfaces;

namespace Door2Door_API.Models;

public class RoomFactory : IFactory<Room>
{
    private readonly IFactory<RoomType> roomTypeFactory;

    public RoomFactory(IFactory<RoomType> roomTypeFactory)
    {
        this.roomTypeFactory = roomTypeFactory;
    }
    public Room Build(IDataReader record)
    {
        var res =  new Room
        {
            Id = (long)record["id"],
            Geometry = (string)record["geom"],
            Type = roomTypeFactory.Build(record),
            Name = record["room_Name"].ToString()!
        };

        return res;
    }
}