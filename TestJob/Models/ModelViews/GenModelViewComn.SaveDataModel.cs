using System;
using TestJob.Models.ModelViews.Templ;

namespace TestJob.Models.ModelViews
{
    public partial class GenModelViewComn
        : GeneralModelView_templ<TaskComment_ModelView, TaskComment, AnyData_Comment>
    {
        public override bool SaveDataModel()
        {
            if (Result == Error)
                return false;

            SetResult_forStart();  // init Result and Message

            if (Model.TypeOperations == ETypeOperations.insert)
            {
                ModelRes = new TaskComment
                {
                    Id = Guid.NewGuid(),
                    TaskId = Guid.Parse(Model.TaskId),
                    CommentType = Model.ContentType,
                    Content = Get_bytesFromStr(Model.Content)
                };


                return Init_model();

            }
            else
            {
                return Init_model();
            }

        }

       
    }
}
