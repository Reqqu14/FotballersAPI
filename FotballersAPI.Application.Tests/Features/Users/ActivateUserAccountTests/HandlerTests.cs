using AutoFixture;
using FotballersAPI.Application.Functions.Users.Commands.ActivateUserAccountCommand;
using FotballersAPI.Application.Interfaces;
using FotballersAPI.Domain.Data;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Handler =
    FotballersAPI.Application.Functions.Users.Commands.ActivateUserAccountCommand.ActivateUserAccountCommand.Handler;

namespace FotballersAPI.Application.Tests.Features.Users.ActivateUserAccountTests
{
    [Collection(nameof(ActivateUserAccountTestsFixture))]
    public class HandlerTests
    {
        private readonly Handler _handler;
        private readonly ActivateUserAccountTestsFixture _fixture;
        private readonly IFixture _autoFixture;

        public HandlerTests(ActivateUserAccountTestsFixture fixture)
        {
            _fixture = fixture;
            _autoFixture = _fixture.GetPreconfiguratedAutoFixture();
            _handler = _autoFixture.Create<Handler>();
        }

        [Fact]
        public async Task ActivateUserAccount_WhenAllDataIsCorrect_ActivateUser()
        {
            var userRepositoryMock = _autoFixture.Create<Mock<IUserRepository>>();

            _fixture.SetupHashRepository();
            _fixture.SetupGetUserById();

            var request = _autoFixture.Create<ActivateUserAccountRequest>();

            await _handler.Handle(request, default);

            userRepositoryMock.Verify(x => x.UpdateAsync(
                    It.IsAny<User>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once);
        }
    }
}