using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Models.UserAPI
{
    public class LoadSql_script
    {
        public static string LoadScript(string PathDir_txt, string file)
        {
            string pathDir = Path.Combine(PathDir_txt, @"..\","sql");
            string res = UserMix.FileDownload(pathDir, file);

            return res;
        }

        public static string LoadScript(string fullPathFile)
        {            
            string res = UserMix.FileDownload(fullPathFile);

            return res;
        }

    }
}
