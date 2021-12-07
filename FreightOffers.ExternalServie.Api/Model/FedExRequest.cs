namespace FreightOffers.ExternalServie.Api.Model
{
    public class FedExRequest
    {
        public Address Consignee { get; set; }
        public Address Consignor { get; set; }
        public Package[] Cartons { get; set; }
    }
}
