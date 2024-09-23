using Microsoft.EntityFrameworkCore;
using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer.RequestService.List
{
    public class RequestListService : IRequestListService
    {
        private readonly AppDbContext _context;

        public RequestListService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Request>?>? GetAll()
        {
            return _context.Requests;
        }

        public async Task<IQueryable<Request>?> GetByStatus(RequestStatus status)
        {
            return _context.Requests.Where(r => r.Status == status);
        }

        public async Task<Request?> Get(int id)
        {
            return await _context.Requests.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
