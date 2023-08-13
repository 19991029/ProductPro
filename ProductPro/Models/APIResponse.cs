using System.Net;

namespace ProductPro.Models
{
    public class Apiresponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public object Result { get; set; }
    }
}
