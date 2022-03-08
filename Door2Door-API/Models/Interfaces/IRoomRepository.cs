namespace Door2Door_API.Models.Interfaces;

public interface IRoomRepository : IRepository, IEntityRepository<Room>
{
    Task<IEnumerable<Room>> GetByTypeAsync(int typeId);
}