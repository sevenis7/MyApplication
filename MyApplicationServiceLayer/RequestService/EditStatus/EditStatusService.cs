using Microsoft.EntityFrameworkCore;
using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer.RequestService.EditStatus
{
    public class EditStatusService : IEditStatusService
    {
        private readonly AppDbContext _context;

        public EditStatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Request?> EditStatus(int id, RequestStatus status)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(x => x.Id == id);

            if (request == null)
                return null;

            request.Status = status;

            _context.Requests.Update(request);

            await _context.SaveChangesAsync();

            return request;
        }
    }
}
