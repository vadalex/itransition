using System.Data.Entity;

namespace CSTraining.WebUI.Models
{ 
    public class CSTrainingContext : DbContext
    {        
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
    }
}
