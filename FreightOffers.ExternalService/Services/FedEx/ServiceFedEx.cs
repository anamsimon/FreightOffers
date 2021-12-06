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
    public class ServiceFedEx : BaseExternalService, IExternalOfferService
    {
        private const string baseUrl = "https://61adbe4fd228a9001703aefb.mockapi.io";
        private const string endpoint = "/api/v1/fedex";
        public ServiceFedEx(IHttpClientFactory clientFactory)
            : base(clientFactory)
        {
            client.BaseAddress = new Uri(baseUrl);
            url = endpoint;
            mapper = new Mapper(new MapperConfiguration(cfg =>
                   cfg.CreateMap<Consignment, ServiceFedExRequest>()
                   .ForMember(dest => dest.Consignee, act => act.MapFrom(src => src.DestinationAddress))
                   .ForMember(dest => dest.Consignor, act => act.MapFrom(src => src.SourceAddress))
                   .ForMember(dest => dest.Cartons, act => act.MapFrom(src => src.Packages))
               ));
        }
        async Task<decimal> IExternalOfferService.GetOffers(Consignment consigment)
        {

            ServiceFedExResponse result = await Helper.ExternalCall<ServiceFedExRequest, ServiceFedExResponse>(
               client, url, consigment, "json", mapper, Helper.JSONSerializer, Helper.JSONDeserializer<ServiceFedExResponse>);

            return result.Amount;
        }




    }
}
