using Newtonsoft.Json;

namespace PharmaManagment.Models
{
    public class ErrorDetails
    {
        public int ErrorStatusCode { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
