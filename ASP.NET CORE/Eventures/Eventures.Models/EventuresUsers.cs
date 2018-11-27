using Microsoft.AspNetCore.Identity;

namespace Eventures.Models
{
    public class EventuresUsers : IdentityUser
    {
        public string FirstName { get; set; }

        public string lastName { get; set; }

        public string UniqueCitizenNumber { get; set; }
    }
}
