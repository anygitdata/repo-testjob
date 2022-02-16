using System;
using System.Linq;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public partial class GeneralModelView: IdentResult
    {
        partial void Init_modelView(Guid id)
        {
            if (Model is TaskComment_ModelView)
            {
                var data = (from tsk in context.Tasks.Where(p => p.Id == id)
                            from pr in context.Projects.Where(p => p.Id == tsk.ProjectId)
                            select new
                            {
                                pr.ProjectName,
                                tsk.Id,
                                tsk.TaskName,
                                tsk.StartDate
                            }).ToList().FirstOrDefault();


                ModelRes = new TaskComment_ModelView
                {
                    IdComment = "",
                    TaskId = data.Id.ToString(),
                    TypeOperations = ETypeOperations.insert,
                    ContentType = false,
                    postedFile = null,
                    Debug = Debug
                };

                Components_date compStart =
                    Components_date.convDate_intoObj(data.StartDate);

                ModelView = new AnyData_Comment
                {
                    ProjectName = data.ProjectName,
                    TaskName = data.TaskName,
                    Date = compStart.date,
                    Time = compStart.time,
                    maxSizeFile = maxSizeFile
                };

            }

        }
    }
}
