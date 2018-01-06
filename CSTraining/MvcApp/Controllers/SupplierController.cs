using System.Net;
using System.Web.Mvc;
using Contracts.BLL;
using Entities;

namespace MvcApp.Controllers
{
    
    public class SupplierController : BaseController    
    {
                
        public SupplierController(IBusinessLogicLayer bll) : base(bll) {}

        // GET: Supplier
        public ActionResult Index()//BusinessLogicLayer bll)
        {            
            return View(bll.Suppliers.GetAll());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = bll.Suppliers.GetSingle((int)id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierID,Name,Phone,Details")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                bll.Suppliers.Add(supplier);
                return RedirectToAction("Index");
            }

            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = bll.Suppliers.GetSingle((int)id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierID,Name,Phone,Details")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                bll.Suppliers.Update(supplier);
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = bll.Suppliers.GetSingle((int)id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = bll.Suppliers.GetSingle(id);
            bll.Suppliers.Remove(supplier);            
            return RedirectToAction("Index");
        }
    }
}
