using System;
using Application.Common.Interfaces;
using Application.Common.Sieve;
using Application.Common.Sieve.CustomFilters;
using Application.Common.Sieve.CustomSortings;
using AutoMapper;
using Domain.Enums;
using Microsoft.Extensions.Options;
using Moq;
using Persistence;
using Sieve.Models;
using Sieve.Services;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ISieveProcessor SieveProcessor;
        protected readonly AppDbContext Context;
        protected readonly IMapper Mapper;
        protected IHasherService HasherServiceMock;

        public CommandTestBase()
        {
            Context = TestContextFactory.Create();
            Mapper = new MappingTestsFixture().Mapper;
            SieveProcessor = new AppSieveProcessor(new OptionsWrapper<SieveOptions>(
                new SieveOptions()),
                new AppSortMethods(),
                new AppFilterMethods());

            InitializeHasherService();
        }

        private void InitializeHasherService()
        {
            var mockHasherService = new Mock<IHasherService>();
            mockHasherService
                .Setup(x => x.GetHash(It.IsAny<string>()))
                .Returns("encryptedPassword");
            HasherServiceMock = mockHasherService.Object;
        }
        
        public void Dispose()
        {
            TestContextFactory.Destroy(Context);
        }
    }
}