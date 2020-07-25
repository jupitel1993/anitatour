using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;

namespace Application.Tours
{
    public class TourDto: IMapObject<Tour>
    {
        public TourDto()
        {
            Tourists = new List<int>();
        }

        public int Id { get; set; }

        public int ProgramId { get; set; }
        public string ProgramName { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<int> Tourists { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tour, TourDto>()
                .ForMember(x => x.ProgramName, _ => _.MapFrom(x => x.Program.Name))
                .ForMember(x => x.Tourists, _ => _.MapFrom(x => x.Tourists.Select(t => t.Id)));
        }
    }
}
