using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Directions
{
    public class DirectionOverviewDto : BaseMapFrom<Direction>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
