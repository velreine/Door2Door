using Door2Door_API.Models.Interfaces;
using NetTopologySuite.Geometries;

namespace Door2Door_API.Models;

public class Room : IRoom
{
    public long Id { get; set; }
    public Geometry Geometry { get; set; }
    
    // Temp change when RoomType entity implemented
    public long RoomType { get; set; }
    public string RoomName { get; set; }
}