using Newtonsoft.Json;

namespace EwalletCommon.Endpoints
{
    public class ServiceError
    {
        public string Code { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
