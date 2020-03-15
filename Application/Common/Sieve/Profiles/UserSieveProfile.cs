using Domain.Entities;
using Sieve.Services;

namespace Application.Common.Sieve.Profiles
{
    public class UserSieveProfile : IAppSieveProfile
    {
        public SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<User>(x => x.Id)
                .CanSort()
                .CanFilter();
            mapper.Property<User>(x => x.Active)
                .CanSort()
                .CanFilter();
            mapper.Property<User>(x => x.Login)
                .CanSort()
                .CanFilter();
            mapper.Property<User>(x => x.Role)
                .CanSort()
                .CanFilter();
            mapper.Property<User>(x => x.Username)
                .CanSort()
                .CanFilter();

            return mapper;
        }
    }
}