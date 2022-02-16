using BaseSettingsTests.Tests;
using System;
using System.Linq;
using TestBaseSettings;
using TestJob.Controllers;
using TestJob.Models;
using TestJob.Models.ModelViews;
using Xunit;


namespace TestJob.Tests
{
    public class ModelView__AjaxProduct_Tests: IClassFixture<SharedDatabaseFixture>
    {
        readonly DataContext context;

        public ModelView__AjaxProduct_Tests(SharedDatabaseFixture fixture)
        {
            BaseSetting_forTests baseSetting = new BaseSetting_forTests(fixture);

            context = baseSetting.dataContext;
        }

        // ---------------------------------------

        [Fact]
        public void AjaxProduct_InitData()
        {
            // arrange 
            Project project = context.Set<Project>().FirstOrDefault();
            Guid Id = project.Id;

            // act
            Ajax_product res = Ajax_product.InitData(context, Id);

            // assert
            Assert.Equal(res.projectId, Id.ToString());

        }


        [Fact]
        public void AjaxProduct_InitData_forEmpty()
        {
            // arrange 
            Guid Id = Guid.Empty;

            // act
            Ajax_product res = Ajax_product.InitData(context, Id);

            // assert
            Assert.Equal(res.projectId, Id.ToString());

        }

    }
}
