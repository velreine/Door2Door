using System.Data;
using Dapper;
using Door2Door_API.ExtensionMethods;
using Door2Door_API.Factories.Interfaces;
using Door2Door_API.Models;
using Door2Door_API.Repositories.Interfaces;

namespace Door2Door_API.Repositories;

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

    public async Task<Room?> GetByIdAsync(long id)
    {
        const string query = "SELECT * FROM get_room_by_id(:id)";
        using var reader = await Connection.ExecuteReaderAsync(query, new {id});
        
        return reader.ReadFirstOrDefault(r => factory.Build(r));
    }

    public async Task<IEnumerable<Room>> GetByTypeAsync(long typeId)
    {
        var rooms = new List<Room>();
        const string query = "SELECT * FROM get_all_rooms_by_type(:typeId)";
        using var reader = await Connection.ExecuteReaderAsync(query, new{typeId});
        while (reader.Read())
        {
            rooms.Add(factory.Build(reader));
        }

        return rooms;
    }

}