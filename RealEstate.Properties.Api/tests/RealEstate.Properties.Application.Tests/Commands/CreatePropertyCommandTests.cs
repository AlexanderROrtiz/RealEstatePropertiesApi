using AutoMapper;
using Moq;
using RealEstate.Properties.Application.Commands.PropertiesCommand;
using RealEstate.Properties.Application.DTOs;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Application.Interfaces.Kafka;
using RealEstate.Properties.Application.Kafka.Interfaces;
using RealEstate.Properties.Domain.Entities;

namespace RealEstate.Properties.Application.Tests.Commands
{
    public class CreatePropertyCommandHandlerTests
    {
        private Mock<IPropertyRepository> _mockRepo;
        private Mock<IMapper> _mockMapper;
        private Mock<IPropertyEventProducer> _mockProducer;
        private Mock<IEventSelector> _mockSelector;
        private Mock<IOwnerRepository> _mockOwnerRep;
        private CreatePropertyCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IPropertyRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockProducer = new Mock<IPropertyEventProducer>();
            _mockSelector = new Mock<IEventSelector>();
            _mockOwnerRep = new Mock<IOwnerRepository>();

            _handler = new CreatePropertyCommandHandler(
                _mockRepo.Object,
                _mockMapper.Object,
                _mockProducer.Object,
                _mockSelector.Object,
                _mockOwnerRep.Object);
        }

        [Test]
        public async Task HandleCreatesPropertyWithOwnerName()
        {
            // Arrange
            var dto = new PropertyDto
            {
                Id = "prop-1",
                Name = "Casa Bonita",
                Address = "Calle 123",
                Price = 100_000,
                OwnerId = "owner1",
                ImageUrl = "img1.jpg"
            };

            var owner = new Owner
            {
                IdOwner = "owner1",
                Name = "JUAN DUEÑO"
            };

            var entity = new Property
            {
                IdProperty = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                IdOwner = dto.OwnerId,
                Images = new List<PropertyImage>
                {
                    new PropertyImage { File = dto.ImageUrl, Enabled = true }
                }
            };

            // Mocks
            _mockMapper.Setup(m => m.Map<Property>(It.IsAny<PropertyDto>())).Returns(entity);
            _mockRepo.Setup(r => r.CreateAsync(entity)).Returns(Task.CompletedTask);
            _mockOwnerRep.Setup(r => r.GetByIdAsync(dto.OwnerId)).ReturnsAsync(owner);
            _mockMapper.Setup(m => m.Map<PropertyDto>(It.IsAny<Property>())).Returns((Property p) =>
            {
                return new PropertyDto
                {
                    Id = p.IdProperty,
                    Name = p.Name,
                    Address = p.Address,
                    Price = p.Price,
                    OwnerId = p.IdOwner,
                    OwnerName = p.Owner?.Name,
                    ImageUrl = p.Images.Count > 0 ? p.Images[0].File : string.Empty
                };
            });
            _mockProducer.Setup(p => p.PublishAsync(It.IsAny<object>())).Returns(Task.CompletedTask);

            var command = new CreatePropertyCommand(dto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(dto.Name, result.Name);
            Assert.AreEqual(owner.Name, result.OwnerName);
            Assert.AreEqual(dto.ImageUrl, result.ImageUrl);
            Assert.AreEqual(dto.Id, result.Id);
        }
    }
}