using System.Data;
using Dapper;
using Door2Door_API.Models.Interfaces;

namespace Door2Door_API.Models;

public class RoomTypeRepository : BaseRepository, IRoomTypeRepository
{
    private readonly IFactory<RoomType> factory;

    public RoomTypeRepository(IDbConnection connection, IFactory<RoomType> factory) : base(connection)
    {
        this.factory = factory;
    }

    public async Task<IEnumerable<RoomType>> GetAllAsync()
    {
        IList<RoomType> types = new List<RoomType>();
        const string query = "SELECT * FROM room_type";
        using var reader = await Connection.ExecuteReaderAsync(query);
        while (reader.Read())
        {
            types.Add(factory.Build(reader));
        }

        return types;
    }

    public async Task<RoomType> GetByIdAsync(long id)
    {
        const string query = "SELECT * FROM room_type WHERE id = :type_id";
        using var reader = await Connection.ExecuteReaderAsync(query, id);
        return reader.ReadFirstOrDefault(r => factory.Build(r));
    }
}