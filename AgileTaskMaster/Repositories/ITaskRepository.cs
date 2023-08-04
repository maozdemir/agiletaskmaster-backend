using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.Models;

namespace AgileTaskMaster.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> GetAllTasks();
        Task<Models.Task> GetTaskById(string taskId);
        Task<Models.Task> CreateTask(Models.Task task);
        Task<Models.Task> UpdateTask(Models.Task task);
        Task<bool> DeleteTask(string taskId);
        Task<Models.Task> UpdateTaskPriority(string taskId, TaskPriority priority);

    }
}
