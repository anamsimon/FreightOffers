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
        private readonly HttpClient client;
        private readonly Mapper mapper;
        private readonly string url;
        public ServiceFedEx(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient();
            client.BaseAddress = new Uri("https://61adbe4fd228a9001703aefb.mockapi.io");
            url = string.Format("/api/v1/fedex");
            mapper = new Mapper(new MapperConfiguration(cfg =>
                   cfg.CreateMap<Consignment, ServiceFedExRequest>()
                   .ForMember(dest => dest.Consignee, act => act.MapFrom(src => src.DestinationAddress))
                   .ForMember(dest => dest.Consignor, act => act.MapFrom(src => src.SourceAddress))
                   .ForMember(dest => dest.Cartons, act => act.MapFrom(src => src.Packages))
               ));
        }
        async Task<decimal> IExternalOfferService.GetOffers(Consignment consigment)
        {
            ServiceFedExRequest request = MapTo(consigment);
            ServiceFedExResponse result;
            var json = JsonSerializer.Serialize(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                result = MapFrom(stringResponse);
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result.Amount;
        }

        ServiceFedExRequest MapTo(Consignment consigment)
        {
            return mapper.Map<ServiceFedExRequest>(consigment);
        }

        static ServiceFedExResponse MapFrom(string stringResponse)
        {
            return JsonSerializer.Deserialize<ServiceFedExResponse>(stringResponse,
                     new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
