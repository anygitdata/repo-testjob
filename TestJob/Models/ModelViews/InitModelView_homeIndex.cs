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
        private List<ModelProjectMenu> lsDataServProc;
        
        private void LsDataProject(int id)
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

                if (id > 0 && key == id)
                {
                    projectView.ProjectId = item.Id;
                    projectView.ProjectName = item.ProjectName;

                    if (item.UpdateDate != null)
                        projectView.idUpdate = "on";

                    item.Disabled = "disabled";
                }
            }


            lsDataServProc = res;
        }

        private List<ModelTask> LsModelTasks(List<Task> tasks)
        {
            var lsTasks = (from ts in tasks
                        join pr in context.Set<Project>() on ts.ProjectId equals pr.Id

                        let dtCreate = Components_date.ConvDate_intoObj(ts.CreateDate)
                        let dtStart = Components_date.ConvDate_intoObj(ts.StartDate)
                        let dtUpdate = Components_date.ConvDate_intoObj(ts.UpdateDate)

                        select new ModelTask
                        {
                            Id = ts.Id,
                            ProjectId = pr.Id,
                            Ticket = $"({pr.ProjectName}) {ts.TaskName}",
                            Description = "empty",

                            Times = dtCreate.time,
                            Start = dtStart.time,
                            End = dtUpdate.time
                        }).ToList();

            var comn = context.Set<TaskComment>();
            foreach (ModelTask par in lsTasks)
            {
                var selComn = comn.Where(p => p.TaskId == par.Id).FirstOrDefault();
                par.lineThrough = lsDataServProc.FirstOrDefault(p => p.Id == par.ProjectId).LineThrough;

                if (selComn == null) continue;

                var descr = UserMix.Enc_GetStrFromBytes(selComn.Content);

                par.Description = descr.Length > 20 ? descr.Substring(0, 20) + " ..." : descr;
            }


            return lsTasks;
        }

        public InitModelView_homeIndex(DataContext cont, IAnyUserData anyUserData, int id)
        {
            context = cont;

            List<Task> tasks = null;

            LsDataProject(id);  // init lsDataServProc

            if (id > 0)
            {
                if (lsDataServProc.Count < id)
                {
                    redirect = "/";
                    return;
                }


                lsDataServProc.Insert(0, new ModelProjectMenu { Key = 0, ProjectName = "All projects" });

                tasks = cont.Set<Task>().Where(p => p.ProjectId == projectView.ProjectId).ToList() ;
            }
            else
                tasks = cont.Set<Task>().ToList();


            List<ModelTask> lsTasks = LsModelTasks(tasks.ToList());

            content_TableModel = new Content_TableTask
            {
                LsProjects = lsDataServProc,    // list for projectMenu
                LsTaskCont = lsTasks,           // list for content table task

                projectName = projectView.ProjectName ?? "All projects",    // for selected project
                projectId = projectView.ProjectId.ToString(),               // for selected project
                
                idUpdate = projectView.idUpdate,            // project Completion ID 
                debug = anyUserData.GetSettingsExt.StrDebug,
                
                numItem = id
            };


        }


    }
}
