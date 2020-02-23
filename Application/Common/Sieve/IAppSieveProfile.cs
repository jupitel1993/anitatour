using Sieve.Services;

namespace Application.Common.Sieve
{
    interface IAppSieveProfile
    {
        SievePropertyMapper MapProperties(SievePropertyMapper mapper);
    }
}