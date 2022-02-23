using Microsoft.AspNetCore.Hosting;
using System.IO;
using TestJob.Models.UserAPI;

namespace TestJob.Models.Interface
{
    public class AnyUserData: IAnyUserData
    {
        public bool Debug { get => GetSettingsExt.Debug; }
        public int MaxSizeFile { get => GetSettingsExt.MaxSizeFile; }


        readonly string _pathDir_txt;
        public string PathDir_txt { get => _pathDir_txt; }

        //private readonly DataSettingsExt _dataSettingsExt;
        public DataSettingsExt GetSettingsExt { get =>
                new DataSettingsExt(PathDir_txt); } 

        // ----------------------------------------------


        public AnyUserData(IWebHostEnvironment environment)
        {
            _pathDir_txt = Path.Combine(environment.WebRootPath, "txt");
            //DataSettings _dataSettings = DataSettings_read.GetSettings(PathDir_txt);

            //_dataSettingsExt = new DataSettingsExt(PathDir_txt);
        }


    }
}
