using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using System.Data.Entity;

namespace WebFormsApp.Views.Product
{
    public partial class Edit : System.Web.UI.Page
    {
        public Entities.Product Item { get; set; }
        private BusinessLogicLayer bll;

        protected void Page_Load(object sender, EventArgs e)
        {            
                int productId = Int32.Parse(Request.QueryString["productId"]);
                bll = new BusinessLogicLayer();
                Item = bll.Products.GetSingle(productId);
                if (!IsPostBack)
                {                    
                    ProductName.Text = Item.Name;
                    ProductPrice.Text = Item.Price.ToString();
                    ProductCategory.Text = Item.Category;
                    suppliers.DataSource = bll.Suppliers.GetAll();
                    suppliers.DataValueField = "SupplierID";
                    suppliers.DataTextField = "Name";
                    suppliers.DataBind();
                    suppliers.Items.FindByValue(Item.SupplierID.ToString()).Selected = true;
                }
        }
        
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Item.Name = ProductName.Text;
            Item.Price = double.Parse(ProductPrice.Text);
            Item.Category = ProductCategory.Text;
            Item.SupplierID = int.Parse(suppliers.SelectedValue);
            bll.Products.Update(Item);            
            Server.Transfer("~/Views/Product/Index.aspx", false);
        }           

    }
}