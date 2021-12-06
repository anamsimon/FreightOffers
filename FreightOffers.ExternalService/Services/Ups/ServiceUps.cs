using FreightOffers.IExternalService;
using FreightOffers.Model;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System;
using AutoMapper;

namespace FreightOffers.ExternalService.Services.Ups
{
    public class ServiceUps : BaseExternalService, IExternalOfferService
    {
        private const string baseUrl = "https://61adbe4fd228a9001703aefb.mockapi.io";
        private const string endpoint = "/api/v1/ups";
        public ServiceUps(IHttpClientFactory clientFactory)
    : base(clientFactory)
        {
            client.BaseAddress = new Uri(baseUrl);
            url = endpoint;
            mapper = new Mapper(new MapperConfiguration(cfg =>
                   cfg.CreateMap<Consignment, ServiceUpsRequest>()
                   .ForMember(dest => dest.Destination, act => act.MapFrom(src => src.DestinationAddress))
                   .ForMember(dest => dest.Source, act => act.MapFrom(src => src.SourceAddress))
                   .ForMember(dest => dest.Packages, act => act.MapFrom(src => src.Packages))
               ));
        }
        async Task<decimal> IExternalOfferService.GetOffers(Consignment consigment)
        {

            ServiceUpsResponse result = await Helper.ExternalCall<ServiceUpsRequest, ServiceUpsResponse>(
             client, url, consigment, "xml", mapper, Helper.XMLSerializer, Helper.XMLDeserializer<ServiceUpsResponse>);

            return result.Quote;
        }


    }
}
