using System.Data;
using Npgsql;

namespace Door2Door_API;

public interface IFactory<out T>
{
    T Build(IDataRecord dataRecord);
}