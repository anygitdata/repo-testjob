using System;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews.ProjectView
{
    public class BaseProjectView: IdentResult
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }

        public string DateUpd { get; set; }
        public string TimeUpd { get; set; }

        public string TypeOperations { get; set; }

        public string IdUpdate { get; set; } = "off";   // Project completion indicator 

    }
}
