using System;
using System.Threading.Tasks;
using AgileTaskMaster.Models;
using AgileTaskMaster.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AgileTaskMaster.Data
{
    public static class AdminSeed
    {
        public static async System.Threading.Tasks.Task SeedAdmin(IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            var passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher<User>>();

            var adminEmail = "admin@example.com";
            var adminRole = UserRole.BusinessOwner;

            
            var existingAdmin = await userRepository.GetUserByEmail(adminEmail);
            if (existingAdmin != null)
            {
                Console.WriteLine("Admin account already exists. Skipping admin seed.");
                return;
            }

            
            var admin = new User
            {
                FirstName = "Admin",
                LastName = "User",
                Email = adminEmail,
                Role = adminRole,
                CreatedAt = DateTime.UtcNow
            };

            
            var adminPassword = "Admin123"; 
            admin.PasswordHash = passwordHasher.HashPassword(admin, adminPassword);

            
            var createdAdmin = await userRepository.CreateUser(admin);

            if (createdAdmin != null)
            {
                Console.WriteLine("Admin account created successfully.");
                Console.WriteLine($"Email: {createdAdmin.Email}");
                Console.WriteLine($"Password: {adminPassword}");
            }
            else
            {
                Console.WriteLine("Failed to create admin account.");
            }
        }
    }
}
