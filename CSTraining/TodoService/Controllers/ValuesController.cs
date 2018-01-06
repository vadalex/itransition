using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Contracts.BLL;
using Entities;

namespace TodoService.Controllers
{
    public class ValuesController : ApiController
    {
        private IBusinessLogic<TodoItem> todoBL;

        public ValuesController()
        {
            this.todoBL = DependencyResolver.Current.GetService<IBusinessLogic<TodoItem>>();
        }

        // GET api/values
        public IEnumerable<TodoItem> Get()
        {
            var items = todoBL.GetAll();
            return items;
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            TodoItem item = todoBL.GetSingle(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]TodoItem value)
        {
            if (ModelState.IsValid)
            {
                var maxOrder = todoBL.GetAll().Select(t => t.Order).Max();
                value.Order = ++maxOrder;
                todoBL.Add(value);
                var newVal = todoBL.GetAll().LastOrDefault();
                return Ok(newVal);
            }
            return BadRequest(ModelState);
        }

        // PUT api/values/5
        public IHttpActionResult Put([FromBody]TodoItem value)
        {
            if (ModelState.IsValid)
            {
                todoBL.Update(value);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(int id)
        {
            TodoItem item = todoBL.GetSingle(id);
            if (item == null)
                return NotFound();
            todoBL.Remove(item);
            return Ok(item);
        }
    }
}