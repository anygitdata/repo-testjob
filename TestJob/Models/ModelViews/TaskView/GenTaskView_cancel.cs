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


            if (Get_DateTime_fromModel() < task.StartDate)
                return Return_withEROR("cancel date is less than dateStart ");


            return Return_withOK();
        }

        public override bool SaveData()
        {
            if (Result == Error)
                return false;


            if (!Debug)
            {
                task.CancelDate = Get_DateTime_fromModel();                
                context.SaveChanges();
            }

            return Return_withOK();
        }

    }
}
