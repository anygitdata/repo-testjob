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
        public string seedData;
        public int maxSizeFile;
        public string debug;
        public string test;
    }


    public class DataSettingsExt
    {

        public DataSettingsExt(string pathDir_txt)
        {
            var settings = DataSettings_read.GetSettings(pathDir_txt);

            _seedData = settings.seedData == "on";
            _debug = settings.debug == "on";
            _maxSizeFile = settings.maxSizeFile;

            _base_debug = settings.debug;
            _base_seedData = settings.seedData;

            _test = settings.test;
        }

        public DataSettingsExt(DataSettings arg)
        {
            _seedData = arg.seedData == "on";
            _debug = arg.debug =="on";
            _maxSizeFile = arg.maxSizeFile;

            _base_debug = arg.debug;
            _base_seedData = arg.seedData;
        }

        readonly bool _seedData;
        readonly bool _debug;
        readonly int _maxSizeFile;

        readonly string _base_seedData;
        readonly string _base_debug;
        readonly string _test;

        public string StrSeedData { get => _base_seedData; }
        public string StrDebug { get => _base_debug; }
        public string Test { get => _test; }

        public bool SeedData { get => _seedData; }
        public bool Debug { get => _debug; }
        public int MaxSizeFile { get => _maxSizeFile; }

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
