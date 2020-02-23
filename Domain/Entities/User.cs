using Domain.Common;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public bool Active { get; set; }

        public bool Super { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

    }
}
