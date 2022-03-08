namespace Door2Door_API.Models.Interfaces;

public interface IEntityRepository<T> where T : class 
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    T? GetById(int id);
}