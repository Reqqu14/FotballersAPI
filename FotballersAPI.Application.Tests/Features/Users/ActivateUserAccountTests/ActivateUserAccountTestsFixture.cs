using AutoFixture;
using AutoFixture.AutoMoq;
using FotballersAPI.Application.Interfaces;
using FotballersAPI.Application.Tests.Configuration;
using FotballersAPI.Domain.Data;
using HashidsNet;
using Moq;
using System.Threading;
using Xunit;

namespace FotballersAPI.Application.Tests.Features.Users.ActivateUserAccountTests
{
    [CollectionDefinition(nameof(ActivateUserAccountTestsFixture))]
    public class ActivateUserAccountTestsFixture : BaseTestFixture, ICollectionFixture<ActivateUserAccountTestsFixture>
    {
        private IFixture _autofixture;

        public override IFixture GetPreconfiguratedAutoFixture()
        {
            _autofixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            _autofixture.Freeze<Mock<IUserRepository>>();
            _autofixture.Freeze<Mock<IHashids>>();

            return _autofixture;
        }

        public void SetupHashRepository()
        {
            var hashRepositoryMock = _autofixture.Create<Mock<IHashids>>();

            hashRepositoryMock.Setup(x => x.Decode(
                    It.IsAny<string>()
                ))
                .Returns(_autofixture.Create<int[]>());
        }

        public void SetupGetUserById()
        {
            var userRepositoryMock = _autofixture.Create<Mock<IUserRepository>>();

            userRepositoryMock.Setup(x => x.GetByIdAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(_autofixture.Create<User>());
        }
    }
}