using System.Collections.Generic;
using System.Threading.Tasks;
using Family_Web_API.Models;


namespace Family_Web_API.Data
{
    public interface IUserService
    {
        User ValidateUser(string userName, string password);
        Task AddUserAsync(User user);
        Task<IList<User>> GetUsersAsync();
        Task<IList<User>> GetBasicUsersAsync();
        Task<User> RemoveUserAsync(string username);
    }
}