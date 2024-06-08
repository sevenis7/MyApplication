using Microsoft.AspNetCore.Identity;
using MyApplicationDataLayer.DataContext;
using MyApplicationDataLayer.Entities;

namespace MyApplicationServiceLayer
{
    public class RequestInitializer
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public RequestInitializer(
            AppDbContext context, 
            UserManager<User> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Initialize()
        {
            if (!_context.Requests.Any())
            {
                await _context.Requests.AddRangeAsync(
                    await CreateRequest("1 request", RequestStatus.Received, DateTime.Now, "SpiderMan"),
                    await CreateRequest("2 request", RequestStatus.InWork, DateTime.Now, "SpiderMan"),
                    await CreateRequest("3 request", RequestStatus.Completed, DateTime.Now, "SpiderMan"),
                    await CreateRequest("4 request", RequestStatus.Rejected, DateTime.Now, "SpiderMan"),
                    await CreateRequest("5 request", RequestStatus.Canceled, DateTime.Now, "SpiderMan"),
                    await CreateRequest("6 request", RequestStatus.Received, DateTime.Now, "IronMan"),
                    await CreateRequest("7 request", RequestStatus.InWork, DateTime.Now, "IronMan"),
                    await CreateRequest("8 request", RequestStatus.Completed, DateTime.Now, "IronMan"),
                    await CreateRequest("9 request", RequestStatus.Rejected, DateTime.Now, "IronMan"),
                    await CreateRequest("10 request", RequestStatus.Canceled, DateTime.Now, "IronMan"),
                    await CreateRequest("11 request", RequestStatus.Received, DateTime.Now, "Venom"),
                    await CreateRequest("12 request", RequestStatus.InWork, DateTime.Now, "Venom"),
                    await CreateRequest("13 request", RequestStatus.Completed, DateTime.Now, "Venom"),
                    await CreateRequest("14 request", RequestStatus.Rejected, DateTime.Now, "Venom"),
                    await CreateRequest("15 request", RequestStatus.Canceled, DateTime.Now, "Venom")
                    );

                await _context.SaveChangesAsync();
            }
        }

        private async Task<Request> CreateRequest(
            string text,
            RequestStatus status,
            DateTime date,
            string userName
            )
        {
            return new Request { 
                Text = text,
                Status = status,
                Date = date,
                User = await _userManager.FindByNameAsync(userName) ?? throw new InvalidOperationException($"there is no {userName} user")};
        }
    }
}
