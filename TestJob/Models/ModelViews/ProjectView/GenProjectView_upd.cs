using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.Interface;

namespace TestJob.Models.ModelViews.ProjectView
{
    public class GenProjectView_upd: GenProjectView_templ
    {
        public GenProjectView_upd(DataContext cont, IAnyUserData userData, BaseProjectView model) : base(cont, userData, model)
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
                    CreateDate = Get_DateTime_fromModel(TEmodel.ext),
                    UpdateDate = default,
                    ProjectName = Model.ProjectName
                };

                if (!Debug)
                {
                    context.Add(project);
                    context.SaveChanges();
                    Model.ProjectId = project.Id;
                }
            }


            return Return_withOK();
        }

        public override bool VerifyData()
        {
            if (!VerifyDateTime(TEmodel.gen))
            {
                return Return_withEROR("Date or time component errors");
            }


            return Return_withOK();
        }

    }
}
