using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using Contracts.BLL;
using Entities;
using Microsoft.Ajax.Utilities;
using MvcApp.Models;
using MvcApp.PaypalAPI;
using MvcApp.Helpers.PayPalHelper;


namespace MvcApp.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
        public PaymentController(IBusinessLogicLayer bll) : base(bll) { }
        
        [HttpGet]
        public ActionResult Donate()
        {
            var model = new PaymentModel();
            //model.CallbackUrl = PayPalHelper.GetDomainName(Request) + "/Payment/PayPalCallback";
            model.ReturnSuccessUrl = PayPalHelper.GetDomainName(Request) + "/Payment/PayPalSuccess";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Donate(int? amount)
        {
            if(!amount.HasValue)
                return RedirectToAction("PayPalCancel");
            string returnSuccessUrl = PayPalHelper.GetDomainName(Request) + "/Payment/PayPalSuccess";
            string redirectUrl = string.Format("{0}?cmd=_donations&business={1}&item_name={2}&amount={3}&return={4}", PayPalHelper.GetPaypalRequestUrl(), ConfigurationManager.AppSettings["PayPalAccount"], "Save red crayfishes in Belarus", amount.Value, returnSuccessUrl);
            return Redirect(redirectUrl);
        }

        public ActionResult PayPalSuccess()
        {
            return View();
        }

        public ActionResult PayPalCancel()
        {
            return View();
        }

        #region ipn
        public ActionResult PayPalCallback()
        {
            var formVals = new Dictionary<string, string> {{"cmd", "_notify-validate"}};
            string response = GetPayPalResponse(formVals, true);
            if (response.Contains("VERIFIED"))
            {
                string transactionID = Request["txn_id"];
                string sAmountPaid = Request["mc_gross"];
                string deviceID = Request["custom"];

                //validate the order
                Decimal amountPaid = 0;
                Decimal.TryParse(sAmountPaid, out amountPaid);
                var order = new Order();
                order.Status = "from paypal";
                bll.Orders.Add(order);
                
            }
            return View();
        }

        private string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {
            string paypalUrl = useSandbox
                ? "https://www.sandbox.paypal.com/cgi-bin/webscr"
                : "https://www.paypal.com/cgi-bin/webscr";

            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(paypalUrl);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (string key in formVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, formVals[key]);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {
                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }
            return response;
        }
        #endregion
        
        #region ExpressCheckout

        public ActionResult BuyProduct(int? productId)
        {
            if(!productId.HasValue)
                return RedirectToAction("PayPalCancel");
            Order order = PayPalHelper.CreateOrder(productId.Value);
            if(order == null)
                return RedirectToAction("PayPalCancel");
            string domain = PayPalHelper.GetDomainName(Request);
            var returnURL = domain + "/Payment/PaymentDetails";
            var cancelURL = domain + "/Payment/PaypalCancel";
            SetExpressCheckoutReq request = PayPalHelper.GetSetExpressCheckoutReq(order, returnURL, cancelURL);
            CustomSecurityHeaderType credentials = PayPalHelper.GetPayPalCredentials();
            PayPalAPIAAInterfaceClient client = new PayPalAPIAAInterfaceClient();
            SetExpressCheckoutResponseType response = client.SetExpressCheckout(ref credentials, request);
            if (response.Errors != null && response.Errors.Length > 0)
                throw new Exception("Exception occured when calling PayPal: " + response.Errors[0].LongMessage);
            string redirectUrl = string.Format("{0}?cmd=_express-checkout&token={1}", PayPalHelper.GetPaypalRequestUrl(), response.Token);
            return Redirect(redirectUrl);
        }

        public ActionResult PaymentDetails(string token)
        {
            if(token.IsNullOrWhiteSpace())
                return RedirectToAction("PayPalCancel");
            GetExpressCheckoutDetailsReq req = PayPalHelper.GetGetExpressCheckoutDetailsReq(token);
            CustomSecurityHeaderType credentials = PayPalHelper.GetPayPalCredentials();
            PayPalAPIAAInterfaceClient client = new PayPalAPIAAInterfaceClient();
            GetExpressCheckoutDetailsResponseType response = client.GetExpressCheckoutDetails(ref credentials, req);
            if (response.Errors != null && response.Errors.Length > 0)
                throw new Exception("Exception occured when calling PayPal: " + response.Errors[0].LongMessage);
            GetExpressCheckoutDetailsResponseDetailsType respDetails = response.GetExpressCheckoutDetailsResponseDetails;
            Order order = PayPalHelper.UpdateOrderAfterAuthentication(respDetails.Custom, respDetails.PayerInfo.PayerID);
            var model = new PaymentModel
            {
                Order = order,
                BuyerName = respDetails.PayerInfo.PayerName.FirstName + respDetails.PayerInfo.PayerName.LastName
            };
            Session["CheckoutDetails"] = response;
            return View("PaymentDetails", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PaymentConfirmation(int? orderId)
        {
            if(!orderId.HasValue)
                return RedirectToAction("PayPalCancel");
            var response = Session["CheckoutDetails"] as GetExpressCheckoutDetailsResponseType;
            if (response == null)
                return RedirectToAction("PayPalCancel");
            DoExpressCheckoutPaymentReq payReq = PayPalHelper.GetDoExpressCheckoutPaymentReq(response);
            CustomSecurityHeaderType credentials = PayPalHelper.GetPayPalCredentials();
            PayPalAPIAAInterfaceClient client = new PayPalAPIAAInterfaceClient();
            DoExpressCheckoutPaymentResponseType doResponse = client.DoExpressCheckoutPayment(ref credentials, payReq);
            if (doResponse.Errors != null && doResponse.Errors.Length > 0)
                throw new Exception("Exception occured when calling PayPal: " + doResponse.Errors[0].LongMessage);
            PayPalHelper.UpdateOrderAfterConfirmation(orderId.Value);
            return RedirectToAction("PayPalSuccess");
        }

        #endregion
    }
}