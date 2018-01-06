using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSTraining.WebUI.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsOnline { get; set; }
    }
}