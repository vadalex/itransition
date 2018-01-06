using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsApp.Views.Product
{
    public partial class Delete : System.Web.UI.Page
    {
        public Entities.Product Item { get; set; }
        private BusinessLogicLayer bll;

        protected void Page_Load(object sender, EventArgs e)
        {
            int productId = Int32.Parse(Request.QueryString["productId"]);
            bll = new BusinessLogicLayer();
            Item = bll.Products.GetSingle(productId);

        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {            
            bll.Products.Remove(Item);                        
            Server.Transfer("~/Views/Product/Index.aspx", false);
        }         
    }
}