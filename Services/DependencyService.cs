using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;
using AgileTaskMaster.Repositories;

namespace AgileTaskMaster.Services
{
    public class DependencyService : IDependencyService
    {
        private readonly IDependencyRepository _dependencyRepository;
        private readonly IMapper _mapper;

        public DependencyService(IDependencyRepository dependencyRepository, IMapper mapper)
        {
            _dependencyRepository = dependencyRepository;
            _mapper = mapper;
        }

        public async Task<List<DependencyDTO>> GetDependenciesByTaskId(string taskId)
        {
            var dependencies = await _dependencyRepository.GetDependenciesByTaskId(taskId);
            return _mapper.Map<List<DependencyDTO>>(dependencies);
        }

        public async Task<DependencyDTO> CreateDependency(DependencyDTO dependencyDTO)
        {
            var dependency = _mapper.Map<Dependency>(dependencyDTO);

            var createdDependency = await _dependencyRepository.CreateDependency(dependency);

            return _mapper.Map<DependencyDTO>(createdDependency);
        }

        public async Task<bool> DeleteDependency(string dependencyId)
        {
            var existingDependency = await _dependencyRepository.GetDependenciesByTaskId(dependencyId);
            if (existingDependency == null)
                return false;

            var deleted = await _dependencyRepository.DeleteDependency(dependencyId);

            return deleted;
        }
    }
}
