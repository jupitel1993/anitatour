using System.Collections.Generic;
using System.Linq;
using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;

namespace Application.Countries
{
    public class CountryDto: IMapObject<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<int> Directions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Country, CountryDto>()
                .ForMember(x => x.Directions, _ => _.MapFrom(x => x.Directions.Select(d => d.Id)));
        }
    }
}
