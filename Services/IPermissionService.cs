using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.DTOs;

namespace AgileTaskMaster.Services
{
    public interface IPermissionService
    {
        Task<List<PermissionDTO>> GetAllPermissions();
        Task<PermissionDTO> GetPermissionById(string id);
        Task<PermissionDTO> CreatePermission(PermissionDTO permissionDTO);
        Task<PermissionDTO> UpdatePermission(PermissionDTO permissionDTO);
        Task<bool> DeletePermission(string id);
    }
}
