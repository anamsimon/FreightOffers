using FreightOffers.Model;

namespace FreightOffers.ExternalService.Services.Ups
{
    public class ServiceUpsRequest
    {
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public Package[] Packages { get; set; }
 
    }
}
