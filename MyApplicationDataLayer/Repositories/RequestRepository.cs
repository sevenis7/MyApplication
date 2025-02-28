using Microsoft.EntityFrameworkCore;
using MyApplicationDataLayer.DataContext;
using MyApplicationDomain.Entities;

namespace MyApplicationDataLayer.Repositories
{

    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _context;

        public RequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Request?> Get(int id)
        {
            return await _context
                .Requests
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public IQueryable<Request> GetAll()
        {
            return _context
                .Requests.
                Include(r => r.User);
        }

        public IQueryable<Request> GetByStatus(RequestStatus status)
        {
            return _context
                .Requests
                .Include(r => r.User)
                .Where(r => r.Status == status);
        }

        public async Task Add(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Request request)
        {
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Request request)
        {
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }

}
