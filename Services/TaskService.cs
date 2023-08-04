
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;
using AgileTaskMaster.Repositories;

namespace AgileTaskMaster.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<List<TaskDTO>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTasks();
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public async Task<TaskDTO> GetTaskById(string taskId)
        {
            var task = await _taskRepository.GetTaskById(taskId);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<TaskDTO> CreateTask(TaskDTO taskDTO)
        {
            var task = _mapper.Map<Models.Task>(taskDTO);

            var createdTask = await _taskRepository.CreateTask(task);

            return _mapper.Map<TaskDTO>(createdTask);
        }

        public async Task<TaskDTO> UpdateTask(TaskDTO taskDTO)
        {
            var existingTask = await _taskRepository.GetTaskById(taskDTO.Id);
            if (existingTask == null)
                return null;

            var taskToUpdate = _mapper.Map(taskDTO, existingTask);

            var updatedTask = await _taskRepository.UpdateTask(taskToUpdate);

            return _mapper.Map<TaskDTO>(updatedTask);
        }

        public async Task<bool> DeleteTask(string taskId)
        {
            var existingTask = await _taskRepository.GetTaskById(taskId);
            if (existingTask == null)
                return false;

            var deleted = await _taskRepository.DeleteTask(taskId);

            return deleted;
        }

        public async Task<TaskDTO> UpdateTaskPriority(string taskId, TaskPriority priority)
        {
            var existingTask = await _taskRepository.GetTaskById(taskId);
            if (existingTask == null)
                return null;

            var updatedTask = await _taskRepository.UpdateTask(existingTask);

            return _mapper.Map<TaskDTO>(updatedTask);
        }
    }
}
