namespace FotballersAPI.Application.Functions.Users.Dto
{
    public class GetUserDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public bool Active { get; set; }
    }
}
