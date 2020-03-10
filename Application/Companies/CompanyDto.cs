﻿

using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Companies
{
    public class CompanyDto : BaseMapFromTo<Company>
    {
        public int Id { get; set; }

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
