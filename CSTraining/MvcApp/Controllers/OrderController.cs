using System.Linq;
using System.Net;
using System.Web.Mvc;
using Entities;
using Contracts.BLL;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {

        public OrderController(IBusinessLogicLayer bll) : base(bll) { }        
        
        // GET: Order
        public ActionResult Index()
        {            
            var orders = bll.Orders.GetAll();
            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = bll.Orders.GetSingle((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ; return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(bll.Customers.GetAll(), "CustomerID", "Name");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,Status,CreatedDate,Details,CustomerID")] Order order)
        {
            if (ModelState.IsValid)
            {
                bll.Orders.Add(order);                
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(bll.Customers.GetAll(), "CustomerID", "Name", order.CustomerID);
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = bll.Orders.GetSingle((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", order.CustomerID);            
            OrderEditModel model = new OrderEditModel();
            model.Order = order;
            model.AllProductItems = bll.Products.GetAll().Select(p => new SelectListItem { Text = p.Name, Value = p.ProductID.ToString() }).ToList();
            model.SelectedProducts = order.Products.Select(p => p.ProductID).ToList();
            model.CustomerItems = bll.Customers.GetAll().Select(c => new SelectListItem() { Text = c.Name, Value = c.CustomerID.ToString() }).ToList();
            model.CustomerItems.First(c => c.Value == order.CustomerID.ToString()).Selected = true;
            return View(model);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderEditModel model)
        {            
            if (ModelState.IsValid)
            {
                foreach(int productId in model.SelectedProducts)
                {
                    var product = bll.Products.GetAll().First(p => p.ProductID == productId);
                    //db.Products.Attach(product);
                    model.Order.Products.Add(product);
                    //db.Entry<Product>(product).State = EntityState.Unchanged;
                }
                bll.Orders.Update(model.Order);                
                return RedirectToAction("Index");
            }

            model.AllProductItems = bll.Products.GetAll().Select(p => new SelectListItem { Text = p.Name, Value = p.ProductID.ToString() }).ToList();            
            model.CustomerItems = bll.Customers.GetAll().Select(c => new SelectListItem() { Text = c.Name, Value = c.CustomerID.ToString() }).ToList();
            model.CustomerItems.First(c => c.Value == model.Order.CustomerID.ToString()).Selected = true;

            return View(model);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = bll.Orders.GetSingle((int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = bll.Orders.GetSingle(id);
            bll.Orders.Remove(order);            
            return RedirectToAction("Index");
        }
    }
}
