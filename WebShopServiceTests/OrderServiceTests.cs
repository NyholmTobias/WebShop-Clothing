using AutoMapper;
using FluentValidation.Results;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServices;
using WebshopServices.Features.Interfaces;
using WebshopServices.Features.OrderServices;
using WebshopServices.Validation.Interfaces;
using WebshopShared.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using Xunit;

namespace WebShopServiceTests
{
    public class OrderServiceTests
    {
        TestHelper _testHelper = new TestHelper();
        private readonly IMapper _mapper;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly Mock<IValidatorFactory> _mockValidatatorFactory;
        private readonly Mock<IValidator> _mockValidator;
        private readonly Mock<ILineItemRepository> _mockLineItemRepsoitory;
        private readonly Mock<ILineItemService> _mockLineItemService;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        public OrderServiceTests()
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
            _mockLineItemService = new Mock<ILineItemService>();

            _mockOrderRepository.Setup(orderRepo => orderRepo.GetOrderById(It.IsAny<Guid>())).ReturnsAsync(new Order());

            _mockValidatatorFactory.Setup(valiFactory => valiFactory.Get(It.IsAny<string>())).Returns(_mockValidator.Object);
            _mockValidator.Setup(validator => validator.Validate(It.IsAny<IValidatable>())).ReturnsAsync(new ValidationResult { });
        }

        [Fact]
        public async Task CreateOrderShouldCallGetFactoryOnce()
        {
            //Arrange
            _mockOrderRepository.Setup(orderRepo => orderRepo.AddAsync(It.IsAny<Order>()));
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            await sut.CreateOrder(new OrderRequest());

            //Assert
            _mockValidatatorFactory.Verify(valiFactory => valiFactory.Get(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task CreateOrderShouldCallValidateOnce()
        {
            //Arrange
            _mockOrderRepository.Setup(orderRepo => orderRepo.AddAsync(It.IsAny<Order>()));
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            await sut.CreateOrder(new OrderRequest());

            //Assert
            _mockValidator.Verify(vali => vali.Validate(It.IsAny<IValidatable>()), Times.Once());
        }

        [Fact]
        public async Task CreateOrderShouldCallAddASyncOnce()
        {
            //Arrange
            _mockOrderRepository.Setup(orderRepo => orderRepo.AddAsync(It.IsAny<Order>()));
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            await sut.CreateOrder(new OrderRequest());

            //Assert
            _mockOrderRepository.Verify(OrderRepo => OrderRepo.AddAsync(It.IsAny<Order>()), Times.Once());
        }

        [Fact]
        public async Task CreateOrderShouldSetStatus()
        {
            //Arrange
            var orderRequest = _testHelper.CreateOrderRequest();
            _mockOrderRepository.Setup(orderRepo => orderRepo.AddAsync(It.IsAny<Order>()));
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            var result = await sut.CreateOrder(orderRequest);

            //Assert
            result.Status.ShouldBe(orderRequest.Status);
        }

        [Fact]
        public async Task CreateOrderShouldSetLineItems()
        {
            //Arrange
            var orderRequest = _testHelper.CreateOrderRequest();
            _mockOrderRepository.Setup(orderRepo => orderRepo.AddAsync(It.IsAny<Order>()));
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            var result = await sut.CreateOrder(orderRequest);

            //Assert
            result.LineItems.Count().ShouldBe(orderRequest.LineItems.Count());
        }

        [Fact]
        public async Task CreateOrderShouldSetTotalPrice()
        {
            //Arrange
            var orderRequest = _testHelper.CreateOrderRequest();
            double totalPrice = 0; 
            orderRequest.LineItems.ToList().ForEach(li =>
            {
                totalPrice += li.Quantity * li.Item.Price;
            });
            _mockOrderRepository.Setup(orderRepo => orderRepo.AddAsync(It.IsAny<Order>()));
            _mockLineItemService.Setup(LIService => LIService.CalculateTotalOrderPrice(It.IsAny<List<LineItem>>())).Returns(totalPrice);
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            var result = await sut.CreateOrder(orderRequest);

            //Assert
            result.TotalPrice.ShouldBe(totalPrice);
        }

        [Fact]
        public async Task GetAllOrdersShouldCallListAllAsyncOnce()
        {
            //Arrange

            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            await sut.GetAllOrders();

            //Assert
            _mockOrderRepository.Verify(orderRepo => orderRepo.ListAllAsync(), Times.Once);
        }


        [Fact]
        public async Task GetOrderByIdShouldCallGetByIdAsync()
        {
            //Arrange
            _mockOrderRepository.Setup(orderRepo => orderRepo.GetOrderById(It.IsAny<Guid>())).ReturnsAsync(new Order());
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            await sut.GetOrderById(It.IsAny<Guid>());

            //Assert
            _mockOrderRepository.Verify(orderRepo => orderRepo.GetOrderById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task UpdateItemShouldCallGetFromFactoryOnce()
        {
            //Arrange
            var orderRequest = _testHelper.CreateOrderRequest();
            _mockOrderRepository.Setup(orderRepo => orderRepo.UpdateAsync(It.IsAny<Order>()));
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            await sut.UpdateOrder(orderRequest);

            //Assert
            _mockValidatatorFactory.Verify(valiFactory => valiFactory.Get(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task UpdateOrderShouldCallValidateOnce()
        {
            //Arrange
            var orderRequest = _testHelper.CreateOrderRequest();
            _mockOrderRepository.Setup(orderRepo => orderRepo.UpdateAsync(It.IsAny<Order>()));
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            await sut.UpdateOrder(orderRequest);

            //Assert
            _mockValidator.Verify(vali => vali.Validate(It.IsAny<IValidatable>()), Times.Once());
        }

        [Fact]
        public async Task UpdateOrderShouldCallUpdateAsyncOnce()
        {
            //Arrange
            var orderRequest = _testHelper.CreateOrderRequest();
            _mockOrderRepository.Setup(orderRepo => orderRepo.UpdateAsync(It.IsAny<Order>()));
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            await sut.UpdateOrder(orderRequest);

            //Assert
            _mockOrderRepository.Verify(orderRepo => orderRepo.UpdateAsync(It.IsAny<Order>()), Times.Once());
        }

        [Fact]
        public async Task UpdateOrderShouldSetStatus()
        {
            //Arrange
            var orderRequest = _testHelper.CreateOrderRequest();
            _mockOrderRepository.Setup(orderRepo => orderRepo.UpdateAsync(It.IsAny<Order>()));
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            var result = await sut.UpdateOrder(orderRequest);

            //Assert
            result.Status.ShouldBe(orderRequest.Status);
        }

        [Fact]
        public async Task UpdateOrderShouldSetLineItems()
        {
            //Arrange
            var orderRequest = _testHelper.CreateOrderRequest();
            _mockOrderRepository.Setup(orderRepo => orderRepo.UpdateAsync(It.IsAny<Order>()));
            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            var result = await sut.UpdateOrder(orderRequest);

            //Assert
            result.LineItems.Count().ShouldBe(orderRequest.LineItems.Count());
        }

        [Fact]
        public async Task UpdateOrderShouldSetTotalPrice()
        {
            //Arrange
            var orderRequest = _testHelper.CreateOrderRequest();
            double totalPrice = 0;
            orderRequest.LineItems.ToList().ForEach(li =>
            {
                totalPrice += li.Quantity * li.Item.Price;
            });
            _mockOrderRepository.Setup(orderRepo => orderRepo.UpdateAsync(It.IsAny<Order>()));
            _mockLineItemService.Setup(LIService => LIService.CalculateTotalOrderPrice(It.IsAny<List<LineItem>>())).Returns(totalPrice);

            //Act
            var sut = new OrderService(_mapper, _mockOrderRepository.Object, _mockItemRepository.Object, _mockValidatatorFactory.Object);
            var result = await sut.UpdateOrder(orderRequest);

            //Assert
            result.TotalPrice.ShouldBe(totalPrice);
        }
    }
}
