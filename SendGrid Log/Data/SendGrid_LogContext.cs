using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SendGrid_Log.Models
{
    public class SendGrid_LogContext : DbContext
    {
        public SendGrid_LogContext (DbContextOptions<SendGrid_LogContext> options)
            : base(options)
        {
        }

        public DbSet<SendGrid_Log.Models.EmailEvent> EmailEvent { get; set; }
    }
}
