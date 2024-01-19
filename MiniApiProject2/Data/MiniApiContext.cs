using Microsoft.EntityFrameworkCore;
using MiniApiProject2.Models;

namespace MiniApiProject2.Data
{
    public class MiniApiContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<InterestUrlLink> InterestUrlLinks { get; set; }


        public MiniApiContext(DbContextOptions<MiniApiContext> options) : base(options) { }
    }
}
