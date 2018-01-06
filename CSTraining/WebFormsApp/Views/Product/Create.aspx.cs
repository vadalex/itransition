using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebFormsApp.Views.Product
{
    public partial class Create : System.Web.UI.Page
    {
        private BusinessLogicLayer bll;        

        protected void Page_Load(object sender, EventArgs e)
        {
            bll = new BusinessLogicLayer();
            if (!IsPostBack)
            {
                suppliers.DataSource = bll.Suppliers.GetAll();
                suppliers.DataValueField = "SupplierID";
                suppliers.DataTextField = "Name";
                suppliers.DataBind();
            }
        }

        protected void CreateButton_Click(object sender, EventArgs e)
        {
            Entities.Product Item = new Entities.Product();
            Item.Name = ProductName.Text;
            Item.Price = double.Parse(ProductPrice.Text);
            Item.Category = ProductCategory.Text;
            Item.SupplierID = int.Parse(suppliers.SelectedValue);
            bll.Products.Add(Item);            
            Server.Transfer("~/Views/Product/Index.aspx", false);
        }           
    }
}