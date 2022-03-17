using BaseSettingsTests.Tests;
using System;
using TestJob.Models.ModelViews;
using Xunit;

namespace TestJob.Tests
{
    public class ModelViews__ComponentsDate_Tests: IClassFixture<SharedDatabaseFixture>
    {

        [Fact]
        public void ComponentsDate__ConvStr_intoDateTime_paramIsNull_test()
        {
            // act 
            var res = Components_date.ConvDate_intoObj(argDate:null);

            // assert
            Assert.Equal("ok", res.Result);
                        
            Assert.Equal("", res.date);
            Assert.Equal("", res.time);
        }


        [Fact]
        public void ComponentsDate__convDate_intoObj_test()
        {
            // arrange
            DateTime dt = DateTime.Now;
            string date = dt.ToString("yyyy-MM-dd");
            string time = dt.ToString("hh:mm");

            // act
            Components_date res = Components_date.ConvDate_intoObj(dt);

            // assert
            Assert.Equal(dt, res.ResDate);
            Assert.Equal(date, res.date);
            Assert.Equal(time, res.time);

        }


        [Fact]
        public void ComponentsDate__convDate_with_invalidParam_intoObj_test()
        {
            // arrange
            DateTime dt = DateTime.Now;
            string date = dt.ToString("yyyy-MM.dd");
            string time = dt.ToString("hhmm");
            string comb = date + ' ' + time;


            // act
            Components_date res = Components_date.ConvDate_intoObj(comb);

            // assert
            Assert.Equal("Error convert datetime by params", res.Message);

        }


        [Fact]
        public void ComponentsDate__ConvDate_intoObj_strParams_test()
        {
            // arrange
            string date = "2022-02-05";
            string time = "12:00";

            DateTime dt = DateTime.Parse(date) + TimeSpan.Parse(time);
            string strDateTime = dt.ToString();

            // act 
            var res = Components_date.ConvDate_intoObj(strDateTime);

            // assert
            Assert.Equal("ok", res.Result);

            Assert.Equal(dt, res.ResDate);
            Assert.Equal(date, res.date);
            Assert.Equal(time, res.time);
        }


        [Fact]
        public void ComponentsDate__ConvStr_intoDateTime_errParams_test()
        {
            // arrange
            string date = "2022-0205";
            string time = "12:00";

            // act 
            var res = Components_date.ConvStr_intoObj(date, time);

            // assert
            Assert.Equal("Parameter error", res.Message);
        }


        [Fact]
        public void ComponentsDate__ConvStr_intoDateTime_test()
        {
            // arrange
            string date = "2022-02-05";
            string time = "12:00";

            DateTime dt = DateTime.Parse(date) + TimeSpan.Parse(time);

            // act 
            var res = Components_date.ConvStr_intoObj(date, time);

            // assert
            Assert.Equal(dt, res.ResDate);
            Assert.Equal(date, res.date);
            Assert.Equal(time, res.time);
        }


    }
}
