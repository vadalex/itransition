using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using System.Configuration;
using MvcApp.AuthClients;

namespace MvcApp
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            OAuthWebSecurity.RegisterFacebookClient(
                appId: ConfigurationManager.AppSettings["FBKey"],
                appSecret: ConfigurationManager.AppSettings["FBSecret"]);        
            
            OAuthWebSecurity.RegisterClient(
                client: new VKAuthenticationClient(
                    ConfigurationManager.AppSettings["VkKey"], 
                    ConfigurationManager.AppSettings["VkSecret"]),
                displayName: "VK",
                extraData: null);            
        }
    }
}
