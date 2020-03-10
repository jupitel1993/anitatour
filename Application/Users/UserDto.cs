using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Users
{
    public class UserDto: IMapObject<User>
    {
        public int Id { get; set; }

        public bool Active { get; set; }

        public ERole Role { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public int? CompanyId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ForMember(x => x.CompanyId, c => c.MapFrom(x => x.Company.Id))
                .ForMember(x => x.Password, _ => _.Ignore());

            profile.CreateMap<UserDto, User>()
                .ForMember(x => x.Company, _ => _.Ignore());
        }
    }
}
