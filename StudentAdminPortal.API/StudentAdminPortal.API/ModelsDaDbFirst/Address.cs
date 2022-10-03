using System;
using System.Collections.Generic;

namespace StudentAdminPortal.API.Models
{
    public partial class Address
    {
        public Guid Id { get; set; }
        public string PhysicalAddress { get; set; } = null!;
        public string? PostalAddress { get; set; }
        public Guid StudentId { get; set; }

        //NAvigation property
        public virtual Student Student { get; set; } = null!;
    }
}
