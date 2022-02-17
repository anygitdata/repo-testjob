using Microsoft.AspNetCore.Hosting;
using Moq;
using System.IO;
using TestBaseSettings;
using TestJob.Models.Interface;
using TestJob.Models.UserAPI;
using Xunit;

namespace BaseSettingsTests.Tests
{
    /// <summary>
    /// Dependency Testing ->  IAnyUserData 
    /// </summary>
    public class IAnyUserData_Tests: IClassFixture<SharedDatabaseFixture>
    {
        //string wwwroot;

        readonly SharedDatabaseFixture fixture;

        public IAnyUserData_Tests(SharedDatabaseFixture fix)
        {
            //string path = Directory.GetCurrentDirectory();
            //wwwroot = Path.GetFullPath(Path.Combine(path, 
            //    @"..\..\..\..\", "TestJob", "wwwroot"));

            fixture = fix;
        }

        // --------------------------------------
        
               

        [Fact]
        public void InitObject_IAnyUserData_test()
        {
            // act
            var baseSetting = new BaseSetting_forTests(fixture);


            // assert
            Assert.NotNull(baseSetting);

            Assert.IsType<DataSettingsExt>(baseSetting.anyUserData.GetSettingsExt as DataSettingsExt);

            DataSettings settings = DataSettings_read.GetSettings(baseSetting.pathTxt);

            bool seedData = settings.seedData == "on"; 

            Assert.Equal(settings.debug == "on", baseSetting.anyUserData.GetSettingsExt.Debug);
            Assert.Equal(settings.debug, baseSetting.anyUserData.GetSettingsExt.StrDebug);

            Assert.Equal(settings.maxSizeFile, baseSetting.anyUserData.MaxSizeFile);


            Assert.Equal(settings.seedData, baseSetting.anyUserData.GetSettingsExt.StrSeedData);
            Assert.Equal(seedData, baseSetting.anyUserData.GetSettingsExt.SeedData);

        }


    }
}
