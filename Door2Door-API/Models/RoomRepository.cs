using System.Data;
using Dapper;
using Door2Door_API.Models.Interfaces;
using NetTopologySuite.GeometriesGraph.Index;

namespace Door2Door_API.Models;

public class RoomRepository : IRoomRepository
{
    private readonly IDbConnection connection;
    private readonly IFactory<Room> factory;

    public RoomRepository(IDbConnection connection, IFactory<Room> factory)
    {
        this.connection = connection;
        this.factory = factory;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        IList<Room> rooms = new List<Room>();

        const string query = "SELECT * FROM get_all_rooms()";
        using var reader = await connection.ExecuteReaderAsync(query);
        rooms.Add(factory.Build(reader));

        return rooms;
    }
}