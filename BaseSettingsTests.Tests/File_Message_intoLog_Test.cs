using BaseSettingsTests.Tests;
using System.IO;
using System.Threading.Tasks;
using TestBaseSettings;
using TestJob.Models.UserAPI;
using Xunit;

namespace BaseSettingsTests.Tests
{
    public class File_Message_intoLog_Test : IClassFixture<SharedDatabaseFixture>
    {
        readonly string pathTxt;

        public File_Message_intoLog_Test(SharedDatabaseFixture fixture)
        {
            BaseSetting_forTests baseSetting = new(fixture);

            pathTxt = baseSetting.pathTxt;
        }

        [Fact]
        public void File_Message_intoLog_createFile__test()
        {
            // arrange
            var pathFile = Path.Combine(pathTxt, @"..\", "anymessage", "message.txt");
            UserMix.FileDelete(pathFile);

            // act
            UserMix.File_Message_intoLog(pathTxt, "Пробное сообщение");

            // assert
            Assert.True(UserMix.FileExists(pathFile));
        }

        [Fact]
        public async Task File_Message_intoLog_appendMessage__test()
        {
            // arrange
            var pathFile = Path.Combine(pathTxt, @"..\", "anymessage", "message.txt");

            // act
            UserMix.File_Message_intoLog(pathTxt, "Созданное сообщение");

            await Task.Delay(5000 );

            UserMix.File_Message_intoLog(pathTxt, "После задержки сообщение");

            // assert
            Assert.True(UserMix.FileExists(pathFile));
        }

    }
}
