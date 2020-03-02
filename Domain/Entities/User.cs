using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public bool Active { get; set; }

        public ERole Role { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

    }
}
