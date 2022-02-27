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

        public string TypeOperTask { get; set; } // ETypeOperTask.ToString()
    }

    public class GenTaskViewExt: GenTaskView
    {
        public string DateExt { get; set; }
        public string TimeExt { get; set; }
    }


    public abstract class GenTaskView_templ<T> : IdentResult
    {
        protected enum TEmodel {gen, ext}

        protected readonly DataContext context;
        
        private bool _debug;
        protected bool Debug { get => _debug; }
        protected string mesError_default = "Cancel operation";

        public GenTaskView_templ(DataContext cont, IAnyUserData userData)
        {
            context = cont;
            _debug = userData.Debug;
        }


        // only for testing
        public void SetDebug(bool arg)
        {
            _debug = arg;
        }
        
        public T Model { get; set; }

        public abstract bool VerifyData();
        public abstract bool SaveData();
        public abstract ViewBag_data ViewBag_data { get; }


        protected string[] Get_compDateTime_fromModel(DateTime arg)
        {
            var comp = Components_date.ConvDate_intoObj(arg);
            string[] res = { comp.date, comp.time };

            return res;
        }
        //protected abstract DateTime Get_DateTime_fromModel();

        protected virtual bool Return_withOK()
        {
            Result = Ok;
            Message = Ok;

            return true;
        }
        
        protected void SetResult_forStart()
        {
            Result = Error;
            Message = mesError_default;
        }

    }
}
