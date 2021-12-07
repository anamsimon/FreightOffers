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

        private readonly string apiKey;
        private readonly string baseUrl;
        private readonly string endpoint;

        private protected Mapper mapper;
        private protected string url;


        public ServiceFedEx()            
        {
            var config = new ExternalServiceConfig();
            apiKey = config.GetValue("ExternalService:FedEx:ApiKey");
            baseUrl = config.GetValue("ExternalService:FedEx:BaseUrl");
            endpoint = config.GetValue("ExternalService:FedEx:Endpoint");
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
                new CustomHttpClient(), url, "json", data, apiKey);
            var result = Helper.JSONDeserializer<ServiceFedExResponse>(response);
            return result.Amount;

        }

    }
}
