using BaseSettingsTests.Tests;
using Moq;
using System.IO;
using System.Linq;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;
using Xunit;

namespace TestJob.Tests
{
    public class DeserializeJSON_Tests : IClassFixture<SharedDatabaseFixture>
    {
        DataContext context;

        public DeserializeJSON_Tests(SharedDatabaseFixture fixture)
        {
            context = fixture.CreateContext();

            string path = Directory.GetCurrentDirectory();
            string PathDir_txt = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\", "TestJob", "wwwroot", "txt"));

            var mock = new Mock<IAnyUserData>();
            mock.Setup(env => env.PathDir_txt).Returns(PathDir_txt);

        }

        // --------------------------------------
        

        [Fact]
        public void Get_DataServProc()
        {
            // arrange
            Project project = context.Set<Project>().FirstOrDefault();
            int id = 1;
            int num = context.Set<Project>().Count();

            // act
            var res = Load_fromServProc.Get_DataServProc(context, id);

            // assert
            Assert.Equal(num, res.Count);

            string projectName = res.FirstOrDefault(p => !string.IsNullOrEmpty(p.Disabled)).ProjectName;

            Assert.Equal(project.ProjectName, projectName);
        }

    }
}
