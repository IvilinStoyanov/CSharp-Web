﻿namespace RunesWebApp.Models
{
    public class User : BaseModel<int>
    {
        public string Username { get; set; }

        public string HashedPassword { get; set; }

        public string Email { get; set; }
    }
}
