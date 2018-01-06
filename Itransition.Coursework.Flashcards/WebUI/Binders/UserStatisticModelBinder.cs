using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;

namespace WebUI.Binders
{
    public class UserStatisticModelBinder : System.Web.Mvc.IModelBinder
    {
        private const string sessionKey = "statistic";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            UserStatistic statistic = (UserStatistic)controllerContext.HttpContext.Session[sessionKey];
            if (statistic == null)
            {
                statistic = new UserStatistic();
                controllerContext.HttpContext.Session[sessionKey] = statistic;
            }
            return statistic;
        }
    }
}