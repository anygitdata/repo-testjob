using BaseSettingsTests.Tests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TestBaseSettings;
using TestJob.Controllers;
using TestJob.Models;
using TestJob.Models.ModelViews;
using TestJob.Models.UserAPI;
using Xunit;

namespace TestJob.Tests.ControllerTests
{
    public class ContrHome_NewProject_Tests : IClassFixture<SharedDatabaseFixture>
    {
        readonly DataContext context;
        readonly HomeController contrHome;

        public ContrHome_NewProject_Tests(SharedDatabaseFixture fixture)
        {
            BaseSetting_forTests baseSetting = new (fixture);

            context = baseSetting.dataContext;
            contrHome = baseSetting.HomeContr;

        }

        private void RemoveProject(Ajax_product prDel)
        {
            var project = context.Set<Project>().Where(p => p.ProjectName == prDel.projectName);
            context.RemoveRange(project);
            context.SaveChanges();
        }

        private void RecoveryProject(Ajax_product pr, string projNameInitial)
        {
            Guid id = Guid.Parse(pr.projectId);

            Project project = context.Set<Project>().Find(id);
            project.ProjectName = projNameInitial;
            project.UpdateDate = null;

            context.SaveChanges();
        }

        // -------------------------

        [Fact]
        public void Home_NewProject_with_idEMPTY_test()
        {
            // arrange
            Ajax_product model = new ()
            {
                projectId = Guid.Empty.ToString(),
                projectName = "Project name for testing",
                date = "2022-02-03",
                time = "12:00"
            };

            // act
            var resOk = contrHome.NewProject(model) as OkObjectResult;


            // assert
            Assert.IsType<OkObjectResult>(resOk as OkObjectResult);

            var item = Assert.IsType<Ajax_product>(resOk.Value);

            Assert.Equal(IdentResult.Ok, item.Result);

            RemoveProject(model);
        }

        [Fact]
        public void Home_NewProject_with_idEMPTY_and_notData_test()
        {
            // arrange
            Ajax_product model = new ()
            {
                projectId = Guid.Empty.ToString(),
                projectName = "Project name for test",
                time = "12:00"
            };

            // act
            var resOk = contrHome.NewProject(model) as OkObjectResult;


            // assert
            Assert.IsType<OkObjectResult>(resOk as OkObjectResult);

            var item = Assert.IsType<Ajax_product>(resOk.Value);

            Assert.Equal(IdentResult.Error, item.Result);
            Assert.Equal("Fill fields date, time", item.Message);

        }


        [Fact]
        public void Home_NewProject_insertNew_project_test()
        {
            // arrange
            Ajax_product model = new ()
            {
                projectId = Guid.Empty.ToString(),
                projectName = "New Project for test",
                date = "2022-02-01",
                time = "12:00"
            };

            // act
            var resOk = contrHome.NewProject(model) as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(resOk as OkObjectResult);

            var item = Assert.IsType<Ajax_product>(resOk.Value);

            Assert.Equal(IdentResult.Ok, item.Result);

            RemoveProject(model);
        }


        //[Fact]
        //public void Home_UpdProject_with_updateData_test()
        //{
        //    // arrange
        //    Project project = context.Set<Project>().FirstOrDefault(p => p.UpdateDate != null);

        //    Ajax_product model = new ()
        //    {
        //        projectId = project.Id.ToString(),
        //        projectName = project.ProjectName,
        //        time = "12:00",
        //        date = "2022-02-03"
        //    };

        //    // act
        //    var resOk = contrHome.UpdProject(model) as OkObjectResult;

        //    // assert
        //    Assert.IsType<OkObjectResult>(resOk as OkObjectResult);

        //    var item = Assert.IsType<Ajax_product>(resOk.Value);

        //    Assert.Equal(IdentResult.Error, item.Result);
        //    Assert.Equal("Project closed", item.Message);

        //}


        //[Fact]
        //public void Home_UpdProject_with_createData_less_updateData_test()
        //{
        //    // arrange
        //    Project project = context.Set<Project>().FirstOrDefault(p => p.UpdateDate == null);

        //    Ajax_product model = new ()
        //    {
        //        projectId = project.Id.ToString(),
        //        projectName = project.ProjectName,
        //        time = "12:00",
        //        date = "2020-02-03"
        //    };

        //    // act
        //    var resOk = contrHome.UpdProject(model) as OkObjectResult;

        //    // assert
        //    Assert.IsType<OkObjectResult>(resOk as OkObjectResult);

        //    var item = Assert.IsType<Ajax_product>(resOk.Value);

        //    Assert.Equal(IdentResult.Error, item.Result);
        //    Assert.Equal("UpdateDate must be greater than CreateDate", item.Message);

        //}


        //[Fact]
        //public void Home_UpdProject_with_not_exists_product_test()
        //{
        //    // arrange
        //    Project project = context.Set<Project>().FirstOrDefault(p => p.UpdateDate == null);

        //    Ajax_product model = new ()
        //    {
        //        projectId = Guid.NewGuid().ToString(),
        //        projectName = "empty",
        //        time = "12:00",
        //        date = "2020-02-03"
        //    };

        //    // act
        //    var resOk = contrHome.UpdProject(model) as OkObjectResult;

        //    // assert
        //    Assert.IsType<OkObjectResult>(resOk as OkObjectResult);

        //    var item = Assert.IsType<Ajax_product>(resOk.Value);

        //    Assert.Equal(IdentResult.Error, item.Result);
        //    Assert.Equal("Project not exist", item.Message);

        //}


        //[Fact]
        //public void Home_UpdProject_without_date_or_time_test()
        //{
        //    // arrange
        //    Project project = context.Set<Project>().FirstOrDefault(p => p.UpdateDate == null);

        //    Ajax_product model = new ()
        //    {
        //        projectId = project.Id.ToString(),
        //        projectName = project.ProjectName,
        //        date = "2022-02-03"
        //    };

        //    // act
        //    var resOk = contrHome.UpdProject(model) as OkObjectResult;

        //    // assert
        //    Assert.IsType<OkObjectResult>(resOk as OkObjectResult);

        //    var item = Assert.IsType<Ajax_product>(resOk.Value);

        //    Assert.Equal(IdentResult.Error, item.Result);
        //    Assert.Equal("Fill fields date, time", item.Message);

        //}

        //[Fact]
        //public void Home_UpdProject_with_error_time_test()
        //{
        //    // arrange
        //    Project project = context.Set<Project>().FirstOrDefault(p => p.UpdateDate == null);

        //    Ajax_product model = new ()
        //    {
        //        projectId = project.Id.ToString(),
        //        projectName = project.ProjectName,
        //        date = "2022-02-03",
        //        time = "11-00"
        //    };

        //    // act
        //    var resOk = contrHome.UpdProject(model) as OkObjectResult;

        //    // assert
        //    Assert.IsType<OkObjectResult>(resOk as OkObjectResult);

        //    var item = Assert.IsType<Ajax_product>(resOk.Value);

        //    Assert.Equal(IdentResult.Error, item.Result);
        //    Assert.Equal("Check date and time fields", item.Message);

        //}


        //[Fact]
        //public void Home_UpdProject_with_error_date_test()
        //{
        //    // arrange
        //    Project project = context.Set<Project>().FirstOrDefault(p => p.UpdateDate == null);

        //    Ajax_product model = new ()
        //    {
        //        projectId = project.Id.ToString(),
        //        projectName = project.ProjectName,
        //        date = "2022-0203",
        //        time = "11:00"
        //    };

        //    // act
        //    var resOk = contrHome.UpdProject(model) as OkObjectResult;

        //    // assert
        //    Assert.IsType<OkObjectResult>(resOk as OkObjectResult);

        //    var item = Assert.IsType<Ajax_product>(resOk.Value);

        //    Assert.Equal(IdentResult.Error, item.Result);
        //    Assert.Equal("Check date and time fields", item.Message);

        //}


        //[Fact]
        //public void Home_UpdProject_updateProject_test()
        //{
        //    // arrange
        //    Project project = context.Set<Project>().FirstOrDefault(p => p.UpdateDate == null);

        //    string projNameInitial = project.ProjectName;

        //    DateTime dtCreate = project.CreateDate.AddDays(1);
        //    var compDateTime = Components_date.ConvDate_intoObj(dtCreate);

        //    Ajax_product model = new ()
        //    {
        //        projectId = project.Id.ToString(),
        //        projectName = project.ProjectName + " updateName",
        //        date = compDateTime.date,
        //        time = compDateTime.time
        //    };

        //    // act
        //    var resOk = contrHome.UpdProject(model) as OkObjectResult;

        //    // assert
        //    Assert.IsType<OkObjectResult>(resOk as OkObjectResult);

        //    var item = Assert.IsType<Ajax_product>(resOk.Value);

        //    Assert.Equal(IdentResult.Ok, item.Result);

        //    RecoveryProject(model, projNameInitial);

        //}


    }
}
