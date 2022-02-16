using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestJob.Models.UserAPI
{
    /// <summary>
    /// Формирует path к произвольной директории
    /// Стартовой является директория wwwroot/txt
    /// </summary>
    public class UserDir_path
    {
        public static string PathDir_Settings(string pathDirTxt)
        {
            return Path.Combine(pathDirTxt, @"..\", "Settings");
        }

        public static string PathDir_sql(string pathDirTxt)
        {
            return Path.Combine(pathDirTxt, @"..\", "sql");
        }
    }
}
