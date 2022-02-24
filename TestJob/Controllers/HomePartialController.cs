using Microsoft.AspNetCore.Mvc;
using System;
using TestJob.Models;
using TestJob.Models.ModelViews;
using TestJob.Models.UserAPI;

namespace TestJob.Controllers
{
    public partial class HomeController : Controller
    {

        [HttpGet("deldescr/{id}")]
        public IActionResult DelDescr(string id)
        {
            var model = new TaskComment_ModelView(id)
            {
                TypeOperations = ETypeOperations.delete
            };

            var res = new GenModelViewComn(context, anyUserData, model);

            if (!res.VerifyData())
            {
                return Ok(res.BasicData);
            }

            res.SaveDataModel();

            return Ok(res.BasicData);
        }


        [HttpPost("upddescr")]
        public IActionResult UpdDescr([FromForm] TaskComment_ModelView model)
        {
            model.TypeOperations = ETypeOperations.update;

            try
            {
                var res = new GenModelViewComn(context, anyUserData, model);

                if (!res.VerifyData())
                {
                    return Ok(res.BasicData);
                }

                res.SaveDataModel();

                return Ok(model);

            }
            catch (Exception ex)
            {
                model.Result = IdentResult.Error;
                model.Message = "Cancel operation update TaskComment";

                UserMix.File_Message_intoLog(PathDir_txt, "Cancel operation update TaskComment");
                UserMix.File_Message_intoLog(PathDir_txt, $"{ex.Message}");

                return Ok(model);
            }
        }


        [HttpGet("ins-comment/{id}")]
        public IActionResult AddTaskComment(Guid id)
        {
            var res = new GenModelViewComn(context, anyUserData, id.ToString() );

            var anyData = res.ModelView;
            var model = res.Model;


            ViewBag.anyData = anyData;
            ViewBag.LstModelView = res.LstModelView;

            return View(model);
        }

        
        [HttpPost("ins-comment")]
        public IActionResult AddTaskComment(TaskComment_ModelView model)
        {
            model.TypeOperations = Models.ETypeOperations.insert;

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


    }
}
