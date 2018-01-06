using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CSTraining.WebUI.Models;

namespace WebUI.Controllers
{
    public class TodoController : ApiController
    {
        private CSTrainingContext db = new CSTrainingContext();
                
        public IQueryable<TodoItem> GetTodoItems()
        {
            return db.TodoItems;
        }
        
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult GetTodoItem(Guid id)
        {
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutTodoItem(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            db.Entry(todoItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(todoItem.TodoItemId))
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

        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult PostTodoItem(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxOrder = db.TodoItems.Select(t => t.Order).Max();
            todoItem.Order = ++maxOrder;
            todoItem.TodoItemId = Guid.NewGuid();
            db.TodoItems.Add(todoItem);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TodoItemExists(todoItem.TodoItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = todoItem.TodoItemId }, todoItem);
        }

        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult DeleteTodoItem(Guid id)
        {
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            db.TodoItems.Remove(todoItem);
            db.SaveChanges();

            return Ok(todoItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TodoItemExists(Guid id)
        {
            return db.TodoItems.Count(e => e.TodoItemId == id) > 0;
        }
    }
}