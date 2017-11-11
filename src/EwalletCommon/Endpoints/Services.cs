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
        readonly ProxySettings _settings;

        public Ewallet Wallet { get; private set; }
        
        public Service(IOptions<ProxySettings> proxySettings)
        {
            _settings = proxySettings.Value;
            Wallet = new Ewallet(_settings.EwalletServiceBaseUrl);
        }
    }
}
