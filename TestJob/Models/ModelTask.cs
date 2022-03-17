using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.ModelViews;

namespace TestJob.Models
{
    public class ModelTask
    {
        public string Times { get; set; }
        public string Ticket { get; set; }
        public string Description { get; set; }

        public string Start { get; set; }
        public string End { get; set; }

        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string lineThrough { get; set; }
    }

    public class Content_TableTask
    {
        public IEnumerable<ModelTask> LsTaskCont { get; set; }

        public IEnumerable<ModelProjectMenu> LsProjects { get; set; }

        public string projectName { get; set; }

        public string projectId { get; set; }

        public string idUpdate { get; set; }

        public string debug { get; set; }

        public int numItem { get; set; }

        public string FullTime { get; set; }

    }

}
