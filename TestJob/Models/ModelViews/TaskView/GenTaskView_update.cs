﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.Interface;

namespace TestJob.Models.ModelViews.TaskView
{
    public class GenTaskView_update : GenTaskView_modf
    {
        public GenTaskView_update(DataContext cont, IAnyUserData userData, Guid id) : base(cont, userData, id)
        {  }

        Task task;


        ///<summary>
        ///Checking the start date and task name  
        /// </summary>
        /// <returns></returns>
        public override bool VerifyData()
            {
                if (string.IsNullOrEmpty(Model.TaskId))
                {
                    return Return_withEROR("No ID task");
                }

                task = context.Set<Task>().Find(Guid.Parse(Model.TaskId));


                if (task == null)
                {
                    return Return_withEROR("Task object not found");
                }

                if (Get_DateTime_fromModel() < task.CreateDate)
                {
                    return Return_withEROR("StartDate is less than creation date");
                }

                if (string.IsNullOrEmpty(Model.TaskName))
                {
                    return Return_withEROR("No data taskName");
                }


                return Return_withOK();
            }


        public override bool SaveData()
            {
                if (Result == Error)
                    return false;


                bool equalsName = Model.TaskName == task.TaskName;
                bool equalsDate = Get_DateTime_fromModel() == task.StartDate;


                if (!equalsDate && !equalsName)
                    return Return_withEROR("Data has not changed ");

                if (!Debug)
                {
                    task.TaskName = Model.TaskName;
                    task.StartDate = Get_DateTime_fromModel();
                    context.SaveChanges();
                }

                return Return_withOK();
            }
        
    }
}
