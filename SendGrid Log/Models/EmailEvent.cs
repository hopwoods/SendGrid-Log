using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace SendGrid_Log.Models
{
    public class EmailEvent
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }

        [Display(Name = "SendGrid Event ID")]
        public string sg_event_id { get; set; }

        [Display(Name = "SendGrid Message ID")]
        public string sg_message_id { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        
        [NotMapped]
        public int timestamp { get; set; }

        private DateTime _eventTimestamp = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);

        [Display(Name = "Date & Time")]
        [DataType(DataType.DateTime)]
        public DateTime eventTimestamp
        {
            get {
                if (_eventTimestamp == Convert.ToDateTime("01/01/1970 00:00:00"))
                {
                    _eventTimestamp = _eventTimestamp.AddSeconds(timestamp).ToLocalTime();
                    return _eventTimestamp;
                }
                else
                {
                    return _eventTimestamp; 
                }
            }
            set
            {
                if (_eventTimestamp == Convert.ToDateTime("01/01/0001 00:00:00"))
                {
                    _eventTimestamp = _eventTimestamp.AddSeconds(timestamp).ToLocalTime();
                }
                else {
                    _eventTimestamp = value;
                }
            }
        }

        [Display(Name = "Event Type")]
        public string @event { get; set; }

        //[Display(Name = "Category")]
        //public string category { get; set; }

        [Display(Name = "ASM Group ID")]
        public int asm_group_id { get; set; }

        [Display(Name = "URL")]
        [DataType(DataType.Url)]
        public string url { get; set; }

        //[Display(Name = "Unique Argument Key")]
        //public string unique_arg_key { get; set; }

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

        [Display(Name = "Response")]
        public string response { get; set; }

        [Display(Name = "Type")]
        public string type { get; set; }

        [Display(Name = "Attempt")]
        public string attempt { get; set; }

        [Display(Name = "Sent At")]
        public string Send_at { get; set; }

        //[Display(Name = "Template")]
        //public string template { get; set; }

        //[Display(Name = "Newsletter")]
        //public string newsletter { get; set; }

        //[Display(Name = "URL Offset")]
        //public string url_offset { get; set; }

    }
}
