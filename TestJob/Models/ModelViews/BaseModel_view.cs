using TestJob.Models.Interface;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public class BaseModel_view: IdentResult
    {
        private static bool idInit = false;


        private static bool _debug;
        public static bool Debug{
            get { return _debug; } }
        // ----------------------------

        private static string _pathTxt;
        public static string BaseModel_GetPathTxt { 
            get { return _pathTxt; } }
        // ----------------------------------------


        public static void Set_IAnyUserData(DataContext context, IAnyUserData anyData)
        {
            if (!idInit)
            {
                //_dataCont = context;

                _pathTxt = anyData.PathDir_txt;
                _debug = anyData.GetDebug;

                idInit = true;
            }
        }



        // -------------------------------
        public object ObjResult { get; set; }
    }
}
