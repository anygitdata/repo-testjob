using System;
using System.IO;
using System.Linq;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews.ComnView
{
    public partial class GenModelViewComn
         : GeneralModelView_templ<TaskComment_ModelView, TaskComment, AnyData_Comment>
    {
        public bool Init_lstModelView(string id)
        {
            // for list TaskComment
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


            ModelView = new AnyData_Comment
            {
                ProjectName = data.ProjectName,
                TaskId = id,
                TaskName = data.TaskName,
                Str_DateTime = Components_date.Get_str_DateTime(data.StartDate),
                maxSizeFile = maxSizeFile,
                Debug = Debug ? "on": "off"
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
                {

                    string fileName = Model.Content;
                    string fullPath = Path.Combine(pathTxt, fileName);

                    if (!Debug)
                    {
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            Model.postedFile.CopyTo(fileStream);
                        }

                        Model.Content = LoadFileTxt(fileName);
                    }
                    else
                        Model.Content = "Testing data: load data from file";


                    Model.StrFileName = fileName;  // Model.Content is comment or fileName                    
                    
                    Model.postedFile = default;
                }


                if (!Debug)
                {
                    context.Add(ModelRes);
                    context.SaveChanges();                                       
                }


                return Return_withOK();
            }


            if (Model.TypeOperations == ETypeOperations.update)
            {
                var comn = Get_itemTaskComn(Model.IdComment);

                if (! (bool)comn.CommentType ) // false -> comment in file
                {
                    string fullPath = Get_pathFromBytes(comn.Content);

                    // remove and create file 
                    if (!Debug)
                        UserMix.FileCreate(fullPath, Model.Content);

                }
                else
                {
                    // update in database
                    comn.Content = Get_bytesFromStr(Model.Content);

                    if (!Debug)
                        context.SaveChanges();
                }


                return Return_withOK();
            }


            if (Model.TypeOperations == ETypeOperations.delete)
            {
                TaskComment comn = Get_itemTaskComn(Model.IdComment);

                if (! (bool)comn.CommentType)
                {
                    if (!Debug)
                        UserMix.FileDelete(Get_pathFromBytes(comn.Content));
                }

                context.Remove(comn);

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
