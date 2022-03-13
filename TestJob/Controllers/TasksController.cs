using Microsoft.AspNetCore.Mvc;
using System;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews;
using TestJob.Models.ModelViews.TaskView;
using TestJob.Models.UserAPI;

namespace TestJob.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {

        readonly IAnyUserData anyUserData;
        readonly DataContext context;

        public TasksController(DataContext cont, IAnyUserData userData)
        {
            anyUserData = userData;
            context = cont;
        }



        // -----------------------------------------

        [HttpPut("/{id}")]
        public ActionResult CancelTask(Guid id)
        {
            var data = new GenTaskView_cancel(context, anyUserData, id);

            return Ok(id);
        }


        [HttpPut]
        public ActionResult UpdTask(GenTaskView model)
        {
            var data = new GenTaskView_update(context, anyUserData, model);

            return Ok(model);
        }


        [HttpPost]
        public ActionResult CreateTask(GenTaskViewExt model)
        {
            var data = new GenTaskView_create(context, anyUserData, model);
            if (!data.VerifyData())
            {
                return Ok(model);
            }

            data.SaveData();
            if (data.Result == IdentResult.Ok && !anyUserData.Debug)
            {
                model.Redirect = $"/ins-comment/{model.TaskId}";
                return Ok(model);
            }

            return Ok(model);
        }

    }
}
