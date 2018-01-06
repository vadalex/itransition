using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Web.Mvc;

namespace MvcApp.Models
{
    public class OrderEditModel
    {
        public Order Order { get; set; }
        public IEnumerable<SelectListItem> AllProductItems { get; set; }
        public IEnumerable<int> SelectedProducts { get; set; }
        public IEnumerable<SelectListItem> CustomerItems { get; set; }
    }
}
