using System;

namespace TestJob.Models.ModelViews
{

    public class TaskCreate: BaseModel_view
    {
        public string projectId { get; set; }
        public string projectName { get; set; } = "";

        // ----------- Task data
        public string TaskId { get; set; }
        public string TaskName { get; set; }

        public string dateCreate { get; set; }
        public string timeCreate { get; set; }

        public ETypeOperTask OperTask { get; set; }
        public string str_operTask { get; set; }

        public Task ObjTask { get; set; }


        // ---------------------------------------
        public static bool VerifyTask(DataContext context, TaskCreate model)
        {
            if (string.IsNullOrEmpty(model.TaskId))
            {
                model.Result = Error;
                model.Message = "No ID task";

                return false;
            }

            try
            {
                if (Debug)
                {
                    model.Result = Ok;

                    return true;
                }
                else
                {
                    Task task = context.Set<Task>().Find(Guid.Parse(model.TaskId));

                    if (task == null)
                    {
                        model.Result = Error;
                        model.Message = "Task object not found";

                        return false;
                    }

                    model.ObjTask = task;
                    model.Result = Ok;
                    model.TaskId = task.Id.ToString();

                    return true;
                }                
            }
            catch
            {
                model.Result = Error;
                model.Message = "Reset processing. Check the data.";

                return false;
            }

        }

        public static void VerifyData(DataContext context, TaskCreate model)
        {
            if (string.IsNullOrEmpty(model.projectId))
            {
                model.Result = Error;
                model.Message = "No project selected";

                return;
            }
            

            if ((model.OperTask | ETypeOperTask.create) > 0)
            { 
                if (string.IsNullOrEmpty(model.TaskName))
                {
                    model.Result = Error;
                    model.Message = "Task field not filled ";

                    return;
                }

                Components_date compCreate = Components_date.ConvStr_intoObj(model.dateCreate, model.timeCreate);
                if (compCreate.Result == Error)
                {
                    model.Result = compCreate.Result;
                    model.Message = compCreate.Message;

                    return;
                }

                Task task = new Task
                {
                    Id = Guid.NewGuid(),
                    ProjectId = Guid.Parse(model.projectId),
                    TaskName = model.TaskName,
                    CreateDate = compCreate.resDate
                };

                model.TaskId = task.Id.ToString();
                model.str_operTask = ETypeOperTask.start.ToString();
                model.ObjTask = task;

                return;

            }
            else
            {
                // only update taskName
                
                if (!VerifyTask(context, model))
                    return; 

                if ( string.IsNullOrEmpty(model.TaskName))
                {
                    model.Result = Error;
                    model.Message = "Fill taskName";

                    return;
                }
                else
                {
                    if (model.TaskName == model.ObjTask.TaskName)
                    {
                        model.Result = Error;
                        model.Message = "Data not changed";

                        return; 
                    }
                }

                model.ObjTask.TaskName = model.TaskName;

                model.Result = Ok;

            }

        }

    }  // class TaskCreate


    public class TaskStart: TaskCreate
    {
        public string StartDate { get; set; }
        public string StartTime { get; set; }

        public static void VerifyData( DataContext context, TaskStart model)
        {
            if (!VerifyTask(context, model))
                return;

            Components_date compStart = 
                Components_date.ConvStr_intoObj(model.StartDate, model.StartTime);
            if (compStart.Result == Error)
            {
                model.Result = Error;
                model.Message = compStart.Message;

                return;
            }
            
            if (!Debug)
            {
                model.ObjTask.StartDate = (DateTime) compStart.resDate;
            }

            model.str_operTask = ETypeOperTask.detail.ToString();
            model.Result = Ok;

        }

    } // class TaskStart

    
    public class TaskUpdate: TaskStart
    {
        public string UpdateDate { get; set; }
        public string UpdateTime { get; set; }


        public string CancelDate { get; set; }
        public string CancelTime { get; set; }

        public string TypeOperExt { get; set; }

        // ----------------------------------------


        public static void VerifyData(DataContext context, 
            TaskUpdate model)
        {
            if (!VerifyTask(context, model))
                return;

            Components_date compDate = null;

            ETypeOperTask typeOper = (ETypeOperTask)
                ConvEnum.ConvStrEnum_intoEnum(typeof(ETypeOperTask).Name, model.TypeOperExt);

            if ((typeOper | ETypeOperTask.start) > 0)
            {
                compDate =
                Components_date.ConvStr_intoObj(model.UpdateDate, model.UpdateTime);
                if (compDate.Result == Error)
                {
                    model.Result = Error;
                    model.Message = compDate.Message;

                    return;
                }

                model.ObjTask.UpdateDate = compDate.resDate;

                return;
            }

            compDate = 
                Components_date.ConvStr_intoObj(model.UpdateDate, model.UpdateTime);
            if (compDate.Result == Error)
            {
                model.Result = Error;
                model.Message = compDate.Message;

                return;
            }

            model.ObjTask.CancelDate = compDate.resDate;

            return;

        }

    } // class TaskUpdate

}
