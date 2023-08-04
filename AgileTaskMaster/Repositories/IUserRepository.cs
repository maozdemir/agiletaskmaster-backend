using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.Models;

namespace AgileTaskMaster.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string userId);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DeleteUser(string userId);
        Task<User> GetUserByEmail(string email);
    }
}
