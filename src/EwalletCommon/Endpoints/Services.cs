using EwalletCommon.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletCommon.Endpoints
{
    public interface IService
    {
        WalletService Wallet { get; }
    }

    public class Service : IService
    {
        readonly ProxySettings _settings;

        public WalletService Wallet { get; private set; }
        public Service(IOptions<ProxySettings> proxySettings)
        {
            _settings = proxySettings.Value;
            Wallet = new WalletService(_settings.EwalletServiceBaseUrl);
        }
    }
}
