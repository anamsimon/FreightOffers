using FreightOffers.Model;

namespace FreightOffers.ExternalService.Services.Dhl
{
    internal class ServiceDhlRequest
    {
        public Address ContactAddress { get; set; }
        public Address WarehouseAddress { get; set; }
        public Package[] Dimensions { get; set; }
    }
}
