using BaseSettingsTests.Tests;
using System;
using System.IO;
using System.Linq;
using TestBaseSettings;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews;
using TestJob.Models.UserAPI;
using Xunit;

namespace TestController.Tests.GeneralModelView_Tests
{
    public class GenModelViewComn_update_delete_Tests: IClassFixture<SharedDatabaseFixture>
    {
        readonly DataContext context;
        readonly IAnyUserData anyUserData;
        TaskComment_ModelView model;

        public GenModelViewComn_update_delete_Tests(SharedDatabaseFixture fixture)
        {
            context = fixture.CreateContext();

            BaseSetting_forTests forTest = new BaseSetting_forTests(fixture);

            anyUserData = forTest.anyUserData;
                        
            model = new TaskComment_ModelView
            {
                IdComment = "",
                TaskId = "",
                TypeOperations = ETypeOperations.update,
                ContentType = true,
                postedFile = null,
                Content = "Edited comment"
            };
        }

        // ----------------------------------


        [Fact]
        public void GeneralModelView_update__test()
        {
            // arrange
            var comn = context.Set<TaskComment>().FirstOrDefault();
            model.IdComment = comn.Id.ToString();


            // act
            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();
            res.SaveModel();

            // assert
            Assert.Equal(IdentResult.Ok, res.Result);


            // Restor comment in database
            if (res.Result == IdentResult.Ok)
            {
                var taskComn = context.Set<TaskComment>().Find(comn.Id);
                taskComn.Content = comn.Content;

                context.SaveChanges();
            }
        }


        [Fact]
        public void GeneralModelView_delete__test()
        {
            // arrange
            var comn = context.Set<TaskComment>().FirstOrDefault();

            model.IdComment = comn.Id.ToString();
            model.TypeOperations = ETypeOperations.delete;

            // act
            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();
            res.SaveModel();

            // assert
            Assert.Equal(IdentResult.Ok, res.Result);

        }


        [Fact]
        public void GeneralModelView_update_file__test()
        {
            // arrange
            var comn = context.Set<TaskComment>().FirstOrDefault(p=> p.CommentType==false);

            model.IdComment = comn.Id.ToString();
            model.ContentType = false;
            model.Content = "Модифайл комментарий в файле";

            string fileName = UserMix.Enc_GetStrFromBytes(comn.Content);
            string fullPath = Path.Combine(anyUserData.PathDir_txt, fileName);

            if (!UserMix.FileExists(fullPath))
            {
                throw new Exception("Файл не найден");
            }
            
            string content = UserMix.FileDownload(fullPath);


            // act
            var res = new GenModelViewComn(context, anyUserData, model);
            res.VerifyData();
            res.SaveModel();

            // assert
            Assert.Equal(IdentResult.Ok, res.Result);


            // Restor file
            if (res.Result == IdentResult.Ok)
            {
                UserMix.FileUpdate(fullPath, content);
            }
        }

    }
}
