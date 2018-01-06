using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcApp.PaypalAPI;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Web;
using BusinessLogic;
using DAL;
using Entities;
using Unity;
using WebMatrix.WebData;
using Microsoft.Practices.Unity;

namespace MvcApp.Helpers.PayPalHelper
{
    public class PayPalHelper
    {
        public static CustomSecurityHeaderType GetPayPalCredentials()
        {
            UserIdPasswordType credentials = new UserIdPasswordType()
            {
                Username = ConfigurationManager.AppSettings["PayPalAPIUsername"],
                Password = ConfigurationManager.AppSettings["PayPalAPIPassword"],
                Signature = ConfigurationManager.AppSettings["PayPalAPISignature"],
            };

            CustomSecurityHeaderType header = new CustomSecurityHeaderType()
            {
                Credentials = credentials
            };
            return header;
        }

        public static string GetDomainName(HttpRequestBase request)
        {
            return request.Url.Scheme + System.Uri.SchemeDelimiter + request.Url.Host + (request.Url.IsDefaultPort ? "" : ":" + request.Url.Port);
        }

        public static bool IsSandbox()
        {
            string res = ConfigurationManager.AppSettings["IsSandbox"];   
            if(res.ToLower() == "true")
                return true;
            return false;
        }

        public static string GetPaypalRequestUrl()
        {
            string url = ConfigurationManager.AppSettings["PayPalRequestLiveUrl"];   
            if(IsSandbox())
                url = ConfigurationManager.AppSettings["PayPalRequestSandboxUrl"];   
            return url;
        }

        public static Order CreateOrder(int productId)
        {
            //BusinessLogicLayer bll = UnityContainerHolder.GetContainer.Resolve<BusinessLogicLayer>();
            MyContext context = new MyContext();
            var product = context.Products.Where(p => p.ProductID == productId).FirstOrDefault(); //bll.Products.GetSingle(productId);
            var customer = context.Customers.Where(c => c.UserID == WebSecurity.CurrentUserId).FirstOrDefault(); //bll.Customers.GetAll(c => c.CustomerID == WebSecurity.CurrentUserId).FirstOrDefault();
            if (customer == null || product == null)
                return null;
            Order order = new Order()
            {
                CustomerID = customer.CustomerID,
                IsPaid = false,
                Status = "before authent.",
                Amount = product.Price,
                PayerId = "null",
                Details = "null",
                CreatedDate = DateTime.Now
            };
            
            order.Products.Add(product);
            //bll.Orders.Add(order);
            context.Orders.Add(order);
            context.SaveChanges();
       
            return order;
        }

        public static Order UpdateOrderAfterAuthentication(string orderId, string payerId)
        {
            BusinessLogicLayer bll = UnityContainerHolder.GetContainer.Resolve<BusinessLogicLayer>();
            int id = int.Parse(orderId);
            Order order = bll.Orders.GetSingle(id);
            order.PayerId = payerId;
            order.Status = "after authent.";
            bll.Orders.Update(order);
            return order;
        }

        public static void UpdateOrderAfterConfirmation(int orderId)
        {
            BusinessLogicLayer bll = UnityContainerHolder.GetContainer.Resolve<BusinessLogicLayer>();
            Order order = bll.Orders.GetSingle(orderId);
            order.IsPaid = true;
            order.CreatedDate = DateTime.Now;
            order.Status = "after confirmation";
            bll.Orders.Update(order);
        }

        public static SetExpressCheckoutReq GetSetExpressCheckoutReq(Order order, string returnUrl, string cancelUrl)
        {
            SetExpressCheckoutRequestDetailsType requestDetails = new SetExpressCheckoutRequestDetailsType
            {
                ReturnURL = returnUrl,
                CancelURL = cancelUrl,
                NoShipping = "1",
                Custom = order.OrderID.ToString(),
                PaymentAction = PaymentActionCodeType.Sale,
                OrderTotal = new BasicAmountType()
                {
                    currencyID = CurrencyCodeType.USD,
                    Value = order.Amount.ToString(CultureInfo.InvariantCulture)
                }
            };

            SetExpressCheckoutReq request = new SetExpressCheckoutReq()
            {
                SetExpressCheckoutRequest = new SetExpressCheckoutRequestType()
                {
                    Version = ConfigurationManager.AppSettings["PayPalAPIVersion"],
                    SetExpressCheckoutRequestDetails = requestDetails
                }
            };
            return request;
        }

        public static GetExpressCheckoutDetailsReq GetGetExpressCheckoutDetailsReq(string token)
        {
            GetExpressCheckoutDetailsReq req = new GetExpressCheckoutDetailsReq()
            {
                GetExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType()
                {
                    Version = ConfigurationManager.AppSettings["PayPalAPIVersion"],
                    Token = token
                }
            };
            return req;
        }

        public static DoExpressCheckoutPaymentReq GetDoExpressCheckoutPaymentReq(GetExpressCheckoutDetailsResponseType response)
        {
            DoExpressCheckoutPaymentReq payReq = new DoExpressCheckoutPaymentReq()
            {
                DoExpressCheckoutPaymentRequest = new DoExpressCheckoutPaymentRequestType()
                {
                    Version = ConfigurationManager.AppSettings["PayPalAPIVersion"],
                    DoExpressCheckoutPaymentRequestDetails = new DoExpressCheckoutPaymentRequestDetailsType()
                    {
                        Token = response.GetExpressCheckoutDetailsResponseDetails.Token,
                        PaymentAction = PaymentActionCodeType.Sale,
                        PayerID = response.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID,
                        PaymentDetails = response.GetExpressCheckoutDetailsResponseDetails.PaymentDetails
                    }
                }
            };
            return payReq;
        }
    }
}
