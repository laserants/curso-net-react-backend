using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.Requests
{
    public class RolCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
