using Microsoft.EntityFrameworkCore;

namespace TestJob.Models
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }


        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }

    }
}
