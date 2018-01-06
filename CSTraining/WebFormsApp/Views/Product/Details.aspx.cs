using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;

namespace WebFormsApp.Views.Product
{
    public partial class Details : System.Web.UI.Page
    {
        public Entities.Product Item { get; set; }
        private BusinessLogicLayer bll;
        protected void Page_Load(object sender, EventArgs e)
        {
            int productId = Int32.Parse(Request.QueryString["productId"]);
            bll = new BusinessLogicLayer();
            Item = bll.Products.GetSingle(productId);
        }       
    }
}