using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebUI.Models.Entities;

namespace WebUI.Models.DataAccessLayer
{
    public class NextPartyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<DrinkAmount> DrinkAmounts { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<BottleShape> BottleShapes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.OrganizedEvents)
                .WithRequired(e => e.Organizer)
                .HasForeignKey(e=>e.OrganizerID);            
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Events)
                .WithMany(e => e.Participants)                
                .Map(m =>
                {
                    m.ToTable("UserEvent");
                    m.MapLeftKey("UserID");
                    m.MapRightKey("EventID");
                });

            modelBuilder.Entity<Track>()
                .HasMany(t => t.Events)
                .WithMany(e => e.Tracks)
                .Map(m =>
                {
                    m.ToTable("TrackEvent");
                    m.MapLeftKey("TrackID");
                    m.MapRightKey("EventID");
                });

            modelBuilder.Entity<Tag>()
                .HasMany(t => t.Events)
                .WithMany(e => e.Tags)
                .Map(m =>
                {
                    m.ToTable("TagEvent");
                    m.MapLeftKey("TagID");
                    m.MapRightKey("EventID");
                });
                            
        }
    }
}