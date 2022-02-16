using System;
using System.Collections.Generic;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{

    public class InsProjectViewBase : IdentResult
    {
        public Guid projectId { get; set; }
        public string projectName { get; set; }
        public string createDate { get; set; }
        public string createTime { get; set; }

        public List<string> lsError { get; set; }
    }

    public class InsProjectView : InsProjectViewBase
    {
        public string updateDate { get; set; }
        public string updateTime { get; set; }

    }

    
}
