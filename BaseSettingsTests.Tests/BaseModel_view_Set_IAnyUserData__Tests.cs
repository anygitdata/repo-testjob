using Microsoft.AspNetCore.Hosting;
using Moq;
using System.IO;
using TestBaseSettings;
using TestJob.Models;
using TestJob.Models.Interface;
using Xunit;

namespace BaseSettingsTests.Tests
{
    /// <summary>
    /// Static properties test -> BaseModel_view
    /// </summary>
    public class BaseModel_view_Set_IAnyUserData__Tests: IClassFixture<SharedDatabaseFixture>
    {
        readonly DataContext context;
        readonly string wwwroot;
        readonly IAnyUserData anyUserData;

        public BaseModel_view_Set_IAnyUserData__Tests(SharedDatabaseFixture fixture)
        {
            var baseSetting = new BaseSetting_forTests(fixture);

            context = baseSetting.dataContext;

            string path = Directory.GetCurrentDirectory();
            wwwroot = Path.GetFullPath(Path.Combine(path,
                @"..\..\..\..\", "TestJob", "wwwroot"));


            var environment = new Mock<IWebHostEnvironment>();
            environment.Setup(id => id.WebRootPath).Returns(wwwroot);

            anyUserData = new AnyUserData(environment.Object);
            
        }
       

    }
}
