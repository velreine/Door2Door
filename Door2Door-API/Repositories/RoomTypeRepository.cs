using System.Data;
using Dapper;
using Door2Door_API.Models.Interfaces;

namespace Door2Door_API.Models;

public class RoomTypeRepository : BaseRepository, IRoomTypeRepository
{
    private readonly IFactory<RoomType> factory;
    
    // This is done because the factory.Build expects a specific name from the columns. 
    private const string TABLE = "room_type rt";
    private const string COLUMNS = "rt.id as room_type_id, rt.room_type as type";

    public RoomTypeRepository(IDbConnection connection, IFactory<RoomType> factory) : base(connection)
    {
        this.factory = factory;
    }

    public async Task<IEnumerable<RoomType>> GetAllAsync()
    {
        IList<RoomType> types = new List<RoomType>();
        const string query = $"SELECT {COLUMNS} FROM {TABLE}";
        using var reader = await Connection.ExecuteReaderAsync(query);
        while (reader.Read())
        {
            types.Add(factory.Build(reader));
        }

        return types;
    }

    public async Task<RoomType> GetByIdAsync(long id)
    {
        const string query = $"SELECT {COLUMNS} FROM {TABLE} WHERE id = :type_id";
        using var reader = await Connection.ExecuteReaderAsync(query, id);
        return reader.ReadFirstOrDefault(r => factory.Build(r));
    }
}