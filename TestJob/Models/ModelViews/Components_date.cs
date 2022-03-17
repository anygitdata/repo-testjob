using System;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public class Components_date: IdentResult
    {
        public string date { get; set; }
        public string time { get; set; }

        public DateTime ResDate { get; set; }


        public static Components_date ConvDate_intoObj(string dt)
        {
            if (DateTime.TryParse(dt, out DateTime val))
            {
                return ConvDate_intoObj(val);
            }

            return new Components_date
            {
                Result = Error,                
                Message = "Error convert datetime by params"
            };

        }


        public static string Get_str_DateTime(DateTime? arg)
        {
            if (arg == null)
                return "";

            var compDate = ConvDate_intoObj(arg);

            if (compDate.Result == IdentResult.Error)
                return "";

            return $"{compDate.date} {compDate.time}";

        }

        public static Components_date ConvDate_intoObj(DateTime? argDate)
        {

            DateTime dt;

            if (argDate == null)
            {                
                return new Components_date { Result = Ok, date = "", time = "" };
            }
            else
                dt = (DateTime)argDate;

            try
            {
                var res = new Components_date {
                    Result = Ok,
                    date = dt.ToString("yyyy-MM-dd"),
                    time = dt.ToString("hh:mm"),
                    ResDate = dt
                };

                return res; 
            }
            catch
            {
                return new Components_date { 
                    Result = Error, 
                    Message = "Error convert datetime by params"
                };
            }

        }


        public static Components_date ConvStr_intoObj(string date, string time)
        {
            var res = new Components_date {Result = Ok };

            try
            {
                DateTime resConv = DateTime.Parse(date);
                resConv += TimeSpan.Parse(time);

                res.ResDate = resConv;
                res.date = date;
                res.time = time;

            }
            catch
            {
                res.Result = Error;
                res.Message = "Parameter error";
            }

            return res;
        }

    }
}
