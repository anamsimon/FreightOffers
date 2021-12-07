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
    public class ServiceUps : IExternalOfferService
    {
        private const string baseUrl = "https://61adbe4fd228a9001703aefb.mockapi.io";
        private const string endpoint = "/api/v1/ups";
        private protected Mapper mapper;
        private protected string url;

        public ServiceUps()
        {
            url = string.Format("{0}{1}", baseUrl, endpoint);
            mapper = new Mapper(new MapperConfiguration(cfg =>
                   cfg.CreateMap<Consignment, ServiceUpsRequest>()
                   .ForMember(dest => dest.Destination, act => act.MapFrom(src => src.DestinationAddress))
                   .ForMember(dest => dest.Source, act => act.MapFrom(src => src.SourceAddress))
                   .ForMember(dest => dest.Packages, act => act.MapFrom(src => src.Packages))
               ));
        }
        async Task<decimal> IExternalOfferService.GetOffers(Consignment consignment)
        {
            var request = Helper.MapTo<ServiceUpsRequest>(consignment, mapper);
            var data = Helper.XMLSerializer(request);
            string response = await Helper.ExternalCall(
                new CustomHttpClient(), url, "xml", data);
            var result = Helper.XMLDeserializer<ServiceUpsResponse>(response);
            return result.Quote;

        }


    }
}
