using AgileTaskMaster.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgileTaskMaster.Repositories
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetAllPermissions();
        Task<Permission> GetPermissionById(string id);
        Task<Permission> CreatePermission(Permission permission);
        Task<Permission> UpdatePermission(Permission permission);
        Task<bool> DeletePermission(string id);
        Task<bool> IsPermissionExists(string name); 
    }
}
