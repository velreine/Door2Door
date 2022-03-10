using Route = Door2Door_API.Models.Route;

namespace Door2Door_API.Repositories.Interfaces;

public interface IRouteRepository : IRepository
{
    public Task<Route> GetRouteTo(long roomId);

    public Task<string?> GetStartingPoint(long id);
}