using System;
using TestJob.Models.Interface;

namespace TestJob.Models.ModelViews.ProjectView
{
    public abstract class GenProjectView_templ: ComnTemplate
    {
        public GenProjectView_templ(DataContext cont, IAnyUserData userData, BaseProjectView model ) : base(cont, userData)
        {
            Model = model;
        }

        protected BaseProjectView Model { get; set; }

        // -------------------------

        protected virtual bool Return_withEROR(string err)
        {
            Model.Result = Error;
            Model.Message = err;

            Result = Error;
            Message = err;

            return false;
        }
        protected virtual bool Return_withOk()
        {
            base.Return_withOK();

            Model.Result = Ok;
            Model.Message = Ok;

            return true; 
        }


        protected bool VerifyDateTime(TEmodel typeModel)
        {
            Components_date compDate = default;

            switch (typeModel)
            {
                case TEmodel.gen:
                    compDate = Components_date.ConvStr_intoObj(Model.Date, Model.Time);
                    break;
                case TEmodel.ext:
                    compDate = Components_date.ConvStr_intoObj(Model.DateUpd, Model.TimeUpd);
                    break;
            }


            if (compDate.Result == Error)
                return Return_withEROR(compDate.Message);


            return true;
        }
        protected DateTime Get_DateTime_fromModel(TEmodel typeModel)
        {
            Components_date compDate = default;

            switch (typeModel)
            {
                case TEmodel.gen:
                    compDate = Components_date.ConvStr_intoObj(Model.Date, Model.Time);
                    break;
                case TEmodel.ext:
                    compDate = Components_date.ConvStr_intoObj(Model.DateUpd, Model.TimeUpd);
                    break;
            }

            return compDate.resDate;
        }


        // ------------------------
        public abstract bool VerifyData();

        public abstract bool SaveData();

    }
}
