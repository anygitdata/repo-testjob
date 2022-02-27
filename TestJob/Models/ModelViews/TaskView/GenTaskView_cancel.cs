using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.Interface;

namespace TestJob.Models.ModelViews.TaskView
{
    public class GenTaskView_cancel : GenTaskView_modf

    {
        public GenTaskView_cancel(DataContext cont, IAnyUserData userData, GenTaskView model) : base(cont, userData, model)
        { }

        Task task;

        public override bool VerifyData() 
        {
            if (string.IsNullOrEmpty(Model.TaskId))
                return Return_withEROR("No ID task");


            task = context.Set<Task>().Find(Guid.Parse(Model.TaskId));

            if (task == null)
            {
                return Return_withEROR("Task object not found");
            }


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
