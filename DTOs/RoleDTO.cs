using System.ComponentModel.DataAnnotations;

namespace AgileTaskMaster.DTOs
{
    public class RoleDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role Name is required.")]
        public string Name { get; set; }
    }
}
