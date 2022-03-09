using System.Data;
using Dapper;
using Door2Door_API.Models.Interfaces;

namespace Door2Door_API.Models;

public class RoomRepository : BaseRepository, IRoomRepository
{
    private readonly IFactory<Room> factory;

    public RoomRepository(IDbConnection connection, IFactory<Room> factory) : base(connection)
    {
        this.factory = factory;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        IList<Room> rooms = new List<Room>();

        const string query = "SELECT * FROM get_all_rooms()";

        using var reader = await Connection.ExecuteReaderAsync(query);
        while (reader.Read())
        {
            rooms.Add(factory.Build(reader));
        }

        return rooms;
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        const string query = "SELECT * FROM get_room_by_id(bigint id)";
        using var reader = await Connection.ExecuteReaderAsync(query, id);
        while (reader.Read())
        {
            return reader.ReadFirstOrDefault(r => factory.Build(r));
        }

        return null;
    }

    public async Task<IEnumerable<Room>> GetByTypeAsync(int typeId)
    {
        var rooms = new List<Room>();
        const string query = "SELECT * FROM get_all_rooms_by_type(bigint id)";
        using var reader = await Connection.ExecuteReaderAsync(query, typeId);
        while (reader.Read())
        {
            rooms.Add(factory.Build(reader));
        }

        return rooms;
    }

}