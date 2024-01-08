using Microsoft.EntityFrameworkCore;
using MinimalAPIproject.Models;

namespace MinimalAPIproject.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<PersonInterest> PersonInterests { get; set; }
        public DbSet<PersonInterestLink> PersonInterestLinks { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    }
}
