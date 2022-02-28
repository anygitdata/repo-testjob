using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews;
using TestJob.Models.ModelViews.ProjectView;
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

        [HttpPost]
        public ActionResult AddProject([FromForm] BaseProjectView model)
        {
            var data = new GenProjectView_add(context, anyUserData, model);
            if (!data.VerifyData())
            {
                return Ok(model);
            }
                        
            data.SaveData();

            return Ok(model);

        }


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
