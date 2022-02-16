using System;
using System.IO;
using System.Linq;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public partial class GeneralModelView : IdentResult
    {
        partial void Save_model()
        {
            if (Result == Error)
                return;


            SetResultOptions();  // init Result and Message


            if (Model is TaskComment_ModelView)
            {
                var model = Model as TaskComment_ModelView;

                if (model.TypeOperations == ETypeOperations.insert)
                {
                    try
                    {
                        // Загрузка файла
                        if (!model.ContentType)
                        {
                            string fullPath = Path.Combine(pathTxt, model.Content);

                            if (!string.IsNullOrEmpty(debug_path_for_copy))
                                File.Copy(debug_path_for_copy, fullPath, true);
                            else
                            {
                                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                                {
                                    model.postedFile.CopyTo(fileStream);
                                }
                            }

                            model.StrFileName = model.Content;  // model.Content is comment or fileName
                        }

                        TaskComment comn = new TaskComment
                        {
                            Id = Guid.NewGuid(),
                            TaskId = Guid.Parse(model.TaskId),
                            CommentType = model.ContentType,
                            Content = UserMix.Enc_GetBytesFromStr(model.Content)
                        };

                        ModelRes = comn;

                        model.IdComment = comn.Id.ToString();


                        Init_model();  // Final operation 

                    }
                    catch
                    {
                        return;
                    }

                }
                else
                {
                    Init_model();
                }
                

            }
        }
    }
}
