using BaseSettingsTests.Tests;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.IO;
using TestJob.Controllers;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;
using Xunit;

namespace TestJob.Tests
{
    public class InitSQLscripts_from_file_Tests : IClassFixture<SharedDatabaseFixture>
    {
        readonly DataContext context;
        readonly RestController contrRest;
        public InitSQLscripts_from_file_Tests(SharedDatabaseFixture fixture)
        {
            context = fixture.CreateContext();

            string path = Directory.GetCurrentDirectory();
            string PathDir_txt = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\", "TestJob", "wwwroot", "txt"));

            var mock = new Mock<IAnyUserData>();
            mock.Setup(env => env.PathDir_txt).Returns(PathDir_txt);

            contrRest = new RestController(mock.Object);
        }

        [Fact]
        public void InitServProc_from_files_test()
        {
            // act
            var res = UserMix.File_AllFiles_SQL(contrRest.PathDir_txt);
            string[] file = new string[res.Length];

            for (int i = 0; i < res.Length; i++)
            {
                file[i] = Path.GetFileName(res[i]);
            }

            // assert
            for (int i = 0; i < res.Length; i++)
            {
                Assert.Equal(Path.GetFileName(res[i]), file[i]);
            }


            int num = 0;
            // Initialization servProcedure
            for (int i = 0; i < res.Length; i++)
            {
                string nameProc = Path.GetFileNameWithoutExtension(file[i]);
                string sqlScript = UserMix.FileDownload(res[i]);

                string sqlDrop = $"DROP PROCEDURE if exists [{nameProc}];";


                var resProc = context.Database.ExecuteSqlRaw(sqlDrop);
                Assert.Equal(resProc, -1);

                resProc = context.Database.ExecuteSqlRaw($"{sqlScript}");
                Assert.Equal(resProc, -1);

                num++; // number of iterations 
            }


            Assert.Equal(num, res.Length);

        }
    }
}
