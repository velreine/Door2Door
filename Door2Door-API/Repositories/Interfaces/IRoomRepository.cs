using Door2Door_API.Models;

namespace Door2Door_API.Repositories.Interfaces;

public interface IRoomRepository : IRepository, IEntityRepository<Room>
{
    Task<IEnumerable<Room>> GetByTypeAsync(long typeId);
}