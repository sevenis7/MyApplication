using Microsoft.AspNetCore.Identity;

namespace MyApplicationDataLayer.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<User> Users { get; set; }
    }
}
