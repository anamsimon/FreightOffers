using FreightOffers.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreightOffers.IExternalService
{
    public interface IExternalOfferService
    {
        Task<decimal> GetOffers(Consignment consigment);
    }
}
