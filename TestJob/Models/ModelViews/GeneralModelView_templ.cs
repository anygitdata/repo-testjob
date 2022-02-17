using System;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews.Templ
{
    public abstract class GeneralModelView_templ<TModel, TResult, TView> : IdentResult
    {

        private bool _debug;
        public bool Debug { get => _debug; }

        // only for testing
        public string debug_path_for_copy = "";
        protected string mesError_default = "Cancel operation";


        public readonly string pathTxt;
        public readonly int maxSizeFile;

        public readonly IAnyUserData anyUserData;
        public readonly DataContext context;
        public readonly DataSettingsExt dataSettingsExt;

        public GeneralModelView_templ(DataContext cont, IAnyUserData anyData,
            TModel model)
        {
            context = cont;
            pathTxt = anyData.PathDir_txt;
            anyUserData = anyData;
            _debug = anyUserData.Debug;

            dataSettingsExt = anyUserData.GetSettingsExt;
            maxSizeFile = anyUserData.GetSettingsExt.MaxSizeFile;

            SetResult_forStart();

            Model = model;            
        }


        // only for testing
        public void SetDebug(bool arg)
        {
            _debug = arg;
        }

        protected void SetResult_forStart()
        {
            Result = Error;
            Message = mesError_default;
        }
        protected bool Return_withOK()
        {
            Result = Ok;
            Message = Ok;
            return true;
        }
        protected bool Return_withEROR(string err)
        {
            Result = Error;
            Message = err;
            return false;
        }


        public virtual TModel Model { get; set; }       // ajax model
        public virtual TResult ModelRes { get; set; }    // for save into database
        public virtual TView ModelView { get; set; }   // for ViewBag


        public abstract bool VerifyData();
        public abstract bool SaveDataModel();
        public abstract bool Init_model();

    }
}
