using System;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    /// <summary>
    /// Generic data processing template for ajax requests 
    /// </summary>
    public partial class GeneralModelView: IdentResult
    {

        protected bool _debug;
        public bool Debug { get => _debug; }

        public readonly string pathTxt;
        public readonly int maxSizeFile;

        public readonly IAnyUserData anyUserData;

        public readonly DataContext context;
        public readonly DataSettingsExt dataSettingsExt;

        public string debug_path_for_copy="";
        private string mesError_default = "Cancel operation";

        public GeneralModelView(DataContext cont, IAnyUserData anyData, 
            object model)
        {
            context = cont;
            pathTxt = anyData.PathDir_txt;
            anyUserData = anyData;
            _debug = anyUserData.Debug;

            dataSettingsExt = anyUserData.GetSettingsExt;
            maxSizeFile = anyUserData.GetSettingsExt.MaxSizeFile;

            Model = model;

            SetResultOptions();

            ModelRes = null;
            ModelView = null;
        }

        // ---------------------------------


        // only for testing
        public void SetDebug(bool arg)
        {
            _debug = arg;
        }

        protected void SetResultOptions()
        {
            Result = Error;
            Message = mesError_default;
        }

        public object Model { get; set; }       // ajax model
        public object ModelRes { get; set; }    // for save into database
        public object ModelView { get; set; }   // for ViewBag


        partial void VerifyData();              // validation of ajax model
        partial void Init_modelView(Guid id);   // data for the view 
        partial void Save_model();              // write model data to database 
        partial void Init_model();              // Loading model data from the database 
                                                // Run from Save_model()


        public void Run_Init_modelView(Guid id)
        {
            Init_modelView(id);
        }
        public void Run_VerifyData()
        {
            VerifyData();
        }
        public void Run_SaveModel()
        {
            Save_model();
        }

        
    }
}
