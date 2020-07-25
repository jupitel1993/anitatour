using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public User()
        {
            Persons = new List<Person>();
        }
        public bool Active { get; set; }

        public ERole Role { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public Company Company { get; set; }

        public List<Person> Persons { get; set; }

    }
}
