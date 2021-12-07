using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FreightOffers.ExternalService.Services
{
    public class ExternalServiceConfig
    {
        private readonly IConfiguration _config;
        public ExternalServiceConfig()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("services/externalservicesettings.json").Build();

        }

        public string GetValue(string key)
        {
            return _config[key];
        }
    }
}
