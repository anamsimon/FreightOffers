using AutoMapper;
using Service = FreightOffers.ExternalService.Services;
using FreightOffers.Model;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FreightOffers.ExternalService.Tests
{
    public class TestingHelper
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        //[Test]
        //public void ExternalCall_InputConsignment_ReturnsOffer()
        //{
        //    // Arrange
        //    Consignment consignment = TestHelper.Helper.ConstructConsignment();
        //    string url = "https://61adbe4fd228a9001703aefb.mockapi.io/api/v1/dhl";
            
        //    var mockHttpClient = new Mock<Service.ICustomHttpClient>();
        //    var content = new StringContent("", Encoding.UTF8,  "application/json");
        //    mockHttpClient.Setup(m => m.PostAsync(url, content))
        //        .ReturnsAsync(new HttpResponseMessage()
        //        {                    
        //            StatusCode = HttpStatusCode.OK,
        //            Content = new StringContent(
        //                JsonConvert.SerializeObject(new Services.Dhl.ServiceDhlResponse() { Total = 100 }),
        //                Encoding.UTF8, "application/json")
        //        });
            

        //   // Act
        //   var result = Task.Run(()=> Services.Helper.ExternalCall(mockHttpClient.Object, url, "json", ""));

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(100m, result);

        //}
    }
}