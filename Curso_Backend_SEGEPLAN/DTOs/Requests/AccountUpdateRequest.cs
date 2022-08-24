using System.ComponentModel.DataAnnotations;

namespace Curso_Backend_SEGEPLAN.DTOs.Requests
{
    public class AccountUpdateRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
