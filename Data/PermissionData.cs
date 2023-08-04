using AgileTaskMaster.Models;
using AgileTaskMaster.Repositories;

namespace AgileTaskMaster.Data
{
    public static class PermissionData
    {
        public static void Seed(IPermissionRepository permissionRepository)
        {
            var permissions = new[]
            {
                new Permission { Name = "CreateTask", Description = "Permission to create tasks" },
                new Permission { Name = "UpdateTask", Description = "Permission to update tasks" },
                new Permission { Name = "DeleteTask", Description = "Permission to delete tasks" },
                new Permission { Name = "CreateUser", Description = "Permission to create users" },
                new Permission { Name = "UpdateUser", Description = "Permission to update users" },
                new Permission { Name = "DeleteUser", Description = "Permission to delete users" },
                new Permission { Name = "CreatePermission", Description = "Permission to create permissions" },
                new Permission { Name = "UpdatePermission", Description = "Permission to update permissions" },
                new Permission { Name = "DeletePermission", Description = "Permission to delete permissions" }
            };

            foreach (var permission in permissions)
            {
                if (!permissionRepository.IsPermissionExists(permission.Name).Result)
                {
                    permissionRepository.CreatePermission(permission);
                }
            }
        }
    }
}
