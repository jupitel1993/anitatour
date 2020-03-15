using Domain.Entities;
using Sieve.Services;

namespace Application.Common.Sieve.Profiles
{
    public class DirectionSieveProfile : IAppSieveProfile
    {
        public SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Direction>(x => x.Id)
                .CanSort()
                .CanFilter();
            mapper.Property<Direction>(x => x.Name)
                .CanSort()
                .CanFilter();
            mapper.Property<Direction>(x => x.Country)
                .CanSort()
                .CanFilter();


            return mapper;
        }
    }
}