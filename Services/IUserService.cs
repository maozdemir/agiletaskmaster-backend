using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.DTOs;

namespace AgileTaskMaster.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(string userId);
        Task<UserDTO> CreateUser(UserDTO userDTO);
        Task<UserDTO> UpdateUser(UserDTO userDTO);
        Task<bool> DeleteUser(string userId);
        Task<UserDTO> GetUserByEmail(string email);
    }
}
