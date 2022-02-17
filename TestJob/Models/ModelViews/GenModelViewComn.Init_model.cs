using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestJob.Models.ModelViews.Templ;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public partial class GenModelViewComn
         : GeneralModelView_templ<TaskComment_ModelView, TaskComment, AnyData_Comment>
    {

        public bool Init_lstModelView(string id)
        {
            _lstModelView = context.Set<TaskComment>()
                .Where(p => p.TaskId == Guid.Parse(id))
                .Select(p => new TaskComment_ModelView
                {
                    IdComment = p.Id.ToString(),
                    TaskId = id,
                    StrFileName = ((bool)p.CommentType == true) ? "" : Get_StrFromByte(p.Content),
                    Content = (bool) p.CommentType 
                        ? Get_StrFromByte(p.Content) 
                        : Get_ComntFromFile(pathTxt, p.Content)
                }).ToList();


            var data = (from tsk in context.Tasks.Where(p => p.Id == Guid.Parse(id))
                        from pr in context.Projects.Where(p => p.Id == tsk.ProjectId)
                        select new
                        {
                            pr.ProjectName,
                            tsk.Id,
                            tsk.TaskName,
                            tsk.StartDate
                        }).ToList()
                          .FirstOrDefault();


            Model = new TaskComment_ModelView
            {
                IdComment = "",
                TaskId = id.ToString(),
                TypeOperations = ETypeOperations.insert,
                ContentType = false,
                postedFile = null,
                Debug = Debug
            };

            ModelView = new AnyData_Comment
            {
                ProjectName = data.ProjectName,
                TaskName = data.TaskName,
                Str_DateTime = Components_date.Get_str_DateTime(data.StartDate),
                maxSizeFile = maxSizeFile,
                NumComment = _lstModelView.Count
            };

            return Return_withOK(); 
        }

        /// <summary>
        /// Final operation 
        /// </summary>
        /// <summary>
        /// Final operation 
        /// </summary>
        public override bool Init_model()
        {
            if (Model.TypeOperations == ETypeOperations.insert)
            {
                if (!Model.ContentType)
                    //  Model.postedFile.CopyTo(fileStream);  in Save
                    Model.Content = UserMix.FileDownload(pathTxt, Model.StrFileName);

                return Return_withOK();
            }

            if (Model.TypeOperations == ETypeOperations.update)
            {
                var comn = context.Set<TaskComment>().Find(Guid.Parse(Model.IdComment));

                if (!Model.ContentType) // comment in file
                {
                    string fileName = UserMix.Enc_GetStrFromBytes(comn.Content);
                    string fullPath = Path.Combine(pathTxt, fileName);

                    // update only file
                    UserMix.FileCreate(fullPath, Model.Content);
                }
                else
                {
                    // update in database
                    comn.Content = UserMix.Enc_GetBytesFromStr(Model.Content);

                    if (!Debug)
                        context.SaveChanges();
                }


                ModelRes = comn;

                return Return_withOK();
            }


            if (Model.TypeOperations == ETypeOperations.delete)
            {
                context.Remove(context.Set<TaskComment>().Find(Guid.Parse(Model.IdComment)));

                if (!Debug)
                    context.SaveChanges();

                return Return_withOK();
            }
            else
            {
                return Return_withEROR("Unidentified operation");
            }


        }
    }
}
