using CSTraining.Entity;
using System.Data.Entity;

namespace CSTraining.DAL
{
    public class CSTrainingContext : DbContext
    {
        public DbSet<Person> People { get; set; }
    }
}
