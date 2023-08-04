using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Services;
using AgileTaskMaster.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AgileTaskMaster.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public ProjectController(IProjectService projectService, IHubContext<NotificationHub> notificationHubContext)
        {
            _projectService = projectService;
            _notificationHubContext = notificationHubContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDTO>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDTO>> GetProjectById(string projectId)
        {
            var project = await _projectService.GetProjectById(projectId);
            if (project == null)
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateProject(ProjectDTO projectDTO)
        {
            var project = await _projectService.CreateProject(projectDTO);

            
            await _notificationHubContext.Clients.Group("BusinessOwner").SendAsync("ReceiveNotification", $"New project created: {project.Name}");

            return CreatedAtAction(nameof(GetProjectById), new { projectId = project.Id }, project);
        }

        [HttpPut("{projectId}")]
        public async Task<ActionResult<ProjectDTO>> UpdateProject(string projectId, ProjectDTO projectDTO)
        {
            if (projectId != projectDTO.Id)
                return BadRequest();

            var updatedProject = await _projectService.UpdateProject(projectDTO);
            if (updatedProject == null)
                return NotFound();

            
            await _notificationHubContext.Clients.Group("BusinessOwner").SendAsync("ReceiveNotification", $"Project updated: {updatedProject.Name}");

            return Ok(updatedProject);
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(string projectId)
        {
            var deletedProject = await _projectService.DeleteProject(projectId);
            if (!deletedProject)
                return NotFound();

            
            await _notificationHubContext.Clients.Group("BusinessOwner").SendAsync("ReceiveNotification", $"Project deleted");

            return NoContent();
        }

        [HttpGet("{projectId}/tasks")]
        public async Task<ActionResult<List<TaskDTO>>> GetTasksByProjectId(string projectId)
        {
            var tasks = await _projectService.GetTasksByProjectId(projectId);
            if (tasks == null)
                return NotFound();

            return Ok(tasks);
        }

        [HttpPost("{projectId}/tasks/{taskId}")]
        public async Task<IActionResult> AddTaskToProject(string projectId, string taskId)
        {
            var added = await _projectService.AddTaskToProject(projectId, taskId);
            if (!added)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{projectId}/tasks/{taskId}")]
        public async Task<IActionResult> RemoveTaskFromProject(string projectId, string taskId)
        {
            var removed = await _projectService.RemoveTaskFromProject(projectId, taskId);
            if (!removed)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{projectId}/teams")]
        public async Task<ActionResult<List<TeamDTO>>> GetTeamsByProjectId(string projectId)
        {
            var teams = await _projectService.GetTeamsByProjectId(projectId);
            if (teams == null)
                return NotFound();

            return Ok(teams);
        }

        [HttpPost("{projectId}/teams/{teamId}")]
        public async Task<IActionResult> AddTeamToProject(string projectId, string teamId)
        {
            var added = await _projectService.AddTeamToProject(projectId, teamId);
            if (!added)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{projectId}/teams/{teamId}")]
        public async Task<IActionResult> RemoveTeamFromProject(string projectId, string teamId)
        {
            var removed = await _projectService.RemoveTeamFromProject(projectId, teamId);
            if (!removed)
                return NotFound();

            return NoContent();
        }
    }
}
