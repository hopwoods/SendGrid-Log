using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SendGrid_Log.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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
        // Redirect for Pagination
        //[HttpGet]
        //public IActionResult FilterIndex(EventSearch searchModel)
        //{
        //    return RedirectToAction("Index", new {
        //        searchString = searchModel.searchString,
        //        searchField = searchModel.searchField,
        //        currentPage = searchModel.currentPage,
        //        showRecords = searchModel.showRecords
        //    });
        //}

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
            string sFileName = @"demo.xlsx";
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

                //Add values
                var i = 2;
                foreach (var item in model)
                {
                    worksheet.Cells["A" + i.ToString()].Value = item.@event;
                    worksheet.Cells["B" + i.ToString()].Value = item.email;
                    worksheet.Cells["C" + i.ToString()].Value = item.eventTimestamp;
                    worksheet.Cells["C" + i.ToString()].Style.Numberformat.Format = "dd-mm-yyyy hh:MM:ss";
                    worksheet.Cells["D" + i.ToString()].Value = item.response;
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
