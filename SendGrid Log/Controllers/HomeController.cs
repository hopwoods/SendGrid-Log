using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid_Log.Models;
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

        // GET: EmailEvents
        public IActionResult Index()
        {
            return View(_context.EmailEvent.ToList());
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
        public IActionResult LogEvent([FromBody][Bind("ID,sg_event_id,sg_message_id,email,@event,url,asm_group_id,ip,tls,cert_err,useragent,userid,reason,type,attempt,send_at")] EmailEvent jsonData)
        {            
            int status = 0;
            //var EventData = Request.QueryString.ToString();
            if (ModelState.IsValid)
            {
                    _context.Add(jsonData);
                    _context.SaveChanges();
                    status = 1;                    

                return Ok(status);
            } else
            {
                return Ok(status);
            }
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
