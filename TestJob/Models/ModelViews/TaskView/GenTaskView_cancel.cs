using System;
using TestJob.Models.Interface;

namespace TestJob.Models.ModelViews.TaskView
{
    public class GenTaskView_cancel : GenTaskView_modf

    {
        public GenTaskView_cancel(DataContext cont, IAnyUserData userData, Guid id) : base(cont, userData, id)
        { }

        Task task;

        public override bool VerifyData() 
        {
            if (Result == Error)  // Verification in the GenTaskView_modf.constructor 
                return false;


            return Return_withOK();
        }

        public override bool SaveData()
        {
            if (Result == Error)
                return false;


            if (!Debug)
            {
                task.CancelDate = DateTime.Now;
                context.SaveChanges();
            }


            Model.Redirect = "/";
            Model.Result = Ok;
            Model.Message = Ok;

            return Return_withOK();
        }

    }
}
