using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public class Api_Ajax_contProject : IdentResult
    {
        public Guid projectId { get; set; }
        public string projectName { get; set; }
        public string date { get; set; }
        public string time { get; set; }

        public string dateUpdate { get; set; }
        public string timeUpdate { get; set; }

        public List<string> lsError { get; set; }
    }
}
