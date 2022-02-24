using System;
using System.Collections.Generic;
using System.IO;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews.Templ;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public partial class GenModelViewComn
        : GeneralModelView_templ<TaskComment_ModelView, TaskComment, AnyData_Comment>
    {
        public GenModelViewComn(DataContext cont, IAnyUserData anyData,
            TaskComment_ModelView model) : base(cont, anyData, model)
        {
            Model = model;
            Return_withOK();
        }


        /// <summary>
        /// id -> Task.Id
        /// </summary>
        /// <param name="cont"></param>
        /// <param name="anyData"></param>
        /// <param name="model"></param>
        /// <param name="id"></param>
        public GenModelViewComn(DataContext cont, IAnyUserData anyData,
            string id) : this(cont, anyData, new TaskComment_ModelView())
        {
            Model = default;
            Init_lstModelView(id);
        }

        protected override bool Return_withOK() {
            base.Return_withOK();

            if (Model != null)
            {
                Model.Result = Result;
                Model.Message = Message;
            }

            return true;
        }

        private bool saveDataCont = false;
        protected override bool Return_withEROR(string err, bool saveCont = false)
        {
            base.Return_withEROR(err);

            saveDataCont = saveCont;

            if (Model != null)
            {
                Model.Result = Result;
                Model.Message = Message;
            }

            return false;
        }

        protected override string LoadFileTxt(string file)
        {
            return UserMix.FileDownload(pathTxt, file);
        }

        protected override string Get_pathFromBytes(byte[] arg)
        {
            string file = Get_StrFromByte(arg);

            return Path.Combine(pathTxt, file);
        }

        protected override string Get_pathFromStr(string arg)
        {
            return Path.Combine(pathTxt, arg);
        }

        public TaskComment_ModelView BasicData
        {
            get {
                var res = new TaskComment_ModelView()
                {
                    IdComment = Model.IdComment,
                    Result = Model.Result,
                    Message = Model.Message
                };

                if (saveDataCont)
                    res.Content = Model.Content;

                return res; 
            }

        }


        private List<TaskComment_ModelView> _lstModelView;
        public List<TaskComment_ModelView> LstModelView {get => _lstModelView; }

        private TaskComment Get_itemTaskComn(string id)
        {
            return context.Set<TaskComment>().Find(Guid.Parse(id));
        }


    }
}
