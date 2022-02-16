namespace TestJob.Models.UserAPI
{
    public class ResultData: IdentResult // MessageError
    {
        //public ResultData()
        //{
        //    Result = IdentResult.Ok;
        //    Error = "";
        //    Message = "";
        //    //resData = null;
        //}

        public object resData { get; set; }
    }
}
