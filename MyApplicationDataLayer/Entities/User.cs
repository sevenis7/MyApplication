using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApplicationDataLayer.Entities
{
    public class User : IdentityUser<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Role Role { get; set; }
        public ICollection<Request>? Requests { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }

        [NotMapped]
        public string FullName => FirstName + " " + LastName;
    }
}
