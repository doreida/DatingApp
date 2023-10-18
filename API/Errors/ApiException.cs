namespace API.Errors
{
   public class ApiException
   {
        public ApiException(int stsusCode, string messsage, string details)
        {
            StatusCode = stsusCode;
            Message = messsage;
            Details = details;
        }

    public int StatusCode { get; set; }
     public string Message { get; set; }
     public string Details  { get; set; }
   }
}