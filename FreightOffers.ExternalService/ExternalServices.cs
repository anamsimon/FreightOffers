using FreightOffers.ExternalService.Services.Dhl;
using FreightOffers.ExternalService.Services.FedEx;
using FreightOffers.ExternalService.Services.Ups;
using FreightOffers.IExternalService;
using System.Collections.Generic;
using System.Net.Http;

namespace FreightOffers.ExternalService
{
    public class ExternalServices : IExternalServices
    {
        private readonly IHttpClientFactory clientFactory;
        public ExternalServices(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public IEnumerator<IExternalOfferService> GetServices()
        {
            return new List<IExternalOfferService>
            { new ServiceDhl(clientFactory), 
                new ServiceFedEx(clientFactory),
                new ServiceUps(clientFactory)
            }.GetEnumerator();
        }
    }
}
