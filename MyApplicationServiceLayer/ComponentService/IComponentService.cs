using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer.ComponentService
{
    public interface IComponentService
    {
        Task<Component?> Edit(int id, string value);
        Task<Component?> Get(int id);
        Task<IQueryable<Component?>?> GetAll();
    }
}
