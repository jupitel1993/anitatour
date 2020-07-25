using System.Threading;
using Application.Common.System.Commands;
using Application.UnitTests.Common;
using MediatR;
using Xunit;

namespace Application.UnitTests.System.Commands
{
    public class SeedSampleDataCommandTests : CommandTestBase
    {
        private readonly IRequestHandler<SeedSampleDataCommand> _handler;
        private readonly SeedSampleDataCommand _request;

        public SeedSampleDataCommandTests()
        {
            _request = new SeedSampleDataCommand();
            _handler = new SeedSampleDataCommandHandler(Context, HasherServiceMock);
        }

        [Fact]
        public async void Seed_ShouldAddSampleData()
        {
            // todo: update test if SeedSampleDataCommand do something

            await _handler.Handle(_request, CancellationToken.None);
            Assert.True(true);
        }
    }
}