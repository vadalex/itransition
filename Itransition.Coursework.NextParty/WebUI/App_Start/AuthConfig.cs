using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using WebUI.Models;
using System.Configuration;

namespace WebUI
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {               
            OAuthWebSecurity.RegisterClient(
                client: new VKontakteAuthenticationClient(
                    ConfigurationManager.AppSettings["VkKey"], 
                    ConfigurationManager.AppSettings["VkSecret"]),
                displayName: "Vkontakte",
                extraData: null);

            OAuthWebSecurity.RegisterFacebookClient(
                appId: ConfigurationManager.AppSettings["FBKey"],
                appSecret: ConfigurationManager.AppSettings["FBSecret"]);        

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: ConfigurationManager.AppSettings["TwKey"],
                consumerSecret: ConfigurationManager.AppSettings["TwSecret"]);

        }
    }
}
