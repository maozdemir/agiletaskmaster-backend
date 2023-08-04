using System.ComponentModel.DataAnnotations;

namespace AgileTaskMaster.DTOs
{
    public class DependencyDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Task ID is required.")]
        public string TaskId { get; set; }

        [Required(ErrorMessage = "Dependent Task ID is required.")]
        public string DependentTaskId { get; set; }
    }
}
