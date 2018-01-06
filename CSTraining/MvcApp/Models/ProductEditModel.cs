using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Web.Mvc;


namespace MvcApp.Models
{
    public class ProductEditModel
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> AllOrderItems { get; set; }
        public IEnumerable<int> SelectedOrders { get; set; }
        public IEnumerable<SelectListItem> SupplierItems { get; set; }
    }
}
