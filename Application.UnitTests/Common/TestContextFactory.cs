using Microsoft.EntityFrameworkCore;
using System;
using Application.Common.Interfaces;
using Moq;
using Persistence;

namespace Application.UnitTests.Common
{
    public static class TestContextFactory
    {
        static TestContextFactory()
        {
            InitializeTokenService();
        }

        static ITokenService _tokenServiceMock;

        public static AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            

            var context = new AppDbContext(options, _tokenServiceMock);

            context.Database.EnsureCreated();
            return context;

        }

        private static void InitializeTokenService()
        {
            var mockTokenService = new Mock<ITokenService>();
            mockTokenService
                .Setup(x => x.GetUserName())
                .Returns("Test user");
            _tokenServiceMock = mockTokenService.Object;
        }

        public static void Destroy(AppDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
