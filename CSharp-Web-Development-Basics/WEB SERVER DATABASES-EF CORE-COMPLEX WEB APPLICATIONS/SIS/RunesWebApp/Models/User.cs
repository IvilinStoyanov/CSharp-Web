namespace RunesWebApp.Models
{
    public class User : BaseModel<string>
    {
        public string Username { get; set; }

        public string HashedPassoword { get; set; }

        public string Email { get; set; }
    }
}
