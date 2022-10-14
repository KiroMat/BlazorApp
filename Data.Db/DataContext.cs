using DataApi.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Db
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 101, Login = "tom", Password = "123"},
                new User { Id = 102, Login = "ewa", Password = "111"}
                );
        }
    }
}
