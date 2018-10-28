using MishMash.Models.Enums;
using System.Collections.Generic;

namespace MishMash.Models
{
   public class User
    {
        public User()
        {
            this.Channels = new HashSet<UserChannel>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<UserChannel> Channels { get; set; }

        public Role Role { get; set; }
    }
}
