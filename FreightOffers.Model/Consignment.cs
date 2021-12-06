using System;
using System.Collections.Generic;

namespace FreightOffers.Model
{
    public class Consignment
    {
        public Address SourceAddress { get; set; }
        public Address DestinationAddress { get; set; }
        public List<Package> Packages { get; set; }

        public bool IsValid()
        {
            return SourceAddress != null
                && DestinationAddress != null
                && Packages != null && SourceAddress.IsValid()
                && DestinationAddress.IsValid()
                && Packages.TrueForAll(x => x.IsValid());
        }
    }


}
