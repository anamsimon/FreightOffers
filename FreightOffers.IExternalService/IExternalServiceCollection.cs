using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightOffers.IExternalService
{
   public interface IExternalServiceCollection
    {
        IEnumerator<IExternalOfferService> GetServices();
    }
}
