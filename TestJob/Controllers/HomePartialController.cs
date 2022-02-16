using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TestJob.Models;
using TestJob.Models.ModelViews;
using TestJob.Models.UserAPI;

namespace TestJob.Controllers
{
    public partial class HomeController : Controller
    {

        [HttpGet("ins-comment/{id}")]
        public IActionResult AddTaskComment(Guid id)
        {
            var res = new GeneralModelView(context, anyUserData, new TaskComment_ModelView() );
            res.Run_Init_modelView(id);

            var anyData = res.ModelRes as AnyData_Comment;
            var model = res.ModelRes as TaskComment_ModelView;
            

            ViewBag.anyData = anyData;

            return View(model);
        }


        /// <summary>
        /// use Ajax 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddTaskComment([FromForm] TaskComment_ModelView model)
        {
            var res = new GeneralModelView(context, anyUserData, model);
            res.Run_VerifyData();

            if (res.Result == IdentResult.Error)
            {
                model.Result = res.Result;
                model.Message = res.Message;
                return Ok(model);
            }

            if (res.Debug)
                return Ok(model);

            res.Run_SaveModel();

            return Ok(model);
        }
        

    }
}
