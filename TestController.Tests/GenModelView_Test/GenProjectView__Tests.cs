using BaseSettingsTests.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBaseSettings;
using TestJob.Models;
using TestJob.Models.Interface;
using TestJob.Models.ModelViews.ProjectView;
using TestJob.Models.UserAPI;
using Xunit;

namespace TestController.Tests.GenModelView_Test
{
    public class GenProjectView__Tests: IClassFixture<SharedDatabaseFixture>
    {
        private readonly DataContext context;
        private readonly IAnyUserData anyUserData;


        public GenProjectView__Tests(SharedDatabaseFixture fixture)
        {
            BaseSetting_forTests forTest = new(fixture);

            context = fixture.CreateContext();
            anyUserData = forTest.anyUserData;

        }

        private BaseProjectView GetModel()
        {
            var dNow = DateTime.Now;
            string date = dNow.ToString("yyyy-MM-dd");
            string time = dNow.ToString("hh:mm");

            var model = new BaseProjectView
            {
                ProjectName = "TestingProject",
                Date = date,
                Time = time
            };

            return model; 
        }


        private void RemoveProject(BaseProjectView model)
        {
            if (!anyUserData.Debug)
            {
                var proj = context.Set<Project>().Find(model.ProjectId);
                context.Remove(proj);

                context.SaveChanges();
            }
        }


        private bool VerifyExists(BaseProjectView model)
        {
            if (context.Set<Project>().Find(model.ProjectId) != null)
                return true;
            else
                return false;
        }

        // -----------------------------------------

        [Fact]
        public void GenProjectView_add__verify()
        {
            // arrange
            var model = GetModel();

            // act
            var res = new GenProjectView_add(context, anyUserData, model);
            res.VerifyData();

            // assert            
            Assert.Equal(IdentResult.Ok, res.Result);

        }


        [Fact]
        public void GenProjectView_add_verify__DateError()
        {
            // arrange
            var dNow = DateTime.Now;
            string date = dNow.ToString("yyyy-md-dd");
            
            var model = GetModel();
            model.Date = date;

            // act
            var res = new GenProjectView_add(context, anyUserData, model);
            res.VerifyData();

            // assert            
            Assert.Equal(IdentResult.Error, res.Result);
            Assert.Equal("Date or time component errors", res.Message);
        }


        [Fact]
        public void GenProjectView_add_verify__noProjName()
        {
            // arrange            

            var model = GetModel();
            model.ProjectName = "";

            // act
            var res = new GenProjectView_add(context, anyUserData, model);
            res.VerifyData();

            // assert            
            Assert.Equal(IdentResult.Error, res.Result);
            Assert.Equal("The projectName field is not filled", res.Message);
        }


        [Fact]
        public void GenProjectView_add_verify__save()
        {
            // arrange
            var model = GetModel();

            // act
            var res = new GenProjectView_add(context, anyUserData, model);
            res.VerifyData();
            res.SaveData();

            // assert
            Assert.Equal(IdentResult.Ok, res.Result);
            Assert.True(VerifyExists(model));

            RemoveProject(model);
        }


    }
}
