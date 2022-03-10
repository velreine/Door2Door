using System.Data;

namespace Door2Door_API.Repositories;

public abstract class BaseRepository
{
    protected readonly IDbConnection Connection;

    protected BaseRepository(IDbConnection connection)
    {
        Connection = connection;
    }
}