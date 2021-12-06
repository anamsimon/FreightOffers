using FreightOffers.Model;
using System;
using System.Collections.Generic;

namespace TestHelper
{
    public class Helper
    {

        public static FreightOffers.Model.Consignment ConstructConsignment()
        {

            var sourceAddress = new Address() { City = "Toronto", Country = "Canada", Line1 = "Line 1", Line2 = "Line 2", Postcode = "ABC 5000", Province = "Ontario" };
            var destinationAddress = new Address() { City = "Edmonton", Country = "Canada", Line1 = "Line 1", Line2 = "Line 2", Postcode = "QWE 5000", Province = "Alberta" };
            var package = new Package() { Height = 10, Depth = 10, Width = 10 };

            var consignment = new FreightOffers.Model.Consignment() { Packages = new List<Package> { package }, SourceAddress = sourceAddress, DestinationAddress = destinationAddress };
            return consignment;
        }
    }
}
