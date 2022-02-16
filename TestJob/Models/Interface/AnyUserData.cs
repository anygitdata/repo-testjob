using Microsoft.AspNetCore.Hosting;
using System.IO;
using TestJob.Models.UserAPI;

namespace TestJob.Models.Interface
{
    public class AnyUserData: IAnyUserData
    {
        
        public bool GetDebug { get => GetSettingsExt.Debug; }
        public int MaxSizeFile { get => GetSettingsExt.maxSizeFile; }


        private string _PathDir_txt;
        public string PathDir_txt { get => _PathDir_txt; }

        private DataSettingsExt _dataSettingsExt;
        public DataSettingsExt GetSettingsExt { get => _dataSettingsExt; }

        // ----------------------------------------------


        public AnyUserData(IWebHostEnvironment environment)
        {
            _PathDir_txt = Path.Combine(environment.WebRootPath, "txt");
            DataSettings _dataSettings = DataSettings_read.GetSettings(PathDir_txt);

            _dataSettingsExt = new DataSettingsExt(_dataSettings);
        }


    }
}
