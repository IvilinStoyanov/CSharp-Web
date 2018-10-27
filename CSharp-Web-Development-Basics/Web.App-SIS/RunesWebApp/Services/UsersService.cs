using RunesWebApp.Data;
using RunesWebApp.Models;
using RunesWebApp.Services.Contracts;
using SIS.HTTP.Exceptions;
using System;
using System.Linq;

namespace RunesWebApp.Services
{
    public class UsersService : IUsersService
    {
        private readonly RunesDbContext context;
        private readonly IHashService hashService;

        public UsersService(RunesDbContext context, IHashService hashService)
        {
            this.context = context;
            this.hashService = hashService;
        }

        public bool ExistsByUsernameAndPassword(string username, string password)
        {
            var hashedPassword = this.hashService.Hash(password);

            var userExists = this.context.Users
                .Any(u => u.Username == username &&
                          u.HashedPassword == hashedPassword);

            return userExists;
        }

        public User CreateUser(string username, string password, string confirmedPassword)
        {
            if (password != confirmedPassword)
            {
                throw new BadRequestException();
            }

            var hashedPassowrd = this.hashService.Hash(password);

            // Create user
            var user = new User
            {
                Username = username,
                HashedPassword = hashedPassowrd
            };
            this.context.Users.Add(user);

            try
            {
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("error");
            }

            return user;
        }
    }
}

