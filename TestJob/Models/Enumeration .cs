using System;

namespace TestJob.Models
{

    [Flags]
    public enum ETypeOperations {insert=1, update=2, delete=3 };


    [Flags]
    public enum ETypeOperTask{ 
                create = 1, start = 2,
                detail = 3, update = 4, cancel = 5 };

    public class ConvEnum {
        public static Enum ConvStrEnum_intoEnum(string typeEnum, string strTypeOper)
        {
            Enum res = null;

            switch (typeEnum)
            {
                case "ETypeOperTask":
                    res = (Enum)Enum.Parse(typeof(ETypeOperTask), strTypeOper);
                    break;
                case "ETypeOperations":
                    res = (Enum)Enum.Parse(typeof(ETypeOperations), strTypeOper);
                    break;
            }

            return res;
        }
    }

}
