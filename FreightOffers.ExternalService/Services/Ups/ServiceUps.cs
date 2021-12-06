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
        private readonly HttpClient client;
        private readonly Mapper mapper;
        private readonly string url;
        public ServiceUps(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient();
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
            ServiceUpsRequest request = MapTo(consigment);
            ServiceUpsResponse result;

            var xml = new XmlSerializer(typeof(ServiceUpsRequest));
            TextWriter writer = new StringWriter();
            xml.Serialize(writer, request);
            var data = new StringContent(writer.ToString(), Encoding.UTF8, "application/xml");
            var response = await client.PostAsync(url, data);
            if (response.IsSuccessStatusCode)
            {
                //var stringResponse = await response.Content.ReadAsStringAsync();
                var stringResponse = "<ServiceUpsResponse><Quote>9.0</Quote></ServiceUpsResponse>";
                result = MapFrom(stringResponse);
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result.Quote;
        }

        ServiceUpsRequest MapTo(Consignment consigment)
        {
            return mapper.Map<ServiceUpsRequest>(consigment);
        }

        static ServiceUpsResponse MapFrom(string stringResponse)
        {
            var reader = new StringReader(stringResponse);
            var xmlD = new XmlSerializer(typeof(ServiceUpsResponse));
            return (ServiceUpsResponse)xmlD.Deserialize(reader);
        }
    }
}
