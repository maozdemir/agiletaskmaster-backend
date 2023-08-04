using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;
using AgileTaskMaster.Services;
using AgileTaskMaster.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AgileTaskMaster.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public TaskController(ITaskService taskService, IMapper mapper, IHubContext<NotificationHub> notificationHubContext)
        {
            _taskService = taskService;
            _mapper = mapper;
            _notificationHubContext = notificationHubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasks()
        {
            var tasks = await _taskService.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskDTO>> GetTaskById(string taskId)
        {
            var task = await _taskService.GetTaskById(taskId);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateTask(TaskDTO taskDTO)
        {
            var task = await _taskService.CreateTask(taskDTO);

            
            await _notificationHubContext.Clients.Group("BusinessOwner").SendAsync("ReceiveNotification", $"New task created: {task.Title}");

            return CreatedAtAction(nameof(GetTaskById), new { taskId = task.Id }, task);
        }

        [HttpPut("{taskId}")]
        public async Task<ActionResult<TaskDTO>> UpdateTask(string taskId, TaskDTO taskDTO)
        {
            if (taskId != taskDTO.Id)
                return BadRequest();

            var updatedTask = await _taskService.UpdateTask(taskDTO);
            if (updatedTask == null)
                return NotFound();

            
            await _notificationHubContext.Clients.Group("BusinessOwner").SendAsync("ReceiveNotification", $"Task updated: {updatedTask.Title}");

            return Ok(updatedTask);
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(string taskId)
        {
            var deletedTask = await _taskService.DeleteTask(taskId);
            if (!deletedTask)
                return NotFound();

            
            await _notificationHubContext.Clients.Group("BusinessOwner").SendAsync("ReceiveNotification", $"Task deleted");

            return NoContent();
        }

        [HttpPut("{taskId}/priority/{priority}")]
        public async Task<ActionResult<TaskDTO>> UpdateTaskPriority(string taskId, TaskPriority priority)
        {
            var updatedTask = await _taskService.UpdateTaskPriority(taskId, priority);
            if (updatedTask == null)
                return NotFound();

            
            await _notificationHubContext.Clients.Group("BusinessOwner").SendAsync("ReceiveNotification", $"Task priority updated: {updatedTask.Title}");

            return Ok(updatedTask);
        }
    }
}
