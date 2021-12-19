using AutoMapper;
using FluentValidation.Results;
using Moq;
using Shouldly;
using System;
using System.Threading.Tasks;
using WebshopServices;
using WebshopServices.Features.ItemServices;
using WebshopServices.Validation.Interfaces;
using WebshopShared.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using Xunit;

namespace WebShopServiceTests
{
    public class ItemServiceTests
    {
        TestHelper _testHelper = new TestHelper();
        private readonly IMapper _mapper;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly Mock<IValidatorFactory> _mockValidatatorFactory;
        private readonly Mock<IValidator> _mockValidator;
        private readonly Mock<ILineItemRepository> _mockLineItemRepsoitory;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        public ItemServiceTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();

            _mockItemRepository = new Mock<IItemRepository>();
            _mockValidatatorFactory = new Mock<IValidatorFactory>();
            _mockValidator = new Mock<IValidator>();
            _mockLineItemRepsoitory = new Mock<ILineItemRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();


        _mockItemRepository.Setup(itemRepo => itemRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Item());

            _mockValidatatorFactory.Setup(valiFactory => valiFactory.Get(It.IsAny<string>())).Returns(_mockValidator.Object);
            _mockValidator.Setup(validator => validator.Validate(It.IsAny<IValidatable>())).ReturnsAsync(new ValidationResult { });
        }
        [Fact]
        public async Task CreateItemShouldCallGetFactoryOnce()
        {
            //Arrange
            _mockItemRepository.Setup(itemRepo => itemRepo.AddAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            await sut.CreateItem(new ItemRequest());

            //Assert
            _mockValidatatorFactory.Verify(valiFactory => valiFactory.Get(It.IsAny<string>()), Times.Once());

        }

        [Fact]
        public async Task CreateItemShouldCallValidateOnce()
        {
            //Arrange
            _mockItemRepository.Setup(itemRepo => itemRepo.AddAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            await sut.CreateItem(new ItemRequest());

            //Assert
            _mockValidator.Verify(vali => vali.Validate(It.IsAny<IValidatable>()), Times.Once());

        }

        [Fact]
        public async Task CreateItemShouldCallAddASyncOnce()
        {
            //Arrange
            _mockItemRepository.Setup(itemRepo => itemRepo.AddAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            await sut.CreateItem(new ItemRequest());

            //Assert
            _mockItemRepository.Verify(itemRepo => itemRepo.AddAsync(It.IsAny<Item>()), Times.Once());

        }
        [Fact]
        public async Task CreateItemShouldSetName()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.AddAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            var result = await sut.CreateItem(itemRequest);

            //Assert
            result.Name.ShouldBe(itemRequest.Name);
        }
        
        [Fact]
        public async Task CreateItemShouldSetPrice()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.AddAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            var result = await sut.CreateItem(itemRequest);

            //Assert
            result.Price.ShouldBe(itemRequest.Price);
        }

        [Fact]
        public async Task CreateItemShouldSetDescription()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.AddAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            var result = await sut.CreateItem(itemRequest);

            //Assert
            result.Description.ShouldBe(itemRequest.Description);
        }


        [Fact]
        public async Task CreateItemShouldSetStockQuantity()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.AddAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            var result = await sut.CreateItem(itemRequest);

            //Assert
            result.StockQuantity.ShouldBe(itemRequest.StockQuantity);
        }

        [Fact]
        public async Task CreateItemShouldSetPublished()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.AddAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            var result = await sut.CreateItem(itemRequest);

            //Assert
            result.Published.ShouldBe(itemRequest.Published);
        }

        [Fact]
        public async Task DeleteItemShouldCallGetByIdAsyncOnce()
        {
            //Arrange

            _mockItemRepository.Setup(itemRepo => itemRepo.DeleteAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            await sut.DeleteItem(It.IsAny<Guid>());

            //Assert
            _mockItemRepository.Verify(itemRepo => itemRepo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteItemShouldCallDeleteAsyncOnce()
        {
            //Arrange

            _mockItemRepository.Setup(itemRepo => itemRepo.DeleteAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            await sut.DeleteItem(It.IsAny<Guid>());

            //Assert
            _mockItemRepository.Verify(itemRepo => itemRepo.DeleteAsync(It.IsAny<Item>()), Times.Once);
        }

        [Fact]
        public async Task GetAllItemsShouldCallListAllAsyncOnce()
        {
            //Arrange

            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            await sut.GetAllItems();

            //Assert
            _mockItemRepository.Verify(itemRepo => itemRepo.ListAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsyncShouldCallGetByIdAsync()
        {
            //Arrange
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            await sut.GetItemById(It.IsAny<Guid>());
            //Assert
            _mockItemRepository.Verify(itemRepo => itemRepo.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task UpdateItemShouldCallGetFromFactoryOnce()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.UpdateAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            await sut.UpdateItem(itemRequest);
            //
            _mockValidatatorFactory.Verify(valiFactory => valiFactory.Get(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task UpdateItemShouldCallValidateOnce()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.UpdateAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            await sut.UpdateItem(itemRequest);
            //
            _mockValidator.Verify(vali => vali.Validate(It.IsAny<IValidatable>()), Times.Once());
        }

        [Fact]
        public async Task UpdateItemShouldCallUpdateAsyncOnce()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.UpdateAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            await sut.UpdateItem(itemRequest);
            //
            _mockItemRepository.Verify(itemRepo => itemRepo.UpdateAsync(It.IsAny<Item>()), Times.Once());
        }

        [Fact]
        public async Task UpdateItemShouldSetName()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.UpdateAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            var result = await sut.UpdateItem(itemRequest);
            //
            result.Name.ShouldBe(itemRequest.Name);
        }

        [Fact]
        public async Task UpdateItemShouldSetPrice()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.UpdateAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            var result = await sut.UpdateItem(itemRequest);
            //
            result.Price.ShouldBe(itemRequest.Price);
        }

        [Fact]
        public async Task UpdateItemShouldSetStockQuantity()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.UpdateAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            var result = await sut.UpdateItem(itemRequest);
            //
            result.StockQuantity.ShouldBe(itemRequest.StockQuantity);
        }

        [Fact]
        public async Task UpdateItemShouldSetDescription()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.UpdateAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            var result = await sut.UpdateItem(itemRequest);
            //
            result.Description.ShouldBe(itemRequest.Description);
        }

        [Fact]
        public async Task UpdateItemShouldSetPublished()
        {
            //Arrange
            var itemRequest = _testHelper.CreateItemRequest();
            _mockItemRepository.Setup(itemRepo => itemRepo.UpdateAsync(It.IsAny<Item>()));
            //Act
            var sut = new ItemService(_mapper, _mockItemRepository.Object, _mockValidatatorFactory.Object, _mockLineItemRepsoitory.Object, _mockOrderRepository.Object);
            var result = await sut.UpdateItem(itemRequest);
            //
            result.Published.ShouldBe(itemRequest.Published);
        }

    }
}