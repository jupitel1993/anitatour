using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Directions
{
    public class DirectionDto : BaseMapFrom<Direction>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CountryId { get; set; }

    }
}
