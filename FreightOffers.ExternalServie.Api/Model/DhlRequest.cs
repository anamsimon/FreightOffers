namespace FreightOffers.ExternalServie.Api.Model
{
    public class DhlRequest
    {
        public Address ContactAddress { get; set; }
        public Address WarehouseAddress { get; set; }
        public Package[] Dimensions { get; set; }
    }
}
