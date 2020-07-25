using System.Security.Authentication;
using System.Threading;
using Application.Authentication.Commands;
using Application.UnitTests.Common;
using Domain.Entities;
using Domain.Enums;
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
            Context.Users.Add(new User()
            {
                Id = expectedId,
                Login = username,
                Password = HasherServiceMock.GetHash(password),
                Role = ERole.Admin,
            });
            Context.SaveChanges();
            
            var command = new LoginCommand()
            {
                Login = username,
                Password = password,
            };

            var (actualId, _) = await _handler.Handle(command, CancellationToken.None);
            Assert.Equal(expectedId, actualId);
        }

    }
}
