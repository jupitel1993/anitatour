using System.Security.Authentication;
using System.Threading;
using Application.Authentication.Commands;
using Application.UnitTests.Common;
using Domain.Entities;
using Xunit;

namespace Application.UnitTests.Authentication.Commands
{
    public class LoginCommandTests : CommandTestBase
    {
        private readonly LoginCommandHandler _handler;
        public LoginCommandTests()
        {
            _handler = new LoginCommandHandler(Context, HasherServiceMock);
        }

        [Theory]
        [InlineData("string", "string")]
        [InlineData("testUsername2", "testPassword2")]
        public async void LoginCommand_CanLoginWithValidCredentials(string username, string password)
        {
            var expectedId = 1;
            Context.Shops.Add(new Shop()
            {
                Id = expectedId,
                MemberAreaUsername = username,
                MemberAreaPassword = HasherServiceMock.GetHash(password),
                Administrator = true,
            });
            Context.SaveChanges();
            
            var command = new LoginCommand()
            {
                UserName = username,
                Password = password,
            };

            var actualId = await _handler.Handle(command, CancellationToken.None);
            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData("string", "string")]
        [InlineData("testUsername2", "testPassword2")]
        public async void LoginCommand_CanNotLoginWithInvalidCredentials(string username, string password)
        {
            Context.Shops.Add(new Shop()
            {
                MemberAreaUsername = username,
                MemberAreaPassword = HasherServiceMock.GetHash(password) + "difference",
                Administrator = true,
            });
            Context.SaveChanges();
            
            var command = new LoginCommand()
            {
                UserName = username,
                Password = password,
            };
            await Assert.ThrowsAsync<InvalidCredentialException>(() => _handler.Handle(command, CancellationToken.None));
        }

    }
}
