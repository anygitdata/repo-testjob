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


        protected override string LoadFileTxt(string file)
        {
            return UserMix.FileDownload(pathTxt, file);
        }

        protected override string Get_pathFromBytes(byte[] arg)
        {
            string file = Get_StrFromByte(arg);

            return Path.Combine(pathTxt, file);
        }


        private List<TaskComment_ModelView> _lstModelView;
        public List<TaskComment_ModelView> LstModelView {get => _lstModelView; }

        private TaskComment Get_itemTaskComn(string id)
        {
            return context.Set<TaskComment>().Find(Guid.Parse(id));
        }


    }
}
