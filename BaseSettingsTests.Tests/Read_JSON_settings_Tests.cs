using BaseSettingsTests.Tests;
using Newtonsoft.Json;
using System;
using System.IO;
using TestBaseSettings;
using TestJob.Models;
using TestJob.Models.UserAPI;
using Xunit;

namespace TestJob.Tests
{
    /// <summary>
    /// Тестирование сервиса управления настройками
    /// </summary>
    public class Read_JSON_settings_Tests: IClassFixture<SharedDatabaseFixture>
    {
        readonly DataContext context;
        readonly string pathTxt;
        //readonly HomeController contrHome;
        
        string[] settings = { "on", "off" };
        string fullParhJSON;

        public Read_JSON_settings_Tests(SharedDatabaseFixture fixture)
        {
            BaseSetting_forTests baseSetting = new BaseSetting_forTests(fixture);

            context = baseSetting.dataContext;
            //contrHome = baseSetting.homeController;
            pathTxt = baseSetting.pathTxt;

            fullParhJSON = Path.Combine(UserDir_path.PathDir_Settings(pathTxt), "settings.json");
        }

        private DataSettings Init_DataSettings()
        {
            string pathJSON = UserDir_path.PathDir_Settings(pathTxt);
            string pathFile = Path.Combine(pathJSON, "settings.json");
            string strJson = UserMix.FileDownload(pathFile);

            return JsonConvert.DeserializeObject<DataSettings>(strJson);
        }

        private void SettingsSeedData_into_file(string seedData)
        {
            DataSettings curSettings = Init_DataSettings();

            curSettings.seedData = seedData;
            File.WriteAllText(fullParhJSON, JsonConvert.SerializeObject(curSettings));
        }

        private void SettingsDebuf_into_file(string seedData)
        {
            DataSettings curSettings = Init_DataSettings();

            curSettings.debug = seedData;
            File.WriteAllText(fullParhJSON, JsonConvert.SerializeObject(curSettings));
        }



        // -------------------------------------------------



        [Fact]
        public void Settings_readJSONdata_SeedData_fromFile()
        {
            // act
            DataSettings res = Init_DataSettings();
         
            // assert
            Assert.True(Array.IndexOf(settings, res.seedData)>=0);
        }


        [Fact]
        public void Settings_readAndSetOff_SeedData_fromFile()
        {
            // arrange
            SettingsSeedData_into_file("off");
            DataSettings curSettings = Init_DataSettings();

            curSettings.seedData = "off";
            File.WriteAllText(fullParhJSON, JsonConvert.SerializeObject(curSettings));

            DataSettings secondSettings = Init_DataSettings();
            
            // assert
            Assert.Equal("off", secondSettings.seedData);

        }


        [Fact]
        public void Settings_DataSettings_read_setOn()
        {
            SettingsSeedData_into_file("on");
            DataSettings curSettings = Init_DataSettings();

            // act
            var resProc = DataSettings_read.GetSettings(pathTxt);
            DataSettings dataSettings = Init_DataSettings();

            // assert
            Assert.Equal(curSettings.seedData, "on");
            Assert.NotEqual(dataSettings.seedData, resProc.seedData);
        }

        [Fact]
        public void Settings_DataSettings_read_setOff()
        {
            // arrange
            SettingsSeedData_into_file("off");
            DataSettings curSettings = Init_DataSettings();

            // act
            var resProc = DataSettings_read.GetSettings(pathTxt);
            DataSettings dataSettings = Init_DataSettings();

            // assert
            Assert.Equal(curSettings.seedData, "off");
            Assert.Equal(dataSettings.seedData, resProc.seedData);
        }


        [Fact]
        public void Settings_DataSettings_read_debug()
        {
            // arrange
            SettingsDebuf_into_file("on");
            
            // act
            var curSettins = DataSettings_read.GetSettings(pathTxt);

            // assert
            Assert.Equal("on", curSettins.debug);

            // ----------- next test

            // arrange
            SettingsDebuf_into_file("off");
            // act
            var updSettins = DataSettings_read.GetSettings(pathTxt);

            // assert
            Assert.Equal("off", updSettins.debug);

        }


    }
}
