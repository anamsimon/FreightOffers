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
        public ServiceUps(IHttpClientFactory clientFactory)
    : base(clientFactory)
        {
            client.BaseAddress = new Uri("https://61adbe4fd228a9001703aefb.mockapi.io");
            url = string.Format("/api/v1/ups");
            mapper = new Mapper(new MapperConfiguration(cfg =>
                   cfg.CreateMap<Consignment, ServiceUpsRequest>()
                   .ForMember(dest => dest.Destination, act => act.MapFrom(src => src.DestinationAddress))
                   .ForMember(dest => dest.Source, act => act.MapFrom(src => src.SourceAddress))
                   .ForMember(dest => dest.Packages, act => act.MapFrom(src => src.Packages))
               ));
        }
        async Task<decimal> IExternalOfferService.GetOffers(Consignment consigment)
        {
            //ServiceUpsRequest request = Helper.MapTo<ServiceUpsRequest>(consigment, mapper);
            //ServiceUpsResponse result;

            //var data = Helper.XMLSerializer(request);
            //var content = new StringContent(data, Encoding.UTF8, "application/xml");
            //var response = await client.PostAsync(url, content);
            //if (response.IsSuccessStatusCode)
            //{
            //    //var stringResponse = await response.Content.ReadAsStringAsync();
            //    var stringResponse = "<ServiceUpsResponse><Quote>9.0</Quote></ServiceUpsResponse>";
            //    result = Helper.XMLDeserializer<ServiceUpsResponse>(stringResponse);
            //}
            //else
            //{
            //    throw new HttpRequestException(response.ReasonPhrase);
            //}

            ServiceUpsResponse result = await Helper.ExternalCall<ServiceUpsRequest, ServiceUpsResponse>(
             client, url, consigment, "xml", mapper, Helper.XMLSerializer, Helper.XMLDeserializer<ServiceUpsResponse>);


            return result.Quote;
        }


    }
}
