using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using WebUI.Models.DataAccessLayer;
using WebUI.Models.Entities;

namespace WebUI.Models
{
    public class TestData
    {
        public void FillBottleShapes()
        {
            using (var context = new NextPartyContext())
            {
                var bs = new List<BottleShape>() {
                    new BottleShape() { Title="Shape1"},
                    new BottleShape() { Title="Shape2"},
                    new BottleShape() { Title="Shape3"},
                    new BottleShape() { Title="Shape4"},
                    new BottleShape() { Title="Shape5"}
                };
                bs.ForEach(d => context.BottleShapes.Add(d));
                context.SaveChanges();
            }
        }

        public void FillDataBase()
        { 
            using (var context = new NextPartyContext())
            {                
                var drinks = new List<Drink>() {
                new Drink() { Title = "Beer", AlcoholByVolume = 6.5, Volume = 500, Color = Color.Black.ToArgb(), BottleShape = context.BottleShapes.First() },
                new Drink() { Title = "Wine", AlcoholByVolume = 12.5, Volume = 700, Color = Color.Green.ToArgb(), BottleShape = context.BottleShapes.OrderBy(e=>e.BottleShapeID).Skip(1).First() },
                new Drink() { Title = "Vodka", AlcoholByVolume = 40, Volume = 1000, Color = Color.White.ToArgb(), BottleShape = context.BottleShapes.OrderBy(e=>e.BottleShapeID).Skip(2).First() },
                new Drink() { Title = "Whisky", AlcoholByVolume = 32, Volume = 500, Color = Color.Magenta.ToArgb(), BottleShape = context.BottleShapes.OrderBy(e=>e.BottleShapeID).Skip(3).First() },
                new Drink() { Title = "Сognac", AlcoholByVolume = 40, Volume = 700, Color = Color.Red.ToArgb(), BottleShape = context.BottleShapes.OrderBy(e=>e.BottleShapeID).Skip(4).First() },
                };
                drinks.ForEach(c=>context.Drinks.Add(c));                                
                context.SaveChanges();

                var events = new List<Event>() {
                new Event() { Title = "Cool party", DatetimeOfStart = new DateTime(2014, 3, 28, 18, 30, 0), Duration = new TimeSpan(4, 30, 0), Latitude = 53.925414639216974, Longitude = 27.59694897514646, Address="R-Club, Minsk", Organizer = context.Users.OrderBy(u=>u.UserID).Skip(1).First(), Participants = new List<User>(){context.Users.First(), context.Users.OrderBy(u=>u.UserID).Skip(1).First(), context.Users.OrderBy(u=>u.UserID).Skip(3).First()}  },
                new Event() { Title = "Trash", DatetimeOfStart = new DateTime(2014, 3, 25, 14, 00, 0), Duration = new TimeSpan(5, 00, 0), Latitude =53.91878700811664, Longitude = 27.591412895739722, Address="CoffeeBox, Minsk", Organizer = context.Users.OrderBy(u=>u.UserID).Skip(2).First(), Participants = new List<User>(){context.Users.First(), context.Users.OrderBy(u=>u.UserID).Skip(2).First(), context.Users.OrderBy(u=>u.UserID).Skip(3).First()}  },
                new Event() { Title = "Bananas", DatetimeOfStart = new DateTime(2014, 4, 5, 15, 20, 0), Duration = new TimeSpan(2, 00, 0), Latitude = 53.917033574137065, Longitude = 27.58523845058744, Address="Lido, Minsk", Organizer = context.Users.OrderBy(u=>u.UserID).Skip(2).First(), Participants = new List<User>(){context.Users.First(), context.Users.OrderBy(u=>u.UserID).Skip(2).First(), context.Users.OrderBy(u=>u.UserID).Skip(4).First(), context.Users.OrderBy(u=>u.UserID).Skip(3).First()}  },
                new Event() { Title = "Big bang", DatetimeOfStart = new DateTime(2014, 4, 10, 21, 40, 0), Duration = new TimeSpan(7, 30, 0), Latitude = 53.91874909680908, Longitude = 27.577647799075294, Address="Max club, Minsk", Organizer = context.Users.OrderBy(u=>u.UserID).Skip(1).First(), Participants = new List<User>(){context.Users.First(), context.Users.OrderBy(u=>u.UserID).Skip(1).First(), context.Users.OrderBy(u=>u.UserID).Skip(3).First(), context.Users.OrderBy(u=>u.UserID).Skip(2).First()}  },
                };
                events.ForEach(c => context.Events.Add(c));
                context.SaveChanges();

                var tags = new List<Tag>() {
                new Tag() { Value = "center", Events = new List<Event>() {context.Events.First(), context.Events.OrderBy(e=>e.EventID).Skip(1).First(), context.Events.OrderBy(e=>e.EventID).Skip(4).First()} },
                new Tag() { Value = "cool", Events = new List<Event>() {context.Events.First(), context.Events.OrderBy(e=>e.EventID).Skip(2).First(), context.Events.OrderBy(e=>e.EventID).Skip(4).First(), context.Events.OrderBy(e=>e.EventID).Skip(3).First()} },
                new Tag() { Value = "trash", Events = new List<Event>() {context.Events.OrderBy(e=>e.EventID).Skip(1).First(), context.Events.OrderBy(e=>e.EventID).Skip(2).First(), context.Events.OrderBy(e=>e.EventID).Skip(3).First()} },
                };
                tags.ForEach(c => context.Tags.Add(c));
                context.SaveChanges();

                var comments = new List<Comment>() {
                new Comment() { MarkdownText = "**cool**", Event = context.Events.First(), User = context.Users.First() },
                new Comment() { MarkdownText = "*blablabla*", Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**blablabla**", Event = context.Events.OrderBy(e=>e.EventID).Skip(2).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**blablabla**", Event = context.Events.OrderBy(e=>e.EventID).Skip(3).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**blablabla**", Event = context.Events.OrderBy(e=>e.EventID).Skip(4).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**cool**", Event = context.Events.First(), User = context.Users.First() },
                new Comment() { MarkdownText = "*blablabla*", Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**blablabla**", Event = context.Events.OrderBy(e=>e.EventID).Skip(2).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**blablabla**", Event = context.Events.OrderBy(e=>e.EventID).Skip(3).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**blablabla**", Event = context.Events.OrderBy(e=>e.EventID).Skip(4).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**cool**", Event = context.Events.First(), User = context.Users.First() },
                new Comment() { MarkdownText = "*blablabla*", Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**blablabla**", Event = context.Events.OrderBy(e=>e.EventID).Skip(2).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**blablabla**", Event = context.Events.OrderBy(e=>e.EventID).Skip(3).First(), User = context.Users.First() },
                new Comment() { MarkdownText = "**blablabla**", Event = context.Events.OrderBy(e=>e.EventID).Skip(4).First(), User = context.Users.First() },
                };
                comments.ForEach(c => context.Comments.Add(c));
                context.SaveChanges();
                /*
                var tracks = new List<Track>() {
                new Track() { Title="fff", Album="ff" , Artist="ff",  Events = new List<Event>() {context.Events.First()} },
                new Track() { Title="fff", Album="ff" , Artist="ff",  Events = new List<Event>() {context.Events.First()} },
                };
                tracks.ForEach(c => context.Tracks.Add(c));
                context.SaveChanges();
                */
                var amounts = new List<DrinkAmount>() {
                new DrinkAmount() { BottleAmount = 2.5, Drink = context.Drinks.First() , Event = context.Events.First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 1, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(1).First() , Event = context.Events.First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 2.0, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(2).First() , Event = context.Events.First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 3.0, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(3).First() , Event = context.Events.First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 4.5, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(4).First() , Event = context.Events.First(), User = context.Users.First() },

                new DrinkAmount() { BottleAmount = 2.5, Drink = context.Drinks.First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 1, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(1).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 2.0, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(2).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 3.0, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(3).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 4.5, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(4).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },

                new DrinkAmount() { BottleAmount = 2.5, Drink = context.Drinks.First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 1, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(1).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(2).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 2.0, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(2).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(2).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 3.0, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(3).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(2).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 4.5, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(4).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(2).First(), User = context.Users.First() },

                new DrinkAmount() { BottleAmount = 2.5, Drink = context.Drinks.First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 1, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(1).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(3).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 2.0, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(2).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(3).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 3.0, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(3).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(3).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 4.5, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(4).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(3).First(), User = context.Users.First() },

                new DrinkAmount() { BottleAmount = 2.5, Drink = context.Drinks.First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(1).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 1, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(1).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(4).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 2.0, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(2).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(4).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 3.0, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(3).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(4).First(), User = context.Users.First() },
                new DrinkAmount() { BottleAmount = 4.5, Drink = context.Drinks.OrderBy(e=>e.DrinkID).Skip(4).First() , Event = context.Events.OrderBy(e=>e.EventID).Skip(4).First(), User = context.Users.First() },


                };
                amounts.ForEach(c => context.DrinkAmounts.Add(c));
                context.SaveChanges();
            }             

        }
    }
}