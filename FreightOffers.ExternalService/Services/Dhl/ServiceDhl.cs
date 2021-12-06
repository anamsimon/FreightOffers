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
    public class ServiceDhl : BaseExternalService, IExternalOfferService
    {
        public ServiceDhl(IHttpClientFactory clientFactory)
            : base(clientFactory)
        {
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
            ServiceDhlResponse result = await Helper.ExternalCall<ServiceDhlRequest, ServiceDhlResponse>(
                client, url, consigment, "json", mapper, Helper.JSONSerializer, Helper.JSONDeserializer<ServiceDhlResponse>);
            
            //ServiceDhlRequest request = Helper.MapTo<ServiceDhlRequest>(consigment, mapper);
            //ServiceDhlResponse result;
            //var data = Helper.JSONSerializer(request);
            //var content = new StringContent(data, Encoding.UTF8, "application/json");
            //var response = await client.PostAsync(url, content);
            //if (response.IsSuccessStatusCode)
            //{
            //    var stringResponse = await response.Content.ReadAsStringAsync();
            //    result = Helper.JSONDeserializer<ServiceDhlResponse>(stringResponse);
            //}
            //else
            //{
            //    throw new HttpRequestException(response.ReasonPhrase);
            //}

            return result.Total;
        }


    }
}
