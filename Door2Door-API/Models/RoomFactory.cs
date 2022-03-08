using System.Data;
using Door2Door_API.Models.Interfaces;

namespace Door2Door_API.Models;

public class RoomFactory : IFactory<Room>
{
    public Room Build(IDataReader record)
    {
        
        var res =  new Room
        {
            Id = (long)record["id"],
            Geometry = (string)record["geom"],
            Type = (long)record["room_Type"],
            Name = record["room_Name"].ToString()!
        };

        return res;
    }
}