using System.Data.Entity;
using Entities;

namespace DAL
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("MyContext") { }
        
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
    }
}
