using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Core.Dto;
using MyWebApi.Infrastructure.Repository___service;
using MyWebApi.Interfaces;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service; 
        }

        [HttpPost("SaveCategory")]
        public async Task<CategorySaveResponse> SaveCategory([FromBody] CategoryPostRequest categoryPostRequest)
        {
            return await _categoryService.SaveOrUpdateCategory(categoryPostRequest);
        }

        [HttpPost("GetAllCategory")]
        public async Task<CategoryPaginatedResponse> GetAllCategory([FromBody] CategoryPaginationRequest request)
        {
            return await _categoryService.GetAllCategory(request);
        }

        [HttpPost("deleteCategory")]
        public async Task<CategoryDeleteRequest> deleteCategory([FromBody] CategoryDeleteRequest request)
        {
            return await _categoryService.DeleteCategory(request);
        }

        [HttpPost("SaveIndividual")]
        public async Task<IndividualSaveResponse> SaveSymbol([FromBody] IndividualPostRequests individualPostRequest)
        {
            return await _categoryService.IndividualSaveOrUpdate(individualPostRequest);
        }

        [Authorize]
        [HttpPost("GetAllIndividual")]
        public async Task<IndividualPaginatedResponse> GetAllSymbol([FromBody] IndividualPaginationRequest request)
        {
            return await _categoryService.IndividualGetAll(request);
        }

        [HttpPost("deleteIndividual")]
        public async Task<IndividualDeleteRequest> deleteSymbol([FromBody] IndividualDeleteRequest request)
        {
            return await _categoryService.IndividualDelete(request);
        }

        [HttpPost("SaveCountry")]
        public async Task<CountrySaveResponse> SaveCountry([FromBody] CountryPostRequests countryPostRequests)
        {
            return await _categoryService.CountrySaveOrUpdate(countryPostRequests);
        }

        [HttpPost("GetAllCountry")]
        public async Task<CountryPaginatedResponse> GetAllCountry([FromBody] CountryPaginationRequest request)
        {
            return await _categoryService.CountryGetAll(request);
        }

        [HttpPost("deleteCountry")]
        public async Task<CountryDeleteRequest> deleteCountry([FromBody] CountryDeleteRequest request)
        {
            return await _categoryService.CountryDelete(request);
        }
    }
}
