using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoh.architecture.CQRS.EntityFramework.Logging
{
    public class LoggingEntity
    {
        public long Id { get; set; }
        public DateTime ExecutionTime { get; set; }

        //TODO add other bits
    }
}
