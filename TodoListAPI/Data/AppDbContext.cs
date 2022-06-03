using Microsoft.EntityFrameworkCore;

namespace TodoListAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem() { Id = 1, IsDone = true, Title = "Do your homework" },
                new TodoItem() { Id = 2, IsDone = true, Title = "Fitness" },
                new TodoItem() { Id = 3, IsDone = true, Title = "Call your parents" },
                new TodoItem() { Id = 4, IsDone = true, Title = "Clean your room" }
                );

        }
    }
}
