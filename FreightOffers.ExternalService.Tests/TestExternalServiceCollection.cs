using FreightOffers.IExternalService;
using NUnit.Framework;
using System.Collections.Generic;

namespace FreightOffers.ExternalService.Tests
{
    public class TestExternalServiceCollection
    {
        [Test]
        public void ExternalServiceCollection_GetServices_ReturnsServices()
        {
            // Arrange

            // Act
            var result = new ExternalServiceCollection().GetServices();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.InstanceOf<IEnumerator<IExternalOfferService>>());

        }

    }
}
