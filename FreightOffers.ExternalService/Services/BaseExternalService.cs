using AutoMapper;
using FreightOffers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreightOffers.ExternalService.Services
{
    public class BaseExternalService
    {
        private protected HttpClient client;
        private protected Mapper mapper;
        private protected string url;

        public BaseExternalService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient();
        }
       
        
    }
}
