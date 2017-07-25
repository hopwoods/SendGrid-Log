using System;
using System.Linq;

namespace SendGrid_Log.Models
{
    public class EventSearch : EmailEvent
    {
        public string searchString { get; set; }
        public string searchField { get; set; }
        public string eventType { get; set; }
        public int showRecords { get; set; }
        public int currentPage { get; set; }          
        public int skipNo { get; set; }
        public int recordsReturned { get; set; }
        public int totalRecords { get; set; }
        public bool isExport { get; set; }
    }

    public class EventSearchLogic
    {
        //Connect to DB
        private readonly SendGrid_LogContext _context;
        public EventSearchLogic(SendGrid_LogContext context)
        {
            _context = context;
        }

        public IQueryable<EmailEvent> GetEvents(EventSearch searchModel)
        {
            //Build DB Query

            //Basic Select Query
            var events = _context.EmailEvent.AsQueryable();       
            
            //Set number of events to retrieve. Either post value from the search form or a default.
            if (searchModel.showRecords == 0)
            {
                searchModel.showRecords = 25;
            }
            //Set number of records to skip for pagination
            searchModel.skipNo = (searchModel.currentPage * searchModel.showRecords) - searchModel.showRecords;
            
            //Use search term
            if (!String.IsNullOrEmpty(searchModel.searchString)) 
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
                        events = events.Where(s => s.response.Contains(searchModel.searchString));
                        break;
                };
            }

            //Filter Event Type
            if (!String.IsNullOrEmpty(searchModel.eventType)) 
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
            //Get total records before pagination
            searchModel.totalRecords = events.Count();

            //Check if Exporting
            if(searchModel.isExport == true)
            {
                events = events.OrderByDescending(x => x.eventTimestamp);
            }
            //Use Pagination
            else if (searchModel.currentPage == 0) 
            {
                events = events.OrderByDescending(x => x.eventTimestamp).Take(searchModel.showRecords);
            }
            else
            {
                events = events.OrderByDescending(x => x.eventTimestamp).Skip(searchModel.skipNo).Take(searchModel.showRecords);
            }
            //Get number of records returned
            searchModel.recordsReturned = events.Count();

            //return records
            return events;
        }
    }
}
