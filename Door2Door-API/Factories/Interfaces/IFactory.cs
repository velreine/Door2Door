using System.Data;

namespace Door2Door_API.Factories.Interfaces;

public interface IFactory<out T>
{
    T Build(IDataReader record);
}