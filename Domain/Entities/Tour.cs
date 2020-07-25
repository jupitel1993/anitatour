using System;
using System.Collections.Generic;
using Domain.Common;


namespace Domain.Entities
{
    public class Tour : AuditableEntity
    {
        public Tour()
        {
            Tourists = new List<Person>();
        }

        public int ProgramId { get; set; }
        public Program Program { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public List<Person> Tourists { get; set; }

    }
}
