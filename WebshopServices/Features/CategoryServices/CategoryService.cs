using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopServices.Features.Interfaces;
using WebshopServices.Validation.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopServices.Features.CategoryServices
{
    public class CategoryService : ICategoryService
    {

        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidatorFactory _validatorFactory;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IValidatorFactory validatorFactory)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _validatorFactory = validatorFactory;
        }

        public async Task<CategoryResponse> CreateCategory(CategoryRequest categoryRequest)
        {
            var categoryResponse = new CategoryResponse();

            categoryResponse.AddValidationResult(await _validatorFactory.Get("CategoryRequestValidator").Validate(categoryRequest));
            if (categoryResponse.Success)
            {
                var category = new Category()
                {
                    Name = categoryRequest.Name
                };

                await _categoryRepository.AddAsync(category);
                return _mapper.Map<CategoryResponse>(category);
            }
            else
            {
                categoryResponse = _mapper.Map<CategoryResponse>(categoryRequest);
                return categoryResponse;
            }
        }

        public async Task<Guid> DeleteCategory(Guid categoryId)
        {
            var categoryToBeDeleted = await _categoryRepository.GetByIdAsync(categoryId);
            await _categoryRepository.DeleteAsync(categoryToBeDeleted);

            return categoryToBeDeleted.CategoryId;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _categoryRepository.ListAllAsync();
            var categoryResponse = _mapper.Map<List<CategoryResponse>>(categories);

            return categoryResponse;
        }

        public async Task<CategoryResponse> GetCategoryById(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            
            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse> GetCategoryByName(string cateGoryName)
        {
            var category = await _categoryRepository.GetCategoryByName(cateGoryName);

            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryResponse> UpdateCategory(CategoryRequest categoryRequest)
        {
            var categoryResponse = new CategoryResponse();

            categoryResponse.AddValidationResult(await _validatorFactory.Get("CategoryRequestValidator").Validate(categoryRequest));
            if (categoryResponse.Success)
            {
                var oldCategory = await _categoryRepository.GetByIdAsync(categoryRequest.CategoryId);

                oldCategory.Name = categoryRequest.Name;
                oldCategory.Items = _mapper.Map<List<Item>>(categoryRequest.Items);

                await _categoryRepository.UpdateAsync(oldCategory);
                return _mapper.Map<CategoryResponse>(oldCategory);
            }
            else
            {
                categoryResponse = _mapper.Map<CategoryResponse>(categoryRequest);
                return categoryResponse;
            }
        }
    }
}
