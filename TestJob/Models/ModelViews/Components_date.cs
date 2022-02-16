using System;
using TestJob.Models.UserAPI;

namespace TestJob.Models.ModelViews
{
    public class Components_date: IdentResult
    {
        public string date { get; set; }
        public string time { get; set; }

        public DateTime resDate { get; set; }


        public static Components_date convDate_intoObj(string dt)
        {
            DateTime val;
            if (DateTime.TryParse(dt, out val))
            {
                return convDate_intoObj(val);
            }

            return new Components_date
            {
                Result = Error,                
                Message = "Error convert datetime by params"
            };

        }


        public static Components_date convDate_intoObj(DateTime? argDate)
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
                Components_date res = new Components_date {
                    Result = Ok,
                    date = dt.ToString("yyyy-MM-dd"),
                    time = dt.ToString("hh:mm"),
                    resDate = dt
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
            Components_date res = new Components_date {Result = Ok };

            try
            {
                DateTime resConv = DateTime.Parse(date);
                resConv = resConv + TimeSpan.Parse(time);

                res.resDate = resConv;
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
