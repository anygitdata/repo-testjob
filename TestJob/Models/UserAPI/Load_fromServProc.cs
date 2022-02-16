using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestJob.Models.ModelViews;

namespace TestJob.Models.UserAPI
{
    /// <summary>
    /// Loading data from a servProcedure -> sp_list_projects -> list<DataServProc>
    /// </summary>
    public class Load_fromServProc
    {

        public static List<ModelProjectMenu> Get_DataServProc(DataContext context, int id)
        {
            SqlParameter prId = new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = id
            };

            SqlParameter parRes = new SqlParameter
            {
                ParameterName = "@res",
                SqlDbType = System.Data.SqlDbType.VarChar,
                Size = 1000,
                Direction = System.Data.ParameterDirection.Output
            };

            context.Database.ExecuteSqlRaw("execute sp_list_projects @id, @res out;", prId, parRes);

            string json = parRes.Value.ToString();

            List<ModelProjectMenu> res = JsonConvert.DeserializeObject<List<ModelProjectMenu>>(json);

            return res;

        }


        //public static List<ProjectView> Get_ListProjectView(DataContext context, Guid id)
        //{
        //    List<ProjectView> res = new List<ProjectView>();
        //    var List_DataServProc = Get_DataServProc(context, id);
        //    foreach(DataServProc pr in List_DataServProc)
        //    {

        //        res.Add(
        //            new ProjectView {key=pr.key,   }
        //            );
        //    }


        //    return res;
        //}


    }
}
