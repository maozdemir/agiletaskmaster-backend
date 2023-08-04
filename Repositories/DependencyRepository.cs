using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using AgileTaskMaster.Models;

namespace AgileTaskMaster.Repositories
{
    public class DependencyRepository : IDependencyRepository
    {
        private readonly IMongoCollection<Dependency> _dependencyCollection;

        public DependencyRepository(IMongoDatabase database)
        {
            _dependencyCollection = database.GetCollection<Dependency>("dependencies");
        }

        public async Task<List<Dependency>> GetDependenciesByTaskId(string taskId)
        {
            return await _dependencyCollection.Find(dependency => dependency.TaskId == taskId).ToListAsync();
        }

        public async Task<Dependency> CreateDependency(Dependency dependency)
        {
            await _dependencyCollection.InsertOneAsync(dependency);
            return dependency;
        }

        public async Task<bool> DeleteDependency(string dependencyId)
        {
            var deleteResult = await _dependencyCollection.DeleteOneAsync(dependency => dependency.Id == dependencyId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
