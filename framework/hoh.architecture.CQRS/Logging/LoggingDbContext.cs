using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace hoh.architecture.CQRS.Logging
{
    public class LoggingDbContext : DbContext
    {
        public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
        {
            
        }
        /*
         * todo hohoptions configure dbcontext, set the entity and table name
         */
    }
}
