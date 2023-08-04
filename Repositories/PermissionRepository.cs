using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using AgileTaskMaster.Models;

namespace AgileTaskMaster.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly IMongoCollection<Permission> _permissionCollection;

        public PermissionRepository(IMongoDatabase database)
        {
            _permissionCollection = database.GetCollection<Permission>("permissions");
        }

        public async Task<List<Permission>> GetAllPermissions()
        {
            return await _permissionCollection.Find(permission => true).ToListAsync();
        }

        public async Task<Permission> GetPermissionById(string id)
        {
            return await _permissionCollection.Find(permission => permission.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Permission> CreatePermission(Permission permission)
        {
            await _permissionCollection.InsertOneAsync(permission);
            return permission;
        }

        public async Task<Permission> UpdatePermission(Permission permission)
        {
            var updateResult = await _permissionCollection.ReplaceOneAsync(p => p.Id == permission.Id, permission);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0 ? permission : null;
        }

        public async Task<bool> DeletePermission(string id)
        {
            var deleteResult = await _permissionCollection.DeleteOneAsync(permission => permission.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> IsPermissionExists(string name)
        {
            var permission = await _permissionCollection.Find(p => p.Name == name).FirstOrDefaultAsync();
            return permission != null;
        }
    }
}
