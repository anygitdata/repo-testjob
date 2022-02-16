using System;

namespace TestJob.Models.UserAPI
{
    public partial class UserMix
    {

        /// <summary>
        /// Helper procedure for date or time
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static string convPartDate_intoStr(int arg)
        {
            string sArg = arg.ToString();

            if (arg < 10)
                return '0' + sArg;
            else
                return sArg;
        }

    }
}
