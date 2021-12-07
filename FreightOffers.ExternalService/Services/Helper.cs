using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FreightOffers.ExternalService.Services
{
    public class Helper
    {
        public static string JSONSerializer<T>(T request)
        {
            return JsonSerializer.Serialize(request);
        }
        public static string XMLSerializer<T>(T request)
        {
            var xml = new XmlSerializer(typeof(T));
            TextWriter writer = new StringWriter();
            xml.Serialize(writer, request);
            return writer.ToString();
        }

        public static T JSONDeserializer<T>(string response)
        {
            return JsonSerializer.Deserialize<T>(response,
                         new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        public static T XMLDeserializer<T>(string response)
        {
            var reader = new StringReader(response);
            var xmlD = new XmlSerializer(typeof(T));
            return (T)xmlD.Deserialize(reader);
        }
        public static T MapTo<T>(object consigment, Mapper mapper)
        {
            return mapper.Map<T>(consigment);
        }

        public static async Task<string> ExternalCall
            (ICustomHttpClient client, string url, string mediatype,
            string data)
        {            
            string stringResponse;
            var content = new StringContent(data, Encoding.UTF8,
                string.Format("application/{0}", mediatype));
            
            var response = await client.PostAsync(url, content);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                stringResponse = await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return stringResponse;

        }
    }

    public interface ICustomHttpClient
    {
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
    }

    public class CustomHttpClient : ICustomHttpClient
    {
        private readonly HttpClient _httpClient;
        public CustomHttpClient()
        {
            _httpClient = new HttpClient();
        }
        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
          return await _httpClient.PostAsync(url, content);
        }
    }
}
