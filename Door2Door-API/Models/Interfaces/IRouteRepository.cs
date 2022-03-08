namespace Door2Door_API.Models.Interfaces;

public interface IRouteRepository : IRepository
{
    public Task<Route> GetRouteTo(long roomId);
}