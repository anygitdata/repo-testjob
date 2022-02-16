using Microsoft.EntityFrameworkCore;
using System.IO;
using TestJob.Models.UserAPI;

namespace TestJob.Models
{
    public partial class SeedData
    {
        static partial void InitializationServProcedure(DataContext cont, string PathDir_txt)
        {

            var res = UserMix.File_GetFilesSQL(PathDir_txt);

            for (int i = 0; i < res.Length; i++)
            {
                string nameProc = Path.GetFileNameWithoutExtension(res[i]);

                string sqlScript = LoadSql_script.LoadScript(res[i]);
                string sqlDrop = $"DROP PROCEDURE if exists [{nameProc}];";


                cont.Database.ExecuteSqlRaw(sqlDrop);
                cont.Database.ExecuteSqlRaw($"{sqlScript}");

            }

        }
    }
}
