using Microsoft.AspNetCore.Mvc;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews;
using TestJob.Models.ModelViews.TaskView;

namespace TestJob.Controllers
{
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


        [HttpPost("createtask")]
        public ActionResult CreateTask([FromForm] GenTaskView model)
        {
            //switch (model.OperTask.ToString())
            //{
            //    case "create":
            //        TaskCreate.VerifyData(context, model as TaskCreate);
            //        break;
            //    case "start":
            //        TaskStart.VerifyData(context, model);
            //        break;
            //}


            //if (model.Result == IdentResult.Error)
            //    return Ok(model);

            //if (!TaskCreate.Debug)
            //{
            //    context.Add(model.ObjTask);
            //    context.SaveChanges();
            //}


            return Ok(model);
        }


    }
}
