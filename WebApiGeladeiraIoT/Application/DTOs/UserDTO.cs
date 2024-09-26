namespace Application.DTOs
{
    public class RegisterUserDTO
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class LoginUserDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
