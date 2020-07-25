using Domain.Common;

namespace Domain.Entities
{
    public class Direction : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

    }
}
