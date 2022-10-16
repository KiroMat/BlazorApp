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
                new Plan { 
                    Id = "101", 
                    Title="Daily plan 1", 
                    Description="wwewewewewfvdfe",
                    CoverPath= "https://images.unsplash.com/photo-1453728013993-6d66e9c9123a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8bGVuc3xlbnwwfHwwfHw%3D&w=1000&q=80"
                },
                new Plan { 
                    Id = "102", 
                    Title="Daily plan 2", 
                    Description="qqqqqqqqq",
                    CoverPath = "https://media.istockphoto.com/photos/wild-grass-in-the-mountains-at-sunset-picture-id1322277517?k=20&m=1322277517&s=612x612&w=0&h=ZdxT3aGDGLsOAn3mILBS6FD7ARonKRHe_EKKa-V-Hws="
                },
                new Plan
                {
                    Id = "103",
                    Title = "Why do we use it?",
                    Description = "wwewewLorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen bookewewfvdfe",
                    CoverPath = "https://images.unsplash.com/photo-1453728013993-6d66e9c9123a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Mnx8bGVuc3xlbnwwfHwwfHw%3D&w=1000&q=80"
                },
                new Plan
                {
                    Id = "104",
                    Title = "Where does it come from",
                    Description = "Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                    CoverPath = "https://www.simplilearn.com/ice9/free_resources_article_thumb/what_is_image_Processing.jpg"
                },
                new Plan
                {
                    Id = "105",
                    Title = "Where does it come from",
                    Description = "Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                    CoverPath = "https://media.istockphoto.com/photos/wild-grass-in-the-mountains-at-sunset-picture-id1322277517?k=20&m=1322277517&s=612x612&w=0&h=ZdxT3aGDGLsOAn3mILBS6FD7ARonKRHe_EKKa-V-Hws="
                },
                new Plan
                {
                    Id = "106",
                    Title = "Where does it come from",
                    Description = "Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).",
                    CoverPath = "https://miro.medium.com/max/775/0*rZecOAy_WVr16810"
                },
                new Plan
                {
                    Id = "107",
                    Title = "Last",
                    Description = "Last",
                    CoverPath = "https://miro.medium.com/max/775/0*rZecOAy_WVr16810"
                });

            modelBuilder.Entity<ToDoItem>().HasData(
                new ToDoItem { Id = "101", PlanId = "101", Description = "ToDo 1"},
                new ToDoItem { Id = "102", PlanId = "101", Description = "ToDo 2"},
                new ToDoItem { Id = "103", PlanId = "101", Description = "ToDo 3"}
                );

            modelBuilder.Entity<ToDoItem>().HasData(
                new ToDoItem { Id = "104", PlanId = "102", Description = "ToDo 4" },
                new ToDoItem { Id = "105", PlanId = "102", Description = "ToDo 5" }
                );

            modelBuilder.Entity<ToDoItem>().HasData(
                new ToDoItem { Id = "106", PlanId = "103", Description = "Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc." },
                new ToDoItem { Id = "107", PlanId = "103", Description = "The standard chunk of Lorem Ipsum used" },
                new ToDoItem { Id = "108", PlanId = "103", Description = "Cicero are also reproduced in their" }
                );

            modelBuilder.Entity<ToDoItem>().HasData(
                new ToDoItem { Id = "109", PlanId = "104", Description = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo." },
                new ToDoItem { Id = "110", PlanId = "104", Description = "Neque porro quisquam est, qui dolorem ipsum" },
                new ToDoItem { Id = "111", PlanId = "104", Description = "At vero eos et accusamus et iusto odio dignissimos" },
                new ToDoItem { Id = "112", PlanId = "104", Description = "On the other hand, we denounce with" }
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
