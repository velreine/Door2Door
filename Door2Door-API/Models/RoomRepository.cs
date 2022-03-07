using System.Data;
using Dapper;
using Door2Door_API.Models.Interfaces;

namespace Door2Door_API.Models;

public class RoomRepository : BaseRepository, IRoomRepository
{
    private readonly IFactory<Room> _factory;

    public RoomRepository(IDbConnection connection, IFactory<Room> factory) : base(connection)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        IList<Room> rooms = new List<Room>();

        const string sql = "SELECT * FROM get_all_rooms";
        using (var scope = _connection.BeginTransaction())
        {
            var reader = _connection.ExecuteReader(sql);
            while (reader.Read())
            {
                
                rooms.Add()
            }
        }
    }
}