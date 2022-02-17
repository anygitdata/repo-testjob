using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Models.ModelViews
{
    // Used in GeneralModelView
    public class AnyData_Comment
    {
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string Str_DateTime { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Debug { get; set; }

        public int NumComment { get; set; }


        public int maxSizeFile { get; set; }
    }

}
