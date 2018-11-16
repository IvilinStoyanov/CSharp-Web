using Microsoft.AspNetCore.Identity;

namespace Pepper.Models
{
    public class PepperUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
