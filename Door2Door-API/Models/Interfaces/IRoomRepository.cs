namespace Door2Door_API.Models.Interfaces;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync();
}