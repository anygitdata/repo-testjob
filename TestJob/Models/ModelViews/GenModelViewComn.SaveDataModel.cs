using System;
using System.IO;
using TestJob.Models.ModelViews.Templ;
using TestJob.Models.UserAPI;

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
                try
                {
                    // Загрузка файла
                    if (!Model.ContentType)
                    {
                        string fullPath = Path.Combine(pathTxt, Model.Content);

                        if (!string.IsNullOrEmpty(debug_path_for_copy))
                            File.Copy(debug_path_for_copy, fullPath, true);
                        else
                        {
                            using (var fileStream = new FileStream(fullPath, FileMode.Create))
                            {
                                Model.postedFile.CopyTo(fileStream);
                            }
                        }

                        Model.StrFileName = Model.Content;  // Model.Content is comment or fileName
                    }

                    TaskComment comn = new TaskComment
                    {
                        Id = Guid.NewGuid(),
                        TaskId = Guid.Parse(Model.TaskId),
                        CommentType = Model.ContentType,
                        Content = UserMix.Enc_GetBytesFromStr(Model.Content)
                    };

                    ModelRes = comn;
                    Model.IdComment = comn.Id.ToString();

                    if (!Debug)
                    {
                        context.Add(ModelRes);
                        context.SaveChanges();
                    }


                    return Return_withOK();

                }
                catch
                {
                    return Return_withEROR("Cancel operation");
                }

            }
            else
            {
                return Init_model();
            }

        }
    }
}
