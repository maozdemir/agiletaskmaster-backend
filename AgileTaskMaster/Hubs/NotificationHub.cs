using AgileTaskMaster.Helpers;
using AgileTaskMaster.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgileTaskMaster.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IConfiguration _configuration;

        public NotificationHub(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async System.Threading.Tasks.Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        public async System.Threading.Tasks.Task SendNotificationToUser(string connectionId, string message)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
        }

        public async System.Threading.Tasks.Task SendNotificationToProjectMembers(string projectId, string message)
        {
            await Clients.Group(projectId).SendAsync("ReceiveNotification", message);
        }

        public async Task<System.Threading.Tasks.Task> JoinGroup(string groupName)
        {
            var token = Context.GetHttpContext().Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            
            Console.WriteLine($"Request to join group {groupName} with token {token}");
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                
                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!Enum.TryParse<UserRole>(roleClaim, out var userRole) || userRole != UserRole.BusinessOwner)
                {
                    throw new SecurityTokenException("User does not have the required role to join the group.");
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                return System.Threading.Tasks.Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Invalid token.", ex);
            }
        }

        public async System.Threading.Tasks.Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
