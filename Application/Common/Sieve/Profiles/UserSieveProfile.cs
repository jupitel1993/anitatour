using Domain.Entities;
using Sieve.Services;

namespace Application.Common.Sieve.Profiles
{
    public class UserSieveProfile : IAppSieveProfile
    {
        public SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<User>(x => x.Id)
                .CanSort();
            
            return mapper;
        }
    }
}