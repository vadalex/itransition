using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models.Abstract;
using WebUI.Models.Entities;

namespace WebUI.Models.DataAccessLayer
{
    public class DataRepositories: IDisposable
    {
        private NextPartyContext context = new NextPartyContext();

        public IRepository<User> Users { get; set; }
        public IRepository<Event> Events { get; set; }
        public IRepository<Comment> Comments  { get; set; }
        public IRepository<Track> Tracks  { get; set; }
        public IRepository<Tag> Tags { get; set; }
        public IRepository<BottleShape> BottleShapes  { get; set; }
        public IRepository<Drink> Drinks { get; set; }
        public IRepository<DrinkAmount> DrinkAmounts  { get; set; }

        public DataRepositories()
        {
            this.Users = new DataRepository<User>(context);
            this.Events = new DataRepository<Event>(context);
            this.Comments = new DataRepository<Comment>(context);
            this.Tracks = new DataRepository<Track>(context);
            this.Tags = new DataRepository<Tag>(context);
            this.BottleShapes = new DataRepository<BottleShape>(context);
            this.Drinks = new DataRepository<Drink>(context);
            this.DrinkAmounts = new DataRepository<DrinkAmount>(context);            
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    } 
}