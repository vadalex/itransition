using DotNetOpenAuth.AspNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace MvcApp.AuthClients
{
    public class VKAuthenticationClient : IAuthenticationClient
    {
        private readonly string appId;
        private readonly string appSecret;
        private string redirectUri;

        public VKAuthenticationClient(string appId, string appSecret)
        {
            this.appId = appId;
            this.appSecret = appSecret;
        }

        string IAuthenticationClient.ProviderName
        {
            get { return "vkontakte"; }
        }
 
        void IAuthenticationClient.RequestAuthentication(HttpContextBase context, Uri returnUrl)
        {
            this.redirectUri = context.Server.UrlEncode(returnUrl.ToString());
            var address = String.Format("https://oauth.vk.com/authorize?client_id={0}&redirect_uri={1}&response_type=code", this.appId, this.redirectUri);
            HttpContext.Current.Response.Redirect(address, false);
        }
 
        AuthenticationResult IAuthenticationClient.VerifyAuthentication(HttpContextBase context)
        {
            try
            {
                string code = context.Request["code"];
                var address =
                    String.Format(
                        "https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}",
                        this.appId, this.appSecret, code, this.redirectUri);
 
                var response = VKAuthenticationClient.Load(address);
                var accessToken = VKAuthenticationClient.DeserializeJson<AccessToken>(response);
 
                address = String.Format(
                        "https://api.vk.com/method/users.get?uids={0}&fields=photo_50",
                        accessToken.user_id);
 
                response = VKAuthenticationClient.Load(address);
                var usersData = VKAuthenticationClient.DeserializeJson<UsersData>(response);
                var userData = usersData.response.First();
 
                return new AuthenticationResult(
                    true, (this as IAuthenticationClient).ProviderName, accessToken.user_id,
                    userData.first_name + " " + userData.last_name,
                    new Dictionary<string, string>());
            }
            catch (Exception ex)
            {
                return new AuthenticationResult(ex);
            }
        }
 
        private static string Load(string address)
        {
            var request = WebRequest.Create(address) as HttpWebRequest;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
 
        private static T DeserializeJson<T>(string input)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(input);
        }

        private class AccessToken
        {
            public string access_token = null;
            public string user_id = null;
        }

        private class UserData
        {
            public string uid = null;
            public string first_name = null;
            public string last_name = null;
            public string photo_50 = null;
        }

        private class UsersData
        {
            public UserData[] response = null;
        }
    } 
}