namespace FreightOffers.ExternalServie.Api.Model
{

    public class ServiceUpsRequest
    {
       
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public Package[] Packages { get; set; }
    }
}
