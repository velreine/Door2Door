using System.Data;
using Door2Door_API.Models.Interfaces;

namespace Door2Door_API.Models;

public abstract class BaseRepository
{
    protected readonly IDbConnection _connection;

    protected BaseRepository(IDbConnection connection)
    {
        _connection = connection;
    }
}