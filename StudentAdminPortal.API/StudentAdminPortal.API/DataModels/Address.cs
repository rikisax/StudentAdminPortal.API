using System;
using System.Collections.Generic;

namespace StudentAdminPortal.API.DataModels
{
    public  class Address
    {
        public Guid Id { get; set; }
        public string PhysicalAddress { get; set; } 
        public string? PostalAddress { get; set; }
      
        //NAvigation property
        public  Guid StudentId { get; set; } 
    }
}
