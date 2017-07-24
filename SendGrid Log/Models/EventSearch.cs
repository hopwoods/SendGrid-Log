using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid_Log.Models;

namespace SendGrid_Log.Models
{
    public class EventSearch : EmailEvent
    {
        public string searchString { get; set; }
        public string searchField { get; set; }
        public string eventType { get; set; }
        public int showRecords { get; set; }
        public int pageNo { get; set; }          
        public int skipNo { get; set; }
        public int recordsReturned { get; set; }
        public int totalRecords { get; set; }
    }

    public class EventSearchLogic
    {
        private readonly SendGrid_LogContext _context;

        public EventSearchLogic(SendGrid_LogContext context)
        {
            _context = context;
        }

        public IQueryable<EmailEvent> GetEvents(EventSearch searchModel)
        {
            //Build DB Query
            var events = _context.EmailEvent.AsQueryable();            
            if (searchModel.showRecords == 0)
            {
                searchModel.showRecords = 10;
            }
            searchModel.skipNo = (searchModel.pageNo * searchModel.showRecords) - searchModel.showRecords;
            if (!String.IsNullOrEmpty(searchModel.searchString)) //Use search term
            {
                switch (searchModel.searchField)
                {
                    default:
                        events = events.Where(s => s.email.Contains("email"));
                        break;
                    case "email":
                        events = events.Where(s => s.email.Contains(searchModel.searchString));
                        break;
                    case "response":
                        events = events.Where(s => s.@event.Contains(searchModel.searchString));
                        break;
                };
            }

            if (!String.IsNullOrEmpty(searchModel.eventType)) //Filter Event Type
            {
                switch (searchModel.eventType)
                {
                    default:
                        events = events.Where(s => s.@event.Contains("delivered"));
                        break;
                    case "processed":
                        events = events.Where(s => s.@event.Contains(searchModel.eventType));
                        break;
                    case "delivered":
                        events = events.Where(s => s.@event.Contains(searchModel.eventType));
                        break;
                };
            }
            searchModel.totalRecords = events.Count();
            //Use Pagination
            if (searchModel.pageNo == 0) 
            {
                events = events.OrderByDescending(x => x.eventTimestamp).Take(searchModel.showRecords);
            }
            else
            {
                events = events.OrderByDescending(x => x.eventTimestamp).Skip(searchModel.skipNo).Take(searchModel.showRecords);
            }
            searchModel.recordsReturned = events.Count();
            return events;
        }
    }
}
