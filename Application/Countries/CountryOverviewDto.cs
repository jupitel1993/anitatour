using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Countries
{
    public class CountryOverviewDto : BaseMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
