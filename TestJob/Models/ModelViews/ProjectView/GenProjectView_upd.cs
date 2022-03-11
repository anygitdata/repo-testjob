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


        private Project project;

        public override bool SaveData()
        {
            if (Result == Error)
                return false;

            if (!Debug)
            {               
                project.UpdateDate = Get_DateTime_fromModel(TEmodel.ext);
                //project.ProjectName = Model.ProjectName;

                if (!Debug)
                {                    
                    context.SaveChanges();                    
                }
            }

            return Return_withOK();
        }

        public override bool VerifyData()
        {
            project = context.Set<Project>().Find(Model.ProjectId);

            if (project == null)
            {
                return Return_withEROR("Project not found");
            }

            if (!VerifyDateTime(TEmodel.gen))
            {
                return Return_withEROR("Date or time component errors");
            }

            return Return_withOK();
        }

    }
}
