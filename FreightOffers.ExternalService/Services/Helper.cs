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
    internal class Helper
    {
        internal static string JSONSerializer<T>(T request)
        {
            return JsonSerializer.Serialize(request);
        }
        internal static string XMLSerializer<T>(T request)
        {
            var xml = new XmlSerializer(typeof(T));
            TextWriter writer = new StringWriter();
            xml.Serialize(writer, request);
            return writer.ToString();
        }

        internal static T JSONDeserializer<T>(string response)
        {
            return JsonSerializer.Deserialize<T>(response,
                         new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        internal static T XMLDeserializer<T>(string response)
        {
            var reader = new StringReader(response);
            var xmlD = new XmlSerializer(typeof(T));
            return (T)xmlD.Deserialize(reader);
        }
        internal static T MapTo<T>(object consigment, Mapper mapper)
        {
            return mapper.Map<T>(consigment);
        }

        internal static async Task<T1> ExternalCall<T, T1>
            (HttpClient client, string url, object consigment, string mediatype,
            Mapper mapper, Func<T, string> serializer, Func<string, T1> deserializer)
        {
            T request = MapTo<T>(consigment, mapper);
            T1 result;

            var data = serializer(request);
            var content = new StringContent(data, Encoding.UTF8,
                string.Format("application/{0}", mediatype));
            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                result = deserializer(stringResponse);
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }

            return result;

        }
    }
}
