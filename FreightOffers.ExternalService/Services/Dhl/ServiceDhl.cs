﻿using FreightOffers.IExternalService;
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


        private const string apiKey = "yk2BS9Xje9";
        private const string baseUrl = "https://localhost:44356";
        private const string endpoint = "/api/ups/dhl";

        //private const string baseUrl = "https://61adbe4fd228a9001703aefb.mockapi.io";
        //private const string endpoint = "/api/v1/dhl";
        private protected Mapper mapper;
        private protected string url;

        public ServiceDhl()
        {
            url = string.Format("{0}{1}", baseUrl, endpoint);
            mapper = new Mapper(new MapperConfiguration(cfg =>
                   cfg.CreateMap<Consignment, ServiceDhlRequest>()
                   .ForMember(dest => dest.ContactAddress, act => act.MapFrom(src => src.DestinationAddress))
                   .ForMember(dest => dest.WarehouseAddress, act => act.MapFrom(src => src.SourceAddress))
                   .ForMember(dest => dest.Dimensions, act => act.MapFrom(src => src.Packages))
               ));
            var config = new ExternalServiceConfig();
            var apiKey = config.GetValue("ExternalService:Dhl:ApiKey");
        }
        async Task<decimal> IExternalOfferService.GetOffers(Consignment consignment)
        {
            var request = Helper.MapTo<ServiceDhlRequest>(consignment, mapper);
            var data = Helper.JSONSerializer(request);
            string response = await Helper.ExternalCall(
                new CustomHttpClient(), url, "json", data, apiKey);
            var result = Helper.JSONDeserializer<ServiceDhlResponse>(response);
            return result.Total;
        }


    }
}
