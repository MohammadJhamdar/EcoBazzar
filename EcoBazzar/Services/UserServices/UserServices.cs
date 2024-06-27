using EcoBazzar.BindingModel;
using EcoBazzar.DataBase;
using EcoBazzar.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace EcoBazzar.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly AppDbContext _context;
        public UserServices(AppDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await _context.users.SingleOrDefaultAsync(u => u.UserName == username);
            if (user != null && VerifyPassword(user.Password, password))
            {
                return user;
            }
            return null;
        }

        public async Task<int> CreateUSer(UserBindingModel bindingModel)
        {
            User user=new User();
            MapToDataModel(bindingModel, user);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user=await _context.users.FindAsync(id);   
            if (user != null)
            {
                 _context.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.users.ToListAsync();
        }

        public async Task<User?> GetUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                
                return user;
            }
            return null;
        }

        private void  MapToDataModel(UserBindingModel bindingModel,User user)
        {
            user.Name= bindingModel.Name;
            user.Email= bindingModel.Email;
            user.Password= HashPassword(bindingModel.Password);
            user.UserName= bindingModel.UserName;
            user.Phone= bindingModel.Phone;
            user.Gender= bindingModel.Gender;
            user.Role= bindingModel.Role;
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string storedHash, string password)
        {
            var hash = HashPassword(password);
            return storedHash == hash;
        }
    }
}
