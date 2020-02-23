using System;
using System.Text;
using Application.Common.Interfaces;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Xunit;

namespace Infrastructure.UnitTests.Services
{
    public class HasherServiceTests
    {
        private readonly IHasherService _hasherService;

        public HasherServiceTests()
        {
            IOptions<HasherSettings> options = Options.Create(new HasherSettings()
            {
                Salt = Convert.ToBase64String(Encoding.ASCII.GetBytes("someBase64string")),
            });
            _hasherService = new HasherService(options);
        }
        
        [Theory]
        [InlineData("password")]
        public void GetHash_ShouldWork(string password)
        {
            var hash = _hasherService.GetHash(password);

            Assert.NotNull(hash);
        }

    }
}
