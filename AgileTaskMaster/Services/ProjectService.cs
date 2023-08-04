using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;
using AgileTaskMaster.Repositories;

namespace AgileTaskMaster.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectDTO>> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjects();
            return _mapper.Map<List<ProjectDTO>>(projects);
        }

        public async Task<ProjectDTO> GetProjectById(string projectId)
        {
            var project = await _projectRepository.GetProjectById(projectId);
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<ProjectDTO> CreateProject(ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            var createdProject = await _projectRepository.CreateProject(project);
            return _mapper.Map<ProjectDTO>(createdProject);
        }

        public async Task<ProjectDTO> UpdateProject(ProjectDTO projectDTO)
        {
            var existingProject = await _projectRepository.GetProjectById(projectDTO.Id);
            if (existingProject == null)
                return null;

            var projectToUpdate = _mapper.Map(projectDTO, existingProject);
            var updatedProject = await _projectRepository.UpdateProject(projectToUpdate);
            return _mapper.Map<ProjectDTO>(updatedProject);
        }

        public async Task<bool> DeleteProject(string projectId)
        {
            var existingProject = await _projectRepository.GetProjectById(projectId);
            if (existingProject == null)
                return false;

            var deleted = await _projectRepository.DeleteProject(projectId);
            return deleted;
        }

        public async Task<List<TaskDTO>> GetTasksByProjectId(string projectId)
        {
            var tasks = await _projectRepository.GetTasksByProjectId(projectId);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public async Task<bool> AddTaskToProject(string projectId, string taskId)
        {
            var added = await _projectRepository.AddTaskToProject(projectId, taskId);
            return added;
        }

        public async Task<bool> RemoveTaskFromProject(string projectId, string taskId)
        {
            var removed = await _projectRepository.RemoveTaskFromProject(projectId, taskId);
            return removed;
        }

        public async Task<List<TeamDTO>> GetTeamsByProjectId(string projectId)
        {
            var teams = await _projectRepository.GetTeamsByProjectId(projectId);
            return _mapper.Map<List<TeamDTO>>(teams);
        }

        public async Task<bool> AddTeamToProject(string projectId, string teamId)
        {
            var added = await _projectRepository.AddTeamToProject(projectId, teamId);
            return added;
        }

        public async Task<bool> RemoveTeamFromProject(string projectId, string teamId)
        {
            var removed = await _projectRepository.RemoveTeamFromProject(projectId, teamId);
            return removed;
        }
    }
}
