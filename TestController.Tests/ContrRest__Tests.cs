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
    public class ContrRest__Tests: IClassFixture<SharedDatabaseFixture>
    {
        DataContext context;
        RestController contrRest;
        public ContrRest__Tests(SharedDatabaseFixture fixture)
        {
            context = fixture.CreateContext();

            string path = Directory.GetCurrentDirectory();
            string PathDir_txt = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\", "TestJob", "wwwroot", "txt"));

            var mock = new Mock<IAnyUserData>();
            mock.Setup(env => env.PathDir_txt).Returns(PathDir_txt);

            contrRest = new RestController(context, mock.Object);
        }


        private void CreateFile(string FileName)
        {
            if (!UserMix.FileExists(Path.Combine(contrRest.PathDir_txt, FileName)))
            {
                BodyRequest bodyRequest = new BodyRequest { FileName = FileName, Data = "dataFor testing" };
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
        public void Load_data()
        {
            // arrange
            string FileName = "VerifyData.txt";
            string filePath = Path.Combine( contrRest.PathDir_txt, FileName) ;

            if (File.Exists(filePath))
                File.Delete(filePath);

            BodyRequest model = new BodyRequest
            {
                FileName = FileName,
                Data = @"Начальная строка       использование произвФормата
Это продолжение начальной строки
Заключительная строка"
            };

            // act
            var resREST = contrRest.Load_data(model) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(resREST as OkObjectResult);

            var items = Assert.IsType<BodyRequest>(resREST.Value);

            Assert.Equal(items.Result, BaseResult.Ok);
            Assert.Equal(items.FileName, FileName);
            Assert.Equal(items.Data, model.Data);

        }

        [Fact]
        public void Delete_File_test()
        {
            // arrange
            string fileName = "FromJS.txt";
            string PathDir_txt = contrRest.PathDir_txt;

            string fileExits = File.Exists(Path.Combine(PathDir_txt, fileName)) ? BodyRequest.Ok : BodyRequest.Error;

            // act
            var resREST = contrRest.Delete_File(fileName) as OkObjectResult ;

            // assert
            Assert.IsType<OkObjectResult>(resREST as OkObjectResult);
            BaseResult item = Assert.IsType<BaseResult>(resREST.Value);

            Assert.Equal(item.FileName, fileName);
            Assert.Equal(fileExits, item.Result);
        }

        [Fact]
        public void update_File__exists_text()
        {
            // arrange

            string fileName = "CreateProject.txt";

            CreateFile(fileName);

            BaseResult model = new BaseResult
            {
                FileName = fileName,
                Data = @"Измененная строка
помещенная в файл
Это следующее описание
Сохранение форматирования"
            };

            // act
            var resREST = contrRest.update_File(model) as OkObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(resREST as OkObjectResult);

            BaseResult item = Assert.IsType<BaseResult>(resREST.Value);

            Assert.Equal(item.FileName, fileName);
            Assert.Equal(item.Result,  BaseResult.Ok);
            Assert.Equal(model.Data, item.Data);

        }

        [Fact]
        public void update_File__not_exists_text()
        {
            // arrange

            string fileName = "CreateProject_notExists.txt";
            BaseResult model = new BaseResult
            {
                FileName = fileName,
            };

            // act
            var resREST = contrRest.update_File(model) as OkObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(resREST as OkObjectResult);

            BaseResult item = Assert.IsType<BaseResult>(resREST.Value);

            Assert.Equal(item.FileName, fileName);
            Assert.Equal(item.Result, BaseResult.Error);
            Assert.Equal("File not exists", model.Message);

        }

    }
}
