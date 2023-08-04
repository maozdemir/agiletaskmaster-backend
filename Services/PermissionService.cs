using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;
using AgileTaskMaster.Repositories;

namespace AgileTaskMaster.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

        public PermissionService(IPermissionRepository permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<List<PermissionDTO>> GetAllPermissions()
        {
            var permissions = await _permissionRepository.GetAllPermissions();
            return _mapper.Map<List<PermissionDTO>>(permissions);
        }

        public async Task<PermissionDTO> GetPermissionById(string id)
        {
            var permission = await _permissionRepository.GetPermissionById(id);
            return _mapper.Map<PermissionDTO>(permission);
        }

        public async Task<PermissionDTO> CreatePermission(PermissionDTO permissionDTO)
        {
            var permission = _mapper.Map<Permission>(permissionDTO);

            var createdPermission = await _permissionRepository.CreatePermission(permission);

            return _mapper.Map<PermissionDTO>(createdPermission);
        }

        public async Task<PermissionDTO> UpdatePermission(PermissionDTO permissionDTO)
        {
            var existingPermission = await _permissionRepository.GetPermissionById(permissionDTO.Id);
            if (existingPermission == null)
                return null;

            var permissionToUpdate = _mapper.Map(permissionDTO, existingPermission);

            var updatedPermission = await _permissionRepository.UpdatePermission(permissionToUpdate);

            return _mapper.Map<PermissionDTO>(updatedPermission);
        }

        public async Task<bool> DeletePermission(string id)
        {
            var existingPermission = await _permissionRepository.GetPermissionById(id);
            if (existingPermission == null)
                return false;

            var deleted = await _permissionRepository.DeletePermission(id);

            return deleted;
        }

        public async Task<bool> IsPermissionExists(string name)
        {
            return await _permissionRepository.IsPermissionExists(name);
        }
    }
}
