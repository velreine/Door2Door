using GeoJSON.Net;
using GeoJSON.Net.Geometry;

namespace Door2Door_API;

public class Room
{
    public long Id { get; set; }
    public string? Geometry { get; set; }
    public RoomType RoomType { get; set; }
    public string RoomName { get; set; }
}