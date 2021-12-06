using FreightOffers.Model;
using System;
using System.Threading.Tasks;

namespace FreightOffers.IService
{
    public interface IFreightService
    {
        decimal BestOffer(Consignment consignment);
    }
}
