using Npgsql;

namespace Door2Door_API.Models.Interfaces;

public interface IFactory<out T>
{
    T Build(NpgsqlDataReader dataRecord);
}