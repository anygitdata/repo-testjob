using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.Interface;

namespace TestJob.Models.ModelViews.TaskView
{
    public class GenTaskView_create: GenTaskView_templ
    {
        public GenTaskView_create(DataContext cont, IAnyUserData userData, GenTaskView model): base(cont, userData, model)
        { }


        protected override bool VerifyData() 
        {
            if (!VerifyDateTime())
            {
                return Return_withEROR("Date or time component errors");
            }

            if (string.IsNullOrEmpty(Model.TaskName))
                return Return_withEROR("The taskName field is not filled ");


            return Return_withOK();
        }

        protected override bool SaveData()
        {
            if (Result == Error)
                return false;


            if (!Debug)
            {
                var task = new Task
                {
                    Id = Guid.NewGuid(),
                    ProjectId = Model.ProjectId,
                    CreateDate = Get_DateTime_fromModel(),
                    TaskName = Model.TaskName
                };

                context.Add(task);
                context.SaveChanges();

                Model.TaskId = task.Id.ToString();
            }


            return Return_withOK();
        }

    }
}
