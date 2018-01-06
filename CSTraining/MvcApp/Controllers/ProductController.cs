using System.Linq;
using System.Net;
using System.Web.Mvc;
using Contracts.BLL;
using Entities;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        public ProductController(IBusinessLogicLayer bll) : base(bll) {}

        // GET: Product
        public ActionResult Index()
        {            
            var products = bll.Products.GetAll();
            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = bll.Products.GetSingle((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(bll.Suppliers.GetAll(), "SupplierID", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,Price,Category,SupplierID")] Product product)
        {
            if (ModelState.IsValid)
            {
                bll.Products.Add(product);                
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(bll.Suppliers.GetAll(), "SupplierID", "Name", product.SupplierID);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = bll.Products.GetSingle((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name", product.SupplierID);
            ProductEditModel model = new ProductEditModel();
            model.Product = product;
            model.AllOrderItems = bll.Orders.GetAll().Select(p => new SelectListItem { Text = p.Details, Value = p.OrderID.ToString() }).ToList();
            model.SelectedOrders = product.Orders.Select(p => p.OrderID).ToList();
            model.SupplierItems = bll.Suppliers.GetAll().Select(c => new SelectListItem() { Text = c.Name, Value = c.SupplierID.ToString() }).ToList();
            model.SupplierItems.First(c => c.Value == product.SupplierID.ToString()).Selected = true;
            return View(model);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductEditModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (int orderId in model.SelectedOrders)
                {
                    var order = bll.Orders.GetAll().First(p => p.OrderID == orderId);
                    model.Product.Orders.Add(order);
                }
                bll.Products.Update(model.Product);
                return RedirectToAction("Index");
            }
            model.AllOrderItems = bll.Orders.GetAll().Select(p => new SelectListItem { Text = p.Details, Value = p.OrderID.ToString() }).ToList();            
            model.SupplierItems = bll.Suppliers.GetAll().Select(c => new SelectListItem() { Text = c.Name, Value = c.SupplierID.ToString() }).ToList();
            model.SupplierItems.First(c => c.Value == model.Product.SupplierID.ToString()).Selected = true;
            return View(model);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = bll.Products.GetSingle((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = bll.Products.GetSingle(id);
            bll.Products.Remove(product);            
            return RedirectToAction("Index");
        }
    }
}
