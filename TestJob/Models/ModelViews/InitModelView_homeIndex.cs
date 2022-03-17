using System;
using System.Collections.Generic;
using System.Linq;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews.ProjectView;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public class InitModelView_homeIndex
    {
        public readonly string redirect = "";

        public readonly Content_TableTask content_TableModel;

        public readonly BaseProjectView projectView =
            new BaseProjectView { TypeOperations = ETypeOperations.insert.ToString() };


        private readonly DataContext context;
        private List<ModelProjectMenu> lsModelProjects;
        private string fullTime;

        private void LsModelProjects(int id)
        {
            int key = 1;
            var res = (from pr in context.Set<Project>()
                       let upDate = pr.UpdateDate
                       select new ModelProjectMenu
                       {
                           Id = pr.Id,
                           ProjectName = pr.ProjectName,
                           CreateDate = pr.CreateDate,
                           UpdateDate = upDate,
                           LineThrough = upDate != null ? "lineThrough" : ""
                       }).ToList();


            foreach (var item in res)
            {
                item.Key = key++;

                if (id > 0 && item.Key == id)
                {
                    projectView.ProjectId = item.Id;
                    projectView.ProjectName = item.ProjectName;

                    if (item.UpdateDate != null)
                        projectView.idUpdate = "on";

                    item.Disabled = "disabled";  // use style disabled
                }
            }


            lsModelProjects = res;
        }

        private List<ModelTask> LsModelTasks(List<Task> tasks, int id)
        {
            TimeSpan  _tSpan = default;

            var lsTasks = (from ts in tasks.Where(p=> p.CancelDate == null)
                           join pr in context.Set<Project>() on ts.ProjectId equals pr.Id

                           let dtCreate = Components_date.ConvDate_intoObj(ts.CreateDate)
                           let dtStart = Components_date.ConvDate_intoObj(ts.StartDate)
                           let dtUpdate = Components_date.ConvDate_intoObj(ts.UpdateDate)

                           select new ModelTask
                           {
                               Id = ts.Id,
                               ProjectId = pr.Id,
                               Ticket = id == 0 ? $"({pr.ProjectName}) {ts.TaskName}" : ts.TaskName,
                               Description = "empty",

                               Times = dtCreate.time,
                               Start = dtStart.time,
                               End = string.IsNullOrEmpty(dtUpdate.time) ? "NotCompl" : dtUpdate.time,
                               StartDate = ts.StartDate,
                               EndDate = ts.UpdateDate
                           }).ToList();

            var comn = context.Set<TaskComment>();
            foreach (ModelTask par in lsTasks)
            {
                if (par.EndDate != null)                    
                    _tSpan += (DateTime)par.EndDate - (DateTime)par.StartDate;


                var selComn = comn.Where(p => p.TaskId == par.Id).FirstOrDefault();
                par.lineThrough = lsModelProjects.FirstOrDefault(p => p.Id == par.ProjectId).LineThrough;

                if (selComn == null) continue;

                var descr = UserMix.Enc_GetStrFromBytes(selComn.Content);

                par.Description = descr.Length > 20 ? descr.Substring(0, 20) + " ..." : descr;
            }


            // Total time for all tasks
            if (_tSpan != default)
            {
                if (_tSpan.Days > 0)
                    fullTime = $"{_tSpan.Days} days " + _tSpan.ToString(@"hh\:mm");
                else
                    fullTime = _tSpan.ToString(@"hh\:mm");
            }            
            else
                fullTime = "Tasks in progress ";


            return lsTasks;

        }

        public InitModelView_homeIndex(DataContext cont, IAnyUserData anyUserData, int id)
        {
            context = cont;

            List<Task> tasks = null;

            LsModelProjects(id);  // init lsDataServProc

            if (id > 0)
            {
                if (lsModelProjects.Count < id)
                {
                    redirect = "/";
                    return;
                }


                lsModelProjects.Insert(0, new ModelProjectMenu { Key = 0, ProjectName = "All projects" });

                tasks = cont.Set<Task>().Where(p => p.ProjectId == projectView.ProjectId).ToList();
            }
            else
                tasks = cont.Set<Task>().ToList();


            List<ModelTask> lsTasks = LsModelTasks(tasks.ToList(), id);

            content_TableModel = new Content_TableTask
            {
                LsProjects = lsModelProjects,    // list for projectMenu
                LsTaskCont = lsTasks,           // list for content table task

                projectName = projectView.ProjectName ?? "All projects",    // for selected project
                projectId = projectView.ProjectId.ToString(),               // for selected project

                idUpdate = projectView.idUpdate,            // project Completion ID 
                debug = anyUserData.GetSettingsExt.StrDebug,

                numItem = id,
                FullTime = fullTime
            };


        }


    }
}
