using Domain.Entities;
using Sieve.Services;

namespace Application.Common.Sieve.Profiles
{
    public class PersonSieveProfile : IAppSieveProfile
    {
        public SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Person>(x => x.Id)
                .CanSort()
                .CanFilter();
            mapper.Property<Person>(x => x.Email)
                .CanSort()
                .CanFilter();
            mapper.Property<Person>(x => x.Document)
                .CanSort()
                .CanFilter();
            mapper.Property<Person>(x => x.FirstName)
                .CanSort()
                .CanFilter();
            mapper.Property<Person>(x => x.LastName)
                .CanSort()
                .CanFilter();
            mapper.Property<Person>(x => x.TourId)
                .CanSort()
                .CanFilter();
            mapper.Property<Person>(x => x.User)
                .CanSort()
                .CanFilter();

            return mapper;
        }
    }
}