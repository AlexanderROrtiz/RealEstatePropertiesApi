using AutoMapper;
using Moq;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Application.Queries.Properties;
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Application.Tests.Queries
{
    public class GetPropertiesQueryHandlerTests
    {
        private Mock<IPropertyRepository> _mockRepo;
        private Mock<IMapper> _mockMapper;
        private GetPropertiesQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IPropertyRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetPropertiesQueryHandler(_mockRepo.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Handle_ReturnsFilteredPropertiesList()
        {
            // Arrange
            var query = new GetPropertiesQuery("Casa", "Centro", 100000, 300000);

            var properties = new List<Property>
            {
                new Property { IdProperty = "1", Name = "Casa Centro", Address = "Centro", Price = 150000, IdOwner = "owner1" },
                new Property { IdProperty = "2", Name = "Casa Bonita", Address = "Centro", Price = 250000, IdOwner = "owner2" }
            };

            var dtos = new List<PropertyDto>
            {
                new PropertyDto { Id = "1", Name = "Casa Centro", Address = "Centro", Price = 150000, OwnerId = "owner1" },
                new PropertyDto { Id = "2", Name = "Casa Bonita", Address = "Centro", Price = 250000, OwnerId = "owner2" }
            };

            _mockRepo.Setup(r => r.FilterAsync("Casa", "Centro", 100000, 300000))
                     .ReturnsAsync(properties);
            _mockMapper.Setup(m => m.Map<IEnumerable<PropertyDto>>(properties))
                       .Returns(dtos);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, ((List<PropertyDto>)result).Count);
            Assert.AreEqual("Casa Centro", ((List<PropertyDto>)result)[0].Name);
            Assert.AreEqual("Casa Bonita", ((List<PropertyDto>)result)[1].Name);
        }
    }
}
