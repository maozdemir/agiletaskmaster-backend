using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.DTOs;

namespace AgileTaskMaster.Services
{
    public interface IDependencyService
    {
        Task<List<DependencyDTO>> GetDependenciesByTaskId(string taskId);
        Task<DependencyDTO> CreateDependency(DependencyDTO dependencyDTO);
        Task<bool> DeleteDependency(string dependencyId);
    }
}
