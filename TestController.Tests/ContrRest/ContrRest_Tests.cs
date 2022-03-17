using BaseSettingsTests.Tests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IO;
using TestJob.Controllers;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;
using Xunit;

namespace TestJob.Tests
{
    public class ContrRest_Tests : IClassFixture<SharedDatabaseFixture>
    {
        readonly DataContext context;
        readonly RestController contrRest;
        public ContrRest_Tests(SharedDatabaseFixture fixture)
        {
            context = fixture.CreateContext();

            string _path = Directory.GetCurrentDirectory();
            string _pathDir_txt = Path.GetFullPath(Path.Combine(_path, @"..\..\..\..\", "TestJob", "wwwroot", "txt"));

            var mock = new Mock<IAnyUserData>();
            mock.Setup(env => env.PathDir_txt).Returns(_pathDir_txt);

            contrRest = new RestController(mock.Object);
        }


        private void CreateFile(string FileName)
        {
            if (!UserMix.FileExists(Path.Combine(contrRest.PathDir_txt, FileName)))
            {
                var bodyRequest = new BodyRequest { FileName = FileName, Data = "dataFor testing" };
                UserMix.FileCreate(contrRest.PathDir_txt, bodyRequest);
            }
        }

        // -------------------------------------------

        [Fact]
        public void Get_dataFromFile_test()
        {
            // arrange
            string FileName = "CreateProject.txt";

            CreateFile(FileName);

            // act
            var resREST = contrRest.Get_dataFromFile(FileName) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(resREST as OkObjectResult);

            var items = Assert.IsType<BaseResult>(resREST.Value);

            Assert.Equal(items.Result, IdentResult.Ok);
            Assert.False(string.IsNullOrEmpty(items.Data));
            Assert.Equal(items.FileName, FileName);
        }

        [Fact]
        public void Get_dataFromFile_NotFound_test()
        {
            // arrange
            string fileName = "AnyFile.txt";

            // Act
            var resREST = contrRest.Get_dataFromFile(fileName) as NotFoundObjectResult;


            // Assert
            Assert.IsType<NotFoundObjectResult>(resREST as NotFoundObjectResult);

            var item = Assert.IsType<BaseResult>(resREST.Value);

            Assert.Equal(item.Result, IdentResult.Error);
            Assert.True(string.IsNullOrEmpty(item.Data));
            Assert.Equal(item.FileName, fileName);
            Assert.Equal(item.FileName, fileName);
        }
                

        [Fact]
        public void Delete_File_test()
        {
            // arrange
            string fileName = "FromJS.txt";
            string PathDir_txt = contrRest.PathDir_txt;

            string fileExits = File.Exists(Path.Combine(PathDir_txt, fileName)) ? BodyRequest.Ok : BodyRequest.Error;

            // act
            var resREST = contrRest.Delete_File(fileName) as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(resREST as OkObjectResult);
            BaseResult item = Assert.IsType<BaseResult>(resREST.Value);

            Assert.Equal(item.FileName, fileName);
            Assert.Equal(fileExits, item.Result);
        }
               

    }
}
