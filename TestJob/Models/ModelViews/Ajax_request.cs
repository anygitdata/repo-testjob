using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    // Simplified model 
    public class Ajax_request: IdentResult
    {
        public string strParam { get; set; }
        public string response { get; set; }
    }
}
