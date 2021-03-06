namespace Door2Door_API.Models;

public class Room
{
    public long Id { get; set; }
    
    // Put the workload of converting it to a JSON string on the Database.
    // Makes it easier since converting Geometry Data Type to JSON String is hard in C#/.NET
    public string Geometry { get; set; }
    
    public RoomType Type { get; set; }
    
    public string Name { get; set; }
}