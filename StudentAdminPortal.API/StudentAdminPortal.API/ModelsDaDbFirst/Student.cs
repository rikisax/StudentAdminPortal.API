using System;
using System.Collections.Generic;

namespace StudentAdminPortal.API.Models
{
    public partial class Student
    {
        public Student()
        {
            Addresses = new HashSet<Address>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public int? Mobile { get; set; }
        public string? ProfileImageUrl { get; set; }
        public Guid? GenderId { get; set; }

        //Navigation properties
        public virtual Gender? Gender { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
