using AutoFixture;

namespace FotballersAPI.Application.Tests.Interfaces
{
    public interface IBaseTestFixture
    {
        IFixture GetPreconfiguratedAutoFixture();
    }
}
