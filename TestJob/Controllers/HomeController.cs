using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews;
using TestJob.Models.ModelViews.ComnView;
using TestJob.Models.ModelViews.TaskView;

namespace TestJob.Controllers
{
    public partial class HomeController : Controller
    {
        readonly string _pathDir_txt;
        public string PathDir_txt { get => _pathDir_txt; }

        readonly DataContext context;
        readonly IAnyUserData anyUserData;

        public HomeController(DataContext cont, IAnyUserData userData)
        {
            _pathDir_txt = userData.PathDir_txt;
            context = cont;

            anyUserData = userData;

            //BaseModel_view.Set_IAnyUserData(context, anyUserData);
        }

        // --------------------------------------------------------------

        [HttpGet("ins-comment/{id}")]
        public IActionResult AddTaskComment(Guid id)
        {
            var res = new GenModelViewComn(context, anyUserData, id.ToString());

            var anyData = res.ModelView;
            var model = res.Model;


            ViewBag.anyData = anyData;
            ViewBag.LstModelView = res.LstModelView;
            ViewBag.test = anyUserData.GetSettingsExt.Test;

            return View(model);
        }


        [HttpGet("newtask")]
        public ActionResult NewTask()
        {
            // POST TasksController.CreateTask 

            var lsProj = context.Set<Project>().Where(p => p.UpdateDate == null)
                .Select(p => new Project { Id = p.Id, ProjectName = p.ProjectName }).ToList() ;

            ViewBag.debug = anyUserData.GetSettingsExt.StrDebug;
            ViewBag.lsProj = lsProj;

            return View("CreateTask02", new GenTaskViewExt());
        }

        

        // ----------------------------------

        [HttpGet("detaildebug")]
        public IActionResult DetailDebug()
        {
            return View();
        }


        public IActionResult Index(int id = 0)
        {
            var modelView = new InitModelView_homeIndex(context, anyUserData, id);

            if (modelView.redirect.Length > 0)
                return Redirect(modelView.redirect);


            ViewBag.projectView = modelView.projectView;
            ViewBag.Content_TableModel = modelView.content_TableModel;     // model for view

            ViewBag.test = anyUserData.GetSettingsExt.Test;

            return View();
        }


    }
}
