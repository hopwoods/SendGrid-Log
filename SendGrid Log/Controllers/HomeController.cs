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
        //Connect to DB
        private readonly SendGrid_LogContext _context; 
        public HomeController(SendGrid_LogContext context)
        {
            _context = context;
        }
        // Render Search Form
        public ActionResult renderSearch() 
        {
            var model = new EventSearch();
            return PartialView("renderSearch", model);
        }
        // Redirect for Pagination
        [HttpGet]
        public IActionResult FilterIndex(EventSearch searchModel)
        {
            return RedirectToAction("Index", new { searchString = searchModel.searchString, pageNo = searchModel.pageNo, showRecords = searchModel.showRecords });
        }

        // GET: POST: EmailEvents
        [HttpPost, ActionName("Index")]
        [HttpGet]
        public IActionResult Index(EventSearch searchModel)
        {

            var eventSearch = new EventSearchLogic(_context);
            var model = eventSearch.GetEvents(searchModel);

            //Set ViewBag Data
            ViewBag.showRecords = searchModel.showRecords;
            ViewBag.pageNo = searchModel.pageNo;
            ViewBag.searchString = searchModel.searchString;
            ViewBag.searchField = searchModel.searchField;
            ViewBag.eventType = searchModel.eventType;
            ViewBag.recordsReturned = searchModel.recordsReturned;
            ViewBag.totalRecords = searchModel.totalRecords;
            return View(model);
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
