using BaseSettingsTests.Tests;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System.IO;
using TestJob.Controllers;
using TestJob.Models;
using TestJob.Models.Interface;
using Xunit;

namespace TestBaseSettings
{
    public class BaseSetting_forTests : IClassFixture<SharedDatabaseFixture>
    {
        public readonly string pathWWWroot;
        public readonly string pathTxt; 
        
        public readonly DataContext dataContext;
        public readonly AnyUserData anyUserData;

        public BaseSetting_forTests(SharedDatabaseFixture fixture)
        {
            dataContext = fixture.CreateContext();

            string _path = Directory.GetCurrentDirectory();

            pathWWWroot = Path.GetFullPath(Path.Combine(_path, @"..\..\..\..\", "TestJob", "wwwroot"));

            pathTxt = Path.GetFullPath(Path.Combine(pathWWWroot, "txt"));

            // init object IAnyUserData
            var _mock = new Mock<IWebHostEnvironment>();
            _mock.Setup(env => env.WebRootPath).Returns(pathWWWroot);

            anyUserData = new AnyUserData(_mock.Object);
        }

        public HomeController HomeContr { get => new (dataContext, anyUserData); }

        public RestController RestContr { get => new ( anyUserData);}

    }
}
