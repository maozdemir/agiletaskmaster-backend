using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.DTOs;

namespace AgileTaskMaster.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDTO>> GetAllProjects();
        Task<ProjectDTO> GetProjectById(string projectId);
        Task<ProjectDTO> CreateProject(ProjectDTO projectDTO);
        Task<ProjectDTO> UpdateProject(ProjectDTO projectDTO);
        Task<bool> DeleteProject(string projectId);
        Task<List<TaskDTO>> GetTasksByProjectId(string projectId);
        Task<bool> AddTaskToProject(string projectId, string taskId);
        Task<bool> RemoveTaskFromProject(string projectId, string taskId);
        Task<List<TeamDTO>> GetTeamsByProjectId(string projectId);
        Task<bool> AddTeamToProject(string projectId, string teamId);
        Task<bool> RemoveTeamFromProject(string projectId, string teamId);
    }
}
