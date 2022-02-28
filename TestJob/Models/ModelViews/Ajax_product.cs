using System;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public class Ajax_product: IdentResult
    {
        public string projectId { get; set; }
        public string projectName { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public Project objProduct { get; set; }


        public static void VerifyData(DataContext context, Ajax_product model)
        {
            if (string.IsNullOrEmpty(model.date)
                    || string.IsNullOrEmpty(model.time))
            {
                model.Result = IdentResult.Error;
                model.Message = "Fill fields date, time";
                return;
            }

            Components_date resData = Components_date.ConvStr_intoObj(model.date, model.time);

            if (resData.Result == IdentResult.Error)
            {
                model.Result = IdentResult.Error;
                model.Message = "Check date and time fields";
                return;
            }

            // -------- End of the block of basic checks -------------


            // for post request
            if ( string.IsNullOrEmpty(model.projectId) || model.projectId == Guid.Empty.ToString())
            {
                if ( string.IsNullOrEmpty(model.projectName)) 
                {
                    model.Result = IdentResult.Error;
                    model.Message = "Fill field projectName";

                    return; 
                }

                Project newProject = new Project
                {
                    Id = Guid.NewGuid(),
                    ProjectName = model.projectName,
                    CreateDate = (DateTime)resData.resDate
                };

                model.projectId = newProject.Id.ToString();
                model.objProduct = newProject;
                model.Result = IdentResult.Ok;
                
                return;
            }
            else   // for put request
            {
                Project project = context.Set<Project>().Find(Guid.Parse(model.projectId));

                if (project == null)
                {
                    model.Result = IdentResult.Error;
                    model.Message = "Project not exist";

                    return;
                }

                if (project.UpdateDate != null)
                {
                    model.Result = IdentResult.Error;
                    model.Message = "Project closed";

                    return;
                }

                if (project.CreateDate > (DateTime)resData.resDate)
                {
                    model.Result = IdentResult.Error;
                    model.Message = "UpdateDate must be greater than CreateDate";

                    return;
                }

                project.UpdateDate = (DateTime)resData.resDate;

                if (!string.IsNullOrEmpty(model.projectName) &&
                    model.projectName != project.ProjectName)
                {
                    project.ProjectName = model.projectName;
                }


                model.Result = IdentResult.Ok;
                model.objProduct = project;

                return; 
            }

        }


        public static Ajax_product InitData(DataContext context, Guid Id)
        {
            Ajax_product res = new Ajax_product { projectId = Guid.Empty.ToString() };

            if (Id != Guid.Empty)
            {
                Project project = context.Set<Project>().Find(Id);
                if (project != null)
                {
                    DateTime dtCreate = project.CreateDate;

                    Components_date comp_dateTime = Components_date.ConvDate_intoObj(dtCreate);

                    res = new Ajax_product
                    {
                        projectId = Id.ToString(),
                        projectName = project.ProjectName,
                        date = comp_dateTime.date,
                        time = comp_dateTime.time
                    };
                }
            }

            return res;
        }


        public static void ReloadModel(DataContext context, Ajax_product model, ETypeOperations typeOperation)
        {
            Project project = context.Set<Project>().Find( Guid.Parse(model.projectId));
            //string[] arDate;
            Components_date comp_dateTime; 

            if (project == null)
            {
                model.Result = IdentResult.Error;
                model.Message = "Error reload model";
                return;
            }


            if ((typeOperation & ETypeOperations.insert) > 0)
                comp_dateTime = Components_date.ConvDate_intoObj(project.CreateDate);
            else
                comp_dateTime = Components_date.ConvDate_intoObj((DateTime) project.UpdateDate);

            model.projectName = project.ProjectName;
            model.date = comp_dateTime.date; 
            model.time = comp_dateTime.time; 

            model.Result = IdentResult.Ok;
        }

    }
}
