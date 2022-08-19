namespace Curso_Backend_SEGEPLAN.DTOs.Responses
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
