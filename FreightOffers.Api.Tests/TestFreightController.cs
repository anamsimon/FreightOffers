using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using Moq;
using FreightOffers.IService;
using FreightOffers.Model;
using FreightOffers.Api.Controllers;
using TestHelper;

namespace FreightOffers.Api.Tests
{
    [TestFixture]
    public class TestFreightController
    {
        

        [Test]
        public void BestOffer_InputConsignment_ReturnsOffer()
        {
            // Arrange
            var consignment = Helper.ConstructConsignment();
            var mockFreightService = new Mock<IFreightService>();
            mockFreightService.Setup(m => m.BestOffer(consignment)).Returns(200);
            var controller = new FreightController(mockFreightService.Object);

            // Act
            var result = controller.BestOffer(consignment);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result);

        }

        [Test]
        public void BestOffer_InputInvalidConsignment_ThrowsError()
        {
            // Arrange
            var mockConsignment = new Mock<Consignment>();
            var mockFreightService = new Mock<IFreightService>();
            mockFreightService.Setup(m => m.BestOffer(mockConsignment.Object)).Returns(200);
            var controller = new FreightController(mockFreightService.Object);

            // Assert
            var ex = Assert.Throws<ArgumentException>(() => controller.BestOffer(mockConsignment.Object));
            Assert.That(ex.Message, Is.EqualTo("Invalid Input"));
        }
    }
}
