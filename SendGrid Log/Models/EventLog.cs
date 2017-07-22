using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Collections;

namespace SendGrid_Log.Models
{
    public class EventLog : EmailEvent
    {
       [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Date & Time")]
        [DataType(DataType.DateTime)]
        public DateTime timestamp { get; set; }

        [Display(Name = "Event Type")]
        public string @event { get; set; }

        [Display(Name = "URL")]
        [DataType(DataType.Url)]
        public string url { get; set; }

        [Display(Name = "IP Address")]
        public string ip { get; set; }

        [Display(Name = "TLS Version")]
        public string tls { get; set; }

        [Display(Name = "Cert Error")]
        public string cert_err { get; set; }

        [Display(Name = "User Agent")]
        public string useragent { get; set; }

        [Display(Name = "User ID")]
        public string userid { get; set; }

        [Display(Name = "Reason")]
        public string reason { get; set; }

        [Display(Name = "Type")]
        public string type { get; set; }

        [Display(Name = "Attempt")]
        public string attempt { get; set; }

        [Display(Name = "Sent At")]
        public string Send_at { get; set; }

            
    }
}
