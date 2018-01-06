using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CSTraining.WebUI.Models;

namespace CSTraining.WebUI.Controllers
{
    public class AnimalController : ApiController
    {
        private CSTrainingContext db = new CSTrainingContext();
                
        public IQueryable<Animal> GetAnimals()
        {
            return db.Animals;
        }

        
        public HttpResponseMessage GetPieChartData()
        {          
            var data = db.Animals.GroupBy(a => a.Class, a => a.AnimalId, (key, g) => new { Class = key, Count = g.Count() }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        public HttpResponseMessage GetLineChartData()
        {
            var animals = db.Animals.Where(a => (a.Class == "Mammal" || a.Class == "Bird" || a.Class == "Fish")).ToList();
            var startYear = animals.OrderBy(a => a.BirthDate.Year).First().BirthDate.Year;
            var endYear = animals.OrderBy(a => a.BirthDate.Year).Last().BirthDate.Year;
            var series = new string[] { "Mammal", "Bird", "Fish" };
            var labels = new List<int>();
            var mammalData = new List<int>();
            var birdlData = new List<int>();
            var fishData = new List<int>();
            for (int i = startYear; i <= endYear; i++)
            {
                var temp = animals.Where(a => a.BirthDate.Year == i);
                mammalData.Add(temp.Where(a => a.Class == "Mammal").Count());
                birdlData.Add(temp.Where(a => a.Class == "Bird").Count());
                fishData.Add(temp.Where(a => a.Class == "Fish").Count());
                labels.Add(i);
            }
            var data = new { Series = series, Labels = labels, Mammal = mammalData, Bird = birdlData, Fish = fishData };
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [ResponseType(typeof(Animal))]
        public IHttpActionResult GetAnimal(Guid id)
        {
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }
          
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAnimal(Guid id, Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != animal.AnimalId)
            {
                return BadRequest();
            }

            db.Entry(animal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [ResponseType(typeof(Animal))]
        public IHttpActionResult PostAnimal(Animal animal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Animals.Add(animal);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AnimalExists(animal.AnimalId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = animal.AnimalId }, animal);
        }
        
        [ResponseType(typeof(Animal))]
        public IHttpActionResult DeleteAnimal(Guid id)
        {
            Animal animal = db.Animals.Find(id);
            if (animal == null)
            {
                return NotFound();
            }

            db.Animals.Remove(animal);
            db.SaveChanges();

            return Ok(animal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AnimalExists(Guid id)
        {
            return db.Animals.Count(e => e.AnimalId == id) > 0;
        }
    }
}