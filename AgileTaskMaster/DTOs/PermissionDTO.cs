using System.ComponentModel.DataAnnotations;

namespace AgileTaskMaster.DTOs
{
    public class PermissionDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Permission Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
