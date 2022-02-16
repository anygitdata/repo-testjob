using BaseSettingsTests.Tests;
using Moq;
using System.IO;
using TestJob.Controllers;
using TestJob.Models;
using TestJob.Models.Interface;
using Xunit;

namespace TestJob.Tests
{
    public class UserAPI_Test: IClassFixture<SharedDatabaseFixture>
    {
        DataContext context;
        HomeController contrHome;

        public UserAPI_Test(SharedDatabaseFixture fixture)
        {
            string path = Directory.GetCurrentDirectory();
            string PathDir_txt = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\", "TestJob", "wwwroot", "txt"));

            var mock = new Mock<IAnyUserData>();
            mock.Setup(env => env.PathDir_txt).Returns(PathDir_txt);


            context = fixture.CreateContext();

            contrHome = new HomeController(context, mock.Object);
        }


        //[Fact]
        //public void ConvStr_intoDateTime_test()
        //{
        //    // arrange
        //    string year = "2022";
        //    string month = "01";
        //    string day = "28";

        //    string hour = "16";
        //    string minute = "21";

        //    string strDate = $"{year}-{month}-{day}";
        //    string strTime = $"{hour}:{minute}";

        //    // act
        //    ResultData res = UserMix.ConvStr_intoDateTime(strDate, strTime);

        //    // assert
        //    Assert.IsType<ResultData>(res);

        //    Assert.Equal(((DateTime) res.resData).Year, int.Parse(year));
        //    Assert.Equal(((DateTime) res.resData).Month, int.Parse(month));
        //    Assert.Equal(((DateTime)res.resData).Day, int.Parse(day));

        //    Assert.Equal(((DateTime)res.resData).Hour, int.Parse(hour));
        //    Assert.Equal(((DateTime)res.resData).Minute, int.Parse(minute));

        //}

        //[Fact]
        //public void ConvStr_intoDateTime_error_test()
        //{
        //    // arrange
        //    string year = "2022";
        //    string month = "01";
        //    string day = "28";

        //    string hour = "25";  // error in hour
        //    string minute = "21";

        //    string strDate = $"{year}-{month}-{day}";
        //    string strTime = $"{hour}:{minute}";

        //    // act
        //    ResultData res = UserMix.ConvStr_intoDateTime(strDate, strTime);

        //    // assert
        //    Assert.IsType<ResultData>(res);
        //    Assert.Equal(res.Result, IdentResult.Error);

        //}

    }
}
