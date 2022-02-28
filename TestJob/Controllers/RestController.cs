using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews;
using TestJob.Models.UserAPI;


namespace TestJob.Controllers
{
    [Route("api/controller")]
    public partial class RestController : Controller
    {

        readonly DataContext context;

        readonly string _PathDir_txt;
        public string PathDir_txt { get { return _PathDir_txt; } }

        //readonly IAnyUserData anyUserData;

        public RestController(DataContext cont, IAnyUserData userData)
        {
            context = cont;
            _PathDir_txt = userData.PathDir_txt;

            //anyUserData = userData;   

            //BaseModel_view.Set_IAnyUserData(context, userData);
        }


        


        


        [HttpGet("checkNameproject")]
        public ActionResult CheckNameProject(Ajax_request model)
        {
            bool resExist = context.Set<Project>().Any(p => p.ProjectName == model.strParam);

            model.Result = IdentResult.Ok;

            if (resExist)
                model.response = "exist";
            else
                model.response = "not exist";

            return Ok(model);
        }


        [HttpGet("dataproject/{id}")]
        public ActionResult Get_dataProject(Guid id)
        {
            Api_Ajax_contProject projDetail = new Api_Ajax_contProject();

            Project project = context.Set<Project>().Find(id);

            if (project == null)
            {
                projDetail.Result = IdentResult.Error;
                projDetail.Message = "No project data ";
                return Ok(projDetail);
            }

            DateTime dtCreate = project.CreateDate;
            DateTime? dtUpdate = null;

            if (project.UpdateDate != null)
                dtUpdate = project.UpdateDate;

            Components_date convDate = Components_date.ConvDate_intoObj(dtCreate);
            Components_date convDateUpd = null;
            if (dtUpdate != null)
                convDateUpd = Components_date.ConvDate_intoObj(dtCreate);

            projDetail = new Api_Ajax_contProject
            {
                projectId = id,
                projectName = project.ProjectName,
                date = convDate.date,
                time = convDate.time,
                dateUpdate = convDateUpd == null ? "" : convDateUpd.date ,
                timeUpdate = convDateUpd == null ? "" : convDateUpd.time,
                Result = IdentResult.Ok
            };

            return Ok(projDetail);
        }


        [HttpGet("file/{file}")]
        public ActionResult  Get_dataFromFile(string file)
        {
            string fromFile = UserMix.FileDownload(PathDir_txt, file);

            BaseResult res = new BaseResult { FileName = file};


            if (string.IsNullOrEmpty(fromFile))
            {
                res.Result = BaseResult.Error;
                res.Message = "File not exists";
                return NotFound(res);
            }
            
            res.Data = fromFile;
                
            return Ok(res);

        }


        [HttpPut("file")]
        public ActionResult update_File([FromForm] BaseResult model)
        {
            UserMix.FileUpdate(PathDir_txt, model);

            return Ok(model);
        }


        [HttpDelete("file/{file}")]
        public ActionResult Delete_File(string file)
        {
            bool resDel = UserMix.FileDelete(PathDir_txt, file);

            BaseResult result = new BaseResult { FileName = file,
                Result = resDel ? BodyRequest.Ok : BodyRequest.Error,
                Message = !resDel ? $"Нет файла {file}" : $"{file} was deleted"
            };

            return Ok(result);
        }


        [HttpPost("file")]
        public ActionResult Load_data([FromForm] BodyRequest model)
        {
            string _pathFile = string.Empty;

            if (!model.CommentType)
            {
                // false into FileName
                _pathFile = Path.Combine(PathDir_txt, model.FileName);

                if (UserMix.FileExists(_pathFile))
                {
                    model.Result = BodyRequest.Error;
                    model.Message = "File exists";
                    return Ok(model);
                }

                // load into database
            }

            UserMix.FileCreate(PathDir_txt, model);

            return Ok(model);
        }

    }
}
