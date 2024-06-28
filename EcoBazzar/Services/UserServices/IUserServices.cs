using EcoBazzar.BindingModel;
using EcoBazzar.DataModel;

namespace EcoBazzar.Services.UserServices
{
    public interface IUserServices
    {
        public Task<int> CreateUSer(UserBindingModel bindingModel);
        public Task<bool> DeleteUser(int id);
        public Task<User> GetUser(int id);
        public Task<List <User> > GetAllUsers();
        public Task<User> AuthenticateUser(string username, string password);

    }
}
