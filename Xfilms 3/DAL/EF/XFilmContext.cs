using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class XFilmContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public XFilmContext(DbContextOptions<XFilmContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(e => e.RD).HasDefaultValue(DateTime.Now.ToShortDateString());
            base.OnModelCreating(modelBuilder);
        }
    }
}
