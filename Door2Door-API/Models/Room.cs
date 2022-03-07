using Door2Door_API.Models.Interfaces;
using GeoJSON.Net;
using NetTopologySuite.Geometries;

namespace Door2Door_API.Models;

public class Room : IRoom
{
    public long Id { get; set; }
    //public Geometry Geometry { get; set; }
    //public GeoJSONObject Geom { get; set; }
    
    // Put the workload of converting it to a JSON string on the Database.
    // Makes it easier since converting Geometry Data Type to JSON String is hard in C#/.NET
    public string Geometry { get; set; }
    
    // Temp change when RoomType entity implemented
    public long Type { get; set; }
    public string Name { get; set; }
}