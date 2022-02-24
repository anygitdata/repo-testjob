using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{

    public class GenTaskView: IdentResult
    {
        public Guid ProjectId { get; set; }
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string date { get; set; }
        public string time { get; set; }

        public string TypeOperTask { get; set; } // ETypeOperTask.ToString()
    }


    public abstract class GenTaskView_templ: IdentResult
    {
        protected readonly DataContext context;
        
        private bool _debug;
        protected bool Debug { get => _debug; }
        protected string mesError_default = "Cancel operation";

        public GenTaskView_templ(DataContext cont, IAnyUserData userData, GenTaskView model)
        {
            context = cont;

            _debug = userData.Debug;
            _model = model;
        }


        // only for testing
        public void SetDebug(bool arg)
        {
            _debug = arg;
        }


        readonly GenTaskView _model;
        public GenTaskView Model { get => _model; }

        protected abstract bool VerifyData();
        protected abstract bool SaveData();
        //protected abstract void InitResult(Guid id);


        protected string[] Get_compDateTime_fromModel(DateTime arg)
        {
            var comp = Components_date.ConvDate_intoObj(arg);
            string[] res = { comp.date, comp.time };

            return res;
        }
        protected DateTime Get_DateTime_fromModel()
        {
            var res = Components_date.ConvStr_intoObj(Model.date, Model.time);
            return res.resDate;

        }
        protected virtual bool VerifyDateTime() {
            Components_date compDate = 
                Components_date.ConvStr_intoObj(Model.date, Model.time);

            if (compDate.Result == Error)            
                return Return_withEROR(compDate.Message);
            

            return true;
        }
        
        

        protected virtual bool Return_withOK()
        {
            Result = Ok;
            Message = Ok;

            return true;
        }
        protected virtual bool Return_withEROR(string err)
        {
            Model.Result = Error;
            Model.Message = err;

            Result = Error;
            Message = err;
                
            return false;
        }
        
        protected void SetResult_forStart()
        {
            Result = Error;
            Message = mesError_default;
        }

    }
}
