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
    public class CreditCardController : ApiController
    {
        private CSTrainingContext db = new CSTrainingContext();

        // GET: api/CreditCard
        public IQueryable<CreditCard> GetCreditCards()
        {
            return db.CreditCards;
        }

        // GET: api/CreditCard/5
        [ResponseType(typeof(CreditCard))]
        public IHttpActionResult GetCreditCard(Guid id)
        {
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return NotFound();
            }

            return Ok(creditCard);
        }

        // PUT: api/CreditCard/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCreditCard(CreditCard creditCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            db.Entry(creditCard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCardExists(creditCard.CreditCardId))
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

        // POST: api/CreditCard
        [ResponseType(typeof(CreditCard))]
        public IHttpActionResult PostCreditCard(CreditCard creditCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CreditCards.Add(creditCard);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CreditCardExists(creditCard.CreditCardId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = creditCard.CreditCardId }, creditCard);
        }

        // DELETE: api/CreditCard/5
        [ResponseType(typeof(CreditCard))]
        public IHttpActionResult DeleteCreditCard(Guid id)
        {
            CreditCard creditCard = db.CreditCards.Find(id);
            if (creditCard == null)
            {
                return NotFound();
            }

            db.CreditCards.Remove(creditCard);
            db.SaveChanges();

            return Ok(creditCard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CreditCardExists(Guid id)
        {
            return db.CreditCards.Count(e => e.CreditCardId == id) > 0;
        }
    }
}