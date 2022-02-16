using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Models.UserAPI
{
    public class IdentResult
    {
        public static string Error { get { return "error"; } }
        public static string Ok { get { return "ok"; } }

        public string Result { get; set; } = Ok;
        public string Message { get; set; }
    }
}
