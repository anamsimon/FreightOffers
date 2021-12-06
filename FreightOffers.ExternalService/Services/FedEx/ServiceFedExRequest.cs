using FreightOffers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightOffers.ExternalService.Services.FedEx
{
    internal class ServiceFedExRequest
    {
        public Address Consignee { get; set; }
        public Address Consignor { get; set; }
        public Package[] Cartons { get; set; }
 
    }
}
