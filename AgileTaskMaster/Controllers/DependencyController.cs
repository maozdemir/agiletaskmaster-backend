using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Services;

namespace AgileTaskMaster.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class DependencyController : ControllerBase
    {
        private readonly IDependencyService _dependencyService;

        public DependencyController(IDependencyService dependencyService)
        {
            _dependencyService = dependencyService;
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<DependencyDTO>> GetDependenciesByTaskId(string taskId)
        {
            var dependencies = await _dependencyService.GetDependenciesByTaskId(taskId);
            if (dependencies == null)
                return NotFound();

            return Ok(dependencies);
        }

        [HttpPost]
        public async Task<ActionResult<DependencyDTO>> CreateDependency(DependencyDTO dependencyDTO)
        {
            var dependency = await _dependencyService.CreateDependency(dependencyDTO);
            return CreatedAtAction(nameof(GetDependenciesByTaskId), new { taskId = dependency.TaskId }, dependency);
        }

        [HttpDelete("{dependencyId}")]
        public async Task<IActionResult> DeleteDependency(string dependencyId)
        {
            var deletedDependency = await _dependencyService.DeleteDependency(dependencyId);
            if (!deletedDependency)
                return NotFound();

            return NoContent();
        }
    }
}
