using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.Models;

namespace AgileTaskMaster.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjects();
        Task<Project> GetProjectById(string projectId);
        Task<Project> CreateProject(Project project);
        Task<Project> UpdateProject(Project project);
        Task<bool> DeleteProject(string projectId);
        Task<List<Models.Task>> GetTasksByProjectId(string projectId);
        Task<bool> AddTaskToProject(string projectId, string taskId);
        Task<bool> RemoveTaskFromProject(string projectId, string taskId);
        Task<List<Team>> GetTeamsByProjectId(string projectId);
        Task<bool> AddTeamToProject(string projectId, string teamId);
        Task<bool> RemoveTeamFromProject(string projectId, string teamId);
    }
}
