using BusinessLogic;
using BusinessLogic.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Unity;
using Microsoft.Practices.Unity;

namespace MvcApp
{
    public class LuceneConfig
    {
        private void UpdateLuceneIndexes()
        {            
            while (true)
            {                  
                BusinessLogicLayer bll = UnityContainerHolder.GetContainer.Resolve<BusinessLogicLayer>();
                var lucene = new LuceneSearch();
                lucene.AddIndexes(bll.Products.GetAll());
                lucene.AddIndexes(bll.Customers.GetAll());
                lucene.AddIndexes(bll.Orders.GetAll());
                lucene.AddIndexes(bll.Suppliers.GetAll());
                Thread.Sleep(100000);
            }
        }

        public static void Start()
        {
            LuceneConfig lucene = new LuceneConfig();
            Task task = new Task(new Action(lucene.UpdateLuceneIndexes));
            task.Start();
        }
    }


}
