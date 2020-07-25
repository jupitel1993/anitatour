using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;

namespace Application.Persons
{
    public class PersonDto : IMapObject<Person>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, PersonDto>();

            profile.CreateMap<PersonDto, Person>();
        }
    }
}
