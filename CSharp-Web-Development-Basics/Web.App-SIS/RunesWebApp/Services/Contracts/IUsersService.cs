using RunesWebApp.Models;

namespace RunesWebApp.Services.Contracts
{
    public interface IUsersService
    {
        bool ExistsByUsernameAndPassword(string username, string password);

        User CreateUser(string username, string password, string comfirmPassword);
    }
}
