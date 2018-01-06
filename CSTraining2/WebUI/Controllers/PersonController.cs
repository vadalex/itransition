﻿using System;
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
    public class PersonController : ApiController
    {
        private CSTrainingContext db = new CSTrainingContext();

        // GET: api/Person
        public IQueryable<Person> GetPersons()
        {
            return db.People;
        }

        // GET: api/Person/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult GetPerson(Guid id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/Person/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }           

            db.Entry(person).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(person.PersonId))
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

        // POST: api/Person
        [ResponseType(typeof(Person))]
        public IHttpActionResult PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PersonExists(person.PersonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = person.PersonId }, person);
        }

        // DELETE: api/Person/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(Guid id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            db.SaveChanges();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(Guid id)
        {
            return db.People.Count(e => e.PersonId == id) > 0;
        }
    }
}