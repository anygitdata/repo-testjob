using System;
using System.IO;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews.ComnView
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
        protected virtual bool Return_withOK()
        {
            Result = Ok;
            Message = Ok;
            return true;
        }
        protected virtual bool Return_withEROR(string err, bool saveCont=false)
        {
            Result = Error;
            Message = err;
            return false;
        }

        protected static string Get_StrFromByte(byte[] arg)
        {
            return UserMix.Enc_GetStrFromBytes(arg);
        }
        protected static byte[] Get_bytesFromStr(string arg)
        {
            return UserMix.Enc_GetBytesFromStr(arg);
        }

        protected static string Get_ComntFromFile(string pathTxt, byte[] arg)
        {
            string fileName = Get_StrFromByte(arg);
            string fullPath = Path.Combine(pathTxt, fileName);
            string res = UserMix.FileDownload(fullPath);

            return string.IsNullOrEmpty(res) ? "No comment file" : res;
        }


        public virtual TModel Model { get; set; }       // ajax model
        public virtual TResult ModelRes { get; set; }    // for save into database
        public virtual TView ModelView { get; set; }   // for ViewBag


        protected abstract string Get_pathFromBytes(byte[] arg);
        protected abstract string Get_pathFromStr(string arg);

        protected abstract string LoadFileTxt(string arg);
        public abstract bool VerifyData();
        public abstract bool SaveDataModel();
        public abstract bool Init_model();

    }
}
