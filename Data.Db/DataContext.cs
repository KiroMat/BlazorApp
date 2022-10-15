using DataApi.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Db
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 101, Login = "tom", Password = "123"},
                new User { Id = 102, Login = "ewa", Password = "111"}
                );

            modelBuilder.Entity<Plan>().HasData(
                new Plan { Id = "101", Title="Daily plan 1", Description="wwewewewewfvdfe" },
                new Plan { Id = "102", Title="Daily plan 2", Description="qqqqqqqqq" }
                );

            modelBuilder.Entity<ToDoItem>().HasData(
                new ToDoItem { Id = "101", PlanId = "101", Description = "ToDo 1"},
                new ToDoItem { Id = "102", PlanId = "101", Description = "ToDo 2"},
                new ToDoItem { Id = "103", PlanId = "101", Description = "ToDo 3"}
                );

            modelBuilder.Entity<ToDoItem>().HasData(
                new ToDoItem { Id = "104", PlanId = "102", Description = "ToDo 4" },
                new ToDoItem { Id = "105", PlanId = "102", Description = "ToDo 5" }
                );

            //modelBuilder.Entity<Plan>().Property(e => e.Id)
            //   .ValueGeneratedOnAdd()
            //   .UseIdentityColumn(1, 1);

            //modelBuilder.Entity<Plan>().Property(e => e.Id)
            //   .ValueGeneratedOnAdd()
            //   .UseIdentityColumn(1, 1);
        }
    }
}
