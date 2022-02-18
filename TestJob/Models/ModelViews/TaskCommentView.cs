using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{

    public class TaskComment_ModelView : IdentResult
    {
        public string IdComment { get; set; }
        public string TaskId { get; set; }

        public string Content { get; set; }
        public bool ContentType { get; set; }
        public IFormFile postedFile { get; set; }

        public string StrFileName { get; set; }

        public bool Debug { get; set; }

        public ETypeOperations TypeOperations { get; set; }
    }



        public class TaskCommentView: BaseModel_view
    {
        public string IdComment { get; set; }
        public string TaskId { get; set; }

        public string Content { get; set; }
        public bool ContentType { get; set; }
        public IFormFile postedFile { get; set; }

        //public bool Debug { get; set; }
       
        public ETypeOperations TypeOperations { get; set; }

        // ----------------------------------------------
               

        private static BaseModel_view InitResult_fromModel(TaskCommentView model)
        {
            var res = new BaseModel_view
            {
                Result = model.Result,
                Message = model.Message
            };

            return res;
        }


        public static TaskCommentView InitModel_forView(DataContext context, 
            IAnyUserData anyUserData, Guid id, out AnyData_Comment anyDataOut)
        {
            var data = (from tsk in context.Tasks.Where(p => p.Id == id)
                        from pr in context.Projects.Where(p => p.Id == tsk.ProjectId)
                        select new { pr.ProjectName, tsk.Id, tsk.TaskName, tsk.StartDate })
                      .ToList().FirstOrDefault();


            var model = new TaskCommentView
            {
                IdComment = "",
                TaskId = data.Id.ToString(),
                TypeOperations = ETypeOperations.insert,
                ContentType = false,
                postedFile = null,

            };

            var compStart =
                Components_date.ConvDate_intoObj(data.StartDate);

            anyDataOut = new AnyData_Comment
            {
                ProjectName = data.ProjectName,
                TaskName = data.TaskName,
                Date = compStart.date,
                Time = compStart.time,
                maxSizeFile = anyUserData.GetSettingsExt.MaxSizeFile
            };


            return model;
        }


        /// <summary>
        /// The verification result is recorded in the ObjTaskComment property 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void VerifyComment(DataContext context, IAnyUserData anyUserData, TaskCommentView model)
        {
            model.Result = Error;
            model.Message = "From verify procedure";


            if ( string.IsNullOrEmpty(model.Content))
            {
                model.Result = Error;
                model.Message = "Not data for Content";
                return;
            }

            if ((model.TypeOperations | ETypeOperations.insert)>0)
            {
                if (!model.ContentType)
                {
                    if (model.postedFile == null)
                    {
                        model.Result = Error;
                        model.Message = "postedFile not data";
                        return;
                    }

                    if ( Path.GetExtension(model.postedFile.Name) != "txt")
                    {
                        model.Result = Error;
                        model.Message = "This is not a text file";
                        return;
                    }

                    if (model.postedFile.Length > anyUserData.GetSettingsExt.MaxSizeFile)
                    {
                        model.Result = Error;
                        model.Message = "big file";
                        return;
                    }
                }

                TaskComment comn = new TaskComment
                {
                    Id = Guid.NewGuid(),
                    CommentType = model.ContentType,
                    Content = UserMix.Enc_GetBytesFromStr(model.Content)
                };

                model.ObjResult = comn;
                model.IdComment = comn.Id.ToString();

                model.Result = Ok;
                model.Message = "";
            }

        }


        public static TaskComment Init_TaskComment(TaskCommentView model)
        {
            byte[] byteContent = null; // = UserMix.Enc_GetBytesFromStr(model.Content);

            TaskComment taskComn = new TaskComment
            {
                Id = Guid.NewGuid(),
                CommentType = model.ContentType,                
                TaskId = Guid.Parse(model.TaskId)
            };
            
            // use file
            if (!model.ContentType)
            {
                string fileName = model.postedFile.FileName;
                byteContent = UserMix.Enc_GetBytesFromStr(fileName);

                UserMix.FileDelete(BaseModel_GetPathTxt, fileName);

                string path = Path.Combine(BaseModel_GetPathTxt, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    model.postedFile.CopyTo(fileStream);
                }
            }
            else
            {
                byteContent = UserMix.Enc_GetBytesFromStr(model.Content);
            }

            taskComn.Content = byteContent;

            return taskComn;
        }

        public static BaseModel_view VerifyData(DataContext context, 
            TaskCommentView model)
        {
            BaseModel_view res = null;
            TaskComment taskComn = null;

            if (string.IsNullOrEmpty(model.TaskId))
            {
                model.Result = Error;
                model.Message = "No task selected";

                return InitResult_fromModel(model);
            }

            
            if ((model.TypeOperations | ETypeOperations.insert) > 0)
            {
                // for new record
                if ( string.IsNullOrEmpty(model.Content))
                {
                    model.Result = Error;
                    model.Message = "Comment field is empty ";

                    return InitResult_fromModel(model);
                }


                if ( !model.ContentType 
                        && Path.GetExtension(model.postedFile.FileName) != "txt")
                {
                    model.Result = Error;
                    model.Message = "Only text files ";

                    return InitResult_fromModel(model);
                }


                taskComn = Init_TaskComment(model);

                res = InitResult_fromModel(model);
                res.ObjResult = taskComn;

                return res;
            }

            return res;

        }
    }




}
