namespace Door2Door_API.Models.Interfaces;

public interface IRepository<T> where T : class 
{
    Task<IEnumerable<T>> GetAllAsync();
}