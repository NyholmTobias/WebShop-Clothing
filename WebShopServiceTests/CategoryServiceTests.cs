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
using WebshopServices.Features.CategoryServices;
using WebshopServices.Validation.Interfaces;
using WebshopShared.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;
using Xunit;

namespace WebShopServiceTests
{
    public class CategoryServiceTests
    {
        TestHelper _testhelper = new TestHelper();
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IValidatorFactory> _mockValidatorFactory;
        private readonly Mock<IValidator> _mockValidator;
        public CategoryServiceTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });

            _mapper = configurationProvider.CreateMapper();

            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockValidatorFactory = new Mock<IValidatorFactory>();
            _mockValidator = new Mock<IValidator>();

            _mockValidatorFactory.Setup(valiFactory => valiFactory.Get(It.IsAny<string>())).Returns(_mockValidator.Object);
            _mockValidator.Setup(validator => validator.Validate(It.IsAny<IValidatable>())).ReturnsAsync(new ValidationResult { });

        }

        [Fact]
        public async Task CreateCategoryShouldCallGetFactoryOnce()
        {
            //Arrange
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.AddAsync(It.IsAny<Category>()));
            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            await sut.CreateCategory(new CategoryRequest());

            //Assert
            _mockValidatorFactory.Verify(valiFactory => valiFactory.Get(It.IsAny<string>()), Times.Once());

        }
        [Fact]
        public async Task CreateCategoryShouldCallValidateOnce()
        {
            //Arrange
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.AddAsync(It.IsAny<Category>()));
            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            await sut.CreateCategory(new CategoryRequest());

            //Assert
            _mockValidator.Verify(validator => validator.Validate(It.IsAny<IValidatable>()), Times.Once());
        }
        [Fact]
        public async Task CreateCategoryShouldCallAddASyncOnce()
        {
            //Arrange
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.AddAsync(It.IsAny<Category>()));
            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            await sut.CreateCategory(new CategoryRequest());

            //Assert
            _mockCategoryRepository.Verify(categoryRepo => categoryRepo.AddAsync(It.IsAny<Category>()), Times.Once());
        }

        [Fact]
        public async Task DeleteCategoryShouldCallGetByIdOnce()
        {
            //Arrange
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.DeleteAsync(It.IsAny<Category>()));
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Category());
            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            await sut.DeleteCategory(Guid.NewGuid());
            //Assert
            _mockCategoryRepository.Verify(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
        }
        [Fact]
        public async Task DeleteCategoryShouldCallDeleteAsyncOnce()
        {
            //Arrange
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.DeleteAsync(It.IsAny<Category>()));
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Category());
            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            await sut.DeleteCategory(Guid.NewGuid());
            //Assert
            _mockCategoryRepository.Verify(categoryRepo => categoryRepo.DeleteAsync(It.IsAny<Category>()), Times.Once());
        }

        [Fact]
        public async Task GetAllCategoriesShouldCallListAllAsyncOnce()
        {
            //Arrange
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.ListAllAsync()).ReturnsAsync(new List<Category>());
            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            await sut.GetAllCategories();
            //Assert
            _mockCategoryRepository.Verify(categoryRepo => categoryRepo.ListAllAsync(), Times.Once());
        }

        [Fact]
        public async Task GetAllCategoriesShouldReturnListOfCategoryResponses()
        {
            //Arrange
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.ListAllAsync()).ReturnsAsync(new List<Category>());
            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            var result = await sut.GetAllCategories();
            //Assert
            result.ShouldBeOfType<List<CategoryResponse>>();
        }

        [Fact]
        public async Task GetCategoryByIdShouldCallGetByIdOnce()
        {
            //Arrange
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Category());
            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            var result = await sut.GetCategoryById(Guid.NewGuid());
            //Assert
            _mockCategoryRepository.Verify(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async Task GetCategoryByNameShouldCallGetByNameOnce()
        {
            //Arrange
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(new Category());
            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            var result = await sut.GetCategoryByName(It.IsAny<string>());
            //Assert
            _mockCategoryRepository.Verify(categoryRepo => categoryRepo.GetCategoryByName(It.IsAny<string>()), Times.Once());
        }
        [Fact]
        public async Task UpdateCategoryShouldCallGetByIdAsyncOnce()
        {
            //Arrange
            var categoryRequest = _testhelper.CreateCategoryRequest();
            var category = _testhelper.CreateCategory();
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.UpdateAsync(It.IsAny<Category>()));
            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            var result = await sut.UpdateCategory(categoryRequest);
            //Assert
            _mockCategoryRepository.Verify(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
        }
        [Fact]
        public async Task UpdateCategoryShouldCallGetFactoryOnce()
        {
            //Arrange
            var categoryRequest = _testhelper.CreateCategoryRequest();
            var category = _testhelper.CreateCategory();
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.UpdateAsync(It.IsAny<Category>()));

            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            var result = await sut.UpdateCategory(categoryRequest);
            //Assert
            _mockValidatorFactory.Verify(valiFactory => valiFactory.Get(It.IsAny<string>()), Times.Once);
          }

        [Fact]
        public async Task UpdateCategoryShouldCallUpdateAsyncOnce()
        {
            //Arrange
            var categoryRequest = _testhelper.CreateCategoryRequest();
            var category = _testhelper.CreateCategory();
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.UpdateAsync(It.IsAny<Category>()));

            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            var result = await sut.UpdateCategory(categoryRequest);
            //Assert
            _mockCategoryRepository.Verify(categoryRepo => categoryRepo.UpdateAsync(It.IsAny<Category>()), Times.Once());
        }

        [Fact]
        public async Task UpdateCategoryShouldCallValidateOnce()
        {
            //Arrange
            var categoryRequest = _testhelper.CreateCategoryRequest();
            var category = _testhelper.CreateCategory();
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.UpdateAsync(It.IsAny<Category>()));

            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            var result = await sut.UpdateCategory(categoryRequest);
            //Assert
            _mockValidator.Verify(validator => validator.Validate(It.IsAny<IValidatable>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCategoryShouldSetName()
        {
            //Arrange
            var categoryRequest = _testhelper.CreateCategoryRequest();
            var category = _testhelper.CreateCategory();
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.UpdateAsync(It.IsAny<Category>()));

            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            var result = await sut.UpdateCategory(categoryRequest);
            //Assert
            result.Name.ShouldBe(categoryRequest.Name);
        }

        [Fact]
        public async Task UpdateCategoryShouldInclude2Items()
        {
            //Arrange
            var categoryRequest = _testhelper.CreateCategoryRequest();
            var category = _testhelper.CreateCategory();
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(category);
            _mockCategoryRepository.Setup(categoryRepo => categoryRepo.UpdateAsync(It.IsAny<Category>()));

            //Act
            var sut = new CategoryService(_mockCategoryRepository.Object, _mapper, _mockValidatorFactory.Object);
            var result = await sut.UpdateCategory(categoryRequest);
            //Assert
            result.Items.Count.ShouldBe(2);
        }


    }
}
