using System.Collections.Generic;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews.Templ;

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
            Init_lstModelView(id);
        }

        private List<TaskComment_ModelView> _lstModelView;
        public List<TaskComment_ModelView> LstModelView {get => _lstModelView; }
                

    }
}
