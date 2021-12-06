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

namespace FreightOffers.Api.Tests
{
    [TestFixture]
    public class TestFreightController
    {
        [SetUp]
        public void SetUp()
        {

        }


        [Test]
        public void BestOffer_InputConsignment_ReturnsOffer()
        {
            // Arrange
            var sourceAddress = new Address() { City = "Toronto", Country = "Canada", Line1 = "Line 1", Line2 = "Line 2", Postcode = "ABC 5000", Province = "Ontario" };
            var destinationAddress = new Address() { City = "Edmonton", Country = "Canada", Line1 = "Line 1", Line2 = "Line 2", Postcode = "QWE 5000", Province = "Alberta" };
            var package = new Package() { Height = 10, Depth = 10, Width = 10 };

            var consignment = new Consignment() { Packages = new List<Package> { package }, SourceAddress = sourceAddress, DestinationAddress = destinationAddress };
           
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
