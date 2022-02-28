using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{

    /// <summary>
    /// base class 
    /// </summary>
    public class ComnTemplate: IdentResult
    {
        private string mesError_default = "Cancel operation";
        protected void SetResult_forStart()
        {
            Result = Error;
            Message = mesError_default;
        }

        protected readonly DataContext context;

        private bool _debug;
        protected bool Debug { get => _debug; }

        // only for testing
        public void SetDebug(bool arg)
        {
            _debug = arg;
        }


        public ComnTemplate(DataContext cont, IAnyUserData userData)
        {
            _debug = userData.Debug;
            context = cont;
        }

        protected virtual bool Return_withOK()
        {
            Result = Ok;
            Message = Ok;

            return true;
        }

        public static string[] Get_compDateTime_fromModel(DateTime arg)
        {
            var comp = Components_date.ConvDate_intoObj(arg);
            string[] res = { comp.date, comp.time };

            return res;
        }
    }
}
