using Newtonsoft.Json;

namespace ScanAndGoMVC.ExceptionHandling
{
    public class ExceptionMessage
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public ExceptionMessage(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
