using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Company : AuditableEntity
    {
        public string LegalForm { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PaymentAccount { get; set; }

        public string Bic { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Unp { get; set; }

        public string Okpo { get; set; }

        public string Link { get; set; }

        public string Email { get; set; }

    }
}
