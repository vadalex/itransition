using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using System.Data;
using System.Data.Entity;

namespace WebFormsApp.Views.Order
{
    public partial class Index : System.Web.UI.Page
    {
        public ICollection<Entities.Order> Orders { get; set; }
        private BusinessLogicLayer bll;
        protected void Page_Load(object sender, EventArgs e)
        {
            bll = new BusinessLogicLayer();
            Orders = bll.Orders.GetAll();
        }
    }
}