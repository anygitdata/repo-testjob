using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews.ComnView
{
    public class TaskComment_ModelView : IdentResult
    {
        public string IdComment { get; set; }
        public string TaskId { get; set; }

        public string Content { get; set; }
        public bool ContentType { get; set; }
        public IFormFile postedFile { get; set; }

        public string StrFileName { get; set; }

        public ETypeOperations TypeOperations { get; set; }


        public TaskComment_ModelView()
        {
            Result = Error;
            Message = "Cancel operation";
        }
        public TaskComment_ModelView(string id) : this()
        {
            IdComment = id;
        }

    }
}
