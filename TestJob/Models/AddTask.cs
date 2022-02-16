 using System;

namespace TestJob.Models
{
    //           
    public class AddTask
    {
        public string ProjectName { get; set; }

        DateTime _CreateDate;
        public DateTime CreateDate { get { return _CreateDate; } 
            set {
                _CreateDate = value;
                Start = _CreateDate;
            } }

        public DateTime Start { get; set; }
        public string Ticket { get; set; }
        public string Description { get; set; }

        public string StartStr
        {
            get
            {
                int hour =  Start.Hour;
                int minute = Start.Minute;
                return $"{hour}:{minute}";

            }
        }

        public AddTask()
        {
            _CreateDate = DateTime.Now;
            Start = CreateDate;
        }
        

        // ----------------------------
        public static string LoadDescr(string fileName)
        {
            return "";
        }

        public static bool SaveData(AddTask addTask)
        {
            return false;
        }

    }

    public class ViewModel_TaskAdd
    {
        public AddTask addTask { get; set; }

        // ---------------------------------------
        public bool ReadOnly { get; set; } = false;
        public string theme { get; set; } = "primary";
        public string bg { get; set; } = "bg-success";
        public string color { get; set; } = "text-success";

    }


}
