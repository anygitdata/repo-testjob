using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews;
using TestJob.Models.UserAPI;

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
        public ActionResult CreateTask([FromForm] TaskStart model)
        {
            switch (model.OperTask.ToString())
            {
                case "create":
                    TaskCreate.VerifyData(context, model as TaskCreate);
                    break;
                case "start":
                    TaskStart.VerifyData(context, model);
                    break;
            }


            if (model.Result == IdentResult.Error)
                return Ok(model);

            if (!TaskCreate.Debug)
            {
                context.Add(model.ObjTask);
                context.SaveChanges();
            }


            return Ok(model);
        }


    }
}
