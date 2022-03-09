using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews;
using TestJob.Models.ModelViews.ComnView;
using TestJob.Models.ModelViews.ProjectView;
using TestJob.Models.ModelViews.TaskView;
using TestJob.Models.UserAPI;

namespace TestJob.Controllers
{
    public partial class HomeController : Controller
    {
        readonly string _PathDir_txt;
        public string PathDir_txt { get { return _PathDir_txt; } }

        readonly DataContext context;
        readonly IAnyUserData anyUserData;

        public HomeController(DataContext cont, IAnyUserData userData)
        {
            _PathDir_txt = userData.PathDir_txt;
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

            return View(model);
        }


        [HttpGet("createtask/{id}")]
        public ActionResult CreateTask(Guid id)
        {
            var model = new GenTaskView_create(context, anyUserData, id);

            ViewBag.anyData = model.ViewBag_data;

            return View(model.Model);
        }


        [HttpGet("newtask")]
        public ActionResult NewTask()
        {
            //var model = new GenTaskView_create(context, anyUserData, Guid.);

            var lsProj = context.Set<Project>().Where(p => p.UpdateDate == null)
                .Select(p => new Project { Id = p.Id, ProjectName = p.ProjectName }).ToList() ;

            ViewBag.debug = anyUserData.GetSettingsExt.StrDebug;
            ViewBag.lsProj = lsProj;

            return View("CreateTask02", new GenTaskViewExt());
        }


        [HttpGet("updtask/{id}")]
        public ActionResult UpdTask(Guid id)
        {
            ViewBag.debug = anyUserData.GetSettingsExt.StrDebug;

            var data = new GenTaskView_update(context, anyUserData, id);

            ViewBag.dataProject = data.ViewBag_data;

            return View(data.Model);
        }

        // ----------------------------------

        [HttpGet("detaildebug")]
        public IActionResult DetailDebug()
        {
            return View();
        }


        public IActionResult Index(int id = 0)
        {
            List<ModelProjectMenu> lsDataServProc =
                Load_fromServProc.Get_DataServProc(context, id);

            if (lsDataServProc.Count < id)
                return Redirect("/");

            string projectId = "";
            string projectName = "All projects";
            string idUpdate = "off";

            DateTime dDefault = default;

            ModelProjectMenu selProject = null;
            Guid selProjId = default;

            BaseProjectView projectView = new BaseProjectView
            {
                TypeOperations = ETypeOperations.insert.ToString()
            };
            IQueryable<Task> tasks = default;

            if (id > 0)
            {
                lsDataServProc.Insert(0, new ModelProjectMenu { key = 0, projectName = "All projects" });

                selProject =
                    lsDataServProc.FirstOrDefault(p => !string.IsNullOrEmpty(p.disabled));

                var prItem = context.Set<Project>().Find(selProject.id);

                projectId = prItem.Id.ToString();
                projectName = prItem.ProjectName;
                selProjId = selProject.id;
                if (selProject.updateDate > dDefault)
                    idUpdate = "on";

                string[] dtime = ComnTemplate.Get_compDateTime_fromModel(prItem.CreateDate);
                projectView.ProjectId = prItem.Id;
                projectView.ProjectName = prItem.ProjectName;

                tasks = context.Set<Task>().Where(p => p.ProjectId == selProjId);
            }
            else
                tasks = context.Set<Task>();

            // Filling content by tasks 
            List<ModelTask> lsTask = new();

            foreach (Task ts in tasks)
            {
                string descr = ts.TaskName;

                var lineThrough = lsDataServProc.FirstOrDefault(p => p.id == ts.ProjectId).lineThrough;

                if (descr.Length > 15)
                    descr = ts.TaskName.Substring(0, 15) + " ..."; // Line reduction 

                Components_date compCreate = Components_date.ConvDate_intoObj(ts.CreateDate);
                Components_date compStart = Components_date.ConvDate_intoObj(ts.StartDate);
                Components_date compUpdate = Components_date.ConvDate_intoObj(argDate: ts.UpdateDate);

                lsTask.Add(
                    new ModelTask
                    {
                        Ticket = ts.TaskName,
                        // Time for table columns 
                        Times = compCreate.time, // UserMix.ConvHour_into_str(ts.CreateDate),
                        Start = compStart.time,  // UserMix.ConvHour_into_str(ts.StartDate),
                        End = ts.UpdateDate == null ? ""
                            : compUpdate.time,   // UserMix.ConvHour_into_str((DateTime)ts.UpdateDate),

                        Description = descr,    //link
                        id = ts.Id,
                        lineThrough = lineThrough
                    });
            }

            Content_TableTask content_TableModel = new()
            {
                LsProjects = lsDataServProc,    // list for projectMenu
                LsTaskCont = lsTask,            // list for content table task
                projectName = projectName,      // for selected project
                projectId = projectId,          // for selected project
                idUpdate = idUpdate,            // project Completion ID 
                debug = anyUserData.GetSettingsExt.StrDebug,
                numItem = id
            };

            ViewBag.projectView = projectView;

            ViewBag.Content_TableModel = content_TableModel;     // model for view
            InsProjectView insProjecView = new(); // for modulDialot

            return View(insProjecView);
        }

        [HttpGet("testjs")]
        public IActionResult TestJS()
        {
            return View();
        }

    }
}
