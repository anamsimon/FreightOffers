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
            string result = string.Empty;

            using (var writer = new Utf8StringWriter())
            {
                xml.Serialize(writer, request);

                result = writer.ToString();
            }
            return result;
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
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
            string data, string apiKey)
        {
            string mediaTypeName = string.Format("application/{0}", mediatype);
            string stringResponse;
            var content = new StringContent(data, Encoding.UTF8, mediaTypeName);

            var response = await client.PostAsync(url, content, mediaTypeName, apiKey);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
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
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string mediaType, string apiKey);
    }

    public class CustomHttpClient : ICustomHttpClient
    {
        private readonly HttpClient _httpClient;
        public CustomHttpClient()
        {
            _httpClient = new HttpClient();
        }
        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string mediaType, string apiKey)
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", mediaType);
            _httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);            
            return await _httpClient.PostAsync(url, content);
        }
    }
}
