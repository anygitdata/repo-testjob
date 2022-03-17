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
    [Route("api/[controller]")]
    public partial class RestController : Controller
    {
               

        readonly string _PathDir_txt;
        public string PathDir_txt { get { return _PathDir_txt; } }

        //readonly IAnyUserData anyUserData;

        public RestController(IAnyUserData userData)
        {
            _PathDir_txt = userData.PathDir_txt;

            //anyUserData = userData;   

            //BaseModel_view.Set_IAnyUserData(context, userData);
        }
       

        [HttpGet("file/{file}")]
        public ActionResult  Get_dataFromFile(string file)
        {
            string fromFile = UserMix.FileDownload(PathDir_txt, file);

            var res = new BaseResult { FileName = file};


            if (string.IsNullOrEmpty(fromFile))
            {
                res.Result = BaseResult.Error;
                res.Message = "File not exists";
                return NotFound(res);
            }
            
            res.Data = fromFile;
                
            return Ok(res);

        }
        


        [HttpDelete("file/{file}")]
        public ActionResult Delete_File(string file)
        {
            bool resDel = UserMix.FileDelete(PathDir_txt, file);

            var result = new BaseResult { FileName = file,
                Result = resDel ? BodyRequest.Ok : BodyRequest.Error,
                Message = !resDel ? $"Нет файла {file}" : $"{file} was deleted"
            };

            return Ok(result);
        }


    }
}
