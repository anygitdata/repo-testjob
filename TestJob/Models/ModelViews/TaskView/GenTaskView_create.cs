using System;
using TestJob.Models.Interface;

namespace TestJob.Models.ModelViews.TaskView
{
    public class GenTaskView_create: GenTaskView_templ<GenTaskViewExt>
    {

        public GenTaskView_create(DataContext cont, IAnyUserData userData) : base(cont, userData)
        { }

        public GenTaskView_create(DataContext cont, IAnyUserData userData, Guid id ): this(cont, userData)
        {
            Model = new GenTaskViewExt { ProjectId = id };

            Init_viewBag_data(id);            
        }


        public GenTaskView_create(DataContext cont, IAnyUserData userData, GenTaskViewExt model) : this(cont, userData)
        {
            Model = model;

            Init_viewBag_data(model.ProjectId);
        }


        private void Init_viewBag_data(Guid id)
        {
            var pr = context.Set<Project>().Find(id);
            var compDateTime = Components_date.ConvDate_intoObj(pr.CreateDate);

            _viewBag_data = new ViewBag_data
            {
                ProjectId = id,
                ProjectName = pr.ProjectName,
                Date = compDateTime.date,
                Time = compDateTime.time,
                Debug = Debug ? "on" : "off"
            };
        }


        private ViewBag_data _viewBag_data;
        public override ViewBag_data ViewBag_data { get => _viewBag_data; }

        protected DateTime Get_DateTime_fromModel(TEmodel typeModel)
        {
            Components_date compDate = default;

            switch (typeModel)
            {
                case TEmodel.gen:
                    compDate = Components_date.ConvStr_intoObj(Model.Date, Model.Time);
                    break;
                case TEmodel.ext:
                    compDate = Components_date.ConvStr_intoObj(Model.DateExt, Model.TimeExt);
                    break;
            }
            
            return compDate.resDate;
        }

        private bool VerifyDateTime(TEmodel typeModel)
        {
            Components_date compDate = default;                

            switch (typeModel)
            {
                case TEmodel.gen:
                    compDate = Components_date.ConvStr_intoObj(Model.Date, Model.Time);
                    break;
                case TEmodel.ext:
                    compDate = Components_date.ConvStr_intoObj(Model.DateExt, Model.TimeExt);
                    break;
            }
            

            if (compDate.Result == Error)
                return Return_withEROR(compDate.Message);


            return true;
        }

        public override bool VerifyData() 
        {
            if (!VerifyDateTime(TEmodel.gen) || !VerifyDateTime(TEmodel.ext ))
            {
                return Return_withEROR("Date or time component errors");
            }

            if (string.IsNullOrEmpty(Model.TaskName))
                return Return_withEROR("The taskName field is not filled ");


            return Return_withOK();
        }

        protected virtual bool Return_withEROR(string err)
        {
            Model.Result = Error;
            Model.Message = err;

            Result = Error;
            Message = err;

            return false;
        }

        public override bool SaveData()
        {
            if (Result == Error)
                return false;


            if (!Debug)
            {
                var task = new Task
                {
                    Id = Guid.NewGuid(),
                    ProjectId = Model.ProjectId,
                    CreateDate = Get_DateTime_fromModel(TEmodel.gen),
                    StartDate = Get_DateTime_fromModel(TEmodel.ext),
                    TaskName = Model.TaskName
                };

                if (!Debug)
                {
                    context.Add(task);
                    context.SaveChanges();
                    Model.TaskId = task.Id.ToString();
                }
            }


            return Return_withOK();
        }

    }
}
