namespace WebApplication.Models
{
    public class SignInResponse
    {
        public bool succes { get; set; }

        public string token { get; set; } = string.Empty;
    }
}
