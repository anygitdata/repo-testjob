using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Models.ModelViews
{
    public class UpLoadFile
    {
        public IFormFile postedFile { get; set; }

        public string userData { get; set; }
    }
}
