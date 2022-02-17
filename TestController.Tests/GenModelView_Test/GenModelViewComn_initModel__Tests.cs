using BaseSettingsTests.Tests;
using System;
using System.Linq;
using TestBaseSettings;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews;
using TestJob.Models.UserAPI;
using Xunit;

namespace TestJob.Tests.GeneralModelView_Tests
{
    /// <summary>
    /// Model creation test for the view 
    /// </summary>
    public class GenModelViewComn_initModel__Tests: IClassFixture<SharedDatabaseFixture>
    {
        readonly DataContext context;
        readonly IAnyUserData anyUserData;

        public GenModelViewComn_initModel__Tests(SharedDatabaseFixture fixture)
        {
            context = fixture.CreateContext();

            BaseSetting_forTests forTest = new(fixture);

            anyUserData = forTest.anyUserData;
        }


        //[Fact]
        //public void GeneralModelView_initModel__test()
        //{
        //    // arrange
        //    Guid id = context.Set<Task>().FirstOrDefault(p => p.CancelDate == null).Id;

        //    var model = new TaskComment_ModelView
        //    {
        //        TaskId = id.ToString(),
        //        Content = "Строка комментария",
        //        TypeOperations= ETypeOperations.insert,
        //        ContentType = true
        //    };

        //    // act
        //    var res = new GenModelViewComn(context, anyUserData, model);
        //    res.VerifyData();
        //    res.SaveDataModel ();

        //    // assert
        //    Assert.Equal(res.Result, IdentResult.Ok);
        //    Assert.IsType<TaskComment>(res.ModelRes as TaskComment);
        //    Assert.IsType<AnyData_Comment>(res.ModelView as AnyData_Comment);
        //}

    }
}
