 
using FreightOffers.IExternalService;
using System.Collections.Generic;
using System.Net.Http;

namespace FreightOffers.ExternalService
{
    public class ExternalServiceCollection : IExternalServiceCollection
    {
        private readonly IHttpClientFactory clientFactory;
        public ExternalServiceCollection(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public IEnumerator<IExternalOfferService> GetServices()
        {
            return new List<IExternalOfferService>
            { new Services.Dhl.ServiceDhl(clientFactory), 
                new Services.FedEx.ServiceFedEx(clientFactory),
                new Services.Ups.ServiceUps(clientFactory)
            }.GetEnumerator();
        }
    }
}
