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

        public readonly BaseProjectView projectView;


        public InitModelView_homeIndex(DataContext context, IAnyUserData anyUserData, int id)
        {
            string projectId = "";
            string projectName = "All projects";
            string idUpdate = "off";
            DateTime dDefault = default;

            IQueryable<Task> tasks = default;

            ModelProjectMenu selectedProjectMenu = default;

            projectView = new BaseProjectView { TypeOperations = ETypeOperations.insert.ToString() };

            List<ModelProjectMenu> lsDataServProc = Load_fromServProc.Get_DataServProc(context, id);


            if (id > 0)
            {
                if (lsDataServProc.Count < id)
                {
                    redirect = "/";
                    return;
                }


                lsDataServProc.Insert(0, new ModelProjectMenu { key = 0, projectName = "All projects" });

                selectedProjectMenu =
                    lsDataServProc.FirstOrDefault(p => !string.IsNullOrEmpty(p.disabled));

                var selectedProj = context.Set<Project>().Find(selectedProjectMenu.id);

                projectId = selectedProj.Id.ToString();
                projectName = selectedProj.ProjectName;
                if (selectedProj.UpdateDate > dDefault)
                    idUpdate = "on";

                string[] dtime = ComnTemplate.Get_compDateTime_fromModel(selectedProj.CreateDate);
                projectView.ProjectId = selectedProj.Id;
                projectView.ProjectName = selectedProj.ProjectName;
                tasks = context.Set<Task>().Where(p => p.ProjectId == selectedProj.Id);
            }
            else
                tasks = context.Set<Task>();


            IEnumerable<ModelTask> lsTasks = (from ts in tasks
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
                par.lineThrough = lsDataServProc.FirstOrDefault(p => p.id == par.ProjectId).lineThrough;

                if (selComn == null) continue;

                var descr = UserMix.Enc_GetStrFromBytes(selComn.Content);

                par.Description = descr.Length > 20 ? descr.Substring(0, 20) + " ..." : descr;
            }


            content_TableModel = new Content_TableTask
            {
                LsProjects = lsDataServProc,    // list for projectMenu
                LsTaskCont = lsTasks,           // list for content table task
                projectName = projectName,      // for selected project
                projectId = projectId,          // for selected project
                idUpdate = idUpdate,            // project Completion ID 
                debug = anyUserData.GetSettingsExt.StrDebug,
                numItem = id
            };


        }


    }
}
