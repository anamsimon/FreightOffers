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
        public ServiceFedEx(IHttpClientFactory clientFactory)
            : base(clientFactory)
        {
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
            //ServiceFedExRequest request = Helper.MapTo<ServiceFedExRequest>(consigment, mapper);
            //ServiceFedExResponse result;
            //var data = Helper.JSONSerializer(request);
            //var content = new StringContent(data, Encoding.UTF8, "application/json");
            //var response = await client.PostAsync(url, content);
            //if (response.IsSuccessStatusCode)
            //{
            //    var stringResponse = await response.Content.ReadAsStringAsync();
            //    result = Helper.JSONDeserializer<ServiceFedExResponse>(stringResponse);
            //}
            //else
            //{
            //    throw new HttpRequestException(response.ReasonPhrase);
            //}

            ServiceFedExResponse result = await Helper.ExternalCall<ServiceFedExRequest, ServiceFedExResponse>(
               client, url, consigment, "json", mapper, Helper.JSONSerializer, Helper.JSONDeserializer<ServiceFedExResponse>);

            return result.Amount;
        }




    }
}
