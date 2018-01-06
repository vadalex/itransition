using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DAL;
using Entities;
using Contracts.DAL;

namespace WebAPIService.Controllers
{

    public class ProductsController : ApiController
    {
        private IRepository<Product> repository;
        private IRepository<Supplier> suppliers;

        public ProductsController()
        {
            var context = new MyContext();
            context.Configuration.ProxyCreationEnabled = false;
            repository = new DataRepository<Product>(context);
            suppliers = new DataRepository<Supplier>(context);
        }
        
        public IEnumerable<Product> Get()
        {
            var products = repository.GetAll().ToList();
            return products;
        }
        
        public IHttpActionResult Get(int id)
        {
            Product product = repository.GetSingle(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        
        public IHttpActionResult Post([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Add(product);
                return Ok();
            }
            return BadRequest(ModelState);
        }
        
        public IHttpActionResult Put([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                repository.Update(product);
                return Ok();
            }
            return BadRequest(ModelState);
        }

       public IHttpActionResult Delete(int id)
        {
            Product product = repository.GetSingle(id);
            if (product == null)
                return NotFound();
            repository.Remove(product);
            return Ok(product);
        }
        public IHttpActionResult PostSupplier([FromBody]Supplier supplier)
       {
           if (ModelState.IsValid)
           {
               suppliers.Add(supplier);
               return Ok();
           }
           return BadRequest(ModelState);
       }
    }
}