using System;
using TestJob.Models.Interface;

namespace TestJob.Models.ModelViews.TaskView
{
    public abstract class GenTaskView_modf: GenTaskView_templ<GenTaskView>
    {
        public GenTaskView_modf(DataContext cont, IAnyUserData userData, GenTaskView model) : base(cont, userData)
        {
            Model = model;

            var pr = context.Set<Project>().Find(Model.ProjectId);

            var compDateTime = Components_date.ConvDate_intoObj(pr.CreateDate);
            _viewBag_data = new ViewBag_data
            {
                ProjectId = Model.ProjectId,
                ProjectName = pr.ProjectName,
                Date = compDateTime.date,
                Time = compDateTime.time
            };
        }


        protected virtual bool Return_withEROR(string err)
        {
            Model.Result = Error;
            Model.Message = err;

            Result = Error;
            Message = err;

            return false;
        }


        private ViewBag_data _viewBag_data;
        public override ViewBag_data ViewBag_data { get => _viewBag_data; }

        protected DateTime Get_DateTime_fromModel()
        {
            var res = Components_date.ConvStr_intoObj(Model.Date, Model.Time);
            return res.resDate;
        }

        public override bool SaveData()
        {
            throw new NotImplementedException();
        }

        public override bool VerifyData()
        {
            throw new NotImplementedException();
        }
    }
}
