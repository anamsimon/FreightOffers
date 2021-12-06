using FreightOffers.IExternalService;
using FreightOffers.IService;
using FreightOffers.Model;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TestHelper;
using FreightOffers.Model;
using System.Threading.Tasks;

namespace FreightOffers.Service.Tests
{
    public class TestFreightService
    {
        [Test]
        public void BestOffer_InputConsignment_GetServicesIsCalled_ReturnsLowestOffer()
        {
            // Arrange

            Consignment consignment = Helper.ConstructConsignment();

            var externalOfferService = ServiceCollection(consignment);
            var mockExternalServiceList = new Mock<IExternalServiceCollection>();

            mockExternalServiceList.Setup(m => m.GetServices())
                .Returns(externalOfferService);

            var freightService = new FreightService(mockExternalServiceList.Object);
            // Act
            var result = freightService.BestOffer(consignment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(100m, result);
            Mock.Get(mockExternalServiceList.Object).Verify(x => x.GetServices(), Times.Once);
        }

        private static IEnumerator<IExternalOfferService> ServiceCollection(Consignment consignment)
        {
            var offer1 = new Mock<IExternalOfferService>();
            offer1.Setup(x => x.GetOffers(consignment)).Returns(Task.FromResult(200m));

            var offer2 = new Mock<IExternalOfferService>();
            offer2.Setup(x => x.GetOffers(consignment)).Returns(Task.FromResult(100m));

            yield return offer1.Object;
            yield return offer2.Object;
        }
    }
}