using Microsoft.EntityFrameworkCore;

namespace StudentAdminPortal.API.DataModels
{
    public partial class StudentAdminContext : DbContext
    {
        public StudentAdminContext(DbContextOptions<StudentAdminContext> options) : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; } = null!;
        public virtual DbSet<Gender> Gender { get; set; } = null!;
        public virtual DbSet<Student> Student { get; set; } = null!;


    }
}
