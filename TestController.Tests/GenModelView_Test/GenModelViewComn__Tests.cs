using BaseSettingsTests.Tests;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Linq;
using System.Text;
using TestBaseSettings;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews;
using TestJob.Models.ModelViews.ComnView;
using TestJob.Models.UserAPI;
using Xunit;

namespace TestJob.Tests.GeneralModelView
{
    public class GenModelViewComn__Tests: IClassFixture<SharedDatabaseFixture>
    {
        readonly DataContext context;
        readonly IAnyUserData anyUserData;
        readonly TaskComment_ModelView model;

        public GenModelViewComn__Tests(SharedDatabaseFixture fixture)
        {
            context = fixture.CreateContext();

            BaseSetting_forTests forTest = new(fixture);

            anyUserData = forTest.anyUserData;

            Task tsk = context.Set<Task>().FirstOrDefault(p => p.CancelDate == null);
            model = new TaskComment_ModelView
            {
                IdComment = "",
                TaskId = tsk.Id.ToString(),
                TypeOperations = ETypeOperations.insert,
                ContentType = true,
                postedFile = null,
                Content = "text"
            };

        }

        // --------------------------------------


        [Fact]
        public void GenModelViewComn__notContent__test()
        {
            // arrange
            model.Content = "";

            // act
            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();

            // assert
            Assert.Equal(IdentResult.Error, res.Result);

            Assert.Equal("Not data for Content", res.Message);

        }

        [Fact]
        public void GenModelViewComn__notPostedFile__test()
        {
            // arrange
            model.ContentType = false;

            // act
            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();

            // assert
            Assert.Equal(IdentResult.Error, res.Result);
            Assert.Equal("postedFile not data", res.Message);            
        }


        [Fact]
        public void GenModelViewComn__notTextFile__test()
        {
            // arrange
            var mock = new Mock<IFormFile>();
            mock.Setup(p => p.Name).Returns(@"E:\VSprojects\MoqForTesting.pdf");

            model.ContentType = false;
            model.postedFile = mock.Object;

            // act
            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();

            // assert
            Assert.Equal(IdentResult.Error, res.Result);
            Assert.Equal("This is not a text file", res.Message);

        }


        [Fact]
        public void GenModelViewComn__bigFile__test()
        {
            // arrange
            var mock = new Mock<IFormFile>();
            mock.Setup(p => p.Name).Returns(@"E:\VSprojects\MoqForTesting.txt");
            mock.Setup(p => p.FileName).Returns("MoqForTesting.txt");
            mock.Setup(p => p.Length).Returns(450);

            model.ContentType = false;
            model.postedFile = mock.Object;

            // act
            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();

            // assert
            Assert.Equal(IdentResult.Error, res.Result);
            Assert.Equal("big file", res.Message);
        }


        [Fact]
        public void GenModelViewComn__ok__test()
        {
            // arrange

            UserMix.FileDelete(anyUserData.PathDir_txt, "MoqForTesting.txt");

            var mock = new Mock<IFormFile>();
            mock.Setup(p => p.Name).Returns(@"E:\VSprojects\MoqForTesting.txt");
            mock.Setup(p => p.FileName).Returns("MoqForTesting.txt");
            mock.Setup(p => p.Length).Returns(400);

            model.ContentType = false;
            model.postedFile = mock.Object;

            // act
            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();

            // assert
            Assert.Equal(IdentResult.Ok, res.Result);
            Assert.Equal(IdentResult.Ok, res.Message);
        }


        [Fact]
        public void GenModelViewComn__fileExist__test()
        {
            // arrange
            if (!UserMix.FileExists(anyUserData.PathDir_txt, "MoqForTesting.txt"))
                throw new Exception("File not exists");

            var mock = new Mock<IFormFile>();
            mock.Setup(p => p.Name).Returns(@"E:\VSprojects\MoqForTesting.txt");
            mock.Setup(p => p.FileName).Returns("MoqForTesting.txt");
            mock.Setup(p => p.Length).Returns(400);

            model.ContentType = false;
            model.postedFile = mock.Object;

            // act
            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();

            // assert
            Assert.Equal(IdentResult.Error, res.Result);
            Assert.Equal("Reloading a file", res.Message);
            
        }


        [Fact]
        public void GenModelViewComn__save_file_comment__test()
        {
            // arrange

            string path = @"E:\VSprojects\MoqForTesting.txt";
            UserMix.FileDelete(anyUserData.PathDir_txt, "MoqForTesting.txt");

            var mock = new Mock<IFormFile>();
            mock.Setup(p => p.Name).Returns(path);
            mock.Setup(p => p.FileName).Returns("MoqForTesting.txt");
            mock.Setup(p => p.Length).Returns(400);

            model.ContentType = false;
            model.postedFile = mock.Object;
            model.Content = model.postedFile.FileName;

            // act
            var res = new GenModelViewComn(context, anyUserData, model);

            res.SetDebug(false);
            res.debug_path_for_copy = path;

            res.VerifyData();
            res.SaveDataModel();

            // assert
            Assert.Equal(IdentResult.Ok, res.Result);
            Assert.Equal(IdentResult.Ok, res.Message);

        }


        [Fact]
        public void GenModelViewComn__save_strComment__test()
        {
            // arrange
            TaskComment comn = context.Set<TaskComment>().FirstOrDefault();
            byte[] content = comn.Content;

            model.Content = "Проверочная строка";
            model.IdComment = comn.Id.ToString();
            model.ContentType = true;

            // act
            var res = new GenModelViewComn(context, anyUserData, model);
            res.SetDebug(false);
            res.VerifyData();
            res.SaveDataModel();
            

            // assert
            Assert.Equal(IdentResult.Ok, res.Result);
            Assert.Equal(IdentResult.Ok, res.Message);                      

            // Restor content
            if (res.Result == IdentResult.Ok)
            {
                res.ModelRes.Content = content;
                context.SaveChanges();
            }

        }

        
    }
}
