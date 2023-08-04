using System.Threading.Tasks;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;
using AgileTaskMaster.Repositories;
using AgileTaskMaster.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AgileTaskMaster.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponseDTO> Login(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetUserByEmail(loginDTO.Email);
            if (user == null)
                return null;

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
                return null;

            var token = GenerateJwtToken(user);

            var authResponse = new AuthResponseDTO
            {
                UserId = user.Id,
                Email = user.Email,
                Token = token
            };

            return authResponse;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
