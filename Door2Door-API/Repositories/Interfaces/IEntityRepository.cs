namespace Door2Door_API.Repositories.Interfaces;

public interface IEntityRepository<T> where T : class 
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(long id);
}