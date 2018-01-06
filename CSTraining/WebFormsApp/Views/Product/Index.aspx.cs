using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Entity;
using Entities;
using BusinessLogic;

namespace WebFormsApp.Views.Product
{
    public partial class Index : System.Web.UI.Page
    {
        public ICollection<Entities.Product> Products { get; set; }
        private BusinessLogicLayer bll;

        protected void Page_Load(object sender, EventArgs e)
        {
            bll = new BusinessLogicLayer();
            Products = bll.Products.GetAll();
        }        
    }
}