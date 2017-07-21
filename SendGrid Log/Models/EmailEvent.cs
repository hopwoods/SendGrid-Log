using System;
using System.ComponentModel.DataAnnotations;

namespace SendGrid_Log.Models
{
    public class EmailEvent
    {
        public int ID { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        [Display(Name = "Event ID")]
        public string EventID { get; set; }
        [Display(Name = "Message Date")]
        public string MessageID { get; set; }
        [Display(Name = "Type")]
        public string EventType { get; set; }
        [Display(Name = "Recipient Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime Timestamp { get; set; }
        [Display(Name = "SMTP ID")]
        public string SmtpID { get; set; }
        [Display(Name = "Unique Argument Key")]
        public string UniqueArgKey { get; set; }
        public string Categories { get; set; }
        public string Newsletter { get; set; }
        [Display(Name = "ASM Group ID")]
        public string ASMGroupID { get; set; }
        public string Reason { get; set; }
        public string Type { get; set; }
        [Display(Name = "IP Address")]
        public string IP { get; set; }
        public string TLS { get; set; }
        [Display(Name = "Certificate Error")]
        public string Cert_Err { get; set; }
        [Display(Name = "User Agent")]
        public string UserAgent { get; set; }
        public string URL { get; set; }
        public string URLOffset { get; set; }
        public string Attempt { get; set; }
        [Display(Name = "Marketing Campaign ID")]
        public string MarketingCampaignID { get; set; }
        [Display(Name = "Marketing Campaign Name")]
        public string MarketingCampaignName { get; set; }
        [Display(Name = "Marketing Campaign Version")]
        public string MarketingCampaignVersion { get; set; }
        [Display(Name = "Marketing Campaign Split ID")]
        public string MarketingCampaignSplitID { get; set; }
        [Display(Name = "Post Type")]
        public string PostType { get; set; }
    }
}
