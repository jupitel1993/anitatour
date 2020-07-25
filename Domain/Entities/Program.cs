using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Program : AuditableEntity
    {
        public Program()
        {
            Tours = new List<Tour>();
            Status = EProgramStatus.Planning;
        }

        public EProgramStatus  Status { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public List<Tour> Tours { get; set; }



    }
}
