using System;
using System.IO;
using System.Linq;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public partial class GeneralModelView : IdentResult
    {
        partial void VerifyData()
        {
            if (Model is TaskComment_ModelView)
            {
                var model = Model as TaskComment_ModelView;

                if (string.IsNullOrEmpty(model.Content))
                {
                    Result = Error;
                    Message = "Not data for Content";
                    return;
                }
                

                if (model.TypeOperations == ETypeOperations.insert)
                {
                    string content = model.Content;

                    if (!model.ContentType)
                    {
                        if (model.postedFile == null)
                        {
                            Result = Error;
                            Message = "postedFile not data";
                            return;
                        }

                        var ext = Path.GetExtension(model.postedFile.FileName);
                        if (ext != ".txt")
                        {
                            Result = Error;
                            Message = "This is not a text file";
                            return;
                        }

                        if (model.postedFile.Length > maxSizeFile)
                        {
                            Result = Error;
                            Message = "big file";
                            return;
                        }

                        // verify exists file
                        if (UserMix.FileExists(pathTxt, model.postedFile.FileName))
                        {
                            Result = Error;
                            Message = "Reloading a file";
                            return;
                        }

                        Result = Ok;
                        Message = Ok;
                    }

                }


                // update or delete
                if (model.TypeOperations > ETypeOperations.insert )
                {
                    if (string.IsNullOrEmpty(model.IdComment))
                    {
                        Result = Error;
                        Message = "no comment id";
                        return;
                    }

                    TaskComment comn = context.Set<TaskComment>().Find(Guid.Parse(model.IdComment));

                    if (comn == null)
                    {
                        Result = Error;
                        Message = "task comment not found";
                        return;
                    }


                    if (!model.ContentType)   // comment in file
                    {
                        string fileName = UserMix.Enc_GetStrFromBytes(comn.Content);
                        string fullPath = Path.Combine(pathTxt, fileName);


                        if (!UserMix.FileExists(fullPath))
                        {
                            Result = Error;
                            Message = "No comment file";
                            return;
                        }
                    }


                    Result = Ok;
                    Message = Ok;
                    
                }

            }

        }
    }
}
