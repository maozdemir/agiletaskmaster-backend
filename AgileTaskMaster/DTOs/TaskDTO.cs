using System.ComponentModel.DataAnnotations;

namespace AgileTaskMaster.DTOs
{
    public class TaskDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public TaskStatus Status { get; set; }

        [Required(ErrorMessage = "Deadline is required.")]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Assignee is required.")]
        public string AssigneeId { get; set; }

        [Required(ErrorMessage = "Created By is required.")]
        public string CreatedById { get; set; }

        public string ProjectId { get; set; } 
    }
}
