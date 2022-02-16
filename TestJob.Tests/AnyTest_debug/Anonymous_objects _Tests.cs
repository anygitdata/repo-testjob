using BaseSettingsTests.Tests;
using System;
using System.Linq;
using TestBaseSettings;
using TestJob.Models;
using Xunit;

namespace TestJob.Tests.AnyTest_debug
{

    public class Anonymous_objects__Tests: IClassFixture<SharedDatabaseFixture>
    {
        readonly DataContext context;

        public Anonymous_objects__Tests(SharedDatabaseFixture fixture)
        {
            BaseSetting_forTests baseSetting = new BaseSetting_forTests(fixture);

            context = baseSetting.dataContext;
        }

        // --------------------------


        [Fact]
        public void Result_anonymous_objects__test()
        {
            // arrange
            Models.Task task = context.Set<Models.Task>().FirstOrDefault();
            Guid id = task.Id;                      

            Project project = context.Set<Project>().Find(task.ProjectId);

            // act
            var data = (from tsk in context.Tasks.Where(p => p.Id == id)
                      from pr in context.Projects.Where(p => p.Id == tsk.ProjectId)
                      select new { pr.ProjectName, tsk.Id, tsk.TaskName, tsk.StartDate })
                      .ToList().FirstOrDefault();

            // assert
            Assert.NotNull(data);
            Assert.Equal(data.Id, id);
            Assert.Equal(task.TaskName, data.TaskName);
            Assert.Equal(project.ProjectName, data.ProjectName);

        }

    }
}
