using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace DataAccessLayer
{
    public class FlashCardsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserStatistic> UserStatistics { get; set; }        
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(p => p.Decks).WithRequired(p => p.User);
            modelBuilder.Entity<User>().HasMany(p => p.UserStatistics).WithRequired(p => p.User);            
            modelBuilder.Entity<Deck>().HasMany(p => p.Cards).WithRequired(p => p.Deck);
            modelBuilder.Entity<UserStatistic>()
                .HasMany(p => p.UnknownCards)
                .WithMany(p => p.UserStatistics)
                .Map(mc =>
            {
                mc.ToTable("UserStatisticsCards");
                mc.MapLeftKey("UserStatisticID");
                mc.MapRightKey("CardID");
            });
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
