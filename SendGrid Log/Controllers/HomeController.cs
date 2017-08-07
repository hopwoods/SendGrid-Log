using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SendGrid_Log.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SendGrid_Log.Controllers
{
    [RequireHttps]
    public class HomeController : Controller

    {
        //Connect to DB
        private readonly SendGrid_LogContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IHostingEnvironment hostingEnvironment, SendGrid_LogContext context)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        // Render Search Form
        public ActionResult renderSearch()
        {
            var model = new EventSearch();
            return PartialView("renderSearch", model);
        }

        // GET: POST: EmailEvents
        [HttpGet]
        public IActionResult Index(EventSearch searchModel)
        {
            //Get records from DB using the search logic
            var eventSearch = new EventSearchLogic(_context);
            var model = eventSearch.GetEvents(searchModel);
            //Set ViewBag Data
            ViewBag.showRecords = searchModel.showRecords;
            ViewBag.currentPage = searchModel.currentPage;
            ViewBag.searchString = searchModel.searchString;
            ViewBag.searchField = searchModel.searchField;
            ViewBag.eventType = searchModel.eventType;
            ViewBag.recordsReturned = searchModel.recordsReturned;
            ViewBag.totalRecords = searchModel.totalRecords;
            ViewBag.sortOrder = searchModel.sortOrder;
            ViewBag.sortField = searchModel.sortField;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EventSearch(EventSearch searchModel)
        {
            //Get records from DB using the search logic
            var eventSearch = new EventSearchLogic(_context);
            var model = eventSearch.GetEvents(searchModel);

            //Set ViewBag Data
            ViewBag.showRecords = searchModel.showRecords;
            ViewBag.currentPage = searchModel.currentPage;
            ViewBag.searchString = searchModel.searchString;
            ViewBag.searchField = searchModel.searchField;
            ViewBag.eventType = searchModel.eventType;
            ViewBag.recordsReturned = searchModel.recordsReturned;
            ViewBag.totalRecords = searchModel.totalRecords;
            ViewBag.sortOrder = searchModel.sortOrder;
            ViewBag.sortField = searchModel.sortField;
            return View("Index", model);
        }

        //Action for exporting query to an Excel spreadsheet.
        public IActionResult Export(EventSearch searchModel)
        {
            //Get records from DB using the search logic
            var eventSearch = new EventSearchLogic(_context);
            var model = eventSearch.GetEvents(searchModel);           

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            DateTime datetime = System.DateTime.Now;
            string sFileName = @"Email-Events.xlsx";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Email Events");
                //First add the headers
                worksheet.Cells[1, 1].Value = "Timestamp";
                worksheet.Cells[1, 2].Value = "Response";
                worksheet.Cells[1, 3].Value = "Event Type";
                worksheet.Cells[1, 4].Value = "Email Address";
                worksheet.Cells[1, 5].Value = "Reason";
                worksheet.Cells[1, 6].Value = "URL";
                worksheet.Cells[1, 7].Value = "IP Address";
                worksheet.Cells[1, 8].Value = "TLS Version";
                worksheet.Cells[1, 9].Value = "Cert Error";
                worksheet.Cells[1, 10].Value = "User Agent";
                worksheet.Cells[1, 11].Value = "User ID";
                worksheet.Cells[1, 12].Value = "Attempt Number";
                worksheet.Cells[1, 13].Value = "Sent At";

                //Add values
                var i = 2;
                foreach (var item in model)
                {
                    worksheet.Cells["A" + i.ToString()].Value = item.eventTimestamp;
                    worksheet.Cells["A" + i.ToString()].Style.Numberformat.Format = "dd-mm-yyyy hh:MM:ss";
                    worksheet.Cells["B" + i.ToString()].Value = item.response;
                    worksheet.Cells["C" + i.ToString()].Value = item.@event;                    
                    worksheet.Cells["D" + i.ToString()].Value = item.email;
                    worksheet.Cells["E" + i.ToString()].Value = item.reason;
                    worksheet.Cells["F" + i.ToString()].Value = item.url;
                    worksheet.Cells["G" + i.ToString()].Value = item.ip;
                    worksheet.Cells["H" + i.ToString()].Value = item.tls;
                    worksheet.Cells["I" + i.ToString()].Value = item.cert_err;
                    worksheet.Cells["J" + i.ToString()].Value = item.useragent;
                    worksheet.Cells["K" + i.ToString()].Value = item.userid;
                    worksheet.Cells["L" + i.ToString()].Value = item.attempt;
                    worksheet.Cells["M" + i.ToString()].Value = item.eventSend_at;
                    worksheet.Cells["M" + i.ToString()].Style.Numberformat.Format = "dd-mm-yyyy hh:MM:ss";
                    i++;
                }
                package.Save(); //Save the workbook.
            }
            return Redirect(URL);
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
