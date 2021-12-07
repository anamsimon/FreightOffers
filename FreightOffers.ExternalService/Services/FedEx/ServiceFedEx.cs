using FreightOffers.IExternalService;
using FreightOffers.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;

namespace FreightOffers.ExternalService.Services.FedEx
{
    public class ServiceFedEx : IExternalOfferService
    {
        private const string baseUrl = "https://localhost:44356";
        private const string endpoint = "/api/ups/fedex";
        //private const string baseUrl = "https://61adbe4fd228a9001703aefb.mockapi.io";
        //private const string endpoint = "/api/v1/fedex";
        private protected Mapper mapper;
        private protected string url;


        public ServiceFedEx()
            
        {
            url = string.Format("{0}{1}", baseUrl, endpoint);
            mapper = new Mapper(new MapperConfiguration(cfg =>
                   cfg.CreateMap<Consignment, ServiceFedExRequest>()
                   .ForMember(dest => dest.Consignee, act => act.MapFrom(src => src.DestinationAddress))
                   .ForMember(dest => dest.Consignor, act => act.MapFrom(src => src.SourceAddress))
                   .ForMember(dest => dest.Cartons, act => act.MapFrom(src => src.Packages))
               ));
        }
        async Task<decimal> IExternalOfferService.GetOffers(Consignment consignment)
        {
            var request = Helper.MapTo<ServiceFedExRequest>(consignment, mapper);
            var data = Helper.JSONSerializer(request);
            string response = await Helper.ExternalCall(
                new CustomHttpClient(), url, "json", data);
            var result = Helper.JSONDeserializer<ServiceFedExResponse>(response);
            return result.Amount;

        }




    }
}
