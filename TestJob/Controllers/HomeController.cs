﻿using Microsoft.AspNetCore.Mvc;
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


        //[HttpGet("updtask/{id}")]
        //public ActionResult UpdTask(Guid id)
        //{
        //    ViewBag.debug = anyUserData.GetSettingsExt.StrDebug;

        //    var data = new GenTaskView_update(context, anyUserData, id);

        //    ViewBag.dataProject = data.ViewBag_data;

        //    return View(data.Model);
        //}

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


        [HttpGet("testjs")]
        public IActionResult TestJS()
        {
            return View();
        }

    }
}
