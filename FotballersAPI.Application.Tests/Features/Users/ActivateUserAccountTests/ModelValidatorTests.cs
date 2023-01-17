using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using FotballerAPI.Helpers.Exceptions;
using FotballersAPI.Application.Functions.Users.Commands.ActivateUserAccountCommand;
using Xunit;
using Validator =
    FotballersAPI.Application.Functions.Users.Commands.ActivateUserAccountCommand.ActivateUserAccountBusinessValidator<
        FotballersAPI.Application.Functions.Users.Commands.ActivateUserAccountCommand.ActivateUserAccountRequest>;

namespace FotballersAPI.Application.Tests.Features.Users.ActivateUserAccountTests
{
    [Collection(nameof(ModelValidatorTestsFixture))]
    public class ModelValidatorTests
    {
        private readonly IFixture _autoFixture;
        private readonly Validator _validator;
        private readonly ModelValidatorTestsFixture _fixture;

        public ModelValidatorTests(ModelValidatorTestsFixture fixture)
        {
            _fixture = fixture;
            _autoFixture = _fixture.GetPreconfiguratedAutoFixture();
            _validator = _autoFixture.Create<Validator>();
        }

        [Fact]
        public async Task ValidateForUser_WhenUserExists_ValidationPassed()
        {
            var request = CreateRequest();

            _fixture.CheckUserExistsById(true);
            _fixture.SetupHashRepository();

            var result = async () => await _validator.TestValidateAsync(request);

            await result.Should().NotThrowAsync();
        }

        [Fact]
        public async Task ValidateForUser_WhenUserNotExists_ValidationNotPassed()
        {
            var request = CreateRequest();

            var result = async () => await _validator.TestValidateAsync(request);

            await result.Should().ThrowAsync<NotFoundException>();
        }

        private ActivateUserAccountRequest CreateRequest()
        {
            return _autoFixture.Create<ActivateUserAccountRequest>();
        }
    }
}