using FreightOffers.IExternalService;
using FreightOffers.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;

namespace FreightOffers.ExternalService.Services.Dhl
{
    public class ServiceDhl : IExternalOfferService
    {
        private readonly HttpClient client;
        private readonly Mapper mapper;
        private readonly string url;
        public ServiceDhl(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient();

            client.BaseAddress = new Uri("https://61adbe4fd228a9001703aefb.mockapi.io");
            url = string.Format("/api/v1/dhl");
            mapper = new Mapper(new MapperConfiguration(cfg =>
                   cfg.CreateMap<Consignment, ServiceDhlRequest>()
                   .ForMember(dest => dest.ContactAddress, act => act.MapFrom(src => src.DestinationAddress))
                   .ForMember(dest => dest.WarehouseAddress, act => act.MapFrom(src => src.SourceAddress))
                   .ForMember(dest => dest.Dimensions, act => act.MapFrom(src => src.Packages))
               ));
        }
        async Task<decimal> IExternalOfferService.GetOffers(Consignment consigment)
        {
            ServiceDhlRequest request = MapTo(consigment);
            ServiceDhlResponse result;
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

            return result.Total;
        }

        ServiceDhlRequest MapTo(Consignment consigment)
        {
            return mapper.Map<ServiceDhlRequest>(consigment);
        }

        static ServiceDhlResponse MapFrom(string stringResponse)
        {
            return JsonSerializer.Deserialize<ServiceDhlResponse>(stringResponse,
                     new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}
