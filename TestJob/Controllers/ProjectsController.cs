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
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {

        readonly IAnyUserData anyUserData;
        readonly DataContext context;

        public ProjectsController(DataContext cont, IAnyUserData userData)
        {
            anyUserData = userData;
            context = cont;
        }

        // ----------------------------------------


        [HttpPut]
        public ActionResult UpdProject(Ajax_product model)
        {
            Ajax_product.VerifyData(context, model);

            if (model.Result == IdentResult.Error)
            {
                return Ok(model);
            }

            context.SaveChanges();

            Ajax_product.ReloadModel(context, model, ETypeOperations.update);


            return Ok(model);
        }


    }
}
