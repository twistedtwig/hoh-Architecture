using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hoh.architecture.scafolding.Extensions
{
    public static class ScafoldingBuilderExtensions
    {
        public static IApplicationBuilder UseHohArchitecture(this IApplicationBuilder app)
        {
            //TODO will setup options and configuration here
            return app;
        }
    }
}
