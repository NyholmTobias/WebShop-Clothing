using AutoMapper;
using FluentValidation.Results;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopServices;
using WebshopServices.Features.LineItemServices;
using WebshopServices.Validation.Interfaces;
using WebshopShared.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.Models;
using Xunit;

namespace WebShopServiceTests
{
    public class LineItemServiceTests
    {
        TestHelper _testHelper = new TestHelper();
        private readonly IMapper _mapper;
        private readonly Mock<ILineItemRepository> _mockLineItemRepository;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly Mock<IValidatorFactory> _mockValidatorFactory;
        private readonly Mock<IValidator> _mockValidator;

        public LineItemServiceTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();

            _mockLineItemRepository = new Mock<ILineItemRepository>();
            _mockItemRepository = new Mock<IItemRepository>();
            _mockValidatorFactory = new Mock<IValidatorFactory>();
            _mockValidator = new Mock<IValidator>();

            _mockValidatorFactory.Setup(valiFactory => valiFactory.Get(It.IsAny<string>())).Returns(_mockValidator.Object);
            _mockValidator.Setup(validator => validator.Validate(It.IsAny<IValidatable>())).ReturnsAsync(new ValidationResult { });

        }

        [Fact]
        public async Task CreateLineItemShouldCallGetFactoryOnce()
        {
            //Arrange
            var lineItemRequest = _testHelper.CreateLineItemRequest();
            _mockLineItemRepository.Setup(lineItemRepo => lineItemRepo.AddAsync(It.IsAny<LineItem>()));

            //Act
            var sut = new LineItemService(_mapper, _mockLineItemRepository.Object, _mockItemRepository.Object,  _mockValidatorFactory.Object);
            await sut.CreateLineItem(lineItemRequest);
            //Assert
            _mockValidatorFactory.Verify(valiFactory => valiFactory.Get(It.IsAny<string>()), Times.Once);

        }

        [Fact]
        public async Task CreateLineItemShouldCallValidateOnce()
        {
            //Arrange
            var lineItemRequest = _testHelper.CreateLineItemRequest();
            _mockLineItemRepository.Setup(lineItemRepo => lineItemRepo.AddAsync(It.IsAny<LineItem>()));

            //Act
            var sut = new LineItemService(_mapper, _mockLineItemRepository.Object, _mockItemRepository.Object, _mockValidatorFactory.Object);
            await sut.CreateLineItem(lineItemRequest);
            //Assert
            _mockValidator.Verify(validator => validator.Validate(It.IsAny<IValidatable>()), Times.Once);

        }

        [Fact]
        public async Task CreateLineItemShouldCallAddASyncOnce()
        {
            //Arrange
            var lineItemRequest = _testHelper.CreateLineItemRequest();
            _mockLineItemRepository.Setup(lineItemRepo => lineItemRepo.AddAsync(It.IsAny<LineItem>()));

            //Act
            var sut = new LineItemService(_mapper, _mockLineItemRepository.Object, _mockItemRepository.Object, _mockValidatorFactory.Object);
            await sut.CreateLineItem(lineItemRequest);
            //Assert
            _mockLineItemRepository.Verify(lineItemRepo => lineItemRepo.AddAsync(It.IsAny<LineItem>()));

        }

        [Fact]
        public async Task CreateLineItemShouldSet()
        {
            //Arrange
            var lineItemRequest = _testHelper.CreateLineItemRequest();
            _mockLineItemRepository.Setup(lineItemRepo => lineItemRepo.AddAsync(It.IsAny<LineItem>()));

            //Act
            var sut = new LineItemService(_mapper, _mockLineItemRepository.Object, _mockItemRepository.Object, _mockValidatorFactory.Object);
            var result = await sut.CreateLineItem(lineItemRequest);
            //Assert
            result.Quantity.ShouldBe(10);

        }

        [Fact]
        public void DecreseStockQuantityShouldDecreaseStockQuantity()
        {
            //Arrange
            var lineItem= _testHelper.CreateLineItem();
            

            //Act
            var sut = new LineItemService(_mapper, _mockLineItemRepository.Object, _mockItemRepository.Object, _mockValidatorFactory.Object);
            var result = sut.DecreseStockQuantity(lineItem);
            //Assert
            result.Item.StockQuantity.ShouldBe(2);

        }
    }
}
