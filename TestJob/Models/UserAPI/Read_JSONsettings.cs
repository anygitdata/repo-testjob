using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestJob.Models.UserAPI
{
    /// <summary>
    /// Считывание файла настроек Settings.json
    /// </summary>
    public class Read_JSONsettings
    {
        public static DataSettings Get_JSONsettings(string pathTxt)
        {
            string dirSettings = UserDir_path.PathDir_Settings(pathTxt);
            string pathFull = Path.Combine(pathTxt, "settings.json");

            string strJSON = UserMix.FileDownload(pathFull);
            DataSettings res = JsonConvert.DeserializeObject<DataSettings>(strJSON);

            return res;
        }
    }
}
