using AutoFixture;
using AutoMapper;
using FotballersAPI.Application.Tests.Interfaces;
using FotballersAPI.Persistence.Context;
using System;

namespace FotballersAPI.Application.Tests.Configuration
{
    public abstract class BaseTestFixture : IBaseTestFixture, IDisposable
    {
        public FotballerDbContext Context { get; private set; }

        public IMapper Mapper { get; }

        protected BaseTestFixture()
        {
            Context = FotballersDbContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
        }
        public void Dispose()
        {
            FotballersDbContextFactory.Destroy(Context);
        }

        public virtual IFixture GetPreconfiguratedAutoFixture()
        {
            return new Fixture();
        }
    }
}
