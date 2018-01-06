using BusinessLogic;
using Contracts.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class BaseController : Controller
    {
        protected IBusinessLogicLayer bll;

        public BaseController(IBusinessLogicLayer bll)
        {
            this.bll = bll;
        }
        
    }
}