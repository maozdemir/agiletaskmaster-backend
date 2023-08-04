using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.Models;

namespace AgileTaskMaster.Repositories
{
    public interface IDependencyRepository
    {
        Task<List<Dependency>> GetDependenciesByTaskId(string taskId);
        Task<Dependency> CreateDependency(Dependency dependency);
        Task<bool> DeleteDependency(string dependencyId);
    }
}
