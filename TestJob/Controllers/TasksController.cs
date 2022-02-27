using Microsoft.AspNetCore.Mvc;
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


        [HttpPost]
        public ActionResult CreateTask([FromForm] GenTaskViewExt model)
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
