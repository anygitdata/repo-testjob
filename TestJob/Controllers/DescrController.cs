using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews.ComnView;
using TestJob.Models.UserAPI;

namespace TestJob.Controllers
{
    [Route("api/[controller]")]
    public class DescrController : ControllerBase
    {

        readonly IAnyUserData anyUserData;
        readonly DataContext context;

        readonly string _PathDir_txt;
        string PathDir_txt { get { return _PathDir_txt; } }

        public DescrController(DataContext cont, IAnyUserData userData)
        {
            anyUserData = userData;
            context = cont;

            _PathDir_txt = anyUserData.PathDir_txt;
        }


        [HttpPost]
        public IActionResult AddDescr(TaskComment_ModelView model)
        {
            model.TypeOperations = ETypeOperations.insert;

            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();

            if (res.Result == IdentResult.Error)
            {
                model.Result = res.Result;
                model.Message = res.Message;
                return Ok(model);
            }

            res.SaveDataModel();

            return Ok(model);
        }


        //[HttpPut]
        //public IActionResult UpdateDescr(TaskComment_ModelView model)
        //{
        //    model.TypeOperations = ETypeOperations.update;

        //    try
        //    {
        //        var res = new GenModelViewComn(context, anyUserData, model);

        //        if (!res.VerifyData())
        //        {
        //            return Ok(res.BasicData);
        //        }

        //        res.SaveDataModel();

        //        return Ok(model);

        //    }
        //    catch (Exception ex)
        //    {
        //        model.Result = IdentResult.Error;
        //        model.Message = "Cancel operation update TaskComment";

        //        UserMix.File_Message_intoLog(PathDir_txt, "Cancel operation update TaskComment");
        //        UserMix.File_Message_intoLog(PathDir_txt, $"{ex.Message}");

        //        return Ok(model);
        //    }
        //}


        //[HttpDelete("{id}")]
        //public IActionResult DelDescr(string id)
        //{
        //    var model = new TaskComment_ModelView(id)
        //    {
        //        TypeOperations = ETypeOperations.delete
        //    };

        //    var res = new GenModelViewComn(context, anyUserData, model);

        //    if (!res.VerifyData())
        //    {
        //        return Ok(res.BasicData);
        //    }

        //    res.SaveDataModel();

        //    return Ok(res.BasicData);
        //}
    }
}
