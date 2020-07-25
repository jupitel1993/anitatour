using Domain.Entities;
using Sieve.Services;

namespace Application.Common.Sieve.Profiles
{
    public class CountrySieveProfile : IAppSieveProfile
    {
        public SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Country>(x => x.Id)
                .CanSort()
                .CanFilter();
            mapper.Property<Country>(x => x.Name)
                .CanSort()
                .CanFilter();


            return mapper;
        }
    }
}