using EwalletCommon.Services;
using EwalletCommon.Settings;
using Microsoft.Extensions.Options;

namespace EwalletCommon.Endpoints
{
    public interface IService
    {
        Ewallet Wallet { get; }
        Picasso Picasso { get; }
    }

    public class Service : IService
    {
        private readonly ProxySettings settings;
        public Ewallet Wallet { get; private set; }
        public Picasso Picasso { get; private set; }

        public Service(IOptions<ProxySettings> proxySettings)
        {
            settings = proxySettings.Value;
            Wallet = new Ewallet(settings.EwalletServiceBaseUrl);
            Picasso = new Picasso(settings.PicassoServicebaseUrl);
        }
    }
}
