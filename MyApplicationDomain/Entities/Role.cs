using Microsoft.AspNetCore.Identity;

namespace MyApplicationDomain.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<User> Users { get; set; }
    }
}
