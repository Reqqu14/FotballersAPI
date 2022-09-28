using FotballersAPI.Application.Functions.Users.Dto;

namespace FotballersAPI.Application.Functions.Users.Queries
{
    public class GetUsersQueryResponse
    {
        public List<GetUserDto> Users { get; set; }
    }
}
