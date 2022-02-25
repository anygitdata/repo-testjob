using System;
using System.IO;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews.ComnView
{
    public partial class GenModelViewComn
        : GeneralModelView_templ<TaskComment_ModelView, TaskComment, AnyData_Comment>
    {
        public override bool VerifyData()
        {
            if (Model.TypeOperations == ETypeOperations.delete)
            {
                try
                {
                    var taskComn = context.Set<TaskComment>().Find(Guid.Parse(Model.IdComment));

                    if (taskComn == null)
                    {
                        if (Debug)
                            return Return_withEROR("Временные данные не изменяются");

                        return Return_withEROR("No data to comment");
                    }

                    return Return_withOK();
                }
                catch {
                    return Return_withEROR("Cancel verifyData");
                }
            }


            if (Model.TypeOperations == ETypeOperations.insert)
            {
                if (!Model.ContentType)
                {
                    if (Model.postedFile == null)
                    {
                        return Return_withEROR("postedFile not data");
                    }

                    var ext = Path.GetExtension(Model.postedFile.FileName);
                    if (ext != ".txt")
                    {
                        return Return_withEROR("This is not a text file");
                    }

                    if (Model.postedFile.Length > maxSizeFile)
                    {
                        return Return_withEROR("big file");
                    }

                    // verify exists file
                    if (UserMix.FileExists(pathTxt, Model.postedFile.FileName))
                    {
                        return Return_withEROR("Reloading a file");
                    }

                    return Return_withOK();
                }
                else
                {
                    if (string.IsNullOrEmpty(Model.Content))
                    {
                        return Return_withEROR("Not data for Content");
                    }

                    return Return_withOK();
                }
            }


            if (Model.TypeOperations == ETypeOperations.update)
            {
                if (string.IsNullOrEmpty(Model.IdComment))
                {
                    return Return_withEROR("no comment id");
                }

                if (string.IsNullOrEmpty(Model.Content))
                {
                    return Return_withEROR("Not data for Content");
                }

                TaskComment comn = context.Set<TaskComment>().Find(Guid.Parse(Model.IdComment));

                if (comn == null)
                {
                    if (Debug)                    
                        return Return_withEROR("Изменение временных данных", true);

                    return Return_withEROR("task comment not found");
                }


                if (!Model.ContentType)   // comment in file
                {                    
                    string fullPath = Get_pathFromBytes(comn.Content);

                    if (!UserMix.FileExists(fullPath))
                    {
                        return Return_withEROR("No comment file");
                    }

                    if (Debug)
                    {
                        return Return_withEROR("Debug: доступ к файлу закрыт");
                    }
                }

                return Return_withOK();
            }
            else
            {
                return Return_withEROR("Unidentified operation");
            }

        }

    }
}
