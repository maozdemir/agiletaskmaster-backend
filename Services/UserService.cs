using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;
using AgileTaskMaster.Repositories;

namespace AgileTaskMaster.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserById(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> CreateUser(UserDTO userDTO)
        {
            var existingUser = await _userRepository.GetUserByEmail(userDTO.Email);
            if (existingUser != null)
                return null;

            var user = _mapper.Map<User>(userDTO);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password); 

            var createdUser = await _userRepository.CreateUser(user);

            return _mapper.Map<UserDTO>(createdUser);
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDTO)
        {
            var existingUser = await _userRepository.GetUserById(userDTO.Id);
            if (existingUser == null)
                return null;

            var userToUpdate = _mapper.Map(userDTO, existingUser);

            var updatedUser = await _userRepository.UpdateUser(userToUpdate);

            return _mapper.Map<UserDTO>(updatedUser);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var existingUser = await _userRepository.GetUserById(userId);
            if (existingUser == null)
                return false;

            var deleted = await _userRepository.DeleteUser(userId);

            return deleted;
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
