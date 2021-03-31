using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<User> GetByEmail(string email)
        {
            // Get the entity
            var result = await _context.Users.FirstAsync(user => user.Email == email);

            // Build our view model            

            return result;
        }

        public async Task<User> EditProfile(User user) //
        {
            var email = user.Email;
            // Get the entity
            var editUser = await _context.Users.FirstAsync(person => person.Email == email);

            // Edit and Update User profile

            user.FirstName = editUser.FirstName;
            user.LastName = editUser.LastName;
            //user.PhoneNumber = editUser.PhoneNumber;
            //user.PasswordHash = editUser.PasswordHash;

            _context.Update(user);
            await _context.SaveChangesAsync();


            //UserName = login.Email,
            //Password = login.Password,
            //ClientId = login.ClientId

            return user;
        }

        //public async Task EditProfile(UserVM userVM)
        //{
        //    // Get the entity
        //    var editUser = await _context.Users.FirstAsync(person => person.Email == email);

        //    // Edit and Update User profile

        //    user.FirstName = editUser.FirstName;
        //    user.LastName = editUser.LastName;
        //    //user.PhoneNumber = editUser.PhoneNumber;
        //    //user.PasswordHash = editUser.PasswordHash;

        //    _context.Update(user);
        //    await _context.SaveChangesAsync();


        //    //UserName = login.Email,
        //    //Password = login.Password,
        //    //ClientId = login.ClientId
        //}
    }
}
