using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;

namespace AgileTaskMaster.Services
{
    public interface ITaskService
    {
        Task<List<TaskDTO>> GetAllTasks();
        Task<TaskDTO> GetTaskById(string taskId);
        Task<TaskDTO> CreateTask(TaskDTO taskDTO);
        Task<TaskDTO> UpdateTask(TaskDTO taskDTO);
        Task<bool> DeleteTask(string taskId);
        Task<TaskDTO> UpdateTaskPriority(string taskId, TaskPriority priority);

    }
}
