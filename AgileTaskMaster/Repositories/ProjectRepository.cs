using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.Models;
using MongoDB.Driver;

namespace AgileTaskMaster.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _projectCollection;
        private readonly IMongoCollection<Models.Task> _taskCollection;
        private readonly IMongoCollection<Team> _teamCollection;

        public ProjectRepository(IMongoDatabase database)
        {
            _projectCollection = database.GetCollection<Project>("projects");
            _taskCollection = database.GetCollection<Models.Task>("tasks");
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _projectCollection.Find(project => true).ToListAsync();
        }

        public async Task<Project> GetProjectById(string projectId)
        {
            return await _projectCollection.Find(project => project.Id == projectId).FirstOrDefaultAsync();
        }

        public async Task<Project> CreateProject(Project project)
        {
            await _projectCollection.InsertOneAsync(project);
            return project;
        }

        public async Task<Project> UpdateProject(Project project)
        {
            var updateResult = await _projectCollection.ReplaceOneAsync(p => p.Id == project.Id, project);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0 ? project : null;
        }

        public async Task<bool> DeleteProject(string projectId)
        {
            var deleteResult = await _projectCollection.DeleteOneAsync(project => project.Id == projectId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<List<Models.Task>> GetTasksByProjectId(string projectId)
        {
            var project = await _projectCollection.Find(p => p.Id == projectId).FirstOrDefaultAsync() ?? throw new ArgumentException("Project can not be found.");
            var tasks = await _taskCollection.Find(task => project.TaskIds.Contains(task.Id)).ToListAsync();
            return tasks;
        }

        public async Task<bool> AddTaskToProject(string projectId, string taskId)
        {
            var project = await _projectCollection.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
                return false;

            project.TaskIds ??= new List<string>();
            project.TaskIds.Add(taskId);

            var updateResult = await _projectCollection.ReplaceOneAsync(p => p.Id == projectId, project);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> RemoveTaskFromProject(string projectId, string taskId)
        {
            var project = await _projectCollection.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
                return false;

            project.TaskIds?.Remove(taskId);

            var updateResult = await _projectCollection.ReplaceOneAsync(p => p.Id == projectId, project);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<List<Team>> GetTeamsByProjectId(string projectId)
        {
            var project = await _projectCollection.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
                return null;

            var teams = await _teamCollection.Find(t => project.TeamIds.Contains(t.Id)).ToListAsync();
            return teams;
        }

        public async Task<bool> AddTeamToProject(string projectId, string teamId)
        {
            var project = await _projectCollection.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
                return false;

            project.TeamIds ??= new List<string>();
            project.TeamIds.Add(teamId);

            var updateResult = await _projectCollection.ReplaceOneAsync(p => p.Id == projectId, project);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> RemoveTeamFromProject(string projectId, string teamId)
        {
            var project = await _projectCollection.Find(p => p.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
                return false;

            project.TeamIds?.Remove(teamId);

            var updateResult = await _projectCollection.ReplaceOneAsync(p => p.Id == projectId, project);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
