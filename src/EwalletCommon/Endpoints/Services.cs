using EwalletCommon.Settings;
using Microsoft.Extensions.Options;

namespace EwalletCommon.Endpoints
{
    public interface IService
    {
        Ewallet Wallet { get; }
    }

    public class Service : IService
    {
        readonly ProxySettings settings;

        public Ewallet Wallet { get; private set; }
        
        public Service(IOptions<ProxySettings> proxySettings)
        {
            settings = proxySettings.Value;
            Wallet = new Ewallet(settings.EwalletServiceBaseUrl);
        }
    }
}
