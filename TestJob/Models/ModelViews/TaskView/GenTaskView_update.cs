using System;
using TestJob.Models.Interface;

namespace TestJob.Models.ModelViews.TaskView
{
    public class GenTaskView_update : GenTaskView_modf
    {
        public GenTaskView_update(DataContext cont, IAnyUserData userData, GenTaskView model) : base(cont, userData, model)
        { }

        //Task Task;


        private readonly string rename = "renameTask";
        private readonly string compl = "complTask";

        private DateTime dateTimeFromModel;

        ///<summary>
        ///Checking the start date and task name  
        /// </summary>
        /// <returns></returns>
        public override bool VerifyData()
        {
            if (Result == Error)  // Ошибка на этапе GenTaskView_modf
                return false; 


            if (string.IsNullOrEmpty(Model.TaskId))
            {
                return Return_withEROR("No ID task");
            }

            //Task = context.Set<Task>().Find(Guid.Parse(Model.TaskId));

            if (Task == null)
            {
                return Return_withEROR("Task object not found");
            }


            if (Model.TypeOperModfTask == rename )
            {
                if (string.IsNullOrEmpty(Model.TaskName))
                    return Return_withEROR("No data taskName");

                if (Model.TaskName.Trim() == Task.TaskName.Trim() )
                    return Return_withEROR("Data has not changed ");

                
                return Return_withOK();
            }

            if (Model.TypeOperModfTask == compl )
            {
                dateTimeFromModel = Get_DateTime_fromModel();

                if (dateTimeFromModel <= Task.StartDate)
                    return Return_withEROR("Closing date is less than start date");
                    

                return Return_withOK();
            }
            else
                return Return_withEROR("Unknown operation");

        }


        public override bool SaveData()
        {
            if (Result == Error)
                return false;


            if (Model.TypeOperModfTask == rename && !Debug)
                Task.TaskName = Model.TaskName;

            if (Model.TypeOperModfTask == compl && !Debug)
                Task.UpdateDate = dateTimeFromModel;


            if (!Debug)
            {
                Model.Redirect = "/";
                context.SaveChanges();
            }


            Model.Result = Ok;
            Model.Message = Ok;

            return Return_withOK();
        }

    }
}
