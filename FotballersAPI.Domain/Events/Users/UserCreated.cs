namespace FotballersAPI.Domain.Events.Users
{
    public class UserCreated
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string ActivateAccountLink { get; set; }
    }
}
