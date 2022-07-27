namespace FotballersAPI.Application.Interfaces.QueryOptions
{
    public abstract class BaseOptions
    {
        public int? Skip { get; set; }

        public int? Take { get; set; }
    }
}
