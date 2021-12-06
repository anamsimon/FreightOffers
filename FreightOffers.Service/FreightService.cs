using FreightOffers.IExternalService;
using FreightOffers.IService;
using FreightOffers.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreightOffers.Service
{
    public class FreightService : IFreightService
    {
        private readonly IExternalServiceCollection _externalServiceList;
        public FreightService(IExternalServiceCollection externalServiceList)
        {
            this._externalServiceList = externalServiceList;
        }
        public decimal BestOffer(Consignment consignment)
        {
            var tasks = new List<Task<decimal>>();
            var services = this._externalServiceList.GetServices();
            while (services.MoveNext())
            {
                tasks.Add(services.Current.GetOffers(consignment));
            }
            Task t = Task.WhenAll(tasks);

            try
            {
                t.Wait();
            }
            catch
            {

            }

            decimal? bestOffer = null;
            tasks.ForEach(task =>
            {
                bestOffer = task.Status == TaskStatus.RanToCompletion
                && (!bestOffer.HasValue || task.Result < bestOffer.Value) ? task.Result : bestOffer;
            });
            return bestOffer ?? 0;
        }
    }
}
