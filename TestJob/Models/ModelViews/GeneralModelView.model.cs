using System;
using System.IO;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public partial class GeneralModelView: IdentResult
    {
        /// <summary>
        /// Final operation 
        /// </summary>
        partial void Init_model()
        {
            if (Model is TaskComment_ModelView)
            {
                var model = Model as TaskComment_ModelView;


                if (model.TypeOperations == ETypeOperations.insert)
                {
                    if (!model.ContentType)
                        //  model.postedFile.CopyTo(fileStream);  in Save
                        model.Content = UserMix.FileDownload(pathTxt, model.StrFileName);

                    if (!Debug)
                    {
                        context.Add(ModelRes as TaskComment);
                        context.SaveChanges();
                    }


                    Result = Ok;
                    Message = Ok;

                    return;
                }


                
                if (model.TypeOperations == ETypeOperations.update)
                {
                    var comn = context.Set<TaskComment>().Find(Guid.Parse(model.IdComment));

                    if (!model.ContentType) // comment in file
                    {
                        string fileName = UserMix.Enc_GetStrFromBytes(comn.Content);
                        string fullPath = Path.Combine(pathTxt, fileName);

                        // update only file
                        UserMix.FileCreate(fullPath, model.Content);
                    }
                    else
                    {
                        // update in database
                        comn.Content = UserMix.Enc_GetBytesFromStr(model.Content);

                        if (!Debug)                        
                            context.SaveChanges();                        
                    }


                    ModelRes = comn;

                    

                    return;
                }



                if (model.TypeOperations == ETypeOperations.delete)
                {
                    context.Remove(context.Set<TaskComment>().Find(Guid.Parse(model.IdComment)));

                    if (!Debug)
                        context.SaveChanges();


                    Result = Ok;
                    Message = Ok;

                    return;
                }

            }


        }
    }
}
