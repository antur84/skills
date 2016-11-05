using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinode.Skills.Api
{
    public class AllowedOrigins
    {
        public static List<string> Whitelist
        {
            get
            {
                return new List<string>() {
                    "http://localhost:4200"
                };
            }
        }
    }
}
