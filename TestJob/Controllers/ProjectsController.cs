using Microsoft.AspNetCore.Mvc;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews.ProjectView;

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
        public ActionResult AddProject(BaseProjectView model)
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
        public ActionResult UpdProject(BaseProjectView model)
        {
            var data = new GenProjectView_upd(context, anyUserData, model);
            if (!data.VerifyData())
            {
                return Ok(model);
            }

            data.SaveData();

            return Ok(model);
            
        }


    }
}
