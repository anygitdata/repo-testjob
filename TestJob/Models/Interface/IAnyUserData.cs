using TestJob.Models.UserAPI;

namespace TestJob.Models.Interface
{
    public interface IAnyUserData
    {
        string PathDir_txt { get; }

        //DataSettings GetSettings { get;}

        DataSettingsExt GetSettingsExt { get; }

        bool GetDebug { get; }
    }
}
