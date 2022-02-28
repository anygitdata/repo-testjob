using System;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews.TaskView
{

    public class ViewBag_data
    {
        public Guid ProjectId { get; set; }                
        public string ProjectName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Debug { get; set; }
    }

    public class GenTaskView: IdentResult
    {
        public Guid ProjectId { get; set; }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Redirect { get; set; } = "";

        public string TypeOperTask { get; set; } // ETypeOperTask.ToString()
    }

    public class GenTaskViewExt: GenTaskView
    {
        public string DateExt { get; set; }
        public string TimeExt { get; set; }
    }


    public abstract class GenTaskView_templ<T> : ComnTemplate
    {
        public GenTaskView_templ(DataContext cont, IAnyUserData userData):base(cont, userData)
        { }
        
        
        public T Model { get; set; }

        public abstract bool VerifyData();
        public abstract bool SaveData();
        public abstract ViewBag_data ViewBag_data { get; }


    }
}
