using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities;
using System.Data;
using System.Data.Entity;
using BusinessLogic;

namespace WebFormsApp.Views.Supplier
{
    public partial class Index : System.Web.UI.Page
    {
        public ICollection<Entities.Supplier> Suppliers { get; set; }
        private BusinessLogicLayer bll;
        protected void Page_Load(object sender, EventArgs e)
        {
            bll = new BusinessLogicLayer();
            Suppliers = bll.Suppliers.GetAll();
        }
    }
}