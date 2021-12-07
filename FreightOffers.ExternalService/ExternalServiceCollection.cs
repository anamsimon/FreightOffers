 
using FreightOffers.IExternalService;
using System.Collections.Generic;
using System.Net.Http;

namespace FreightOffers.ExternalService
{
    public class ExternalServiceCollection : IExternalServiceCollection
    {
       
        public IEnumerator<IExternalOfferService> GetServices()
        {
            return new List<IExternalOfferService>
            { 
                new Services.Dhl.ServiceDhl(), 
                new Services.FedEx.ServiceFedEx(),
                new Services.Ups.ServiceUps()
            }.GetEnumerator();
        }
    }
}
