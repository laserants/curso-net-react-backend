namespace AdminProjectsDemo.DTOs
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
