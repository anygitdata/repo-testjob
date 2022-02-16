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
            if (model.TypeOperations == ETypeOperations.insert)
            {
                Init_model();
            }
        }


    }
}
