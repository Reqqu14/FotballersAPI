using System.Threading;
using AutoFixture;
using AutoFixture.AutoMoq;
using FotballersAPI.Application.Interfaces;
using HashidsNet;
using Moq;
using Xunit;

namespace FotballersAPI.Application.Tests.Features.Users.ActivateUserAccountTests;

[CollectionDefinition(nameof(ModelValidatorTestsFixture))]
public class ModelValidatorTestsFixture : ICollectionFixture<ModelValidatorTestsFixture>
{
    private IFixture _autofixture;

    public IFixture GetPreconfiguratedAutoFixture()
    {
        _autofixture = new Fixture().Customize(new AutoMoqCustomization());

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

    public void CheckUserExistsById(bool exists)
    {
        var userRepositoryMock = _autofixture.Create<Mock<IUserRepository>>();

        if (exists)
        {
            userRepositoryMock.Setup(x => x.CheckUserExistsInDatabaseAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(exists);
        }
        else
        {
            userRepositoryMock.Setup(x => x.CheckUserExistsInDatabaseAsync(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(exists);
        }
    }
}