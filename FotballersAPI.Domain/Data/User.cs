using FotballersAPI.Domain.Enums;

namespace FotballersAPI.Domain.Data
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public GenderType Gender { get; set; }

        public bool Active { get; set; }        
    }
}
