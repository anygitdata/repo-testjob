using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Models.UserAPI
{
    /// <summary>
    /// Используется для считывания произДанных
    /// </summary>
    public class DataSettings
    {
        public string seedData { get; set; }
        public string debug { get; set; }
        public int maxSizeFile { get; set; }
        
    }


    public class DataSettingsExt: DataSettings
    {
        public DataSettingsExt(DataSettings arg)
        {
            seedData = arg.seedData;
            debug = arg.debug;
            maxSizeFile = arg.maxSizeFile;
        }

        public bool Debug { get => debug == "on" ? true : false; }
        public bool SeedData { get => seedData == "on" ? true : false; }
    }


    /// <summary>
    /// Считывание структуры settings
    /// if seedDat == on -> seedData = off
    /// </summary>
    public class DataSettings_read
    {
        public static DataSettings GetSettings(string pathTxt)
        {
            string pathJSON = UserDir_path.PathDir_Settings(pathTxt);
            string fullParhJSON = Path.Combine(pathJSON, "settings.json");
            string strJson = UserMix.FileDownload(fullParhJSON);

            var curSettings = JsonConvert.DeserializeObject<DataSettings>(strJson);

            // if curSettings.seedData == on -> set res.seedData = off
            if (curSettings.seedData == "on")
            {
                var defaultObj = JsonConvert.DeserializeObject<DataSettings>(strJson);
                defaultObj.seedData = "off";

                File.WriteAllText(fullParhJSON, JsonConvert.SerializeObject(defaultObj));
            }

            return curSettings;
        }


    }

}
