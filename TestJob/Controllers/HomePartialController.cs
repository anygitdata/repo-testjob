using Microsoft.AspNetCore.Mvc;
using System;
using TestJob.Models.ModelViews;
using TestJob.Models.UserAPI;

namespace TestJob.Controllers
{
    public partial class HomeController : Controller
    {

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


        [HttpDelete("del-comment/{id}")]
        public IActionResult DelTaskComment(string id)
        {
            var model = new TaskComment_ModelView(id);
            model.TypeOperations = Models.ETypeOperations.delete;


            var res = new GenModelViewComn(context, anyUserData, model);

            if (!res.VerifyData())
            {
                return Ok(res.BasicData);
            }

            res.SaveDataModel();

            return Ok(res.BasicData);
        }


        [HttpPut("upd-comment/{id}")]
        public IActionResult UpdTaskComment(TaskComment_ModelView model)
        {
            model.TypeOperations = Models.ETypeOperations.update;

            var res = new GenModelViewComn(context, anyUserData, model);

            if (!res.VerifyData())
            {
                return Ok(res.BasicData);
            }

            res.SaveDataModel();

            return Ok(model);
        }



        /// <summary>
        /// use Ajax 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddTaskComment([FromForm] TaskComment_ModelView model)
        {
            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();

            if (res.Result == IdentResult.Error)
            {
                model.Result = res.Result;
                model.Message = res.Message;
                return Ok(model);
            }

            if (res.Debug)
                return Ok(model);

            res.SaveDataModel();

            return Ok(model);
        }


    }
}
