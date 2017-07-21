using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SendGrid_Log.Models;
using StrongGrid;


namespace SendGrid_Log.Controllers
{
    public class HomeController : Controller
    {
        private readonly SendGrid_LogContext _context;

        public HomeController(SendGrid_LogContext context)
        {
            _context = context;
        }

        // GET: EmailEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmailEvent.ToListAsync());
        }

        // GET: EmailEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailEvent = await _context.EmailEvent
                .SingleOrDefaultAsync(m => m.ID == id);
            if (emailEvent == null)
            {
                return NotFound();
            }

            return View(emailEvent);
        }

        // GET: Home/LogEvent
        //public IActionResult LogEvent()
        //{
        //    return View();
        //}

        // POST: Home/LogEvent
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
        public JsonResult LogEvent(Person person)
        {
            //var parser = new WebhookParser();
            //var events = await parser.ParseWebhookEventsAsync(Request.Body).ConfigureAwait(false);

            return Json(person);



        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailEvent = await _context.EmailEvent
                .SingleOrDefaultAsync(m => m.ID == id);
            if (emailEvent == null)
            {
                return NotFound();
            }

            return View(emailEvent);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailEvent = await _context.EmailEvent.SingleOrDefaultAsync(m => m.ID == id);
            _context.EmailEvent.Remove(emailEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EmailEventExists(int id)
        {
            return _context.EmailEvent.Any(e => e.ID == id);
        }
    }
}
