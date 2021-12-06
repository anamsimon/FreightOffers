using System;

namespace FreightOffers.Model
{
    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Line1)
                && !string.IsNullOrEmpty(Line2)
                && !string.IsNullOrEmpty(City)
                && !string.IsNullOrEmpty(Province)
                && !string.IsNullOrEmpty(Postcode)
                && !string.IsNullOrEmpty(Country);
        }
    }
}
