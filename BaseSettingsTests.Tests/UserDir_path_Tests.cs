using BaseSettingsTests.Tests;
using System.IO;
using TestBaseSettings;
using TestJob.Models.UserAPI;
using Xunit;

namespace TestJob.Tests
{
    public class UserDir_path_Tests: IClassFixture<SharedDatabaseFixture>
    {
        readonly string pathTxt;

        public UserDir_path_Tests(SharedDatabaseFixture fixture)
        {
            BaseSetting_forTests baseSetting = new (fixture);

            pathTxt = baseSetting.pathTxt;
        }

        [Fact]
        public void Path_Settings_test()
        {
            string dirSettings = UserDir_path.PathDir_Settings(pathTxt);
            string fullPath = Path.Combine(dirSettings, "settings.json");
            Assert.True(UserMix.FileExists(fullPath));
        }

        [Fact]
        public void Path_sql_test()
        {
            string dirSettings = UserDir_path.PathDir_sql(pathTxt);
            string fullPath = Path.Combine(dirSettings, "sp_list_projects.sql");
            Assert.True(UserMix.FileExists(fullPath));
        }

    }
}
