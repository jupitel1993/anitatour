using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Country : AuditableEntity
    {
        public string Name { get; set; }

        public List<Direction> Directions { get; set; }
    }
}
