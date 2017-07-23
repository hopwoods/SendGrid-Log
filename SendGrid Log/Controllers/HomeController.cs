using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid_Log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SendGrid_Log.Controllers
{
    public class HomeController : Controller
    {
        private readonly SendGrid_LogContext _context;

        public HomeController(SendGrid_LogContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult FilterIndex(string searchString, int pageNo, int showRecords)
        {
            return RedirectToAction("Index", new { searchString = searchString, pageNo = pageNo, showRecords = showRecords });
        }

        // GET: POST: EmailEvents
        [HttpPost, ActionName("Index")]
        [HttpGet]
        public IActionResult Index(string searchString, string searchField, string eventType, int pageNo, int showRecords)
        {

            int numberOfrecords = 10;
            if (showRecords != 0)
            {
                numberOfrecords = showRecords;
            }
            int skipNo = (pageNo * numberOfrecords) - numberOfrecords;

            
            //Build DB Query
            var events = from e in _context.EmailEvent
                         select e;
            
            if (!String.IsNullOrEmpty(searchString)) //Use search term
            {
                //Use Search Field
                switch (searchField)
                {
                    default:
                        events = events.Where(s => s.email.Contains("email"));
                        break;
                    case "email":
                        events = events.Where(s => s.email.Contains(searchString));
                        break;                        
                    case "event":
                        events = events.Where(s => s.@event.Contains(searchString));
                        break;
                };
                
            }

            if (!String.IsNullOrEmpty(eventType)) //Filter Event Type
            {
                //Use Search Field
                switch (eventType)
                {
                    default:
                        events = events.Where(s => s.@event.Contains("delivered"));
                        break;
                    case "processed":
                        events = events.Where(s => s.@event.Contains(eventType));
                        break;
                    case "delivered":
                        events = events.Where(s => s.@event.Contains(eventType));
                        break;
                };

            }

            if (pageNo == 0) //Use Pagination
            {
                events = events.OrderByDescending(x => x.eventTimestamp).Take(numberOfrecords);
            }
            else
            {
                events = events.OrderByDescending(x => x.eventTimestamp).Skip(skipNo).Take(numberOfrecords);
            }

            //Set ViewBag Data
            ViewBag.selectedRecords = events.Count();
            ViewBag.noOfRecords = numberOfrecords;
            ViewBag.pageNo = pageNo;
            ViewBag.searchString = searchString;
            ViewBag.searchField = searchField;
            ViewBag.eventType = eventType;
            ViewBag.totalRecords = events.Count();

            if (skipNo < 1)
            { ViewBag.startRecordNo = 1; }
            else
            { ViewBag.startRecordNo = skipNo + 1; }


            return View(events.ToList());
        }
        
        // GET: Home/LogEvent
        public IActionResult LogEvent()
        {
            return View();
        }

        // POST: Home/LogEvent
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        public IActionResult LogEvent([FromBody]
        [Bind("ID,sg_event_id,sg_message_id,email,@event,url,asm_group_id,ip,tls,cert_err,useragent,userid,reason,type,attempt,send_at")]
        List<EmailEvent> jsonData)
        {
            int status = 0;
            if (jsonData.Count != 0) //Check for Events
            {
                if (ModelState.IsValid)
                {
                    foreach (var emailEvent in jsonData) //Loop through events and add to DB
                    {
                        //Check if event exists, if not add it to the db.
                        string eventID = emailEvent.sg_event_id;
                        var dbEvent = from e in _context.EmailEvent
                                      select e;
                        dbEvent = dbEvent.Where(i => i.sg_event_id.Equals(eventID));
                        int count = dbEvent.Count();
                        if (count == 0)
                        {
                            _context.Add(emailEvent);
                        };
                    }
                    _context.SaveChanges(); //Save DB Changes
                    status = 1;
                }
                return Ok(status);
            }
            else
            {
                return Ok(status);
            }
        }
    }
}
