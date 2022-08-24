using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.Requests
{
    public class AssingRoleToUserRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
