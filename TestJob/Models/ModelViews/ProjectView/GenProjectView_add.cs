using System;
using TestJob.Models.Interface;

namespace TestJob.Models.ModelViews.ProjectView
{
    public class GenProjectView_add: GenProjectView_templ
    {
        public GenProjectView_add(DataContext cont, IAnyUserData userData, BaseProjectView model) : base(cont, userData, model)
        {  }

        public override bool SaveData()
        {
            if (Result == Error)
                return false;

            if (!Debug)
            {
                var project = new Project
                {
                    Id = Guid.NewGuid(),
                    CreateDate = Get_DateTime_fromModel(TEmodel.gen),
                    UpdateDate = default,
                    ProjectName = Model.ProjectName
                };

                context.Add(project);
                context.SaveChanges();
                Model.ProjectId = project.Id;
            }

            return Return_withOK();
        }

        public override bool VerifyData()
        {
            if (!VerifyDateTime(TEmodel.gen))
            {
                return Return_withEROR("Date or time component errors");
            }

            if (string.IsNullOrEmpty(Model.ProjectName))
                return Return_withEROR("The projectName field is not filled");


            return Return_withOK();
        }
    }
}
